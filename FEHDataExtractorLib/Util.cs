using FEHDataExtractorLib.Struct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEHDataExtractorLib {
    public static class Util {
        public static string GetString(StringXor str) {
            return GetString(str.Value);
        }

        public static string GetString(string str) {
            return Base.Table.Contains("M" + str) ?
                Base.Table["M" + str].ToString()
                :
                ""
            ;
        }

        public static string GetString(StringXor str, string prefix) {
            return Base.Table.Contains("M" + str.Value) ?
                prefix + Base.Table["M" + str.Value].ToString() + Environment.NewLine
                :
                ""
            ;
        }

        public static string GetStringOrRaw(StringXor str, string prefix) {
            return (Base.Table.Contains("M" + str.Value) ?
                prefix + Base.Table["M" + str.Value].ToString()
                :
                prefix + str)
                + Environment.NewLine
            ;
        }

        public static string GetHeroName(string str) {
            return (Base.Table.Contains("M" + str) ?
                (
                    Base.Table["M" + str] +
                    (
                        Base.Table.Contains("M" + str.Insert(3, "_HONOR")) ?
                        " - " + Base.Table["M" + str.Insert(3, "_HONOR")]
                        :
                        ""
                    )
                )
                :
                str
            );
        }
    }
}
