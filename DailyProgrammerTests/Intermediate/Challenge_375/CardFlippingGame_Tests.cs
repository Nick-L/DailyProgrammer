using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using DailyProgrammer.Intermediate.Challenge_375;

namespace DailyProgrammerTests.Intermediate.Challenge_375
{
    [TestClass]
    public class CardFlippingGame_Tests
    {
        private const string SAMPLE_INPUT_1 = "0100110";
        private const string SAMPLE_INPUT_2 = "01001100111";
        private const string SAMPLE_INPUT_3 = "100001100101000";

        private const string Challenge_INPUT_1 = "0100110";
        private const string Challenge_INPUT_2 = "001011011101001001000";
        private const string Challenge_INPUT_3 = "1010010101001011011001011101111";
        private const string Challenge_INPUT_4 = "1101110110000001010111011100110";

        private const string BONUS_INPUT = "010111111111100100101000100110111000101111001001011011000011000";

        [TestMethod]
        public void SampleInputTests()
        {
            Game game = new Game(SAMPLE_INPUT_1);
            TestSolution(game);

            game = new Game(SAMPLE_INPUT_2);
            Assert.ThrowsException<InvalidOperationException>(() => TestSolution(game));

            game = new Game(SAMPLE_INPUT_3);
            TestSolution(game);
        }

        [TestMethod]
        public void ChallengeInputTests()
        {
            Game game = new Game(Challenge_INPUT_1);
            TestSolution(game);

            game = new Game(Challenge_INPUT_2);
            TestSolution(game);

            game = new Game(Challenge_INPUT_3);
            Assert.ThrowsException<InvalidOperationException>(() => TestSolution(game));

            game = new Game(Challenge_INPUT_4);
            TestSolution(game);
        }

        [TestMethod]
        public void BonusInputTests()
        {
            Game game = new Game(BONUS_INPUT);

            TestSolution(game);
        }

        private void TestSolution(Game game)
        {
            Assert.AreEqual(FinishedGame(game.GetNumberOfCards()), PlayGame(CardFlippingGame.GetSolutionToGame(game), game).CurrentGameState());
        }
        
        private Game PlayGame(List<int> solution, Game game)
        {
            foreach (int move in solution)
            {
                game.RemoveCard(move);
            }

            return game;
        }

        private string FinishedGame(int numCards)
        {
            StringBuilder sb = new StringBuilder(numCards);

            for(int i = 0; i < numCards; i++)
            {
                sb.Append('.');
            }

            return sb.ToString();
        }
    }
}
