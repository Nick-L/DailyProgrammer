using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DailyProgrammer.Intermediate.Challenge_375
{
    public class Game
    {
        private List<Card> Cards;
        private int LastCardIndex;

        public Game(string initialGameState)
        {
            Cards = new List<Card>();
            LastCardIndex = initialGameState.Length - 1;
            foreach(char card in initialGameState)
            {
                Cards.Add(new Card(card));
            }
        }

        public void RemoveCard(int index)
        {
            if(index < 0 || index > LastCardIndex)
            {
                throw new IndexOutOfRangeException($"Error: Attempted to flip card outside of range index was {index}");
            }
            else if(Cards[index].State != CardState.FACE_UP)
            {
                throw new InvalidOperationException($"Error: Cannot remove a card that is not face up card was {Cards[index].State}");
            }
            if(index == 0)
            {
                Cards[0].FlipCard();
                Cards[1].FlipCard();
            }
            else if(index == LastCardIndex)
            {
                Cards[LastCardIndex - 1].FlipCard();
                Cards[LastCardIndex].FlipCard();
            }
            else
            {
                Cards[index - 1].FlipCard();
                Cards[index].FlipCard();
                Cards[index + 1].FlipCard();
            }
        }

        public bool IslandExists()
        {
            string[] possibleIslands = CurrentGameState().Split(".");

            foreach(string s in possibleIslands)
            {
                if (s.Contains('1') || !s.Contains('0'))
                {
                    // Doesn't contain an island
                    continue;
                }
                else
                {
                    return true;
                }
            }
            return false;
        }

        public string CurrentGameState()
        {
            StringBuilder sb = new StringBuilder(Cards.Count);
            Char cardValue;
            foreach(Card card in Cards)
            {
                switch (card.State)
                {
                    case CardState.FACE_UP:
                        cardValue = '1';
                        break;
                    case CardState.FACE_DOWN:
                        cardValue = '0';
                        break;
                    case CardState.REMOVED:
                        cardValue = '.';
                        break;
                    default:
                        throw new Exception($"Error: Unexpected Value for card.State was {card.State}");
                }
                sb.Append(cardValue);
            }

            return sb.ToString();
        }
    }
}
