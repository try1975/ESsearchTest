using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using GzDocs.Services;

namespace GzDocs.Controllers
{
    [RoutePrefix("api/docsearch")]
    public class DocSearchController : ApiController
    {
        [HttpGet, Route(""), ResponseType(typeof(Dictionary<string, string>))]
        public Dictionary<string, string> Get(string key)
        {
            var docs = new Dictionary<string, string>();

            var connection = new OleDbConnection(@"Provider=Search.CollatorDSO;Extended Properties=""Application=Windows""");

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

            

            var query5 = $@"SELECT TOP {AppGlobal.GzMaxDocCount} System.ItemName, System.ItemUrl FROM SystemIndex " +
                         $@"WHERE scope = '{AppGlobal.FileStorePath}' AND FREETEXT('%{key}%') ";


            var baseUrl = $"{Request.RequestUri.Scheme}://{Request.RequestUri.Host}:{Request.RequestUri.Port}/api/doccontent";

            connection.Open();

            var command = new OleDbCommand(query5, connection);

            try
            {
                using (var r = command.ExecuteReader())
                {
                    while (r.Read())
                    {
                        var uri = r[1].ToString();
                        uri = uri.Remove(0, AppGlobal.FileStorePath.Length+1);
                        uri = HttpUtility.UrlEncode(uri, System.Text.Encoding.UTF8);
                        uri = baseUrl + "?docPath=" + uri;

                        docs.Add(uri , r[0].ToString());
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //throw;
            }

            connection.Close();


            return docs;
        }
    }

}