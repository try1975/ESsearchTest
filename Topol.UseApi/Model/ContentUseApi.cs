﻿using System;
using System.Linq;
using Common.Dto;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using PriceCommon.Enums;

namespace Topol.UseApi.Model
{

    /// <summary>
    ///     Результат поиска
    /// </summary>
    public class ContentDto
    {
        private string _priceVariants;

        [JsonIgnore]
        public bool Selected { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public PriceType PriceType { get; set; }

        [JsonIgnore]
        public string PriceTypeString => PriceCommon.Utils.Utils.GetDescription(PriceType);

        /// <summary>
        ///     Наименование позиции
        /// </summary>
        public string Name { get; set; }


        /// <summary>
        ///     Цена позиции
        /// </summary>
        //[JsonIgnore]
        public string Price { get; set; }

        /// <summary>
        ///     Цена позиции
        /// </summary>
        public double Nprice
        {
            get {
                try
                {
                    return Convert.ToDouble(Price);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
                return 0.00;
            }
        }

        /// <summary>
        ///     Ссылка на источник
        /// </summary>
        public string Uri { get; set; }

        /// <summary>
        ///     Продавец
        /// </summary>
        public string Seller { get; set; }

        /// <summary>
        ///     Дата и время сбора цены (UTC)
        /// </summary>
        public long? CollectedAt { get; set; }

        /// <summary>
        ///     Дата и время сбора цены
        /// </summary>
        public DateTime Collected
        {
            get { return new DateTime(1970, 1, 1, 0, 0, 0).AddSeconds(Convert.ToDouble(CollectedAt)) /*.Date*/; }
        }

        /// <summary>
        ///     Идентификатор позиции
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        ///     Производитель
        /// </summary>
        public string Producer { get; set; }

        /// <summary>
        ///     Телефоны
        /// </summary>
        public string Phones { get; set; }

        /// <summary>
        ///     ОКПД2
        /// </summary>
        public string Okpd2 { get; set; }

        /// <summary>
        ///     Единица измерения
        /// </summary>
        public string Okei { get; set; }

        /// <summary>
        ///     Валюта
        /// </summary>
        public string Currency { get; set; }

        /// <summary>
        ///     Варианты цен
        /// </summary>
        public string PriceVariants
        {
            get { return _priceVariants; }
            set
            {
                _priceVariants = value;
                if(string.IsNullOrEmpty(_priceVariants)) return;
                PriceVariant = _priceVariants.Split(new[] {"  "}, StringSplitOptions.RemoveEmptyEntries).FirstOrDefault();
            }
        }

        [JsonIgnore]
        public string PriceVariant { get; set; }



        public static ContentDto FromAnalystCsv(string csvLine)
        {
            var values = csvLine.Split(';');
            var contentDto = new ContentDto
            {
                Uri = values[0].Replace("\"", ""),
                Name = values[1].Replace("\"", ""),
                Price = values[2].Replace("\"", ""),
                CollectedAt = Convert.ToInt64(values[8].Replace("\"", "")),
                PriceVariants = values[10].Replace("\"", "")
            };
            contentDto.Id = Md5Logstah.GetDefaultId(contentDto.Uri, contentDto.Name);

            return contentDto;
        }

        
    }
}