using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace HasherBot.Classes {
    internal class DownloadManager {
        static string tempDir = Environment.GetEnvironmentVariable("TEMP");
        public static string Download(string url, string fileName) {
            string downloadPath = tempDir + "\\" + fileName + ".bin"; // we add .bin just to make sure the server doesnt accidentally execute it if its a piece of malware
            using (WebClient webClient = new WebClient()) {
                webClient.DownloadFile(url, downloadPath);
            }
            return downloadPath;
        }
    }
}
