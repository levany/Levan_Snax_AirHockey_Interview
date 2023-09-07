using System.Linq;
using System.Threading.Tasks;
using LevanInterview.Models;
using LevanInterview.Views;
using UnibusEvent;

namespace LevanInterview.Controllers
{
    public class ScoreController : Controller
    {
        public void Setup()
        {
            Logger.Trace();
            ResetScores();
        }

        /// <summary>
        /// resets the scores for a new game
        /// </summary>
        public void ResetScores()
        {
            Logger.Trace();
            
            var playersCollection = Link.GetData<PlayersCollection>();

            foreach (var player in playersCollection)
            {
                player.Score = 0;
            }
        }


        // Methods

        /// <summary>
        /// Updates score and returns the updated score for the given player
        /// </summary>
        public int AddScoreToPlayer(string playerName, int score)
        {
            Logger.Trace($"Adding {score} points to player {playerName}");

            var playersCollection = Link.GetData<PlayersCollection>();

            var player      = playersCollection.First(p => p.Name == playerName);
            var playerIndex = playersCollection.IndexOf(player);
            
            player.Score   += score;

            Unibus.Dispatch(new Events.Game.PlayerScoreUpdated() { PlayerIndex = playerIndex
                                                                 , PlayerName  = playerName
                                                                 , PlayerScore = player.Score });
            return player.Score;
        }
    }
}

