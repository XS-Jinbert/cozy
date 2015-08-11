﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CozyNote.Model.APIModel;
using CozyNote.Model.APIModel.Input;
using CozyNote.Model.APIModel.Output;
using CozyNote.ServerCore.Database;
using Newtonsoft.Json;
using CozyNote.Model.ObjectModel;

namespace CozyNote.ServerCore.Module
{
    public partial class UserModule
    {
        public string OnUserNotebook(string args)
        {
            var Input   = JsonConvert.DeserializeObject<UserNotebookInput>(args);
            var Result  = new UserNotebookOutput();

            if (DbHolding.User.IsExist(Input.UserName))
            {
                var User = DbHolding.User.Get(Input.UserName);
                if (User.pass == Input.UserPass)
                {
                    var NotebookList        = new List<Notebook>();
                    var UserNotebookList    = User.notebook_list;

                    foreach (var id in UserNotebookList)
                    {
                        if (DbHolding.Notebook.IsExist(id))
                        {
                            var notebook = DbHolding.Notebook.Get(id);
                            NotebookList.Add(notebook);
                        }
                    }

                    Result.ResultStatus = ResultStatus.SuccessStatus;
                    Result.NotebookList = NotebookList;
                }
            }

            var Output = JsonConvert.SerializeObject(Result);
            return Output;
        }

        public string OnUserCreate(string args)
        {
            var Input   = JsonConvert.DeserializeObject<UserCreateInput>(args);
            var Result  = new UserCreateOutput();

            if(!DbHolding.User.IsExist(Input.UserName))
            {
                var user = new User()
                {
                    nickname    = Input.UserName,
                    pass        = Input.UserPass,
                };
                var id = DbHolding.User.Create(user);

                Result.ResultStatus = ResultStatus.SuccessStatus;
                Result.UserId       = id;
            }

            var Output = JsonConvert.SerializeObject(Result);
            return Output;
        }

        public string OnUserUpdate(string args)
        {
            var Input   = JsonConvert.DeserializeObject<UserUpdateInput>(args);
            var Result  = new UserUpdateOutput();

            if(DbHolding.User.IsExist(Input.UserName) && !DbHolding.User.IsExist(Input.NewName))
            {
                var user = DbHolding.User.Get(Input.UserName);

                if(user.pass == Input.UserPass)
                {
                    user.nickname   = Input.NewName;
                    user.pass       = Input.UserPass;

                    Result.ResultStatus = ResultStatus.SuccessStatus;
                }
            }

            var Output = JsonConvert.SerializeObject(Result);
            return Output;
        }
    }
}