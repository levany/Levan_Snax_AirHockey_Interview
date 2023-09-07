using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LevanInterview.Models;
using UnibusEvent;
using UnityEngine;

namespace LevanInterview.Controllers
{
    public class PlayersController : Controller
    {
        //////////////////////// Members
        
        [NonSerialized] public Models.PlayersCollection PlayersCollection;

        //////////////////////// Lifecycle

        public override async Task OnSystemInit()
        {
            Logger.Log($"PlayerController.OnSystemInit()");
            this.PlayersCollection = ScriptableObject.CreateInstance<PlayersCollection>();
        }

        public void Setup()
        {
            Logger.Trace();

            SetupPlayers();
        }

        //////////////////////// Methods

        /// <summary>
        /// Initializas player model objects to hold player data.
        /// using presets we can easly have data ready, and its eaasy to edit if needed.
        /// </summary>
        public void SetupPlayers()
        {            
            Logger.Trace();

            var presetPlayersCollection = Link.Data.GetPreset<Models.PlayersCollection>(); // collection of player data PRESETS - set in edit time
            
            this.PlayersCollection?.Clear();

            // Create player model objects up to the current player count
            for (int i = 0; i < presetPlayersCollection.Count; i++)
            {
                Logger.Trace($"PlayerController.InitializePlayers() i={i} ");

                // Clone player model preset into player model object
                var preset  = presetPlayersCollection[i];
                var Player  = Instantiate(preset);
                Player.name = preset.Name; // remove the "(clone)"

                this.PlayersCollection.Add( Player );
            }

            // Saving data in data service
            Logger.Trace($"PlayerController.SettingModel");
            Link.Data.SetModelSingle(PlayersCollection);
        }
    }
}
