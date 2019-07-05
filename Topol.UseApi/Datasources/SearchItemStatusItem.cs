using PriceCommon.Enums;

namespace Topol.UseApi.Datasources
{
    public class SearchItemStatusItem
    {
        public string Text { get; set; }
        public TaskStatus? TaskStatus { get; set; }

        public static SearchItemStatusItem[] GetSearchItemStatusItems()
        {
            return new[] {
                new SearchItemStatusItem { Text="Любое", TaskStatus=null},
                new SearchItemStatusItem { Text=PriceCommon.Utils.Utils.GetDescription(PriceCommon.Enums.TaskStatus.InProcess), TaskStatus=PriceCommon.Enums.TaskStatus.InProcess},
                new SearchItemStatusItem { Text=PriceCommon.Utils.Utils.GetDescription(PriceCommon.Enums.TaskStatus.Checked), TaskStatus=PriceCommon.Enums.TaskStatus.Checked},
                new SearchItemStatusItem { Text=PriceCommon.Utils.Utils.GetDescription(PriceCommon.Enums.TaskStatus.Ok), TaskStatus=PriceCommon.Enums.TaskStatus.Ok},
                new SearchItemStatusItem { Text=PriceCommon.Utils.Utils.GetDescription(PriceCommon.Enums.TaskStatus.BreakByTimeout), TaskStatus=PriceCommon.Enums.TaskStatus.BreakByTimeout},
                new SearchItemStatusItem { Text=PriceCommon.Utils.Utils.GetDescription(PriceCommon.Enums.TaskStatus.Break), TaskStatus=PriceCommon.Enums.TaskStatus.Break},
                new SearchItemStatusItem { Text=PriceCommon.Utils.Utils.GetDescription(PriceCommon.Enums.TaskStatus.Error), TaskStatus=PriceCommon.Enums.TaskStatus.Error}
            };
        }
    }
}