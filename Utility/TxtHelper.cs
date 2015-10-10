using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace LiveSplit.Utility
{
    public static class TxtHelper
    {
        public static List<string> readFile(string Path)
        {
            StreamReader inputStream;
            try {
                inputStream = new StreamReader(Path);
            }
            catch (FileNotFoundException e)
            {
                return null;
            }

            List<string> result = new List<string>();
            string currentLine;

            while ((currentLine = inputStream.ReadLine()) != null)
            {
                result.Add( currentLine);
            }

            return result;
        }
    }
}
