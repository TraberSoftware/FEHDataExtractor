using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEHDataExtractorLib.Struct.Reward {
    public class Throne : SingleCountDependant {
        public string ThroneType;

        public Throne(FEHDataExtractorLib.SingleCountDependant singleCountDependant) : base(singleCountDependant) {
            this.Type = RewardType.THRONE;
        }

        public Throne(FEHDataExtractorLib.Throne throne) : this(throne as FEHDataExtractorLib.SingleCountDependant) {
            this.ThroneType = Base.Thrones[throne.Thronetype];
        }
    }
}
