using System;
using System.Collections.Generic;
using System.Text;

/*
 * Link to reddit problem
 * https://old.reddit.com/r/dailyprogrammer/comments/aq6gfy/20190213_challenge_375_intermediate_a_card/
 */
namespace DailyProgrammer.Intermediate.Challenge_375
{
    public static class CardFlippingGame
    {
        public static string PlayCardFlippingGame(String input)
        {
            
            Game game = new Game(input);

            if (GameIsWinnable(game))
            {
                List<int> solution = GetSolutionToGame(game);

                StringBuilder sb = new StringBuilder();
                for(int i = 0; i < solution.Count; i++)
                {
                    if (i != solution.Count - 1)
                    {
                        sb.Append(solution[i] + " -> ");
                    }
                    else
                    {
                        sb.Append(solution[i]);
                    }
                }
                return sb.ToString();
            }

            return "No Solution";
        }

        private static bool GameIsWinnable(Game game)
        {
            int counter = 0;
            for(int i = 0; i < game.GetNumberOfCards(); i++)
            {
                if(game.GetCardState(i) == CardState.FACE_UP)
                {
                    counter++;
                }
            }

            return !(counter % 2 == 0);
        }

        public static List<int> GetSolutionToGame(Game game)
        {
            List<int> solution = new List<int>();
            bool nextIsHigher = false;
            for(int i = 0; i < game.GetNumberOfCards(); i++)
            {
                
                if (nextIsHigher)
                {
                    solution.Add(i);
                }
                else
                {
                    solution.Insert(0, i);
                }

                // xor of nextIsHigher and cardState = faceUp
                nextIsHigher ^= game.GetCardState(i) == CardState.FACE_UP;
            }

            return solution;
        }

        /*
         def flip(number):
            order, next_card_higher = [], False
            for index, num in enumerate(number):
                order.append(index) if next_card_higher else order.insert(0, index)
                next_card_higher ^= num=="1"
            return order if next_card_higher else "No solution" */
    }
}
