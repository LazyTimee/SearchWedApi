using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchWedApi.Domain
{
    public class Metrics
    {
        public Guid Id { get; set; }
        public int WorkTime { get; set; }
        public string SearchType { get; set; }
        public string Result { get; set; }
    }
}
