using Microsoft.VisualStudio.TestTools.UnitTesting;
using SequenceGenerator;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;

namespace GeneratorTesting
{
    [TestClass]
    public class RollTests
    {
        [TestMethod]
        public void TestDiceIDX() //tests functionality within Roll.cs.
        {
            //idx - d2
            Roll roll = new Roll();
            roll.Die = Dice.coin;

            AssertRollValues(roll, 10, 1, 2, 2);

            //idx - d4
            roll.Die = Dice.d4;
            AssertRollValues(roll, 15, 1, 4, 4);

            //idx - d6
            roll.Die = Dice.d6;
            AssertRollValues(roll, 15, 1, 6, 6);

            //idx - d10
            roll.Die = Dice.d10;
            AssertRollValues(roll, 30, 1, 10, 10);

            //idx - d20
            roll.Die = Dice.d20;
            AssertRollValues(roll, 90, 1, 20, 20);



        }

        public void AssertRollValues(Roll roll, int ValuesToTest, int ExpectedMin, int ExpectedMax, int ExpectedRange)
        {
            List<int> Results = new List<int>();
            for(int i = 0; i < ValuesToTest; i++)
            {
                Results.Add(roll.PerformRoll());
            }

            Assert.AreEqual(ExpectedMin, roll.GetMinValue());
            Assert.AreEqual(ExpectedMax, roll.GetMaxValue());
            foreach(int result in Results)
                Assert.IsTrue(result >= roll.GetMinValue() && result <= roll.GetMaxValue());
            Assert.AreEqual(ExpectedRange, roll.GetRangeDifference());
        }

        [TestMethod]
        public void TestDiceIDXY()
        {
            Roll roll = new Roll();
            roll.Die = Dice.d6;


        }

        public void TestDiceMIDXY()
        {

        }
    }
}
