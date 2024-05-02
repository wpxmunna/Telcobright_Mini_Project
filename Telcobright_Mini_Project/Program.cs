using System;
using System.Collections.Generic;
using System.IO;

namespace Telcobright_Mini_Project
{
    class Program
    {
        static void Main(string[] args)
        {
            string sourceDirectory = "C:/temp";
            string[] destinationRootDirectories =
            {
                @"D:\telcobright\vault\resources\cdr", @"E:\telcobright\vault\resources\cdr",
                @"F:\telcobright\vault\resources\cdr", @"G:\telcobright\vault\resources\cdr",
                @"H:\telcobright\vault\resources\cdr", @"I:\telcobright\vault\resources\cdr"
            };

            MultyVaultFileMover(sourceDirectory, destinationRootDirectories);
        }

        static void MultyVaultFileMover(string sourceDirectory, string[] destinationRootDirectories)
        {
            int numDestinationDirectories = destinationRootDirectories.Length;
            string[] sourceFiles = Directory.GetFiles(sourceDirectory);
            int destinationIndex = 0;
            HashSet<string> processedFiles = new HashSet<string>();

            foreach (string sourceFilePath in sourceFiles)
            {
                string sourceFileName = Path.GetFileName(sourceFilePath);

                try
                {
                    // Check if the file has already been processed
                    //if (processedFiles.Contains(sourceFileName))
                    //{
                    //    Console.WriteLine($"File '{sourceFileName}' has already been processed.");
                    //    continue;
                    //}

                    string destinationRootDirectory = destinationRootDirectories[destinationIndex];
                    int index = Math.Abs(sourceFileName.GetHashCode()) % numDestinationDirectories;
                    string destinationDirectory = destinationRootDirectory;
                    string destinationFilePath = Path.Combine(destinationDirectory, sourceFileName);

                    File.Copy(sourceFilePath, destinationFilePath, true);
                    Console.WriteLine($"File '{sourceFileName}' copied successfully to {destinationFilePath}");

                    // Move to the next destination directory in round-robin fashion
                    destinationIndex = (destinationIndex + 1) % destinationRootDirectories.Length;

                    // Add the processed file to the set
                    //processedFiles.Add(sourceFileName);
                }
                catch (IOException iox)
                {
                    Console.WriteLine($"Error copying file '{sourceFileName}': {iox.Message}");
                }
            }
        }
    }
}
