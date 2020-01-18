using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEHDataExtractorLib.Struct {
    public class Area {
        public uint                  ID;
        public string                MapID;
        public string                Name;
        public uint                  Node;
        public uint                  X;
        public uint                  Y;
        public bool                  IsBase;
        public string                Army;
        public uint                  AreaNumber;
        public List<string>          AreaBonuses;
        public string                AdjacentAreaBonuses;
        public Dictionary<int, uint> Neighbours;

        public Area(GCArea area) {
            this.ID          = area.Node.Value;
            this.MapID       = area.MapId.Value;
            this.Name        = area.Name;
            this.X           = area.X.Value;
            this.Y           = area.Y.Value;
            this.AreaNumber  = area.Area_No.Value;

            this.IsBase      = area.IsBase.Value == 1 ? true : false;
            this.Army        = this.__GetArmyName(area.Army.Value);
            this.AreaBonuses = new List<string> {
                area.AreaBonuses[0].Value,
                area.AreaBonuses[1].Value
            };
            this.AdjacentAreaBonuses = area.AdjacentAreaBonus.Value;
            this.Neighbours = new Dictionary<int, uint>();
            for (int i = 0; i < area.Neighbours.Length; i++) {
                this.Neighbours.Add(
                    i + 1,
                    area.Neighbours[i].Value
                );
            }
        }

        private string __GetArmyName(uint ArmyID) {
            switch (ArmyID) {
                case 0:
                    return "Red";
                case 1:
                    return "Blue";
                default:
                    return "Green";
            }
        }
    }

    public class World {
        public string     Image;
        public List<Area> Areas;

        public World(GCWorld world) {
            this.Image = world.Image.Value;
            this.Areas = new List<Area>();

            foreach(GCArea gcarea in world.Areas) {
                this.Areas.Add(new Area(gcarea));
            }
        }
    }
}
