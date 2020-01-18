using System;
using System.Configuration;

namespace FEH_Data_Downloader {
    public static class SettingsManager {
        private static Configuration __Instance;
        private static System.Threading.ReaderWriterLock __Locker = new System.Threading.ReaderWriterLock();

        private static Configuration __GetInstance() {
            if (__Instance == null) {
                __Instance = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            }
            return __Instance;
        }

        private static void __Set(Configuration Settings) {
            __Instance = Settings;
        }

        public static string Get(string Key) {
            return Get<string>(Key);
        }

        public static T Get<T>(string Key) {
            Configuration Settings = __GetInstance();

            string value = null;
            if (Settings.AppSettings.Settings[Key] != null) {
                value = Settings.AppSettings.Settings[Key].Value;
            }

            if(value != null) {
                return (T) Convert.ChangeType(
                    value,
                    typeof(T)
                );
            }

            return default (T);
        }

        public static void Set(string Key, string Value) {
            Configuration Settings = __GetInstance();

            if (Settings.AppSettings.Settings[Key] == null) {
                Settings.AppSettings.Settings.Add(Key, Value);
            }
            Settings.AppSettings.Settings[Key].Value = Value;

            __Set(Settings);
            __Save();
        }

        public static void __Save() {
            Configuration Settings = __GetInstance();

            __Locker.AcquireWriterLock(int.MaxValue);
            Settings.Save(ConfigurationSaveMode.Modified);
            __Locker.ReleaseWriterLock();

            __Set(Settings);
        }
    }
}
