using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TradingSystem.BuissnessLayer
{
    public class DirAppend
    {
        public static void AddToLogger(string action, Result result)
        {
            using (StreamWriter w = File.AppendText("log.txt"))
            {
                Log("Action=> " + action + "-------------" + "Result " + result + "\n", w);
            }

            using (StreamReader r = File.OpenText("log.txt"))
            {
                DumpLog(r);
            }

        }

        public static void Log(string logMessage, TextWriter w)
        {
            w.Write("\r\nLog Entry : ");
            w.WriteLine($"{DateTime.Now.ToLongTimeString()} {DateTime.Now.ToLongDateString()}");
            w.WriteLine("  :");
            w.WriteLine($"  :{logMessage}");
            w.WriteLine("-------------------------------");
        }

        public static void DumpLog(StreamReader r)
        {
            string line;
            while ((line = r.ReadLine()) != null)
            {
                Console.WriteLine(line);
            }
        }
    }
}

