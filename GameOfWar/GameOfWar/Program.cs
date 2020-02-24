/*
 * The Game of War 
 * 2/23/2020
 * Joshua Dietz
 * 
 * The deck is divided evenly, with each player receiving 26 cards, dealt one at a time, face down.
 * Anyone may deal first. Each player places their stack of cards face down, in front of them.
 * 
 * Each player turns up a card at the same time and the player with the higher card takes both
 * cards and puts them, face down, on the bottom of his stack.
 * 
 * If the cards are the same rank, it is War. Each player turns up one card face down and one card face up. 
 * The player with the higher cards takes both piles (six cards). If the turned-up cards are again the same rank,
 * each player places another card face down and turns another card face up. The player with the higher card
 * takes all 10 cards, and so on.
 * 
 * The game ends when one player has won all the cards. - https://bicyclecards.com/how-to-play/war/
 * 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGameOfWar
{
    public class Cards
    {
        protected int value;
        protected string suit;
        protected string cardName;

        public Cards(int p1, string p2, string p3)
        {
            value = p1;
            suit = p2;
            cardName = p3;
        }

        public int GetValue()
        {
            return value;
        }

        public string GetSuit()
        {
            return suit;
        }

        public string GetCardName()
        {
            return cardName;
        }
    }

    public class GameOfWar
    {
        public static void CreateDeck(LinkedList<Cards> deck)
        {
            int i;

            for (i = 2; i <= 14; i++)
            {
                deck.AddLast(new Cards(i, "clubs", ConvertName(i) + " of clubs"));
            }

            for (i = 2; i <= 14; i++)
            {
                deck.AddLast(new Cards(i, "spades", ConvertName(i) + " of spades"));
            }

            for (i = 2; i <= 14; i++)
            {
                deck.AddLast(new Cards(i, "hearts", ConvertName(i) + " of hearts"));
            }

            for (i = 2; i <= 14; i++)
            {
                deck.AddLast(new Cards(i, "diamonds", ConvertName(i) + " of diamonds"));
            }

        }

        public static string ConvertName(int x)
        {
            if (x <= 10)
            {
                return x.ToString();
            }
            else if (x == 11)
            {
                return "jack";
            }
            else if (x == 12)
            {
                return "queen";
            }
            else if (x == 13)
            {
                return "king";
            }
            else if (x == 14)
            {
                return "ace";
            }
            else
            {
                return "error";
            }
        }

        public static void ShuffleDeck(LinkedList<Cards> deck, int randomness)
        {
            Random random = new Random();

            int num;
            int count = 0;
            int swappedCount = 0;

            for (int i = 0; i < randomness; i++)
            {
                num = random.Next(52);
                num++;

                foreach (var item in deck)
                {
                    count++;
                    if (count == num)
                    {
                        swappedCount++;
                        deck.AddLast(item);
                        deck.Remove(item);
                        count = 0;
                        break;
                    }
                }
            }                        
        }

        public static void DealCards(LinkedList<Cards> deck, LinkedList<Cards> deckOne, LinkedList<Cards> deckTwo)
        {
            Random random = new Random();

            int i = random.Next(2); //choose who deals       

            foreach (var item in deck)
            {
                if (i == 0)
                {
                    deckOne.AddFirst(item);
                    i = 1;
                }
                else
                {
                    deckTwo.AddFirst(item);
                    i = 0;
                }
            }
        }

        public static void Play(LinkedList<Cards> playerDeck, LinkedList<Cards> computerDeck, LinkedList<Cards> warCards)
        {
            var playerCard = playerDeck.First.Value;
            var computerCard = computerDeck.First.Value;

            if (playerCard.GetValue() > computerCard.GetValue()) //Player wins, add both cards to bottom of playerDeck
            {
                Console.WriteLine("You drew the " + playerCard.GetCardName());
                Console.WriteLine("Computer drew the " + computerCard.GetCardName());
                Console.WriteLine("You win the round\n");
                computerDeck.Remove(computerCard);
                playerDeck.Remove(playerCard);
                playerDeck.AddLast(computerCard);
                playerDeck.AddLast(playerCard);
            }
            else if (playerCard.GetValue() < computerCard.GetValue()) //Computer wins, add both cards to bottom of computerDeck
            {
                Console.WriteLine("You drew the " + playerCard.GetCardName());
                Console.WriteLine("Computer drew the " + computerCard.GetCardName());
                Console.WriteLine("Computer wins the round\n");
                computerDeck.Remove(computerCard);
                playerDeck.Remove(playerCard);
                computerDeck.AddLast(playerCard);
                computerDeck.AddLast(computerCard);
            }
            else
            {
                Console.WriteLine("You drew the " + playerCard.GetCardName());
                Console.WriteLine("Computer drew the " + computerCard.GetCardName());
                Console.WriteLine("War!");
                War(playerDeck, computerDeck, warCards);
            }

            Console.WriteLine("Press any key play the next round.\n");
            Console.ReadKey();
        }

        public static void War(LinkedList<Cards> playerDeck, LinkedList<Cards> computerDeck, LinkedList<Cards> warCards)
        {
            WarWinTest(playerDeck, computerDeck); //check player and computer have enough cards for war and didnt loose
            
            //store tied cards
            var playerCard = playerDeck.First.Value;
            var computerCard = computerDeck.First.Value;

            //remove tied cards
            playerDeck.Remove(playerCard);
            computerDeck.Remove(computerCard);

            //store facedown card
            var playerCardTwo = playerDeck.First.Value;
            var computerCardTwo = computerDeck.First.Value;

            //remove facedown card
            playerDeck.Remove(playerCardTwo);
            computerDeck.Remove(computerCardTwo);

            //face up card
            var playerCardThree = playerDeck.First.Value;
            var computerCardThree = computerDeck.First.Value;

            if (playerCardThree.GetValue() > computerCardThree.GetValue()) //Player wins, add 6 cards to bottom of playerDeck
            {
                Console.WriteLine("You drew the " + playerCardThree.GetCardName());
                Console.WriteLine("Computer drew the " + computerCardThree.GetCardName());
                Console.WriteLine("You win the War");
                computerDeck.Remove(computerCardThree);
                playerDeck.Remove(playerCardThree);
                playerDeck.AddLast(computerCardThree);
                playerDeck.AddLast(playerCardThree);
                playerDeck.AddLast(playerCard);
                playerDeck.AddLast(computerCard);
                playerDeck.AddLast(playerCardTwo);
                playerDeck.AddLast(computerCardTwo);

                Console.WriteLine("\nBounty cards won:");                

                //check if war cards empty if not add them to player deck and empty warCards

                if (warCards.Count != 0)
                {                    
                    foreach (var item in warCards)
                    {                        
                        playerDeck.AddLast(item);
                        Console.WriteLine(item.GetCardName());
                    }

                    warCards.Clear();
                }

                Console.WriteLine(playerCardTwo.GetCardName());
                Console.WriteLine(computerCardTwo.GetCardName());
                Console.WriteLine(" ");
            }
            else if (playerCardThree.GetValue() < computerCardThree.GetValue()) //Computer wins, add 6 cards to bottom of computerDeck
            {
                Console.WriteLine("You drew the " + playerCardThree.GetCardName());
                Console.WriteLine("Computer drew the " + computerCardThree.GetCardName());
                Console.WriteLine("Computer wins the War");
                computerDeck.Remove(computerCardThree);
                playerDeck.Remove(playerCardThree);
                computerDeck.AddLast(playerCardThree);
                computerDeck.AddLast(computerCardThree);
                computerDeck.AddLast(playerCard);
                computerDeck.AddLast(computerCard);
                computerDeck.AddLast(playerCardTwo);
                computerDeck.AddLast(computerCardTwo);

                Console.WriteLine("\nBounty cards lost:");

                //check if warCards empty. if not add them to computer deck and empty warCards

                if (warCards.Count != 0)
                {
                    foreach (var item in warCards)
                    {                        
                        computerDeck.AddLast(item);
                        Console.WriteLine(item.GetCardName());
                    }

                    warCards.Clear();
                }

                Console.WriteLine(playerCardTwo.GetCardName());
                Console.WriteLine(computerCardTwo.GetCardName());
                Console.WriteLine(" ");
            }
            else
            {
                Console.WriteLine("You drew the " + playerCardThree.GetCardName());
                Console.WriteLine("Computer drew the " + computerCardThree.GetCardName());
                Console.WriteLine("War!");

                computerDeck.Remove(computerCardThree);
                playerDeck.Remove(playerCardThree);

                //add cards to warCards

                warCards.AddLast(playerCard);
                warCards.AddLast(computerCard);
                warCards.AddLast(playerCardTwo);
                warCards.AddLast(computerCardTwo);
                warCards.AddLast(playerCardThree);                
                warCards.AddLast(computerCardThree);

                War(playerDeck, computerDeck, warCards);
            }

        }

        public static void WarWinTest(LinkedList<Cards> playerDeck, LinkedList<Cards> computerDeck)
        {
            char exitCharacter = 'n';

            if (playerDeck.Count < 3)
            {
                Console.WriteLine("The computer won the game of war");
                WantToExit(exitCharacter);
            }
            else if (computerDeck.Count < 3)
            {
                Console.WriteLine("You won the game of war");
                WantToExit(exitCharacter);
            }
            else
            {
                Console.WriteLine("Let the war begin.");
            }
        }


        public static void WantToExit(char exitKey)
        {
            while (exitKey != 'e')
            {
                Console.WriteLine("Press 'e' key to exit.");
                exitKey = Console.ReadKey().KeyChar;
            }
            
            System.Environment.Exit(0);

        }

        public static void BeginGame()
        {
            Console.WriteLine("Welcome to the Game of War");
            Console.WriteLine("Press any key to begin.\n");
            Console.ReadKey();
        }


        static void Main(string[] args)
        {
            LinkedList<Cards> deck = new LinkedList<Cards>();
            LinkedList<Cards> playerDeck = new LinkedList<Cards>();
            LinkedList<Cards> computerDeck = new LinkedList<Cards>();
            LinkedList<Cards> warCards = new LinkedList<Cards>();
            
            char exitCharacter = 'n';

            BeginGame();
            CreateDeck(deck);
            ShuffleDeck(deck, 500);
            DealCards(deck, playerDeck, computerDeck);

            while (playerDeck.Count > 0 && computerDeck.Count > 0) //check for a win condition
            {                
                Play(playerDeck, computerDeck, warCards);
            }

            if (playerDeck.Count == 0)  //determine winner
            {
                Console.WriteLine("The computer won the game of war");
            }
            else
            {
                Console.WriteLine("You won the game of war");
            }

            WantToExit(exitCharacter);            
        }
    }
}