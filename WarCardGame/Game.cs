using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WarCardGame
{
    public class Game
    {
        public int TotalRounds { get; set; }
        private List<Card> _shuffledDeck { get; set; }
        private List<Player> _players { get; set; }
        public Player _player1 { get; set; }
        public Player _player2 { get; set; }
        private int _rounds { get; set; }
        private string _gameResults { get; set; }

        public Game(Player player1, Player player2)
        {
            _player1 = player1;
            _player2 = player2;
        }

        public void Play()
        {
            _gameResults = "";
            _players = new List<Player>() { _player1, _player2};
            DealCards();
            TotalRounds = (TotalRounds == 0) ? 100 : TotalRounds;
            _rounds = 1;
            while (_rounds < TotalRounds + 1 && (_player1.Cards.Count > 0 && _player2.Cards.Count > 0))
            {
                _gameResults += String.Format("<p>Round {0}</p>", _rounds);
                PlayRound();
                _rounds++;
            }
            
        }

        private void DealCards()
        {
            Deck deck = new Deck();
            _shuffledDeck = deck.Shuffle();
            int counter = 0;
            while (_shuffledDeck.Count > counter)
            {
                foreach (var player in _players)
                {
                    player.Cards.Add(_shuffledDeck.ElementAt(counter));
                    counter++;
                }
            }
        }

        private void PlayRound()
        {
            Battle battle = new Battle(_player1, _player2);
            _gameResults += battle.GameResults;
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
                result += String.Format("{0} has {1} cards<br>", player.Name, player.Cards.Count);
            result += "</p>";
            if (_player1.Cards.Count > _player2.Cards.Count) result += String.Format("<p>{0} Wins!</p>",_player1.Name);
            else result += String.Format("<p>{0} Wins!</p>", _player2.Name);
            return result;
        }

        private string showPlayerDeck()
        {
            string result = "";
            foreach (var player in _players)
            {
                foreach (var card in player.Cards)
                    result += String.Format("{0} received the {1} of {2}<br>", player.Name, card.Name, card.Suit);
            }
            return result;
        }

        private string showShuffledDeck()
        {
            string result = "";
            foreach (var card in _shuffledDeck)
                result += String.Format("Total Cards: {0}<br>{1} of {2} - #{3}<br>",_shuffledDeck.Count, card.Name, card.Suit, card.Number);
            return result;
        }
    }
}