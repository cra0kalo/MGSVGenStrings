using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace genstr
{
    using genstr.FOXEngine;

    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("MGSV FPK StringBuilder");
            Console.WriteLine("Author: Cra0");
            Console.WriteLine("-----------------------");
            //get the data from the arg1
            if (args.Length == 0 || args.Length < 1)
            {
                Console.WriteLine("Usage: genstr folderpath");
                Console.WriteLine("Example: genstr C:/mgs5/fpk_all");
                Console.ReadKey();
                return;
            }

            string folderPath = args[0];
            if (!Directory.Exists(folderPath))
            {
                Console.WriteLine(folderPath + " doesn't seem to be a valid folder??");
                Console.ReadKey();
                return;
            }

            string dictionaryPath = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "fpk_dictionary.txt");
            StreamWriter sw = new StreamWriter(dictionaryPath, false, Encoding.ASCII);

            //All good lets start looking for fpk files
            var ext = new List<string> {".fpk"};
            var myFiles = Directory.GetFiles(folderPath, "*.*", SearchOption.AllDirectories)
                          .Where(s => ext.Any(e => s.EndsWith(e)));

            foreach (var item in myFiles)
            {
                // do your stuff
                string item_path = item;
                FPK cur_fpk = new FPK(sw);
                bool parsed = cur_fpk.ParseFPK(item_path);
                if (parsed)
                {
                    Console.WriteLine("Parsed: " + item_path);
                }
                else
                {
                    Console.WriteLine("Error Parsing: " + item_path);
                }
            }

            sw.Flush();
            sw.Close();



        }
    }
}
