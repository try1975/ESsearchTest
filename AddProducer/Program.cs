using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using AddProducer.Model;
using Nest;

namespace AddProducer
{
    internal static class Program
    {
        private static void Main()
        {
            var changes = new List<Change>
            {
                //new Change {Seller = "Е-Лекарь", FieldName = "column9", Host = "http://www.e-lekar.ru"},
                //new Change {Seller = "Александровская", FieldName = "column6", Host = "http://www.apteka-a.ru"},
                //new Change {Seller = "ФармПростор", FieldName = "column6", Host = "http://www.farmprostor.ru"},
                new Change {Seller = "Аптека_Диалог", FieldName = "column6", Host = "https://www.dialog.ru/"},
                new Change {Seller = "Аптекаинет", FieldName = "column6", Host = "http://www.aptekainet.ru"},
                new Change {Seller = "Kremlewka", FieldName = "column6", Host = "http://www.kremlewka.ru"},
                new Change {Seller = "LuxFarma", FieldName = "column7", Host = "http://luxfarma.com"}

            };
            try
            {
                var connectionSettings = new ConnectionSettings(new Uri(AppSettings.Host))
                    //.OnRequestCompleted(details =>
                    //{
                    //    var s = details.RequestBodyInBytes != null
                    //        ? Encoding.UTF8.GetString(details.RequestBodyInBytes)
                    //        : null;
                    //    Debug.WriteLine($"{s}");
                    //})
                    .DisableDirectStreaming()
                    .DefaultIndex(AppSettings.DefaultIndex)
                    .BasicAuthentication(AppSettings.UserName, AppSettings.Password);
                var client = new ElasticClient(connectionSettings);

                foreach (var change in changes)
                {
                    var response = client.Search<Content>(s => s
                        .Take(1)
                        .Query(q => q
                            .Match(m => m
                                .Field(p => p.seller)
                                .Query(change.Seller)))
                        .Sort(ss => ss.Descending(p => p.collectedAt))
                        );
                    var total = response.Total;
                    var newContent = response.Hits.Select(s => s.Source).FirstOrDefault();
                    if (newContent == null) continue;
                    var dateValue = newContent.Timestamp;
                    dateValue = dateValue.AddDays(-5);
                    // ? удалить старые

                    while (total > 0)
                    {
                        // 3 day minus
                        //var dateValue = newContent.collectedAt- 259200;

                        response = client.Search<Content>(s => s
                            .Take(200)
                            .Query(q => q

                                .Bool(b => b
                                    .MustNot(mun => mun
                                        .Exists(ex => ex
                                            .Field(f => f.producer)))
                                    .Must(mu => mu
                                        .Match(m => m
                                            .Field(p => p.seller)
                                            .Query(change.Seller)))


                            .Filter(fi => fi.DateRange(ra => ra.Field(fil => fil.Timestamp).GreaterThan(dateValue))))));
                        Debug.WriteLine(response.Total);
                        if (response.Hits.Count == 0)
                        {
                            total = 0;
                            continue;
                        }
                        var contents = response.Hits.Select(s => s.Source).ToList();
                        foreach (var content in contents)
                        {
                            var producerValue = "";
                            if (change.FieldName == "column6") producerValue = content.column6;
                            if (change.FieldName == "column7") producerValue = content.column7;
                            if (change.FieldName == "column8") producerValue = content.column8;
                            if (change.FieldName == "column9") producerValue = content.column9;

                            client.Update<Content, ContentProducer>(DocumentPath<Content>.Id(content.id),
                                descriptor => descriptor
                                    .Doc(new ContentProducer
                                    {
                                        Producer = producerValue
                                    }));
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception);
                throw;
            }

            //http://stackoverflow.com/questions/35630189/how-to-update-an-elasticsearch-document-in-nest2/35632211#35632211
        }
    }
}