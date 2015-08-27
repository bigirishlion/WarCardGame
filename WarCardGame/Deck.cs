using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WarCardGame
{
    public class Deck
    {
        public List<Card> Cards { get; set; }

        List<string> SuitsList = new List<string>() { "Spades", "Clubs", "Diamonds", "Hearts" };
        List<int> CardNumbers = new List<int>() { 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14 };
        
        Random random = new Random();

        public Deck()
        {
            CreateDeck();
        }

        public List<Card> CreateDeck()
        {
            Cards = new List<Card>();

            foreach (var number in CardNumbers)
            {
                foreach (var suit in SuitsList)
                {
                    Card card = new Card() { Number = number, Suit = suit};
                    if (number == 14) card.Name = "Ace";
                    else if (number == 11) card.Name = "Jack";
                    else if (number == 12) card.Name = "Queen";
                    else if (number == 13) card.Name = "King";
                    else card.Name = number.ToString();
                    Cards.Add(card);
                }
            }
            return Cards;
        }

        public List<Card> Shuffle()
        {
            return Cards.OrderBy<Card, int>(c => random.Next()).ToList();
        }
    }
}