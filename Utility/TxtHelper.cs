using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

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
                MessageBox.Show(e.ToString());
                return null;
            }

            List<string> result = new List<string>();
            string currentLine;

            while ((currentLine = inputStream.ReadLine()) != null)
            {
                result.Add( currentLine);
            }

            inputStream.Close();

            return result;
        }
    }
}
