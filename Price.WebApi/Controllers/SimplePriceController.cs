using AutoMapper;
using Common.Dto.Model;
using Newtonsoft.Json;
using Price.WebApi.Maintenance.Interfaces;
using PricePipeCore;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;
using Common.Dto.Model.NewApi;

namespace Price.WebApi.Controllers
{
    /// <summary>
    ///     Простой поиск цен
    ///  </summary>
    [RoutePrefix("api/simpleprice")]
    //[Authorize]
    public class SimplePriceController : ApiController
    {
        private IFindCompanyApi _findCompanyApi;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="findCompanyApi"></param>
        public SimplePriceController(IFindCompanyApi findCompanyApi)
        {
            _findCompanyApi = findCompanyApi;
        }

        /// <summary>
        ///     Получение списка результатов поиска без нормализации
        /// </summary>
        /// <param name="text">Текст для поиска</param>
        /// <param name="source">Источник, в котором осуществляется поиск (пусто - источник по умолчанию, gz - госзакупки)</param>
        /// <returns></returns>
        [HttpGet]
        [Route("contents", Name = nameof(GetSimplePriceContents) + "Route")]
        public IEnumerable<ContentDto> GetSimplePriceContents(string text, string source = "")
        {
            Logger.Log.Info($"{nameof(GetSimplePriceContents)}: text={text}&source={source}");
            if (string.IsNullOrEmpty(source)) source = AppSettings.DefaultIndex;
            return Mapper.Map<IEnumerable<ContentDto>>(new SimpleSearcher(source).SimpleSearch(text));
        }

        /// <summary>
        ///     Получение списка результатов поиска в госзакупках по коду ОКПД2 
        /// </summary>
        /// <param name="okpd2">Код ОКПД2 для поиска</param>
        /// <returns></returns>
        [HttpGet]
        [Route("okpd2", Name = nameof(GetByOkpd2) + "Route")]
        public IEnumerable<ContentDto> GetByOkpd2(string okpd2)
        {
            Logger.Log.Info($"{nameof(GetByOkpd2)}: okpd2={okpd2}");
            return Mapper.Map<IEnumerable<ContentDto>>(new SimpleSearcher(nameof(ElacticIndexName.Gz)).Okpd2Search(okpd2));
        }


        /// <summary>
        ///  GET - Аналог поиска maybe без нормализации
        /// </summary>
        /// <param name="must">Массив строк (через точку с запятой) - обязательное вхождение</param>
        /// <param name="should">Массив строк (через точку с запятой) - возможное вхождение</param>
        /// <param name="mustNot">Массив строк (через точку с запятой) - обязательное отсутствие</param>
        /// <param name="source">Источник, в котором осуществляется поиск (пусто - источник по умолчанию, gz - госзакупки)</param>
        /// <returns></returns>
        [HttpGet]
        [Route("maybe", Name = nameof(GetMaybeSearch) + "Route")]
        public IEnumerable<ContentDto> GetMaybeSearch(string must = "", string should = "", string mustNot = "", string source = "")
        {
            Logger.Log.Info($"{nameof(GetMaybeSearch)}: {nameof(must)}={must}&{nameof(should)}={should}&{nameof(mustNot)}={mustNot}&{nameof(source)}={source}");
            if (string.IsNullOrEmpty(source)) source = AppSettings.DefaultIndex;
            return Mapper.Map<IEnumerable<ContentDto>>(new SimpleSearcher(source).MaybeSearch(must, should, mustNot));
        }

        /// <summary>
        ///  POST - Аналог поиска maybe без нормализации
        /// </summary>
        /// <param name="maybeDto">Структура для задания параметров поиска типа Maybe</param>
        /// <param name="source">Источник, в котором осуществляется поиск (пусто - источник по умолчанию, gz - госзакупки)</param>
        /// <returns></returns>
        [HttpPost]
        [Route("maybe", Name = nameof(PostMaybeSearch) + "Route")]
        public IEnumerable<ContentDto> PostMaybeSearch(MaybeDto maybeDto, [FromUri]string source = "")
        {
            Logger.Log.Info($"{nameof(PostMaybeSearch)}: {JsonConvert.SerializeObject(maybeDto)}");
            if (string.IsNullOrEmpty(source)) source = AppSettings.DefaultIndex;
            var delimiter = SimpleSearcher.ListDelimiter.FirstOrDefault();
            var must = maybeDto.Musts == null ? "" : string.Join(delimiter, maybeDto.Musts);
            var should = maybeDto.Shoulds == null ? "" : string.Join(delimiter, maybeDto.Shoulds);
            var mustNot = maybeDto.MustNots == null ? "" : string.Join(delimiter, maybeDto.MustNots);
            return Mapper.Map<IEnumerable<ContentDto>>(new SimpleSearcher(source).MaybeSearch(must, should, mustNot));
        }

        /// <summary>
        /// Получить количество поставщиков
        /// </summary>
        /// <returns>Количество поставщиков</returns>
        [HttpGet]
        [Route("sellerCount", Name = nameof(GetSellerCount) + "Route")]
        public int GetSellerCount()
        {
            return _findCompanyApi.GetSellerCount();

            //var source = AppSettings.DefaultIndex;
            //return (new SimpleSearcher(source)).GetSellerCount();
        }

        [HttpGet]
        [Route("gzDocs/{regNum}")]
        public async Task<Dictionary<string, string>> GetGzDocs(string regNum)
        {
            var apiHttpClient = new HttpClient();
            apiHttpClient.DefaultRequestHeaders.Accept.Clear();
            apiHttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            using (var response = await apiHttpClient.GetAsync($"http://localhost:53986/api/docs/{regNum}"))
            {
                if (!response.IsSuccessStatusCode) return null;
                var result = await response.Content.ReadAsAsync<Dictionary<string, string>>();
                return result;
            }
        }
    }
}
