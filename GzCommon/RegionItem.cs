using Newtonsoft.Json;

namespace GzCommon
{
    public class RegionItem
    {
        public static readonly RegionItem[] RegionItems = GetRegionItems();

        [JsonProperty(PropertyName = "name")]
        public string Name { get; private set; }
        [JsonProperty(PropertyName = "code")]
        public string Code { get; private set; }

        private static RegionItem[] GetRegionItems()
        {
            return new[]
            {
                new RegionItem {Name = "Республика Адыгея", Code = "01"}, 
                new RegionItem {Name = "Республика Башкортостан", Code = "02"},
                new RegionItem {Name = "Республика Бурятия", Code = "03"},
                new RegionItem {Name = "Республика Алтай", Code = "04"},
                new RegionItem {Name = "Республика Дагестан", Code = "05"},
                new RegionItem {Name = "Республика Ингушетия", Code = "06"},
                new RegionItem {Name = "Кабардино-Балкарская республика", Code = "07"},
                new RegionItem {Name = "Республика Калмыкия", Code = "08"},
                new RegionItem {Name = "Карачаево-Черкесская республика", Code = "09"},
                new RegionItem {Name = "Республика Карелия", Code = "10"},
                new RegionItem {Name = "Республика Коми", Code = "11"},
                new RegionItem {Name = "Республика Марий Эл", Code = "12"},
                new RegionItem {Name = "Республика Мордовия", Code = "13"},
                new RegionItem {Name = "Республика Саха (Якутия)", Code = "14"},
                new RegionItem {Name = "Республика Северная Осетия — Алания", Code = "15"},
                new RegionItem {Name = "Республика Татарстан", Code = "16"},
                new RegionItem {Name = "Республика Тыва", Code = "17"},
                new RegionItem {Name = "Удмуртская республика", Code = "18"},
                new RegionItem {Name = "Республика Хакасия", Code = "19"},
                new RegionItem {Name = "Чеченская республика", Code = "20"},
                new RegionItem {Name = "Челябинская область", Code = "74"},
                new RegionItem {Name = "Чувашская республика", Code = "21"},
                new RegionItem {Name = "Алтайский край", Code = "22"},
                new RegionItem {Name = "Краснодарский край", Code = "23"},
                new RegionItem {Name = "Красноярский край", Code = "24"},
                new RegionItem {Name = "Приморский край", Code = "25"},
                new RegionItem {Name = "Ставропольский край", Code = "26"},
                new RegionItem {Name = "Хабаровский край", Code = "27"},
                new RegionItem {Name = "Амурская область", Code = "28"},
                new RegionItem {Name = "Архангельская область", Code = "29"},
                new RegionItem {Name = "Астраханская область", Code = "30"},
                new RegionItem {Name = "Белгородская область", Code = "31"},
                new RegionItem {Name = "Брянская область", Code = "32"},
                new RegionItem {Name = "Камчатский край", Code = "41"},

                new RegionItem {Name = "Московская область", Code = "50"},
                new RegionItem {Name = "Пермский край", Code = "59"},
                new RegionItem {Name = "Забайкальский край", Code = "75"},
                new RegionItem {Name = "Москва", Code = "77"},
                new RegionItem {Name = "Республика Крым", Code = "91"},
                new RegionItem {Name = "Иные территории, включая город и космодром Байконур", Code = "99"}
                
            };
        }
    }
}