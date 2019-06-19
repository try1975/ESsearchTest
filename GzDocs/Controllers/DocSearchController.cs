using GzDocs.Services;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;

namespace GzDocs.Controllers
{
    [RoutePrefix("api/docsearch")]
    public class DocSearchController : ApiController
    {
        [HttpGet, Route(""), ResponseType(typeof(Dictionary<string, string>))]
        public Dictionary<string, string> Get(string key, string regions = "", string months = "")
        {
            var docs = new Dictionary<string, string>();
            var path = AppGlobal.FileStorePath;
            var baseUrl = $"{Request.RequestUri.Scheme}://{Request.RequestUri.Host}:{Request.RequestUri.Port}/api/doccontent";
            var count = AppGlobal.GzMaxDocCount;

            if (string.IsNullOrEmpty(regions) && string.IsNullOrEmpty(months))
            {
                GetDocsByCondition(key, path, baseUrl, count, docs);
            }
            else if (string.IsNullOrEmpty(regions))
            {
                foreach (var month in months.Split(','))
                {
                    GetDocsByCondition(key, path, baseUrl, count, docs, "", month.Trim());
                }
            }
            else if (string.IsNullOrEmpty(months))
            {
                foreach (var region in regions.Split(','))
                {
                    GetDocsByCondition(key, path, baseUrl, count, docs, region.Trim(), "");
                }
            }
            else
            {
                foreach (var region in regions.Split(','))
                {
                    foreach (var month in months.Split(','))
                    {
                        GetDocsByCondition(key, path, baseUrl, count, docs, region.Trim(), month.Trim());
                    }
                }
            }

            return docs;
        }

        private static void GetDocsByCondition(string key, string path, string baseUrl, int count, Dictionary<string, string> docs, string region = "", string month = "")
        {
            //// File name search (case insensitive), also searches sub directories
            //var query1 = @"SELECT System.ItemName FROM SystemIndex " +
            //    $@"WHERE scope ='file:C:/' AND System.ItemName LIKE '%{key}%'";

            //// File name search (case insensitive), does not search sub directories
            //var query2 = @"SELECT System.ItemName FROM SystemIndex " +
            //             $@"WHERE directory = 'file:C:/' AND System.ItemName LIKE '%{key}%' ";

            //// Folder name search (case insensitive)
            //var query3 = @"SELECT System.ItemName FROM SystemIndex " +
            //             $@"WHERE scope = 'file:C:/' AND System.ItemType = 'Directory' AND System.Itemname LIKE '%{key}%' ";

            //// Folder name search (case insensitive), does not search sub directories
            //var query4 = @"SELECT System.ItemName FROM SystemIndex " +
            //             $@"WHERE directory = 'file:C:/' AND System.ItemType = 'Directory' AND System.Itemname LIKE '%{key}%' ";

            // Content file search
            //var query5 = @"SELECT TOP 20 System.ItemName, System.ItemUrl, System.ContainedItems, System.FindData, System.Keywords FROM SystemIndex " +

            var filter = "%" + Path.GetFileName(path) + "\\";
            if (string.IsNullOrEmpty(region) && string.IsNullOrEmpty(month))
            {
                filter = "";
            }
            else if (!string.IsNullOrEmpty(region) && string.IsNullOrEmpty(month))
            {
                filter = filter + region + "_%";
            }
            else if (string.IsNullOrEmpty(region) && !string.IsNullOrEmpty(month))
            {
                filter = filter + "%_" + month + "\\%";
            }
            else
            {
                filter = filter + region + "_" + month + "\\%";
            }

                var connection = new OleDbConnection(@"Provider=Search.CollatorDSO;Extended Properties=""Application=Windows""");

            var query5 = $@"SELECT TOP {count} System.ItemName, System.ItemUrl FROM SystemIndex " +
                         $@"WHERE scope = '{path}' AND FREETEXT('%{key.Trim()}%') ";
            if (!string.IsNullOrEmpty(filter)) query5 = query5 + $" AND System.ItemPathDisplay LIKE '{filter}'";

            connection.Open();

            var command = new OleDbCommand(query5, connection);

            try
            {
                using (var r = command.ExecuteReader())
                {
                    while (r.Read())
                    {
                        var uri = r[1].ToString();
                        uri = uri.Remove(0, path.Length + 1);
                        uri = HttpUtility.UrlEncode(uri, System.Text.Encoding.UTF8);
                        uri = baseUrl + "?docPath=" + uri;

                        docs.Add(uri, r[0].ToString());
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //throw;
            }

            connection.Close();

        }
    }

}