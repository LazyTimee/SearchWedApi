using Microsoft.AspNetCore.Mvc;
using SearchWedApi.Domain;
using SearchWedApi.Dto;
using SearchWedApi.ExternalSearch;
using SearchWedApi.Interfaces;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SearchWedApi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class SearchController : ControllerBase
    {
        private readonly ISearchDbContext _context;
        public SearchController(ISearchDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Получение отчета по времени выполнения запросов
        /// </summary>
        /// <returns>Отчет</returns>
        [HttpGet]
        public List<MetricsDto> GetMetrics()
        {
            var groupList = _context
                .Metrics
                .OrderBy(x => x.WorkTime)
                .ToLookup(x => x.WorkTime / 1000 + 1)
                .Select(x => new
                {
                    sec = x.Key,
                    typeList = x.GroupBy(x => x.SearchType)
                        .Select(y => new
                        {
                            type = y.Key,
                            count = y.Count()
                        })
                        .ToList()
                })
                .ToList();

            var resultList = new List<MetricsDto>();

            groupList.ForEach(timeGroup =>
            {
                timeGroup.typeList.ForEach(typeEl =>
                {
                    resultList.Add(new MetricsDto
                    {
                        Second = timeGroup.sec,
                        SearchType = typeEl.type,
                        RequestCount = typeEl.count
                    });
                });
            });

            return resultList;
        }

        /// <summary>
        /// Имитация обращения к поисковым системам
        /// </summary>
        /// <param name="wait"> Время ожидания ответа в запросе</param>
        /// <param name="randomMin"> Минимальное время исполнения запроса</param>
        /// <param name="randomMax"> Максимальное время исполнения запроса</param>
        /// <returns>Сптсок выполненных запросов</returns>
        [HttpGet]
        public ActionResult<List<Metrics>> Search(int wait, int randomMin, int randomMax)
        {
            var searchA = Task.Run(() => new ExternalSearchA().Request(wait, randomMin, randomMax));
            var searchB = Task.Run(() => new ExternalSearchB().Request(wait, randomMin, randomMax));
            var searchC = Task.Run(() => new ExternalSearchC().Request(wait, randomMin, randomMax));

            var resultC = searchC.Result;
            var listResult = new List<Metrics>();

            if (resultC.Result == "OK")
            {
                var searchD = Task.Run(() => new ExternalSearchD().Request(wait - resultC.WorkTime, randomMin, randomMax));
                listResult.Add(searchD.Result);
            }

            listResult.Add(resultC);
            listResult.Add(searchB.Result);
            listResult.Add(searchA.Result);

            _context.Metrics.AddRange(listResult);
            _context.SaveChangesAsync(CancellationToken.None);

            return Ok(listResult);
        }
    }
}
