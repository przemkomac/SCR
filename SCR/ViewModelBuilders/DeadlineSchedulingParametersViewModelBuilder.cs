using System.Collections.Generic;
using BusinessLogic.Threads;
using WebApp.Models;
using WebApp.Models.Parameters;

namespace WebApp.ViewModelBuilders
{
    public static class DeadlineSchedulingParametersViewModelBuilder
    {
        public static DeadlineSchedulingParametersViewModel Build()
        {
            var model = new DeadlineSchedulingParametersViewModel
            {
                ExecutionTime = 20,
                DeadlineParameters = new List<DeadlineParameterViewModel>
                {
                    new DeadlineParameterViewModel {Capacity = 3, Period = 7, Deadline = 20},
                    new DeadlineParameterViewModel {Capacity = 2, Period = 4, Deadline = 5},
                    new DeadlineParameterViewModel {Capacity = 2, Period = 8, Deadline = 10},
                }
            };

            return model;
        }

        public static DeadlineThread ToDeadlineThread(DeadlineParameterViewModel model)
        {
            return new DeadlineThread
            {
                ThreadNo = model.ThreadNo,
                Capacity = model.Capacity,
                Period = model.Period,
                Deadline = model.Period
            };
        }

        public static DeadlineParameterViewModel ToDeadlineParameterViewModel(DeadlineThread model)
        {
            return new DeadlineParameterViewModel
            {
                ThreadNo = model.ThreadNo,
                Capacity = model.Capacity,
                Period = model.Period,
                Deadline = model.Period
            };
        }
    }
}