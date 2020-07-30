using DailyProgrammer.Intermediate.Challenge_375;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace DailyProgrammerTests.Intermediate.Challenge_375
{
    [TestClass]
    public class Game_Tests
    {
        private Game game;

        private const string INPUT_WITHOUT_ISLANDS = "1.10.010.101.";
        private const string INPUT_WITH_ISLANDS = "1.10.000.101.";

        [TestMethod]
        public void CardsHaveCorrectState()
        {
            CreateGame_WithoutIslands();

            Assert.AreEqual(INPUT_WITHOUT_ISLANDS, game.CurrentGameState());

            CreateGame_WithIslands();

            Assert.AreEqual(INPUT_WITH_ISLANDS, game.CurrentGameState());
        }

        [TestMethod]
        public void CardsHaveCorrectStateAfterRemovingCard()
        {
            CreateGame_WithoutIslands();

            game.RemoveCard(0);

            Assert.AreEqual("..10.010.101.", game.CurrentGameState());

            game.RemoveCard(6);

            Assert.AreEqual("..10.1.1.101.", game.CurrentGameState());
        }
        [TestMethod]
        public void GameFailsToCreateWithInvalidInput()
        {
            Assert.ThrowsException<Exception>(() => { new Game(".01c"); });
        }
        
        [TestMethod]
        public void FailsToRemoveCardAtInvalidIndex()
        {
            CreateGame_WithIslands();

            Assert.ThrowsException<IndexOutOfRangeException>(() => { game.RemoveCard(-1); });
            Assert.ThrowsException<IndexOutOfRangeException>(() => { game.RemoveCard(INPUT_WITH_ISLANDS.Length); });
            Assert.ThrowsException<InvalidOperationException>(() => { game.RemoveCard(INPUT_WITH_ISLANDS.IndexOf('0')); });
            Assert.ThrowsException<InvalidOperationException>(() => { game.RemoveCard(INPUT_WITH_ISLANDS.IndexOf('.')); });
        }

        private void CreateGame_WithoutIslands()
        {
            game = new Game(INPUT_WITHOUT_ISLANDS);
        }

        private void CreateGame_WithIslands()
        {
            game = new Game(INPUT_WITH_ISLANDS);
        }
    }
}
