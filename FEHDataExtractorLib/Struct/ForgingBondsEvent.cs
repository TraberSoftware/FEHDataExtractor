using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEHDataExtractorLib.Struct {
    public class ForgingBondsEvent {
        public string ID;
        public string IDAlt;
        public string Reference;
        public string Image;
        public string Icon;

        public DateTime DateStart;
        public DateTime DateEnd;

        public List<string> Heroes;
        public List<string> BonusAccessories;
        public List<string> Assets;

        public Dictionary<string, Dictionary<uint, string>> HeroRewards;
        public Dictionary<int, string>                      DailyRewards;
        public Dictionary<int, Dictionary<int, string>>     Multipliers;

        public ForgingBondsEvent(Forging_Bonds forgingBonds) {
            this.ID        = forgingBonds.Id_tag.Value;
            this.IDAlt     = forgingBonds.Id_tag2.Value;
            this.Reference = forgingBonds.Reference.Value;
            this.Image     = forgingBonds.Image.Value;
            this.Icon      = forgingBonds.Icon.Value;

            this.DateStart = DateTimeOffset.FromUnixTimeSeconds((long)forgingBonds.Start.Value).DateTime.ToLocalTime();
            this.DateEnd   = DateTimeOffset.FromUnixTimeSeconds((long)forgingBonds.Finish.Value).DateTime.ToLocalTime();

            this.Heroes = new List<string>();
            foreach (StringXor character in forgingBonds.Characters) {
                this.Heroes.Add(Util.GetHeroName(character.Value));
            }

            this.BonusAccessories = new List<string>();
            foreach (StringXor bonusAccessory in forgingBonds.BonusAccessories) {
                this.BonusAccessories.Add(Util.GetString(bonusAccessory));
            }

            this.Assets = new List<string>();
            for (int i = 0; i < 3; i++) {
                this.Assets.Add(
                    forgingBonds.Assets[i, 0] + " | " +
                    forgingBonds.Assets[i, 1] + " | " +
                    forgingBonds.Assets[i, 2] + " | " +
                    forgingBonds.Assets[i, 3]
                );
            }

            this.HeroRewards = new Dictionary<string, Dictionary<uint, string>>();
            for (int i = 0; i < forgingBonds.CharacterSize.Value; i++) {
                string HeroID = forgingBonds.Characters[i].Value;
                string HeroName = Util.GetHeroName(forgingBonds.Characters[i].Value);
                this.HeroRewards.Add(
                    HeroName,
                    new Dictionary<uint, string>()
                );


                for (int j = 0; j < forgingBonds.Points.Length; j++) {
                    PointReward reward = forgingBonds.Points[j];

                    if (reward.Character.Value.Equals(HeroID)) {
                        this.HeroRewards[HeroName].Add(
                            reward.Points.Value,
                            reward.Reward.Rewards.Rewards[0].Thing.ToString()
                        );
                    }
                }
            }

            this.DailyRewards = new Dictionary<int, string>();
            for (int i = 0; i < forgingBonds.Dailies.Length; i++) {
                this.DailyRewards.Add(
                    i+1,
                    forgingBonds.Dailies[i].Reward.Rewards.Rewards[0].Thing.ToString()
                );
            }

            int days = this.DateEnd.Subtract(this.DateStart).Days;
            this.Multipliers = new Dictionary<int, Dictionary<int, string>>();
            for (int i = 0; i < days; i++) {
                this.Multipliers.Add(i + 1, new Dictionary<int, string>());
                for (int j = 0; j < forgingBonds.MultiplierSize.Value / days; j++) {
                    float Multiplier = (float) (
                        Util.GetMultiplier(
                            forgingBonds.Start.Value,
                            (uint)((i * ((int)(forgingBonds.MultiplierSize.Value / days))) + j),
                            forgingBonds.MultipliersValues.Probs,
                            forgingBonds.MultipliersValues.Mults
                        ) / 100.0
                    );
                    string Color = Base.ForgingBondsHearts.Get(
                        (int)forgingBonds.MultipliersCharacters[
                            (i * (forgingBonds.MultiplierSize.Value / days)) + j
                        ].Value
                    );

                    this.Multipliers[i + 1].Add(
                        j,
                        string.Format(new System.Globalization.CultureInfo("en-US"), "{0:N1}", Multiplier) + "x" + 
                        " - " + 
                        Color
                    );
                }
            }
        }
    }
}
