using System.Collections.Generic;
using System.Linq;
using Flurl.Http;

namespace SySSensor.Core.Services
{
    public class RemotingService
    {
        public IList<string> GetRemoteLogFileNames()
        {
            var fileList = "http://192.168.1.110/list-log-files".GetStringAsync().Result;
            return fileList.Split(',').ToList().Where(x => !string.IsNullOrWhiteSpace(x)).Select(x => x.Replace("\r\n", "")).ToList();
        }

        public string GetRemoteLogFileName(string fileName)
        {
            var url = string.Format("{0}{1}", "http://192.168.1.110/read-log-file?filename=", fileName);
            return url.GetStringAsync().Result;
        }
    }
}
