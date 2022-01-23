using SearchWedApi.Domain;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace SearchWedApi.ExternalSearch
{
    public class BaseExternalSearch
    {
        protected string Type { get; set; } = "None";

        public Metrics Request(int wait, int rndMin, int rndMax)
        {
            var delay = new Random().Next(rndMin, rndMax);
            var result = new Metrics() { Id = Guid.NewGuid(), SearchType = Type, Result = "TIMEOUT", WorkTime = wait };
            var task = Task.Run(() => GetResult(delay));
            if (task.Wait(wait))
            {
                result.Result = task.Result.result;
                result.WorkTime = task.Result.workTime;
                return result;
            }
            return result;
        }

        private (string result, int workTime) GetResult(int delay)
        {
            Thread.Sleep(delay);
            return delay % 2 == 0 ? ("OK", delay) : ("ERROR", delay);
        }
    }
}
