using System.Collections;
using System.Collections.Generic;
using LevanInterview.Models;
using LevanInterview;
using TMPro;
using UnityEngine;
using System.Threading.Tasks;
using System.Linq;

namespace LevanInterview
{
    public class ScoreViewPanel : MonoBehaviour
    {
        //////////////// Members

        [Header("Player")]
        public string           playerID;

        [Header("UI references")]
        public TextMeshProUGUI  NameText;
        public TextMeshProUGUI  ScoreText;

        //////////////// Lifecycle Events

        public void OnEnable()
        {
            Logger.Trace($"Displaying score for player : {this.playerID}");

            var players = Link.GetData<PlayersCollection>();

            var player  = players.FirstOrDefault(p => p.name == playerID);

            if (player == null)
            {
                Logger.LogError($"No player found with ID {this.playerID}");
                return;
            }

            this.NameText.text   = player.name;
            this.ScoreText.text  = player.Score.ToString();
            this.ScoreText.color = player.Color;
        }
    }
}