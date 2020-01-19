using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEHDataExtractorLib.Struct.Reward {
    public class Counted : SingleCountDependant{
        public Counted(FEHDataExtractorLib.SingleCountDependant singleCountDependant) : base(singleCountDependant) {
            this.Type = RewardType.COUNTED;
        }

        public Counted(FEHDataExtractorLib.CountStr countStr) : this(countStr as FEHDataExtractorLib.SingleCountDependant) {

        }
    }
}
