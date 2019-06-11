using System;
using DontEatTheChiliLib;
using DontEatTheChiliLib.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DontEatTheChiliTests
{
    [TestClass]
    public class GameAITests
    {
        Random r = new Random();

        [TestMethod]
        public void SmartAI_TakeTurn_PreviousChoiceThreeAIChoosesOne()
        {
            SmartGameAI ai = new SmartGameAI();

            int candyCount = 13;
            Moves previousMove = Moves.Three;

            for (int i = 0; i < 500; i++)
            {
                Moves choice = ai.TakeTurn(previousMove, candyCount);
                Assert.IsTrue(choice == Moves.One);
            }
        }

        [TestMethod]
        public void SmartAI_TakeTurn_PreviousChoiceTwoAIChoosesTwo()
        {
            SmartGameAI ai = new SmartGameAI();

            int candyCount = 13;
            Moves previousMove = Moves.Two;

            for (int i = 0; i < 500; i++)
            {
                Moves choice = ai.TakeTurn(previousMove, candyCount);
                Assert.IsTrue(choice == Moves.Two);
            }
        }

        [TestMethod]
        public void SmartAI_TakeTurn_PreviousChoiceOneAIChoosesThree()
        {
            SmartGameAI ai = new SmartGameAI();

            int candyCount = 13;
            Moves previousMove = Moves.One;

            for (int i = 0; i < 500; i++)
            {
                Moves choice = ai.TakeTurn(previousMove, candyCount);
                Assert.IsTrue(choice == Moves.Three);
            }
        }

        [TestMethod]
        public void SmartAI_TakeTurn_ThreeCandiesRemainValidChoice()
        {
            SmartGameAI ai = new SmartGameAI();

            int candyCount = 13;
            Moves previousMove = Moves.Three;

            for (int i = 0; i < 500; i++)
            {
                Moves choice = ai.TakeTurn(previousMove, candyCount);
            }

            // Any Error Here from the ai will break this test
        }

        [TestMethod]
        public void SmartAI_TakeTurn_TwoCandiesRemainValidChoice()
        {
            SmartGameAI ai = new SmartGameAI();

            int candyCount = 2;
            Moves previousMove = Moves.Three;

            for (int i = 0; i < 500; i++)
            {
                Moves choice = ai.TakeTurn(previousMove, candyCount);

                // AI Should never choose Three
                Assert.IsFalse(choice == Moves.Three);
            }

            // Any Error Here from the ai will break this test
        }

        [TestMethod]
        public void SmartAI_TakeTurn_OneCandyRemainsValidChoice()
        {
            SmartGameAI ai = new SmartGameAI();

            int candyCount = 1;
            Moves previousMove = Moves.Three;

            for (int i = 0; i < 500; i++)
            {
                Moves choice = ai.TakeTurn(previousMove, candyCount);

                // AI Should always choose One
                Assert.IsTrue(choice == Moves.One);
            }

            // Any Error Here from the ai will break this test
        }

        [TestMethod]
        public void RandomAI_TakeTurn_ThreeCandiesRemainValidChoice()
        {
            RandomGameAI ai = new RandomGameAI();

            int candyCount = 13;
            Moves previousMove = Moves.Three;

            for (int i = 0; i < 500; i++)
            {
                Moves choice = ai.TakeTurn(previousMove, candyCount);
            }

            // Any Error Here from the ai will break this test
        }

        [TestMethod]
        public void RandomAI_TakeTurn_TwoCandiesRemainValidChoice()
        {
            RandomGameAI ai = new RandomGameAI();

            int candyCount = 2;
            Moves previousMove = Moves.Three;

            for (int i = 0; i < 500; i++)
            {
                Moves choice = ai.TakeTurn(previousMove, candyCount);

                // AI Should never choose Three
                Assert.IsFalse(choice == Moves.Three);
            }

            // Any Error Here from the ai will break this test
        }

        [TestMethod]
        public void RandomAI_TakeTurn_OneCandyRemainsValidChoice()
        {
            RandomGameAI ai = new RandomGameAI();

            int candyCount = 1;
            Moves previousMove = Moves.Three;

            for (int i = 0; i < 500; i++)
            {
                Moves choice = ai.TakeTurn(previousMove, candyCount);

                // AI Should always choose One
                Assert.IsTrue(choice == Moves.One);
            }

            // Any Error Here from the ai will break this test
        }
    }
}
