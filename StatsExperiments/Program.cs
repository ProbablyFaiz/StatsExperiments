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
            var startInfo = new ProcessStartInfo("/usr/local/bin/rscript")
            {
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                Arguments = "LinReg.R"
            };

            var process = new Process {StartInfo = startInfo};
            process.Start();

            var data = File.ReadAllText(dataFilePath);
            var streamWriter = process.StandardInput;
            streamWriter.WriteLine(data);
            streamWriter.Close();
            
            var output = process.StandardOutput;
            var error = process.StandardError;

            var reader = new CsvReader(output, CultureInfo.InvariantCulture);
            reader.Read();
            var record = reader.GetRecord<dynamic>() as IDictionary<string, object>;
            if (record == null)
                return;
            watch.Stop();
            Console.WriteLine("Time: {0}", watch.ElapsedMilliseconds);
            
            Console.WriteLine("r-squared: {0}", record["r-squared"]);
            Console.WriteLine("adjusted r-squared: {0}", record["adjusted-r-squared"]);
            Console.WriteLine("f-statistic: {0}", record["f-statistic"]);
        }
    }
}