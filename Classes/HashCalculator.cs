using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HasherBot.Classes {
    internal class HashCalculator {
        public struct Hashes {
            public string MD5;
            public string SHA256;
        }

        static string GetMD5(string filename) {
            using (var md5 = MD5.Create()) {
                using (var stream = File.OpenRead(filename)) {
                    var hash = md5.ComputeHash(stream);
                    return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
                }
            }
        }

        static string GetSHA256(string filename) {
            using (SHA256 SHA256 = SHA256.Create()) {
                using (FileStream fileStream = File.OpenRead(filename)) {
                    return BitConverter.ToString(SHA256.ComputeHash(fileStream)).Replace("-", "").ToLowerInvariant();
                }
            }
        }


        public static Hashes GetHashesFromFile(string path) { 
            var hashes = new Hashes();

            hashes.MD5 = GetMD5(path);
            hashes.SHA256 = GetSHA256(path);

            return hashes;
        }
    }
}
