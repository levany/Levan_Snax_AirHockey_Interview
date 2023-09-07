using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using LevanInterview.Events.Game;
using LevanInterview.Models;
using TMPro;
using UnibusEvent;
using UnityEngine;
using UnityEngine.UI;

namespace LevanInterview.Views
{
	public class ScoreHUDViewItem : MonoBehaviour
	{
        //// Members

        public string          playerID;
		public TextMeshProUGUI PlayerScoreText;

        private bool isDataAvailable = false;

        //// Lifecycle Events

        private void OnEnable()
        {
            try
            {
                this.BindUntilDisable<Events.Game.PlayerScoreUpdated>(onPlayerScoreUpdated);
                this.PlayerScoreText.text = "0";

                if (isDataAvailable)
                {
                    var players = Link.GetData<PlayersCollection>();
                    var player  = players.First(p => p.name == playerID);
                    this.PlayerScoreText.color = player.Color;
                }
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
        }

        public void Start()
        {
            isDataAvailable = true;
        }

        //// Game Events

        private void onPlayerScoreUpdated(PlayerScoreUpdated e)
        {
            if (e.PlayerName == playerID)
            {
                this.PlayerScoreText.text = e.PlayerScore.ToString();
            }
        }
    } 
}
