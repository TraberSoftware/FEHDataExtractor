using System;
using System.Collections.Generic;
using System.Text;

namespace FEH_Data_Downloader {
    public class ManifestItem {
        public string   path;
        public long     n;
        public long     unzip_size;
        public long     size;
        public string   md5;
        public string[] split_hashs;
    }
}
