using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

namespace LevanInterview.Models
{
    [CreateAssetMenu(fileName = "AppSettings", menuName = "Models/AppSettings", order = 0)]
    public class AppSettings : Model
    {
        public Key               EascapeKey                     = Key.Escape;
        public ScreenOrientation ScreenOrientation              = ScreenOrientation.LandscapeLeft;

        public int               MaxSupportedPlayers            = 2;

        public int              POINTS_TO_WIN                   = 7;
        public int              POINTS_FOR_GOAL                 = 1;
    }
}
