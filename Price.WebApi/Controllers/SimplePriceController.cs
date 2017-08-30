using System.Collections.Generic;
using System.Web.Http;
using AutoMapper;
using Price.WebApi.Models;
using PricePipeCore;

namespace Price.WebApi.Controllers
{/// <summary>
 ///     Простой поиск цен
 ///  </summary>
    [RoutePrefix("api/simpleprice")]
    //[Authorize]
    public class SimplePriceController : ApiController
    {
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


    }
}
