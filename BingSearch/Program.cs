using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Text;

namespace BingSearch
{
    internal static class Program
    {
        // **********************************************
        // *** Update or verify the following values. ***
        // **********************************************

        // Verify the endpoint URI.  At this writing, only one endpoint is used for Bing
        // search APIs.  In the future, regional endpoints may be available.  If you
        // encounter unexpected authorization errors, double-check this value against
        // the endpoint for your Bing Web search instance in your Azure dashboard.
        private const string UriBase = "https://api.cognitive.microsoft.com/bing/v7.0/search";

        private const string SearchTerm = "цитрамон таблетки купить";

        // Used to return search results including relevant headers
        private struct SearchResult
        {
            // ReSharper disable once InconsistentNaming
            public string jsonResult;
            // ReSharper disable once InconsistentNaming
            public Dictionary<string, string> relevantHeaders;
        }

        private static void Main()
        {
            //Console.OutputEncoding = Encoding.UTF8;
            var bingAccessKey = ConfigurationManager.AppSettings["BingAccessKey"];
            if (bingAccessKey.Length == 32)
            {
                Console.WriteLine("Searching the Web for: " + SearchTerm);

                SearchResult result = BingWebSearch(SearchTerm);

                Console.WriteLine("\nRelevant HTTP Headers:\n");
                foreach (var header in result.relevantHeaders)
                    Console.WriteLine(header.Key + ": " + header.Value);

                Console.WriteLine("\nJSON Response:\n");
                Console.WriteLine(JsonPrettyPrint(result.jsonResult));
            }
            else
            {
                Console.WriteLine("Invalid Bing Search API subscription key!");
                Console.WriteLine("Please paste yours into the source code.");
            }

            Console.Write("\nPress Enter to exit ");
            Console.ReadLine();
        }

        /// <summary>
        /// Performs a Bing Web search and return the results as a SearchResult.
        /// </summary>
        private static SearchResult BingWebSearch(string searchQuery)
        {
            // Construct the URI of the search request
            var uriQuery = $"{UriBase}?q={Uri.EscapeDataString(searchQuery)}&count=50&offset=0";

            // Perform the Web request and get the response
            WebRequest request = WebRequest.Create(uriQuery);
            var bingAccessKey = ConfigurationManager.AppSettings["BingAccessKey"];
            request.Headers["Ocp-Apim-Subscription-Key"] = bingAccessKey;
            HttpWebResponse response = (HttpWebResponse)request.GetResponseAsync().Result;
            string json = new StreamReader(response.GetResponseStream()).ReadToEnd();

            // Create result object for return
            var searchResult = new SearchResult()
            {
                jsonResult = json,
                relevantHeaders = new Dictionary<string, string>()
            };

            // Extract Bing HTTP headers
            foreach (string header in response.Headers)
            {
                if (header.StartsWith("BingAPIs-") || header.StartsWith("X-MSEdge-"))
                    searchResult.relevantHeaders[header] = response.Headers[header];
            }

            return searchResult;
        }

        /// <summary>
        /// Formats the given JSON string by adding line breaks and indents.
        /// </summary>
        /// <param name="json">The raw JSON string to format.</param>
        /// <returns>The formatted JSON string.</returns>
        private static string JsonPrettyPrint(string json)
        {
            if (string.IsNullOrEmpty(json))
                return string.Empty;

            json = json.Replace(Environment.NewLine, "").Replace("\t", "");

            StringBuilder sb = new StringBuilder();
            bool quote = false;
            bool ignore = false;
            char last = ' ';
            int offset = 0;
            int indentLength = 2;

            foreach (char ch in json)
            {
                switch (ch)
                {
                    case '"':
                        if (!ignore) quote = !quote;
                        break;
                    case '\\':
                        if (quote && last != '\\') ignore = true;
                        break;
                }

                if (quote)
                {
                    sb.Append(ch);
                    if (last == '\\' && ignore) ignore = false;
                }
                else
                {
                    switch (ch)
                    {
                        case '{':
                        case '[':
                            sb.Append(ch);
                            sb.Append(Environment.NewLine);
                            sb.Append(new string(' ', ++offset * indentLength));
                            break;
                        case '}':
                        case ']':
                            sb.Append(Environment.NewLine);
                            sb.Append(new string(' ', --offset * indentLength));
                            sb.Append(ch);
                            break;
                        case ',':
                            sb.Append(ch);
                            sb.Append(Environment.NewLine);
                            sb.Append(new string(' ', offset * indentLength));
                            break;
                        case ':':
                            sb.Append(ch);
                            sb.Append(' ');
                            break;
                        default:
                            if (quote || ch != ' ') sb.Append(ch);
                            break;
                    }
                }
                last = ch;
            }

            return sb.ToString().Trim();
        }
    }
}
