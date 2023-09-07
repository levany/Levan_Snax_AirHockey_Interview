using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
using LevanInterview.Models;
using System.Linq;

namespace LevanInterview
{
    [SelectionBase]
    public class Paddle : MonoBehaviour
    {
        public string PlayerID;
        public Color  Color;

        public void Start()
        {
            var players = Link.GetData<PlayersCollection>();
            var player  = players.FirstOrDefault(p => p.name == PlayerID);
            this.Color  = player.Color;
        }

        private void OnDisable()
        {
            this.GetComponentInChildren<TrailRenderer>().Clear();
        }
    }
}

