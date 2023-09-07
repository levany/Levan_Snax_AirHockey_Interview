using System;
using System.Collections;
using System.Collections.Generic;
using LevanInterview.Controllers;
using TMPro;
using UnibusEvent;
using UnityEngine;
using UnityEngine.InputSystem;

namespace LevanInterview.Views
{
    public class HowToPlayView : View
    {
        //// Members
        
        public TextMeshProUGUI HowToPlayText;

        //// Lifecycle Events

        public void OnEnable()
        {
            if (HowToPlayText != null)
            {
                string WIN_POINTS_PLACEHOLDER_TEXT = "WIN_POINTS"; // in real life - this could be taken from configuration
                HowToPlayText.text = HowToPlayText.text.Replace(WIN_POINTS_PLACEHOLDER_TEXT, Link.AppSettings.POINTS_TO_WIN.ToString());
            }
        }

        //// UI Events

        public void OnOkButtonClicked()
        {
            Logger.Trace();

            this.CompleteExectiotion();
        }

        // Lifecycle Events

        public void Update()
        {
            if (Keyboard.current[Link.AppSettings.EascapeKey].wasPressedThisFrame)
            {
                Logger.Log("Escape!");
                this.CompleteExectiotion();
            }
        }

    }
}