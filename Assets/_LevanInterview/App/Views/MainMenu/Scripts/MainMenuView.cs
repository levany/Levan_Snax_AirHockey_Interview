using System;
using System.Collections;
using System.Collections.Generic;
using UnibusEvent;
using UnityEngine;

namespace LevanInterview.Views
{
    public enum MainMenuResult
    {
        None,
        Play,
        HowToPlay,
    }

    public class MainMenuView : View
    {
        //// Properties
        
        [NonSerialized] public MainMenuResult result = MainMenuResult.None;

        //// UI Events

        public void OnPlayButtonClicked()
        {
            Logger.Trace();

            this.result = MainMenuResult.Play;

            this.CompleteExectiotion();
        }

        public void OnHowToPlayButtonClicked()
        {
            Logger.Trace();

            this.result = MainMenuResult.HowToPlay;

            this.CompleteExectiotion();
        }
    }
}