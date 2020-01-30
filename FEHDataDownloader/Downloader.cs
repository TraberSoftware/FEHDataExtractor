using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Text;
using System.Threading;

namespace FEH_Data_Downloader {

    public class Downloader {
        private string __BaseURL           = "";
        private string __AssetsToken       = "";
        private string __Manifest          = "";
        private string __ManifestLowVolume = "";

        private string __LocalZIPPath     = "";
        private string __LocalExtractPath = "";
        private string __LocalTemptPath   = "";
        private string __Language         = "";

        public Downloader() {
            // Local variables
            this.__LocalZIPPath      = SettingsManager.Get("ZIPPath");
            this.__LocalExtractPath  = SettingsManager.Get("DataPath");
            this.__LocalTemptPath    = SettingsManager.Get("TempPath");

            // Remote variables
            this.__BaseURL           = SettingsManager.Get("BaseURL");
            this.__AssetsToken       = SettingsManager.Get("AssetsToken");
            this.__Manifest          = SettingsManager.Get("Manifest");
            this.__ManifestLowVolume = SettingsManager.Get("ManifestLowVolume");

            // Language
            this.__Language          = SettingsManager.Get("Language");

            if (Directory.Exists(this.__LocalTemptPath)) {
                Directory.Delete(this.__LocalTemptPath, true);
            }

            this.__Download();
        }

        private void __Download() {
            Threadify<ManifestItem> DownloadQueue = new Threadify<ManifestItem>();

            new Thread(() => {
                using (WebClient Downloader = new WebClient()) {
                    string JSONData = Downloader.DownloadString(
                        this.__AssetPath(
                            this.__Manifest
                        )
                    );
                    Manifest JSONDataParsed = Newtonsoft.Json.JsonConvert.DeserializeObject<Manifest>(JSONData);

                    foreach (ManifestItem Item in JSONDataParsed.infos) {
                        DownloadQueue.Enqueue(Item);
                    }

                    DownloadQueue.CompleteYieldAdd();
                }
            }).Start();

            DownloadQueue.Yarn(
                (Item) => {
                    string AssetPathRemote = this.__AssetPath(Item.path);
                    string AssetPathLocal  = this.__LocalAssetPath(Item.path);
                    string TargetDirectory = Path.GetDirectoryName(AssetPathLocal);

                    if (File.Exists(AssetPathLocal)) {
                        if(new FileInfo(AssetPathLocal).Length == Item.size) {
                            //Logger.Log("Skipping existing file: " + Item.path, Logger.LOG_LEVEL.INFO);

                            return;
                        }
                        else {
                            Logger.Log("Redownloading corrupted/updated ZIP file: " + Item.path);

                            File.Delete(AssetPathLocal);
                        }
                    }

                    using (WebClient Downloader = new WebClient()) {
                        try {
                            if(!Directory.Exists(TargetDirectory)) {
                                Directory.CreateDirectory(TargetDirectory);
                            }

                            int retries = 0;
                            while (
                                !Directory.Exists(TargetDirectory)
                                &&
                                retries < 15
                            ) {
                                Thread.Sleep(25);
                            }
                        }
                        catch { }

                        string AssetLanguage = this.__GetAssetLanguage(Item.path);
                        switch (true) {
                            // Download if no language selected
                            case true when this.__Language == "":
                            // Download if all languages selected
                            case true when this.__Language == "*":
                            // Download if is common data
                            case true when AssetLanguage   == "Common":
                            case true when AssetLanguage   == "ENCommon":
                            // Download if is data from selected language
                            case true when AssetLanguage   == this.__Language:
                                Logger.Log("Downloading file: " + Item.path);
                                try {
                                    Downloader.DownloadFile(
                                        AssetPathRemote,
                                        AssetPathLocal
                                    );
                                }
                                // Skip forbidden files :(
                                catch (Exception e) {
                                    using (FileStream fileStream = new FileStream(AssetPathLocal, FileMode.Create, FileAccess.Write, FileShare.None)) {
                                        fileStream.SetLength(Item.size);
                                    }
                                }

                                if (File.Exists(AssetPathLocal)) {
                                    this.__Unzip(AssetPathLocal);
                                }
                                break;
                            // Otherwise, skip bitch
                            default:
                                //Logger.Log("Skipping file: " + Item.path);
                                break;
                        }
                    }
                }, SettingsManager.Get<int>("Threads")
            );
        }

        private void __Unzip(string file) {
            string FileExtension = Path.GetExtension(file);

            if (FileExtension == ".zip") {
                // Unzip bro
                Logger.Log("Unzipping file: " + file);

                using (ZipArchive archive = ZipFile.OpenRead(file)) {
                    foreach(ZipArchiveEntry entry in archive.Entries) {
                        string TargetDirectory = Path.GetDirectoryName(file).Replace(
                            __LocalZIPPath,
                            __LocalExtractPath
                        );

                        string TargetDirectoryTemp = Path.GetDirectoryName(file).Replace(
                            __LocalZIPPath,
                            __LocalTemptPath
                        );

                        string TargetFile     = Path.Combine(TargetDirectory,     entry.FullName);
                        string TargetFileTemp = Path.Combine(TargetDirectoryTemp, entry.FullName);

                        if (!Directory.Exists(Path.GetDirectoryName(TargetFile))) {
                            Directory.CreateDirectory(Path.GetDirectoryName(TargetFile));
                        }
                        if (!Directory.Exists(Path.GetDirectoryName(TargetFileTemp))) {
                            Directory.CreateDirectory(Path.GetDirectoryName(TargetFileTemp));
                        }

                        entry.ExtractToFile(TargetFile,     true);
                        entry.ExtractToFile(TargetFileTemp, true);
                    }
                }

                /*System.IO.Compression.ZipFile.ExtractToDirectory(
                    file,
                    TargetDirectory
                );*/
            }
            else {
                string TargetFile     = file.Replace(__LocalZIPPath, __LocalExtractPath);
                string TargetFileTemp = file.Replace(__LocalZIPPath, __LocalTemptPath);

                if (!Directory.Exists(Path.GetDirectoryName(TargetFile))) {
                    Directory.CreateDirectory(Path.GetDirectoryName(TargetFile));
                }
                if (!Directory.Exists(Path.GetDirectoryName(TargetFileTemp))) {
                    Directory.CreateDirectory(Path.GetDirectoryName(TargetFileTemp));
                }

                File.Copy(
                    file,
                    TargetFile,
                    true
                );
                File.Copy(
                    file,
                    TargetFileTemp,
                    true
                );
            }
        }

        private string __GetAssetLanguage(string asset) {
            string[] AssetData = asset.Trim('/').Split('/');
            return AssetData[0];
        }

        private string __AssetPath(string asset) {
            return
                this.__BaseURL     + "/" +
                this.__AssetsToken + "/" +
                asset
            ;
        }

        private string __LocalAssetPath(string asset) {
            return 
                Path.Combine(
                    this.__LocalZIPPath,
                    asset
                )
            ;
        }
    }
}
