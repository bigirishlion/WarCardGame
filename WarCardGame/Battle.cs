using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WarCardGame
{
    public class Battle
    {
        private Player _player1 { get; set; }
        private Player _player2 { get; set; }
        private List<Card> _player1Cards { get; set; }
        private List<Card> _player2Cards { get; set; }
        public string GameResults { get; set; }
        private List<Card> _bounty { get; set; }

        public Battle(Player player1, Player player2)
        {
            _player1 = player1;
            _player2 = player2;
            _bounty = new List<Card>();

            Card card1 = DrawCard(_player1);
            Card card2 = DrawCard(_player2);

            performEvaluation(_player1, _player2, card1, card2);
        }

        private void performEvaluation(Player player1, Player player2, Card player1Card, Card player2Card)
        {
            GameResults += String.Format("<p>{0} draws a {1} of {2}<br>", _player1.Name, player1Card.Name, player1Card.Suit);
            GameResults += String.Format("{0} draws a {1} of {2}<br>", _player2.Name, player2Card.Name, player2Card.Suit);

            if (player1Card.Number == player2Card.Number)
                War(player1Card, player2Card);
            else if (player1Card.Number > player2Card.Number)
                _player1.Cards.AddRange(_bounty);
            else
                _player2.Cards.AddRange(_bounty);

            _bounty.Clear();
        }

        public void War(Card player1Card, Card player2Card)
        {
            GameResults += "<br><p>!!!!! WAR !!!!!</p>";

            DrawCard(_player1);
            Card card1 = DrawCard(_player1);
            DrawCard(_player1);

            DrawCard(_player2);
            Card card2 = DrawCard(_player2);
            DrawCard(_player2);

            performEvaluation(_player1, _player2, card1, card2);
        }

        public Card DrawCard(Player player)
        {
            Card card = player.Cards.ElementAt(0);
            player.Cards.Remove(card);
            _bounty.Add(card);
            return card;
        }
    }
}