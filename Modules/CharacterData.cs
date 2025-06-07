using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamServer.Modules
{

    public class CharacterData
    {
        public int acc_id { get; set; }
        public string Player_Name { get; set; }
        public string Player_Skin { get; set; }
        public int Level { get; set; }
        public int Xp { get; set; }
        public int XpToLevel { get; set; }
        public float Pos_X { get; set; }
        public float Pos_Y { get; set; }
        public float Pos_Z { get; set; }
        public int str { get; set; }
        public int Endurance { get; set; }
        public int Agility { get; set; }
        public int Intelligence { get; set; }
        public int Sagesse { get; set; }
        public int AC { get; set; }
    }
}
