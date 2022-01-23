using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchWedApi.Dto
{
    public class MetricsDto
    {
        public int Second { get; set; }
        public string SearchType { get; set; }
        public int RequestCount { get; set; }
    }
}
