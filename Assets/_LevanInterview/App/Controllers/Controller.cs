using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace LevanInterview.Controllers
{
    public class Controller : MonoBehaviour
    {
        // API

        public virtual void Enable()
        {
            Logger.Log($"Controller [{this.name}] Enabled");
            this.gameObject.SetActive(true);
        }

        public virtual void Disable()
        {   
            Logger.Log($"Controller [{this.name}] Disabled");
            this.gameObject.SetActive(false);
        }

        // Virtual Methods

        public virtual async Task OnSystemInit()
        {
            Logger.Log($"SystemInit");
        }
    }
}
