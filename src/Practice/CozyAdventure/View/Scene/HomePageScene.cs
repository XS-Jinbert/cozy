﻿using CocosSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyAdventure.View.Scene
{
    public class HomePageScene : CCScene
    {
        public HomePageScene() : base(AppDelegate.SharedWindow)
        {
            AddChild(new BgLayer(@"pic\1.png"));
        }
    }
}
