using System;
using System.Threading.Tasks;
using LevanInterview.Controllers;
using LevanInterview.Views;
using UnityEngine;

namespace LevanInterview
{
    public class AirHockeyApp : Singleton<AirHockeyApp>
    {
        //////////////////////////////////////////////// Members
        
        [HideInInspector] public bool IsRunning = true;
        
        private MainMenuView   mainMenuView;
        private GameController gameController;
        private ScoreView      scoreView;

        //////////////////////////////////////////////// Lifecycle 

        protected override async Task OnAwake()
        {
            Logger.Log("App.Awake");

            Link.Instance.Initialize(this);
            
            // Chaching References
            this.mainMenuView    = Link.GetView<MainMenuView>();
            this.gameController  = Link.GetController<GameController>();
            this.scoreView       = Link.GetView<ScoreView>();

            await ResetApp();
            await SystemInit();
        }

        protected async Task Start()
        {
            Logger.LogSeperator();
            Logger.Log("Start");
            
            await Task.Yield();

          //await ResetApp();
            await StartApp();
        }

        protected async Task StartApp()
        {
            Logger.LogSeperator();
            Logger.Log("StartApp");

            await Task.Yield(); // Skip frame to let objects possibly Initialize on Start()

            await Execute();
        }

        //////////////////////////////////////////////// Initialization
        
        protected async Task ResetApp()
        {
            try
            {
                Logger.Trace();

                // Hide All Views
                
                Logger.LogSeperator();
                Logger.Log("Hiding Views");

                foreach (var view in Link.Instance.Views)
                {
                    view.Hide();                
                }

                // Disable Game Controller 
                Logger.Log("Disabling Game Controller");
                this.gameController.Disable();
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                throw;
            }
        }

        protected async Task SystemInit()
        {
            try
            {                
                Logger.LogSeperator();
                Logger.Log($"SystemInit");

                // Initialize Services

                Logger.LogSeperator();
                Logger.Log($"initializing Services");
                
                await Link.Data.OnSystemInit();
                
                // Initialize Controllers

                Logger.LogSeperator();
                Logger.Log($"Initializing Controllers");

                foreach (var controller in Link.Instance.Controllers)
                {
                    await controller.OnSystemInit();
                }
                
                // Initialize Views
                Logger.LogSeperator();
                Logger.Log($"Initializing Views");

                foreach (var view in Link.Instance.Views)
                {
                    await view.OnSystemInit();                
                }
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                throw;
            }
        }

        //////////////////////////////////////////////// Main Flow Steps

        protected async Task Execute()
        {
            while (IsRunning)
            {
                await RunMainMenu();
            }
        }

        protected async Task RunMainMenu()
        {
            await this.mainMenuView.Execute();

            switch (mainMenuView.result)
            {
                case MainMenuResult.Play      : await RunGmae();          break;
                case MainMenuResult.HowToPlay : await RunHowToPlayMenu(); break;
                default: break;
            }            
        }

        protected async Task RunHowToPlayMenu()
        {
            var   view = Link.GetView<HowToPlayView>();
            await view.Execute();

            return; // To main menu
        }

        protected async Task RunGmae()
        {
            Logger.LogSeperator();
            Logger.Log($"RunGmae()");
            
            // Run Game

            var game = Link.GetController<GameController>();
            game.Enable();
            await game.Execute();
            game.Disable();

            // Check Result
            
            var result = game.Result;

            if (result ==  GameResult.Esc)
            {
                return; // To main menu
            }
            else
            {
                await RunGameFinishedFlow();
            }
        }

        protected async Task RunGameFinishedFlow()
        {   
            Logger.LogSeperator();
            Logger.Log("RunGameFinishedFlow");

            await scoreView.Execute();
        }
    }
}
