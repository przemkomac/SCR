﻿using BusinessLogic.Schedulers;
using DataAccess.Models;
using System.Collections.Generic;

namespace Logic.Schedulers
{
    public class DmsScheduler : BaseSchedule<DeadlineThread>
    {
        public override DeadlineThread GetNextThread(List<BaseThread> threads)
        {
            throw new System.NotImplementedException();
        }
    }
}