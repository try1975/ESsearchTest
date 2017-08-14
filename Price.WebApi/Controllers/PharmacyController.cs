using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using AutoMapper;
using Norm.MedPrep;
using Price.WebApi.Models;
using Price.WebApi.Logic;
using PriceCommon.Norm;
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
        #region Content

        /// <summary>
        ///     Поиска по одному ТРУ с нормализацией по фармацевтике
        /// </summary>
        /// <param name="text">Наименование ТРУ для поиска, вкючающее лекарственную форму, упаковку, дозировку</param>
        /// <param name="firstWords">Точное наменование ТРУ</param>
        /// <param name="lekForm">Лекарственная форма</param>
        /// <param name="upak">Количество в упаковке</param>
        /// <param name="doz">Значение дозировки</param>
        /// <param name="dozEd">Единица измерения дозировки</param>
        /// <param name="normNumber">Номер пользовательского нормализатора</param>
        /// <param name="source">Источник, в котором осуществляется поиск (пусто - источник по умолчанию, gz - госзакупки)</param>
        /// <returns></returns>
        [HttpGet]
        [Route("contents", Name = nameof(GetPharmacyContents) + "Route")]
        public IEnumerable<ContentDto> GetPharmacyContents(string text = "", string firstWords = "",
            string lekForm = "", string upak = "", string doz = "", string dozEd = "", string normNumber = "", string source="")
        {
            Logger.Log.Info($"{nameof(GetPharmacyContents)}: {text}");
            if (string.IsNullOrEmpty(source)) source = AppSettings.DefaultIndex;
            var seacher = new PharmacySearcher(source); 
            var contents = seacher.Search(text, firstWords, lekForm, upak, doz, dozEd, normNumber);
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
                                         Okpd2 = o.Okpd2,
                                         Okei = o.Okei,
                                         Currency = o.Currency,
                                         Phones = p.Equals(new KeyValuePair<string, SourceDto>()) ? "" : p.Value.Phones
                                     };

            return Mapper.Map<IEnumerable<ContentDto>>(contentsWithPhones);
        }

        /// <summary>
        ///     Расчет НМЦК по одному ТРУ с нормализацией по фармацевтике
        /// </summary>
        /// <param name="text">Наименование ТРУ для поиска, вкючающее лекарственную форму, упаковку, дозировку</param>
        /// <returns></returns>
        [HttpGet]
        [Route("nmck", Name = nameof(GetPharmacyNmck) + "Route")]
        public ContentNmckDto GetPharmacyNmck(string text)
        {
            Logger.Log.Info($"{nameof(GetPharmacyNmck)}: {text}");
            var seacher = new PharmacySearcher(AppSettings.DefaultIndex);
            var contents = seacher.Search(text);
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
                                         Okpd2 = o.Okpd2,
                                         Okei = o.Okei,
                                         Currency = o.Currency,
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

        #endregion //Content

        #region Normalizers

        #region LekForm

        /// <summary>
        /// Получить нормализатор лекарственной формы по умолчанию
        /// </summary>
        [HttpGet]
        [Route("normalizers/lekForm", Name = nameof(GetDefaultLekFormNormalizer) + "Route")]
        public Dictionary<string, Detect> GetDefaultLekFormNormalizer()
        {
            return new LekFormNorm().GetDetects();
        }

        /// <summary>
        /// Получить нормализатор лекарственной формы по номеру
        /// </summary>
        /// <param name="normNumber">Номер пользовательского нормализатора</param>
        [HttpGet]
        [Route("normalizers/lekForm/{normNumber}", Name = nameof(GetLekFormNormalizer) + "Route")]
        public Dictionary<string, Detect> GetLekFormNormalizer(string normNumber)
        {
            return new LekFormNorm(normNumber).GetDetects();
        }

        /// <summary>
        /// Создать нормализатор лекарственной формы с указанным номером
        /// </summary>
        /// <param name="normNumber">Номер пользовательского нормализатора</param>
        /// <param name="detects">Нормализатор лекарственной формы</param>
        /// <returns></returns>
        [HttpPost]
        [Route("normalizers/lekForm/{normNumber}", Name = nameof(PostLekFormNormalizer) + "Route")]
        public HttpStatusCode PostLekFormNormalizer(string normNumber, Dictionary<string, Detect> detects)
        {
            return new LekFormNorm().CreateDetects(normNumber, detects) ? HttpStatusCode.Created : HttpStatusCode.NotFound;
        }

        /// <summary>
        /// Удалить нормализатор лекарственной формы с указанным номером
        /// </summary>
        /// <param name="normNumber">Номер пользовательского нормализатора</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("normalizers/lekForm/{normNumber}", Name = nameof(DeleteLekFormNormalizer) + "Route")]
        public HttpStatusCode DeleteLekFormNormalizer(string normNumber)
        {
            return new LekFormNorm().DeleteDetects(normNumber) ? HttpStatusCode.Created : HttpStatusCode.NotFound;
        }

        #endregion //LekForm

        #region Upak

        /// <summary>
        /// Получить нормализатор упаковки по умолчанию
        /// </summary>
        [HttpGet]
        [Route("normalizers/upak", Name = nameof(GetDefaultUpakNormalizer) + "Route")]
        public List<IDetect> GetDefaultUpakNormalizer()
        {
            return new UpakNorm().GetDetects();
        }

        /// <summary>
        /// Получить нормализатор упаковки по номеру
        /// </summary>
        /// <param name="normNumber">Номер пользовательского нормализатора</param>
        [HttpGet]
        [Route("normalizers/upak/{normNumber}", Name = nameof(GetUpakNormalizer) + "Route")]
        public List<IDetect> GetUpakNormalizer(string normNumber)
        {
            return new UpakNorm(normNumber).GetDetects();
        }

        /// <summary>
        /// Создать нормализатор упаковки с указанным номером
        /// </summary>
        /// <param name="normNumber">Номер пользовательского нормализатора</param>
        /// <param name="detects">Нормализатор упаковки</param>
        /// <returns></returns>
        [HttpPost]
        [Route("normalizers/upak/{normNumber}", Name = nameof(PostUpakNormalizer) + "Route")]
        public HttpStatusCode PostUpakNormalizer(string normNumber, List<Detect> detects)
        {
            return new UpakNorm().CreateDetects(normNumber, detects) ? HttpStatusCode.Created : HttpStatusCode.NotFound;
        }

        /// <summary>
        /// Удалить нормализатор упаковки с указанным номером
        /// </summary>
        /// <param name="normNumber">Номер пользовательского нормализатора</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("normalizers/upak/{normNumber}", Name = nameof(DeleteUpakNormalizer) + "Route")]
        public HttpStatusCode DeleteUpakNormalizer(string normNumber)
        {
            return new UpakNorm().DeleteDetects(normNumber) ? HttpStatusCode.Created : HttpStatusCode.NotFound;
        }

        #endregion //Upak

        #region Doz

        /// <summary>
        /// Получить нормализатор дозировки по умолчанию
        /// </summary>
        [HttpGet]
        [Route("normalizers/doz", Name = nameof(GetDefaultDozNormalizer) + "Route")]
        public Dictionary<string, DozDetector> GetDefaultDozNormalizer()
        {
            return new DozNorm().GetDetects();
        }

        /// <summary>
        /// Получить нормализатор дозировки по номеру
        /// </summary>
        /// <param name="normNumber">Номер пользовательского нормализатора</param>
        [HttpGet]
        [Route("normalizers/doz/{normNumber}", Name = nameof(GetDozNormalizer) + "Route")]
        public Dictionary<string, DozDetector> GetDozNormalizer(string normNumber)
        {
            return new DozNorm(normNumber).GetDetects();
        }

        /// <summary>
        /// Создать нормализатор дозировки с указанным номером
        /// </summary>
        /// <param name="normNumber">Номер пользовательского нормализатора</param>
        /// <param name="detects">Нормализатор дозировки</param>
        /// <returns></returns>
        [HttpPost]
        [Route("normalizers/doz/{normNumber}", Name = nameof(PostDozNormalizer) + "Route")]
        public HttpStatusCode PostDozNormalizer(string normNumber, Dictionary<string, DozDetector> detects)
        {
            return new DozNorm().CreateDetects(normNumber, detects) ? HttpStatusCode.Created : HttpStatusCode.NotFound;
        }

        /// <summary>
        /// Удалить нормализатор дозировки с указанным номером
        /// </summary>
        /// <param name="normNumber">Номер пользовательского нормализатора</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("normalizers/doz/{normNumber}", Name = nameof(DeleteDozNormalizer) + "Route")]
        public HttpStatusCode DeleteDozNormalizer(string normNumber)
        {
            return new DozNorm().DeleteDetects(normNumber) ? HttpStatusCode.Created : HttpStatusCode.NotFound;
        }

        #endregion //Doz

        #endregion //Normalizers

    }
}