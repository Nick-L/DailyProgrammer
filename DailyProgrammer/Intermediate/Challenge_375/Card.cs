using System;
using System.Collections.Generic;
using System.Text;

namespace DailyProgrammer.Intermediate.Challenge_375
{
    public class Card
    {
        public CardState State { get; set; }

        public Card(char input)
        {
            this.State = input switch
            {
                '1' => CardState.FACE_UP,
                '0' => CardState.FACE_DOWN,
                '.' => CardState.REMOVED,
                _ => throw new Exception($"Error: Unexpected character in input expected chars: [1, 0, .] was {input}"),
            };
        }

        public Card FlipCard()
        {
            switch (this.State)
            {
                case CardState.FACE_UP:
                    this.State = CardState.FACE_DOWN;
                    break;
                case CardState.FACE_DOWN:
                    this.State = CardState.FACE_UP;
                    break;
                case CardState.REMOVED:
                    // Do nothing card is already removed
                    break;
                default:
                    throw new Exception($"Error: Unexpected card state was {this.State}");
            }
            return this;
        }

        public Card RemoveCard()
        {
            if(this.State == CardState.FACE_UP)
            {
                this.State = CardState.REMOVED;
            }
            else
            {
                throw new InvalidOperationException($"Error: Cannot remove card that isn't face up");
            }
            return this;
        }
    }

    public enum CardState
    {
        FACE_UP,
        FACE_DOWN,
        REMOVED
    }
}
