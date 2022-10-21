using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Players
    {
        public int health { get; set; }
        public int mana { get; set; }
        public int Happnies { get; set; }
        public int fatigue { get; set; }
        public int money { get; set; }

        public Players()
        {
            health = 100;
            mana = 0;
            Happnies = 0;
            fatigue = 0;
            money = 100;
        }
    }
    
}
