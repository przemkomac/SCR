using System.Collections.Generic;
using BusinessLogic.Threads;
using WebApp.Models;
using WebApp.Models.Parameters;

namespace WebApp.ViewModelBuilders
{
    public static class PrioritySchedulingParametersViewModelBuilder
    {
        public static PrioritySchedulingParametersViewModel Build()
        {
            var model = new PrioritySchedulingParametersViewModel
            {
                ExecutionTime = 20,
                PriorityParameters = new List<PriorityParameterViewModel>
                {
                    new PriorityParameterViewModel {Capacity = 1, Period = 2, Priority = 1},
                    new PriorityParameterViewModel {Capacity = 1, Period = 4, Priority = 5},
                    new PriorityParameterViewModel {Capacity = 1, Period = 2, Priority = 5},
                }
            };

            return model;
        }

        public static PriorityThread ToPriorityThread(PriorityParameterViewModel model)
        {
            return new PriorityThread
            {
                Capacity = model.Capacity,
                Period = model.Period,
                Priority = model.Priority
            };
        }
    }
}