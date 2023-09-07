using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevanInterview.Events.Game
{
    public struct PlayerScoreUpdated
    {
        public string PlayerName;
        public int    PlayerIndex;
        public int    PlayerScore;
    }

    public class Goal
    {
        public string GoalOwnerPlayerID;
    }
}
