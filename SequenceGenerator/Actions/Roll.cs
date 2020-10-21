using System;

namespace SequenceGenerator.Actions
{
    public struct Roll  // supports only M(IdX+Y)
    {
        public int? Multiplier; //M
        public int? Count; //I
        public Dice Die; //dX
        public int? bonus; //Y

        public int PerformRoll()
        {
            Random random = new Random();
            int value = 0;

            for(int i = 0; i < Count.GetValueOrDefault(1); i++ )
            {
                value += random.Next(1, (int)Die);
            }

            value += bonus.GetValueOrDefault(0);
            value *= Multiplier.GetValueOrDefault(1);

            return value;
        }

        public int GetRangeDifference()
        {
            return GetMaxValue() - (GetMinValue() - 1);
            
        }

        public int GetMaxValue()
        {
            int value = (int)Die;
            value *= Count.GetValueOrDefault(1);
            value += bonus.GetValueOrDefault(0);
            value *= Multiplier.GetValueOrDefault(1);

            return value;
        }

        public int GetMinValue()
        {
            int value = 1;
            value *= Count.GetValueOrDefault(1);
            value += bonus.GetValueOrDefault(0);
            value *= Multiplier.GetValueOrDefault(1);

            return value;
        }

        public override string ToString()
        {
            string dice = Die.ToString();
            dice = Count.HasValue ? $"{Count.Value}{dice}" : dice;
            dice = bonus.HasValue ? $"{dice}+{bonus.Value}" : dice;
            dice = Multiplier.HasValue ? $"{Multiplier.Value}({dice})" : dice;

            return dice;
        }
    }

    public enum Dice
    {
        coin = 2, d3, d4, d5, d6, d8 = 8, d10 = 10, d12 = 12, d20 = 20, d100 = 100, d0 = 0
    }
}
