using System;
using System.Linq;
using System.Threading.Tasks;
using LevanInterview.Models;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace LevanInterview.Views
{
    public class ScoreView : View
    {
        //// Lifecycle Events

        public void Update()
        {
            if (Keyboard.current[Link.AppSettings.EascapeKey].wasPressedThisFrame)
            {
                Logger.Log("Escape!");
                this.CompleteExectiotion();
            }
        }

        // UI Events

        public void OnOkButtonClicked()
        {
            this.CompleteExectiotion();
        }
    }

    


}