using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IUE7VU_HFT_2022231.Logic.Classes
{
    public abstract class IDGeneratorUtil
    {
        private static int lastID = 100;
        public static int GenerateID()
        {
            return ++lastID; 
        }
    }
}
