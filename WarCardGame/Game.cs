using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WarCardGame
{
    public class Game
    {
        private List<Card> _shuffledDeck { get; set; }
        private List<Player> _players { get; set; }
        public Player Player1 { get; set; }
        public Player Player2 { get; set; }
        private int _rounds { get; set; }
        private int _totalRounds { get; set; }
        private string _gameResults { get; set; }

        public void Play()
        {
            _gameResults = "";
            Player1 = new Player() { Name = "Player 1" };
            Player2 = new Player() { Name = "Player 2" };
            _players = new List<Player>();
            _players.Add(Player1);
            _players.Add(Player2);

            Deck deck = new Deck();
            _shuffledDeck = deck.Shuffle();

            bool alternate = true;
            foreach (var card in _shuffledDeck)
            {
                if (alternate)
                {
                    Player1.Cards.Add(card);
                    alternate = false;
                }
                else
                {
                    Player2.Cards.Add(card);
                    alternate = true;
                }
                    
            }

            _totalRounds = 100;
            _rounds = 1;
            while (_rounds < _totalRounds + 1 && (Player1.Cards.Count > 0 && Player2.Cards.Count > 0))
            //while (Player1.Cards.Count > 0 || Player2.Cards.Count > 0)
            {
                _gameResults += String.Format("<p>Round {0}</p>", _rounds);
                PlayRound();
                _rounds++;
            }
            
        }

        private void PlayRound()
        {
            // check each players card and determine which one wins
            Card player1Card = Player1.Cards.FirstOrDefault();
            Card player2Card = Player2.Cards.FirstOrDefault();

            if (player1Card != null && player2Card != null)
            {
                _gameResults += String.Format("<p>{0} draws a {1} of {2}<br>", Player1.Name, player1Card.Name, player1Card.Suit);
                _gameResults += String.Format("{0} draws a {1} of {2}<br>", Player2.Name, player2Card.Name, player2Card.Suit);

                Player1.Cards.Remove(player1Card);
                Player2.Cards.Remove(player2Card);
                   
                // if players draw matching numbers, begin WAR
                if (player1Card.Number == player2Card.Number)
                {
                    if (Player1.Cards.Count > 10 && Player2.Cards.Count > 10)
                    {
                        _gameResults += "<br><p>!!!!! WAR !!!!!</p>";
                        // WAR is 3 cards down and 1 card up. whoever has thehighest number wins
                        List<Card> player1WarList = new List<Card>() { player1Card };
                        List<Card> player2WarList = new List<Card>() { player2Card };

                        for (int i = 0; i < 4; i++)
                        {
                            player1WarList.Add(Player1.Cards.ElementAt(i));
                            Player1.Cards.Remove(Player1.Cards.ElementAt(i));
                            player2WarList.Add(Player2.Cards.ElementAt(i));
                            Player2.Cards.Remove(Player2.Cards.ElementAt(i));
                        }

                        Card player1WarCard = player1WarList.LastOrDefault();
                        Card player2WarCard = player2WarList.LastOrDefault();

                        _gameResults += String.Format("<p>Player1 and Player2 draw 4 cards and flip the last card over</p>");
                        _gameResults += String.Format("<p>Player1 flips over a {0} of {1}<br>", player1WarCard.Name, player1WarCard.Suit);
                        _gameResults += String.Format("Player2 flips over a {0} of {1}<br>", player2WarCard.Name, player2WarCard.Suit);

                        if (player1WarCard.Number > player2WarCard.Number)
                        {
                            _gameResults += String.Format("<b>Player 1 wins with a {0} of {1}</b></p>", player1WarCard.Name, player1WarCard.Suit);
                            foreach (var card in player1WarList)
                                Player1.Cards.Add(card);
                            foreach (var card in player2WarList)
                                Player1.Cards.Add(card);
                        }
                        else
                        {
                            _gameResults += String.Format("<b>Player 2 wins with a {0} of {1}</b></p>", player2WarCard.Name, player2WarCard.Suit);
                            foreach (var card in player1WarList)
                                Player2.Cards.Add(card);
                            foreach (var card in player2WarList)
                                Player2.Cards.Add(card);
                        }
                    }
                    else if (player1Card.Number > player2Card.Number)
                    {
                        _gameResults += String.Format("<b>Player 1 wins with a {0} of {1}</b></p>", player1Card.Name, player1Card.Suit);
                        Player1.Cards.Add(player1Card);
                        Player1.Cards.Add(player2Card);
                    }
                    else
                    {
                        _gameResults += String.Format("<b>Player 2 wins with a {0} of {1}</b></p>", player2Card.Name, player2Card.Suit);
                        Player2.Cards.Add(player2Card);
                        Player2.Cards.Add(player1Card);
                    }

                }   // winner takes both cards and adds it back to their deck
                else if (player1Card.Number > player2Card.Number)
                {
                    _gameResults += String.Format("<b>Player 1 wins with a {0} of {1}</b></p>", player1Card.Name, player1Card.Suit);
                    Player1.Cards.Add(player1Card);
                    Player1.Cards.Add(player2Card);
                }
                else
                {
                    _gameResults += String.Format("<b>Player 2 wins with a {0} of {1}</b></p>", player2Card.Name, player2Card.Suit);
                    Player2.Cards.Add(player2Card);
                    Player2.Cards.Add(player1Card);
                }
            }

        }

        public string DisplayResults()
        {
            string result = "";
            result += _gameResults;
            //result += showShuffledDeck();
            //result += showPlayerDeck();
            result += showPlayerResults();
            return result;
        }

        private string showPlayerResults()
        {
            string result = "<p>";
            foreach (var player in _players)
            {
                result += String.Format("{0} has {1} cards<br>", player.Name, player.Cards.Count);
            }
            result += "</p>";
            if (Player1.Cards.Count > Player2.Cards.Count)
                result += "<p>Player 1 Wins!</p>";
            else
                result += "<p>Player 2 Wins!</p>";
            return result;
        }

        private string showPlayerDeck()
        {
            string result = "";
            foreach (var player in _players)
            {
                foreach (var card in player.Cards)
                {
                    result += String.Format("{0} received the {1} of {2}<br>", player.Name, card.Name, card.Suit);
                }
            }
            return result;
        }

        private string showShuffledDeck()
        {
            string result = "";
            foreach (var card in _shuffledDeck)
            {
                result += String.Format("Total Cards: {0}<br>{1} of {2} - #{3}<br>",_shuffledDeck.Count, card.Name, card.Suit, card.Number);
            }
            return result;
        }
    }
}