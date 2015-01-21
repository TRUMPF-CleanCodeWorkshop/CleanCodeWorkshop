using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silbentrenner.Fileadapter
{
    using System.IO;

    public class Fileadapter
    {
        public static string ReadTextFromFile(string path)
        {
            using (StreamReader reader = new StreamReader(path))
            {
                return reader.ReadToEnd();
            }
        }

        public static void SaveToOutputFile(string dividedText, string path)
        {
            throw new NotImplementedException();
        }
    }
}
