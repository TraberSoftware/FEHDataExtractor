using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEHDataExtractorLib {
    public static class Util {
        public static string GetString(StringXor Str) {
            return SinglePerson.Table.Contains("M" + Str.Value) ? 
                SinglePerson.Table["M" + Str.Value].ToString()
                : 
                ""
            ;
        }
    }
}
