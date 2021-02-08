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
                    new DeadlineParameterViewModel {Capacity = 3, Period = 20, Deadline = 7},
                    new DeadlineParameterViewModel {Capacity = 2, Period = 5, Deadline = 4},
                    new DeadlineParameterViewModel {Capacity = 2, Period = 10, Deadline = 8},
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
                Deadline = model.Deadline
            };
        }

        public static DeadlineParameterViewModel ToDeadlineParameterViewModel(DeadlineThread model)
        {
            return new DeadlineParameterViewModel
            {
                ThreadNo = model.ThreadNo,
                Capacity = model.Capacity,
                Period = model.Period,
                Deadline = model.Deadline
            };
        }
    }
}