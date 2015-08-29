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

        public Battle(Player player1, Player player2)
        {
            _player1 = player1;
            _player2 = player2;
            _player1Cards = _player1.Cards;
            _player2Cards = _player2.Cards;

            Card player1Card = _player1Cards.FirstOrDefault();
            _player1Cards.Remove(player1Card);
            Card player2Card = _player2Cards.FirstOrDefault();
            _player2Cards.Remove(player2Card);

            if (player1Card != null && player2Card != null)
            {
                GameResults += String.Format("<p>{0} draws a {1} of {2}<br>", _player1.Name, player1Card.Name, player1Card.Suit);
                GameResults += String.Format("{0} draws a {1} of {2}<br>", _player2.Name, player2Card.Name, player2Card.Suit);

                if (player1Card.Number == player2Card.Number)
                    War(player1Card, player2Card);
                else if (player1Card.Number > player2Card.Number)
                    RedistributeCards(_player1, player1Card, player2Card);
                else
                    RedistributeCards(_player2, player2Card, player1Card);
            }
        }

        public void War(Card player1Card, Card player2Card)
        {
            if (_player1Cards.Count > 10 && _player2Cards.Count > 10)
            {
                GameResults += "<br><p>!!!!! WAR !!!!!</p>";

                List<Card> warcards = new List<Card>() { player1Card, player2Card };

                for (int i = 0; i < 2; i++)
                {
                    warcards.Add(_player1Cards.ElementAt(i));
                    warcards.Add(_player2Cards.ElementAt(i));
                    _player1Cards.Remove(_player1Cards.ElementAt(i));
                    _player2Cards.Remove(_player2Cards.ElementAt(i));
                }

                GameResults += String.Format("<p>{0} and {1} draw 4 cards and flip the last card over</p>", _player1.Name, _player2.Name);
                GameResults += "<p>";
                foreach (var card in warcards)
                    GameResults += String.Format("{0} of {1}<br>", card.Name, card.Suit);

                foreach (var card in _player1Cards)
                    GameResults += String.Format("Player 1 Cards: {0} of {1}<br>", card.Name, card.Suit);
                foreach (var card in _player2Cards)
                    GameResults += String.Format("Player 2 Cards: {0} of {1}<br>", card.Name, card.Suit);

                Card player1WarCard = _player1Cards.FirstOrDefault();
                _player1Cards.Remove(player1WarCard);
                Card player2WarCard = _player2Cards.FirstOrDefault();
                _player2Cards.Remove(player2WarCard);

                GameResults += "</p>";
                GameResults += String.Format("<p>{0} flips over a {1} of {2}<br>",_player1.Name, player1WarCard.Name, player1WarCard.Suit);
                GameResults += String.Format("{0} flips over a {1} of {2}<br>", _player2.Name, player2WarCard.Name, player2WarCard.Suit);

                if (player1WarCard == player2WarCard)
                    War(player1WarCard, player2WarCard);
                else if (player1WarCard.Number > player2WarCard.Number)
                    RedistributeWarCards(_player1, warcards, player1WarCard);
                else
                    RedistributeWarCards(_player2, warcards, player2WarCard);
            }
            else if (player1Card.Number > player2Card.Number)
                RedistributeCards(_player1, player1Card,player2Card);
            else
                RedistributeCards(_player2, player2Card, player1Card);
        }

        public void RedistributeCards(Player winningPlayer, Card winnerCard, Card loserCard)
        {
            GameResults += String.Format("<b>{0} wins with a {1} of {2}</b></p>", winningPlayer.Name, winnerCard.Name, winnerCard.Suit);
            winningPlayer.Cards.Add(winnerCard);
            winningPlayer.Cards.Add(loserCard);
        }

        public void RedistributeWarCards(Player winningPlayer, List<Card> warCards, Card winnerCard)
        {
            GameResults += String.Format("<b>{0} wins with a {1} of {2}</b></p>", winningPlayer.Name, winnerCard.Name, winnerCard.Suit);
            foreach (var card in warCards)
                winningPlayer.Cards.Add(card);
        }

        public void DrawCards()
        {
            Card player1WarCard = _player1Cards.FirstOrDefault();
            Card player2WarCard = _player2Cards.FirstOrDefault();
            _player1Cards.Remove(player1WarCard);
            _player2Cards.Remove(player2WarCard);
        }
    }
}