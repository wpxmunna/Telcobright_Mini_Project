using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SplitLargeSQL
{
    class Program
    {
        static void Main()
        {
            string large_file_dir = "C:/temp/largefile";
            string split_file_dir = "C:/temp/splitfile";

            Directory.CreateDirectory(split_file_dir);

            string[] fileNames = Directory.GetFiles(large_file_dir);

            foreach (string fileName in fileNames)
            {
                string inputFilePath = fileName;
                SplitSqlFile(inputFilePath, split_file_dir, 50000);
            }

            Console.WriteLine("SQL file has been successfully split into smaller files.");
        }

        static void SplitSqlFile(string inputFilePath, string outputDirectory, int linesPerFile)
        {
            string sqlContent = File.ReadAllText(inputFilePath);
            string largeFileName = Path.GetFileNameWithoutExtension(inputFilePath);
            int fileIndex = 1;
            int linesWritten = 0;
            List<string> currentStatements = new List<string>();

            using (StringReader reader = new StringReader(sqlContent))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (!string.IsNullOrWhiteSpace(line))
                    {
                        // Trim the line and remove trailing semicolon if exists
                        string trimmedLine = line.TrimEnd(' ', '\t', '\r', '\n', ';');

                        // Append semicolon to the trimmed line
                        string sqlStatement = trimmedLine + ";";
                        currentStatements.Add(sqlStatement);
                        linesWritten++;

                        if (linesWritten >= linesPerFile)
                        {
                            string outputFileName = $"{largeFileName}_{fileIndex}.sql";
                            string outputFilePath = Path.Combine(outputDirectory, outputFileName);
                            File.WriteAllLines(outputFilePath, currentStatements);

                            fileIndex++;
                            linesWritten = 0;
                            currentStatements.Clear();
                        }
                    }
                }
            }

            // Write remaining statements to a file
            if (currentStatements.Count > 0)
            {
                string outputFileName = $"{largeFileName}_{fileIndex}.sql";
                string outputFilePath = Path.Combine(outputDirectory, outputFileName);
                File.WriteAllLines(outputFilePath, currentStatements);
            }
        }
    }
}
