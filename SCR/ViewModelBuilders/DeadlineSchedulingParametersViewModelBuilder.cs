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
                    new DeadlineParameterViewModel {Capacity = 1, Period = 2, Deadline = 2},
                    new DeadlineParameterViewModel {Capacity = 1, Period = 4, Deadline = 4},
                    new DeadlineParameterViewModel {Capacity = 1, Period = 2, Deadline = 4},
                }
            };

            return model;
        }

        public static DeadlineThread ToDeadlineThread(DeadlineParameterViewModel model)
        {
            return new DeadlineThread
            {
                Capacity = model.Capacity,
                Period = model.Period,
                Deadline = model.Period
            };
        }

        public static DeadlineParameterViewModel ToDeadlineParameterViewModel(DeadlineThread model)
        {
            return new DeadlineParameterViewModel
            {
                Capacity = model.Capacity,
                Period = model.Period,
                Deadline = model.Period
            };
        }
    }
}