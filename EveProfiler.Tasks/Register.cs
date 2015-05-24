using EveProfiler.Logic;
using System;
using Windows.ApplicationModel.Background;

namespace EveProfiler.Tasks
{
    public sealed class Register
    {
        public static async void RegisterNewMailTimer(DateTimeOffset scheduledTime, long characterId)
        {
            string taskName = $"RetrieveMailTask_{characterId}";

            if (!IsTaskRegistered(taskName))
            {
                BackgroundTaskBuilder builder = new BackgroundTaskBuilder();

                builder.Name = taskName;
                builder.TaskEntryPoint = "EveProfiler.Tasks.RetrieveMailTask";

                double scheduleOffset = scheduledTime.Subtract(DateTime.UtcNow).TotalMinutes;
                if (scheduleOffset < 15)
                {
                    scheduleOffset = 15;
                }

                await BackgroundExecutionManager.RequestAccessAsync();
                builder.SetTrigger(new TimeTrigger(Convert.ToUInt32(scheduleOffset), true));
                builder.Register();
            }
        }

        private static bool IsTaskRegistered(string taskName)
        {
            foreach (var task in BackgroundTaskRegistration.AllTasks)
            {
                if (task.Value.Name == taskName)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
