using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using ClassLibrary1;

namespace HelloApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string YourReadFileAddress = @"C:\Users\Inkko\Downloads\tolstoj_lew_nikolaewich-text_0040.fb2"; //Замените на свой путь к считываемому файлу, начиная с @
            string YourWriteFileName = @"C:\Users\Inkko\Downloads\Test.txt"; //Замените на Свой путь к создаваемому файлу с указанием его имени.

            var sw = Stopwatch.StartNew();
            string path = YourReadFileAddress;
            try
            {
                Dict a = new Dict();
                var method = typeof(Dict).GetMethod("CountWords", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static);
                object[] param = new object[] {path};
                var result = new ConcurrentDictionary<string, int>();
                result = (ConcurrentDictionary<string, int>)method.Invoke(a, param);

                StreamWriter swr = new(YourWriteFileName, true);
                var resultD = result.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
                foreach (var word in resultD)
                {
                    swr.WriteLine("{0}\t {1} ", word.Key, word.Value);
                }
                swr.Close();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            sw.Stop();
            Console.WriteLine($"Время выполнения: {sw.Elapsed:hh\\:mm\\:ss\\:fff}");
        }
    }
}