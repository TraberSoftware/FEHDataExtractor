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

        public static string SuperStats(Stats Base_stats, Stats Growth_rates, Stats lvl40, bool greater, string phrase = "") {
            string text = "";
            int growthInc = 5;
            if (!greater) {
                growthInc = -5;
            }

            Stats newRates = new Stats();
            newRates.Hp.Value  = (short)(Growth_rates.Hp.Value + growthInc);
            newRates.Atk.Value = (short)(Growth_rates.Atk.Value + growthInc);
            newRates.Spd.Value = (short)(Growth_rates.Spd.Value + growthInc);
            newRates.Def.Value = (short)(Growth_rates.Def.Value + growthInc);
            newRates.Res.Value = (short)(Growth_rates.Res.Value + growthInc);

            int i = 1;
            if (!greater) {
                i = -1;
            }
            int val = 4;

            Stats newBaseStats = new Stats();
            newBaseStats.Hp.Value  = (short)(Base_stats.Hp.Value + i);
            newBaseStats.Atk.Value = (short)(Base_stats.Atk.Value + i);
            newBaseStats.Spd.Value = (short)(Base_stats.Spd.Value + i);
            newBaseStats.Def.Value = (short)(Base_stats.Def.Value + i);
            newBaseStats.Res.Value = (short)(Base_stats.Res.Value + i);
            Stats tmpStats = new Stats(newBaseStats, newRates);

            int len = 0;
            if ((greater && tmpStats.Hp.Value - val >= lvl40.Hp.Value) || (!greater && tmpStats.Hp.Value + val <= lvl40.Hp.Value)) {
                len++;
            }
            if ((greater && tmpStats.Atk.Value - val >= lvl40.Atk.Value) || (!greater && tmpStats.Atk.Value + val <= lvl40.Atk.Value)) {
                len++;
            }
            if ((greater && tmpStats.Spd.Value - val >= lvl40.Spd.Value) || (!greater && tmpStats.Spd.Value + val <= lvl40.Spd.Value)) {
                len++;
            }
            if ((greater && tmpStats.Def.Value - val >= lvl40.Def.Value) || (!greater && tmpStats.Def.Value + val <= lvl40.Def.Value)) {
                len++;
            }
            if ((greater && tmpStats.Res.Value - val >= lvl40.Res.Value) || (!greater && tmpStats.Res.Value + val <= lvl40.Res.Value)) {
                len++;
            }
            if(phrase != string.Empty) {
                text += len > 0 ? len > 1 ? phrase + "s: " : phrase + ": " : "";
            }

            if ((greater && tmpStats.Hp.Value - val >= lvl40.Hp.Value) || (!greater && tmpStats.Hp.Value + val <= lvl40.Hp.Value)) {
                text += "Hp";
                len--;
                if (len > 0)
                    text += ", ";
            }
            if ((greater && tmpStats.Atk.Value - val >= lvl40.Atk.Value) || (!greater && tmpStats.Atk.Value + val <= lvl40.Atk.Value)) {
                text += "Atk";
                len--;
                if (len > 0)
                    text += ", ";
            }
            if ((greater && tmpStats.Spd.Value - val >= lvl40.Spd.Value) || (!greater && tmpStats.Spd.Value + val <= lvl40.Spd.Value)) {
                text += "Spd";
                len--;
                if (len > 0)
                    text += ", ";
            }
            if ((greater && tmpStats.Def.Value - val >= lvl40.Def.Value) || (!greater && tmpStats.Def.Value + val <= lvl40.Def.Value)) {
                text += "Def";
                len--;
                if (len > 0)
                    text += ", ";
            }
            if ((greater && tmpStats.Res.Value - val >= lvl40.Res.Value) || (!greater && tmpStats.Res.Value + val <= lvl40.Res.Value)) {
                text += "Res";
                len--;
                if (len > 0)
                    text += ", ";
            }

            return text;

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

        public static Int32 GetMultiplier(long Start, UInt32 period, Int32Xor[] probs, Int32Xor[] mults) {
            UInt32[] randbuf = { 0, 0, 0, 0 };
            initRandBuf(randbuf, period + (uint) Start);
            Int32 x = RandMagicCycle(randbuf, 1, 100);
            for (int i = 0; i < probs.Length; ++i) {
                x -= probs[i].Value;
                if (x <= 0)
                    return mults[i].Value;
            }
            return 0;
        }

        public static void initRandBuf(UInt32[] v0, UInt32 v1) {

            UInt32 state = v1;
            for (int i = 0; i < 4; ++i)
                v0[i] = state = 0x6C078965 * (state ^ (state >> 30)) + (uint)i;
        }

        public static Int32 RandMagicCycle(UInt32[] v0, Int32 v1, Int32 v2) {
            UInt32 t = v0[0];
            UInt32 s = v0[3];
            t ^= t << 11;
            t ^= t >> 8;
            t ^= s;
            t ^= s >> 19;
            v0[0] = v0[1];
            v0[1] = v0[2];
            v0[2] = v0[3];
            v0[3] = t;
            return (Int32)(Math.Min(v1, v2) + (t & 0x7FFFFFFF) % (Math.Abs(v1 - v2) + 1));
        }

    }
}
