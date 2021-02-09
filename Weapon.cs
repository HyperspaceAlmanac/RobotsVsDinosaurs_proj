using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobosVsDinosaurs
{
    class Weapon
    {
        public string attackType;
        public int attackPower;

        public Weapon(string atkType = "basic blaster", int atkPower=100)
        {
            attackType = atkType;
            attackPower = atkPower;
        }
    }
}
