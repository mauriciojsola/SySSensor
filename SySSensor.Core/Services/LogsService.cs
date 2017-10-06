using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using SySSensor.Core.DAL;
using SySSensor.Core.Entities;

namespace SySSensor.Core.Services
{
    public class LogsService
    {
        public int SaveLogFile(string fileName, string fileContent)
        {
            var db = new SySDB();

            var logFile = db.LogFiles.FirstOrDefault(x => x.FileName == fileName);
            if (logFile != null) return -1;

            logFile = new LogFile
            {
                FileName = fileName,
                FileContent = fileContent,
                DateCreated = DateTime.Today
            };
            db.LogFiles.Add(logFile);

            db.SaveChanges();
            return logFile.Id;
        }

        public void ProcessPendingLogs()
        {
            var db = new SySDB();
            var pendingProcessLogs = db.LogFiles.Where(x => !x.ProcessDate.HasValue).ToList();
            foreach (var pendingProcessLog in pendingProcessLogs)
            {
                ProcessLogFile(pendingProcessLog);
            }
        }

        private void ProcessLogFile(LogFile pendingProcessLog)
        {
            Debug.WriteLine("Processing Log File: " + pendingProcessLog.FileName);
            var logRecords = new List<string>();
            using (var sr = new StringReader(pendingProcessLog.FileContent))
            {
                string line;
                while (!string.IsNullOrWhiteSpace(line = sr.ReadLine()))
                    logRecords.Add(line.Trim());
            }
            ProcessRecords(logRecords, pendingProcessLog.FileName);
        }

        private void ProcessRecords(List<string> logRecords, string fileName)
        {
            Debug.WriteLine("Total records: " + logRecords.Count);
            var db = new SySDB();
            foreach (var logRecord in logRecords)
            {
                if (string.IsNullOrWhiteSpace(logRecord)) continue;
                var fields = logRecord.Split(',');

                var readDate = ParseToDate(fields[0]);
                var exist = db.SensorLogs.Any(x => x.ReadDate == readDate);
                if (!exist)
                {
                    db.SensorLogs.Add(new SensorLog
                    {
                        SensorId = "001",
                        ReadDate = readDate,
                        Temperature = ToDouble(fields[1]),
                        Humidity = ToDouble(fields[2])
                    });
                }
            }
            db.SaveChanges();

            // Mark the file as processed
            var logFile = db.LogFiles.FirstOrDefault(x => x.FileName.Equals(fileName, StringComparison.InvariantCultureIgnoreCase));
            if (logFile != null)
            {
                logFile.ProcessDate = DateTime.Now;
                db.SaveChanges();
            }

            Debug.WriteLine("SAVED!!!");
        }

        private int ToInt(string value)
        {
            return Convert.ToInt16(value);
        }

        private double ToDouble(string value)
        {
            return Convert.ToDouble(value);
        }

        private DateTime ParseToDate(string text)
        {
            return new DateTime(ToInt(text.Substring(0, 2)) + 2000, ToInt(text.Substring(2, 2)),
                     ToInt(text.Substring(4, 2)), ToInt(text.Substring(6, 2)), ToInt(text.Substring(8, 2)),
                     ToInt(text.Substring(10, 2)));
        }
    }
}
