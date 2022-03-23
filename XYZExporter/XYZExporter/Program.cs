using System;
using System.Collections.Generic;
using System.IO;

namespace XYZExporter
{
    internal class Program
    {
        static void Main(string[] args)
        {

            string outputFilePath = args[0];
            string inputFilePath = args[1];
            string outputFormat = args[2];

            Logger.ResultPath = outputFilePath;
              
            if (!File.Exists(inputFilePath))
            {
                Logger.Log("[Error] Wrong path or file does not exist");
                throw new FileNotFoundException("Plik " + inputFilePath + " nie istnieje");
            }

            var fileReader = new FileManager();
            fileReader.ReadAllLines(inputFilePath);
            fileReader.SaveToFile(outputFilePath);
        }
    }
}
