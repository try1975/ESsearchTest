using System.IO;

namespace Price.WebApi.Logic
{
    public static class PathService
    {
        public static string GetElangPath(string host)
        {
            var fileName = $"{GetFileNameFromHostName(host)}.txt";
            var elangPath = Path.Combine(AppGlobal.ElangPath, fileName);
            if (File.Exists(elangPath)) return elangPath;
            var sourceDto = SourceNames.GetSourceDtoByHost(host);
            if (sourceDto == null) return string.Empty;
            File.WriteAllText(elangPath, sourceDto.Expression);
            return elangPath;
        }

        public static string GetUrlStatePath(string partialFileName, string taskId)
        {
            return Path.Combine(AppGlobal.UrlStatePath, $"{partialFileName}_{taskId}.txt");
        }

        public static string GetUrlPath(string partialFileName, string taskId)
        {
            return Path.Combine(AppGlobal.UrlPath, $"{partialFileName}_{taskId}.txt");
        }

        public static string GetCsvPath(string partialFileName, string taskId)
        {
            return Path.Combine(AppGlobal.CsvPath, $"{partialFileName}_{taskId}.csv");
        }

        public static string GetFileNameFromHostName(string host)
        {
            return $"{host.Replace('.', '_')}";
        }
    }
}