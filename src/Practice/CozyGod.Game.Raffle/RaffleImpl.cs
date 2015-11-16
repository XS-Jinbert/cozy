﻿using CozyGod.Game.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CozyGod.Game.Model;

namespace CozyGod.Game.Raffle
{
    public class RaffleImpl : IRaffle
    {
        Random rd = new Random();
        const int RaffleMaxLevel = 9;
        float[] m_raffleProbabilityArray;
        private ICardLibrary mCL;
        private ICozyGodEngine m_engine;

        public bool Init(ICozyGodEngine engine)
        {
            bool bRet = false;
            do
            {
                m_engine = engine;
                mCL = engine.GetCardLibrary();
                if (mCL == null)
                {
                    break;
                }
                LoadRaffleProbabilityArray();
                if (m_raffleProbabilityArray == null)
                {
                    break;
                }
                bRet = true;
            } while (false);
            return bRet;
        }

        /// <summary>
        /// 抽卡接口
        /// </summary>
        /// <param name="rank">保底等级</param>
        /// <returns>随机获取一张大于等于保底等级rank值的卡牌</returns>
        public Card Draw(int rank = 0)
        {
            Card cardRet = null;
            float _fRd = (float)rd.NextDouble();
            int rankRet = -1;
            for (int i = rank; i < m_raffleProbabilityArray.Length; i++)
            {
                if (_fRd < m_raffleProbabilityArray[i])
                {
                    rankRet = i + 1;
                }
                else
                {
                    _fRd -= m_raffleProbabilityArray[i];
                }
            }

            if (rankRet == -1)
            {
                rankRet = rank;
            }

            if (mCL.Get() != null && mCL.Get().Cards[rankRet] != null)
            {
                int _iRd = rd.Next(0, mCL.Get().Cards[rankRet].Count);
                cardRet = mCL.Get().Cards[rankRet][_iRd];
            }
            return cardRet;
        }

        public Card[] PentaDraw()
        {
            Card [] cardArrayRet = new Card[5];

            cardArrayRet[0] = Draw(2);
            for(int i = 1; i < cardArrayRet.Length; i++)
            {
                cardArrayRet[i] = Draw();
            }


            // shuffle 打乱顺序
            for(int i = 0; i < cardArrayRet.Length; i++)
            {
                int _iRdIndex = rd.Next(0, cardArrayRet.Length);

                Card tmp = cardArrayRet[i];
                cardArrayRet[i] = cardArrayRet[_iRdIndex];
                cardArrayRet[_iRdIndex] = tmp;
            }

            return cardArrayRet;
        }

        void LoadRaffleProbabilityArray()
        {
            m_raffleProbabilityArray = new float[RaffleMaxLevel];
            for(int i = 0; i < RaffleMaxLevel; i++)
            {
                m_raffleProbabilityArray[i] = 1.0f / (5.0f * (float)Math.Pow(3, i));
            }
        }
    }
}