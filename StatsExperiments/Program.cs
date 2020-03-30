using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using CsvHelper;

namespace StatsExperiments
{
    class Program
    {
        static void Main(string[] args)
        {
            RunRScript("LinReg.R", "TestData.csv");
        }

        static void RunRScript(string scriptFilePath, string dataFilePath)
        {
            var watch = new Stopwatch();
            watch.Start();
            var startInfo = new ProcessStartInfo(@"C:\Program Files\R\R-3.6.3\bin\x64\rscript.exe")
            {
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                Arguments = $"{scriptFilePath} {dataFilePath}"
            };

            var process = new Process { StartInfo = startInfo };
            process.Start();

            // var data = File.ReadAllText(dataFilePath);
            // var streamWriter = process.StandardInput;
            // streamWriter.WriteLine(dataFilePath);
            // streamWriter.Close();
            
            var output = process.StandardOutput;
            var error = process.StandardError;

            var outputStr = output.ReadToEnd();
            watch.Stop();
            Console.WriteLine("Time: {0}", watch.ElapsedMilliseconds);
            Console.WriteLine(outputStr);

            /*var reader = new CsvReader(output, CultureInfo.InvariantCulture);
            reader.Read();
            var dynamicRecord = reader.GetRecord<dynamic>();
            var record = dynamicRecord as IDictionary<string, object>;
            if (record == null)
                return;
            
            Console.WriteLine("r-squared: {0}", record["r-squared"]);
            Console.WriteLine("adjusted r-squared: {0}", record["adjusted-r-squared"]);
            Console.WriteLine("f-statistic: {0}", record["f-statistic"]);*/
        }
    }
}