using System.Collections.Concurrent;

namespace Price.WebApi.Logic
{
    public static class StateFiles
    {
        public static readonly ConcurrentDictionary<int, int> Dictionary;
        static StateFiles()
        {
            Dictionary = new ConcurrentDictionary<int, int>();
        }
    }
}