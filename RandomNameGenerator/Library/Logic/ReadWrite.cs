using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Library.Logic
{
    class ReadWrite
    {
        public static List<string> ReadFile(string filePath)
        {
            var extension = Path.GetExtension(filePath).ToLower();

            return extension switch
            {
                ".txt" => ReadTextFile(filePath),
                ".docx" => ReadWordFile(/*filePath*/),
                ".xlsx" => ReadExcelFile(/*filePath*/),
                _ => throw new NotSupportedException("File format not supported."),
            };
        }

        public static List<string> ReadTextFile(string filePath)
        {
            List<string> names = [];

            using (StreamReader reader = new(filePath))
            {
                string? line;
                while ((line = reader.ReadLine()) != null)
                {
                    names.Add(line);
                }
            }

            return names ?? [];
        }

        public static List<string> ReadWordFile(/*string filePath*/)
        {
            List<string> names = [];

            return names ?? [];
        }

        public static List<string> ReadExcelFile(/*string filePath*/)
        {
            return [];
        }
    }
}
