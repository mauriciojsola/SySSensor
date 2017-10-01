using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using Flurl.Http;
using SySSensor.Core.DAL;
using SySSensor.Core.Entities;

namespace SySSensor.Core.Services
{
    public class RemotingService
    {
        private string ArduinoBaseUrl
        {
            get { return ConfigurationManager.AppSettings["ArduinoWebServerUrl"]; }
        }

        public IList<string> GetRemoteLogFileNames()
        {
            try
            {
                var fileList = string.Format("{0}/list-log-files", ArduinoBaseUrl).GetStringAsync().Result;
                return fileList.Split(',').ToList().Where(x => !string.IsNullOrWhiteSpace(x)).Select(x => x.Replace("\r\n", "")).ToList();
            }
            catch (FlurlHttpTimeoutException ex)
            {
                Debug.WriteLine("ERROR: " + ex.Message);
            }
            catch (FlurlHttpException ex)
            {
                Debug.WriteLine("ERROR: " + ex.Message);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("ERROR: " + ex.Message);
            }
            return new List<string>();
        }

        public string GetRemoteLogContent(string fileName)
        {
            try
            {
                var url = string.Format("{0}/read-log-file?filename={1}", ArduinoBaseUrl, fileName);
                return url.GetStringAsync().Result;
            }
            catch (FlurlHttpTimeoutException ex)
            {
                Debug.WriteLine("ERROR: " + ex.Message);
            }
            catch (FlurlHttpException ex)
            {
                Debug.WriteLine("ERROR: " + ex.Message);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("ERROR: " + ex.Message);
            }
            return string.Empty;
        }

        public void UpdateLogFiles()
        {
            var db = new SySDB();
            var remoteFiles = GetRemoteLogFileNames();
            foreach (var remoteFile in remoteFiles)
            {
                var logFile = db.LogFiles.FirstOrDefault(x => x.FileName == remoteFile);
                if (logFile != null) continue;
                logFile = new LogFile
                {
                    FileName = remoteFile
                };
                db.LogFiles.Add(logFile);
            }
            db.SaveChanges();
        }
    }
}
