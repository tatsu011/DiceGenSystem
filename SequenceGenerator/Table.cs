using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SequenceGenerator
{
    class Table
    {
        public Roll dicecheck;

        public Result[] results;



    }

    public enum Dice
    {
        coin = 2, d3,d4,d5,d6,d8 = 8,d10 = 10,d12 = 12,d20 = 20
    }
}
