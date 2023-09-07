using System;
using System.Linq;
using System.Threading.Tasks;
using LevanInterview.Controllers;
using LevanInterview.Models;
using UnibusEvent;
using UnityEngine;

namespace LevanInterview
{
    public class AirHockeyGameplayController : Controller
    {
        //////////////////////////// Consts

        public const int GOAL_POINTS = 1;

        //////////////////////////// Members

        [Header("Entities")]
        public Puck   puck;
        public Paddle leftPaddle;
        public Paddle rightPaddle;

        [Header("Positions")]
        public Transform Player1PuckStartPosition;
        public Transform Player2PuckStartPosition;

        [Header("systems")]
        public GameSounds sounds;

        // cache
        GameController  gameController;
        ScoreController scoreController;

        //////////////////////////// Lifecycle Events

        public override async Task OnSystemInit()
        {
            this.BindUntilDisable<Events.Game.Goal>(OnGoal);

            this.gameController  = Link.GetController<GameController>();
            this.scoreController = Link.GetController<ScoreController>();
        }

        //////////////////////////// Game Events

        private void OnGoal(Events.Game.Goal e)
        {
            /// Note : IM CHEATING HERE - to make time for higher other priorities.
            /// for a more complex project with more than 2 players
            /// we can do this in a generic,
            /// and write code that we can just copy-paste to other projects and its just works.
            /// 
            /// But for the purpuses of this simple project we can safly assume 
            /// that thae player that scored points is the onw that doesnt own the goal

            var players = Link.GetData<PlayersCollection>();
            var winner  = players.FirstOrDefault(p => p.name != e.GoalOwnerPlayerID);

            if (winner == null) 
            {
                Debug.LogError($"No player found in player collection");
                return;
            }

            // Add points
            scoreController.AddScoreToPlayer(winner.name, GOAL_POINTS);

            // play sound
            sounds.PlayScore();

            // Reset the Stage
            ResetStage();

            // set pluck position
            // note : again, In a real world project - this would a generic implementation,
            // but its not a priority at this moment
            if (e.GoalOwnerPlayerID == "Player2")
            {
                puck.transform.position = Player2PuckStartPosition.position;
            }
            else
            {
                puck.transform.position = Player1PuckStartPosition.position;
            }
            
            // Did we win ?
            CheckForFinishedGame();
        }

        //////////////////////////// Methods

        public void CheckForFinishedGame()
        {
            var players = Link.GetData<PlayersCollection>();
            
            if (players.Any(p => p.Score >= Link.AppSettings.POINTS_TO_WIN))
            {
                gameController.FinishGame();
            }
        }

        public void ResetStage()
        {
            leftPaddle.gameObject.SetActive(false);
            rightPaddle.gameObject.SetActive(false);
            puck.gameObject.SetActive(false);

            leftPaddle.gameObject.SetActive(true);
            rightPaddle.gameObject.SetActive(true);
            puck.gameObject.SetActive(true);
        }
    }
}