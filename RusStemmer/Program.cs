using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Iveonik.Stemmers;

namespace RusStemmer
{
    class Program
    {
        static void Main(string[] args)
        {
            //TestStemmer(new RussianStemmer(), "краcОта", "красоту", "красоте", "КрАсОтОй", "раcписание", "рейсов", "москву", "московских");
            //TestStemmer(new EnglishStemmer(), "jump", "jumping", "jumps", "jumped");
            //TestStemmer(new GermanStemmer(), "mochte", "mochtest", "mochten", "mochtet");

            TestStemmer(new RussianStemmer(), "раcписание", "рейсов", "москву", "московских", "московскen");
            Console.ReadKey();
        }

        private static void TestStemmer(IStemmer stemmer, params string[] words)
        {
            Console.WriteLine("Stemmer: " + stemmer);
            foreach (string word in words)
            {
                Console.WriteLine(word + " --> " + stemmer.Stem(word));
            }
        }
    }
}
