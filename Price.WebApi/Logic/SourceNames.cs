using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Common.Dto.Model;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;

namespace Price.WebApi.Logic
{
    public static class SourceNames
    {
        public static readonly ConcurrentDictionary<string, SourceDto> Names;


        static SourceNames()
        {
            var namesFile = Path.Combine(AppGlobal.ElangPath, "dictionary.txt");
            if (File.Exists(namesFile))
            {
                Names =
                    JsonConvert.DeserializeObject<ConcurrentDictionary<string, SourceDto>>(File.ReadAllText(namesFile));
            }
            else
            {
                Names = new ConcurrentDictionary<string, SourceDto>();
            }
        }

        public static string GetSourceName(string host)
        {
            var sourceDto = GetSourceDtoByHost(host);
            return sourceDto != null ? sourceDto.Nm : string.Empty;
        }

        public static SourceDto GetSourceDtoByHost(string host)
        {
            if (Names.ContainsKey(host))
            {
                return Names[host];
            }
            var pair = Names.Where(d => d.Value.Dsc.Contains(host))
                .Select(e => (KeyValuePair<string, SourceDto>?) e)
                .FirstOrDefault()
                ;
            if (pair != null) return pair.Value.Value;
            var connectionString = ConfigurationManager.ConnectionStrings["Sources"].ConnectionString;
            using (var dbConn = new MySqlConnection(connectionString))
            {
                try
                {
                    dbConn.Open();
                }
                catch (Exception exception)
                {
                    Debug.WriteLine(exception);
                    Logger.Log.Error($"{exception}");
                    return null;
                }

                var cmd = dbConn.CreateCommand();
                cmd.CommandText =
                    $"SELECT nm, dsc, expression, coalesce(tel, '') FROM source.source where lower(dsc) like '%{host}%' order by id DESC";

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var dto = new SourceDto
                    {
                        Nm = reader.GetString(0),
                        Dsc = reader.GetString(1),
                        Expression = reader.GetString(2),
                        Phones = reader.GetString(3)
                    };
                    Names[host] = dto;
                    File.WriteAllText(Path.Combine(AppGlobal.ElangPath, "dictionary.txt"),
                        JsonConvert.SerializeObject(Names, Formatting.Indented));
                    return dto;
                }
            }
            return null;
        }
    }
}