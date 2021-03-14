    using System;
    using System.Collections.Concurrent;
    using System.IO;
    using System.Text;
    using System.Threading.Tasks;

    namespace ClassLibrary1
    {
        public class Dict
        {
        static ConcurrentDictionary<string, int> CountWords(string path)
        {
            var result = new ConcurrentDictionary<string, int>();
            Parallel.ForEach(File.ReadLines(path, Encoding.UTF8), line =>
            {
                var words = line.Split(new[] { ' ', '-', ':', '.', '"', '!', '?', ',', ';', ')', '(', '\\', '/', '<', '>', '*', '[', ']'}, StringSplitOptions.RemoveEmptyEntries);
                foreach (var word in words)
                {
                    result.AddOrUpdate(word, 1, (_, x) => x + 1);
                }
            });
            return result;
        }
        }
    }
