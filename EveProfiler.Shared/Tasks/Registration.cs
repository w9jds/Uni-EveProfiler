using EveProfiler.Logic;
using System;
using Windows.ApplicationModel.Background;

namespace EveProfiler.Shared.Tasks
{
    public class Registration
    {
        public Registration()
        {

        }

        public void RegisterNewMailTimer(DateTime scheduledTime, Character character)
        {
            string taskName = $"RetrieveMailTask_{character.CharacterId}";

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

                builder.SetTrigger(new TimeTrigger(Convert.ToUInt32(scheduleOffset), true));
                builder.Register();
            }
        }

        private bool IsTaskRegistered(string taskName)
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
