using EveProfiler.Logic;
using System;
using Windows.ApplicationModel.Background;

namespace EveProfiler.Tasks
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
                builder.SetTrigger(new TimeTrigger(
                    Convert.ToUInt32(scheduledTime.Subtract(DateTime.UtcNow).TotalMinutes), true));

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
