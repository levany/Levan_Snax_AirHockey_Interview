using System;
using System.Threading.Tasks;
using LevanInterview.Views;
using UnityEngine.InputSystem;

namespace LevanInterview.Controllers
{
    public enum GameResult
    {
        None,
        Esc,
        Finish
    }

    public class GameController : Controller
    {
        //////////////////////////////////////////////////////////////// Members

        private bool      IsPlaying;

        // cache
        PlayersController PlayerController;
        ScoreController   ScoreController;
        ScoreHUDView      ScoreHUDView;

        //////////////////////////////////////////////////////////////// Properties

        public GameResult Result {get; set;} = GameResult.None;

        //////////////////////////////////////////////////////////////// Lifecycle

        public override async Task OnSystemInit()
        {
            this.PlayerController  = Link.GetController<PlayersController>();
            this.ScoreController   = Link.GetController<ScoreController>();
            this.ScoreHUDView      = Link.GetView<ScoreHUDView>();
        }

        //////////////////////////////////////////////////////////////// Main Flow

        public async Task Execute()
        {
            Logger.LogSeperator();
            Logger.Log("GameController.Execute()");

            Setup();
            await this.RunGame();
            CleaUp();

            Logger.Trace("GameController.Execute().Finished");
        }

        //////////////////////////////////////////////////////////////// Main Flow Steps

        public void Setup()
        {
            try
            {
                Logger.Trace();

                // Player and score
                PlayerController.Setup();
                ScoreController.Setup();

                ScoreHUDView.gameObject.SetActive(true);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                throw;
            }
        }

        private void CleaUp()
        {
            Logger.Trace();

            ScoreHUDView.gameObject.SetActive(false);
        }

        public async Task RunGame()
        {
            IsPlaying = true;
            
            while ( IsPlaying ) 
            {
                await Task.Yield();
            }
        }

        //////////////////////////////////////////////////////////////// API

        public void FinishGame()
        {
            this.IsPlaying = false;
            this.Result    = GameResult.Finish;
        }

        //////////////////////////////////////////////////////////////// Lifecycle Events
        
        public void Update()
        {
            if (IsPlaying)
            {
                if (Keyboard.current[Link.AppSettings.EascapeKey].wasPressedThisFrame)
                {
                    Logger.Log("Escape!");
                    this.IsPlaying  = false;
                    this.Result     = GameResult.Esc;
                }
            }
        }
    }
}
