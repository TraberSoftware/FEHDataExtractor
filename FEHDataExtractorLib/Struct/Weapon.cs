using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEHDataExtractorLib.Struct {
    public class Weapon {
        public int    Index;
        public string Name;
        public string Color;
        public int    Range;

        public bool   IsBreath;
        public bool   IsStaff;
        public bool   IsDagger;
        public bool   IsBeast;

        public Weapon(SingleWeaponClass weapon) {
            this.Index = weapon.Index;
            this.Name  = weapon.Name;
            this.Color = weapon.Color;
            this.Range = weapon.Range;

            this.IsBreath = weapon.Is_breath;
            this.IsStaff  = weapon.Is_staff;
            this.IsDagger = weapon.Is_dagger;
            this.IsBeast  = weapon.Is_beast;
        }

        public Weapon(FEHDataExtractorLib.WeaponClass weapon) {
            this.Index = (int) weapon.Index.Value;
            this.Name  = weapon.Name;
            this.Color = Base.Colours.Get((weapon.Color.Value - 1) & 3);
            this.Range = weapon.Range.Value;
            
            this.IsBreath = weapon.Is_breath.Value == 1 ? true : false;
            this.IsStaff  = weapon.Is_staff.Value  == 1 ? true : false;
            this.IsDagger = weapon.Is_dagger.Value == 1 ? true : false;
            this.IsBeast  = weapon.Is_beast.Value  == 1 ? true : false;
        }
    }

    public class WeaponClass : Weapon{
        public string       TagID;
        public List<string> SpriteBase;
        public string       BaseWeapon;
        public int          EquipmentGroup;
        public string       DamageTarget;

        public WeaponClass(FEHDataExtractorLib.WeaponClass weapon) : base(weapon) {
            this.TagID          = weapon.Id_tag.Value;
            this.SpriteBase     = new List<string>{
                weapon.Sprite_base[0].Value,
                weapon.Sprite_base[1].Value
            };
            this.BaseWeapon     = Util.GetString(weapon.Base_weapon);
            this.EquipmentGroup = weapon.Equip_group.Value;
            this.DamageTarget   = weapon.Res_damage.Value == 1 ? "RESISTANCE" : "DEFENSE";
        }
    }
}
