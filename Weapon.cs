using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobosVsDinosaurs
{
    class Weapon
    {
        string attackType;
        int attackPower;

        public Weapon(string atkType = "lazer blaster", int atkPower=100)
        {
            attackType = atkType;
            attackPower = atkPower;
        }
    }
}
