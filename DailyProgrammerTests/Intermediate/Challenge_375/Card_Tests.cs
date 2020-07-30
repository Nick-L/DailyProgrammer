using DailyProgrammer.Intermediate.Challenge_375;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace DailyProgrammerTests.Intermediate.Challenge_375
{
    [TestClass]
    public class Card_Tests
    {
        private Card faceUpCard;
        private Card faceDownCard;
        private Card removedCard;

        [TestMethod]
        public void CardsHaveCorrectState()
        {
            CreateCards();

            Assert.AreEqual(CardState.FACE_UP, faceUpCard.State);
            Assert.AreEqual(CardState.FACE_DOWN, faceDownCard.State);
            Assert.AreEqual(CardState.REMOVED, removedCard.State);
        }

        [TestMethod]
        public void CardsFlipCorrectly()
        {
            CreateCards();

            Assert.AreEqual(CardState.FACE_DOWN, faceUpCard.FlipCard().State);
            Assert.AreEqual(CardState.FACE_UP, faceDownCard.FlipCard().State);
            Assert.AreEqual(CardState.REMOVED, removedCard.FlipCard().State);
        }

        [TestMethod]
        public void FaceUpCardsRemoveCorrectly()
        {
            CreateCards();

            Assert.AreEqual(CardState.REMOVED, faceUpCard.RemoveCard().State);
        }

        [TestMethod]
        public void CreatingInvalidCardThrowsError()
        {
            Assert.ThrowsException<Exception>(() => { new Card('2'); });
        }

        [TestMethod]
        public void RemovingInvalidCardThrowsError()
        {
            CreateCards();

            Assert.ThrowsException<InvalidOperationException>(() => { faceDownCard.RemoveCard(); });
            Assert.ThrowsException<InvalidOperationException>(() => { removedCard.RemoveCard(); });
        } 

        private void CreateCards()
        {
            faceUpCard = new Card('1');
            faceDownCard = new Card('0');
            removedCard = new Card('.');
        }
    }
}
