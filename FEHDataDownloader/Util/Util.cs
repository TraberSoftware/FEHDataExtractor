using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.ComponentModel;

namespace FEH_Data_Downloader {
    public static class Util {
        private static Dictionary<string, Type> __HashingClasses = new Dictionary<string, Type>();
        private static readonly int __HASH_BUFFERSIZE = 1048576;

        public static string ProgramDirectory() {
            return Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

        }

        public static string ProgramName() {
            return Assembly.GetExecutingAssembly().GetName().Name;
        }

        public static string CurrentDate(string separator = "-", DateTime? date = null) {
            if (!date.HasValue) {
                date = DateTime.Now;
            }
            if (separator != string.Empty) {
                separator = " " + separator + " ";
            }
            string dateString = "yyyy" + separator + "MM" + separator + "dd";

            return date.Value.ToString(dateString);
        }

        public static string CurrentTimestamp(bool millis = false) {
            string dateString = "yyyy-MM-dd HH:mm:ss";
            if (millis) {
                dateString += ".ffffff";
            }
            DateTime date = DateTime.Now;
            return date.ToString(dateString);
        }

        public static string AddTrailingSlash(string input) {
            char[] toTrim = {
                Path.DirectorySeparatorChar,
                Path.AltDirectorySeparatorChar
            };

            return input.TrimEnd(toTrim) + Path.DirectorySeparatorChar;
        }

        public static HashAlgorithm GetHasher(string algo = "") {
            Dictionary<string, Type> algorithms = __GetHashingClasses();

            HashAlgorithm hasher = null;
            if (algorithms.ContainsKey(algo.ToUpper())) {
                MethodInfo hasherCreateMethod = algorithms[algo.ToUpper()].GetMethod(
                    "Create",
                    new Type[0]
                );
                return (HashAlgorithm)hasherCreateMethod.Invoke(null, null);
            }

            return hasher;
        }

        public static string HashString(string input, string algo = "md5") {
            algo = algo.ToLower();
            HashAlgorithm hasher = GetHasher(algo);

            if (hasher != null) {
                string hash = BitConverter.ToString(hasher.ComputeHash(Encoding.UTF8.GetBytes(input))).Replace("-", "").ToLowerInvariant();
                hasher.Dispose();

                return hash;
            }

            return null;
        }

        private static Dictionary<string, Type> __GetHashingClasses() {
            // Multithreading lock
            lock (__HashingClasses) {
                if (
                    __HashingClasses.Count == 0
                ) {
                    string assemblyName = "mscorlib";

                    Assembly assembly = Assembly.Load(assemblyName);
                    Type[] algorithms = assembly.GetTypes();

                    if (algorithms.Length > 0) {
                        foreach (Type algorithm in algorithms) {
                            if (
                                algorithm.Name != "Implementation"
                                &&
                                Util.Implements(algorithm, typeof(HashAlgorithm))
                            ) {
                                if (!__HashingClasses.ContainsKey(algorithm.Name)) {
                                    __HashingClasses.Add(
                                        algorithm.Name,
                                        algorithm
                                    );
                                }
                            }
                        }
                    }
                }
            }

            return __HashingClasses;
        }

        public static bool Implements(Type ObjectType, Type ImplementedInterface, bool Generic = false) {
            if (Generic) {
                return ObjectType
                    .GetInterfaces()
                    .Any(
                        Interface => {
                            return
                                Interface.IsGenericType &&
                                Interface.GetGenericTypeDefinition() == ImplementedInterface
                            ;
                        }
                    )
                ;
            }

            return ImplementedInterface.IsAssignableFrom(ObjectType);
            //return typeof(T).GetInterfaces().Contains(ImplementedInterface);
        }
    }
}
