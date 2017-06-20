using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using AutoMapper;
using Price.WebApi.Models;
using Price.WebApi.Logic;
using PriceCommon.Utils;
using PricePipeCore;

namespace Price.WebApi.Controllers
{
    /// <summary>
    ///     Раздел Фармацевтика
    /// </summary>
    [RoutePrefix("api/pharmacy")]
    //[Authorize]
    public class PharmacyController : ApiController
    {
        private readonly Searcher _seacher;

        /// <summary>
        ///     Конструктор контроллера фармацевтика
        /// </summary>
        public PharmacyController()
        {
            _seacher = new Searcher(
                AppSettings.DefaultIndex,
                AppSettings.Host,
                AppSettings.UserName,
                AppSettings.Password);
        }

        /// <summary>
        ///     Поиска по одному ТРУ с нормализацией по фармацевтике
        /// </summary>
        /// <param name="text">Наименование ТРУ для поиска</param>
        /// <returns></returns>
        [HttpGet]
        [Route("contents", Name = nameof(GetPharmacyContents) + "Route")]
        public IEnumerable<ContentDto> GetPharmacyContents(string text)
        {
            Logger.Log.Info($"{nameof(GetPharmacyContents)}: {text}");
            var contents = _seacher.Search(text);
            // add phones: left join SourceNames.Names and contents
            var contentsWithPhones = from o in contents
                                     join i in SourceNames.Names on new Uri(o.Uri).Host equals i.Key into ps
                                     from p in ps.DefaultIfEmpty()
                                     select new ContentDto()
                                     {
                                         Selected = o.Selected,
                                         Name = o.Name,
                                         Price = o.Price,
                                         Uri = o.Uri,
                                         Seller = o.Seller,
                                         CollectedAt = o.CollectedAt,
                                         Id = o.Id,
                                         Producer = o.Producer,
                                         Phones = p.Equals(new KeyValuePair<string, SourceDto>()) ? "" : p.Value.Phones
                                     };

            return Mapper.Map<IEnumerable<ContentDto>>(contentsWithPhones);
        }

        /// <summary>
        ///     Расчета НМЦК по одному ТРУ с нормализацией по фармацевтике
        /// </summary>
        /// <param name="text">Наименование ТРУ для поиска</param>
        /// <returns></returns>
        [HttpGet]
        [Route("nmck", Name = nameof(GetPharmacyNmck) + "Route")]
        public ContentNmckDto GetPharmacyNmck(string text)
        {
            Logger.Log.Info($"{nameof(GetPharmacyNmck)}: {text}");
            var contents = _seacher.Search(text);
            // add phones: left join SourceNames.Names and contents
            var contentsWithPhones = from o in contents
                                     join i in SourceNames.Names on new Uri(o.Uri).Host equals i.Key into ps
                                     from p in ps.DefaultIfEmpty()
                                     select new ContentDto()
                                     {
                                         Selected = o.Selected,
                                         Name = o.Name,
                                         Price = o.Price,
                                         Uri = o.Uri,
                                         Seller = o.Seller,
                                         CollectedAt = o.CollectedAt,
                                         Id = o.Id,
                                         Producer = o.Producer,
                                         Phones = p.Equals(new KeyValuePair<string, SourceDto>()) ? "" : p.Value.Phones
                                     };
            var dto = new ContentNmckDto
            {
                Text = text,
                CalcDate = DateTime.UtcNow,
                Contents = Mapper.Map<IEnumerable<ContentDto>>(contentsWithPhones)
            };
            if (!dto.Contents.Any()) return dto;
            string calcText;
            var prices = dto.Contents.Select(z => z.Nprice).ToList();
            dto.Nmck = (decimal)Utils.GetPriceCalculation(prices, out calcText);
            dto.CalcText = calcText;
            return dto;
        }
    }
}