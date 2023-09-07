using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;

namespace LevanInterview
{
    /*
    This code was tacken from the internet.

    a singleton doesnt really have to be this bombastic,
    here is some benifits they describe : 

    ////////////////////////////////////////////////////////////////////////////////////////////////////////

    implementation of a generic Singleton 

    It can do everything, and it does so neatly and efficiently:

    Create object        Removes scene        Global access?               Keep across
    if not in scene?     duplicates?                                       Scene loads?

         Yes                  Yes                  Yes                     Yes (optional)
    
    Other advantages:

    It's thread-safe.
    It avoids bugs related to acquiring (creating) singleton instances when the application is quitting by ensuring that singletons cannot be created after OnApplicationQuit(). (And it does so with a single global flag, instead of each singleton type having their own)
    It uses Unity 2017's Mono Update (roughly equivalent to C# 6). (But it can easily be adapted for the ancient version)
    It comes with some free candy!

    ////////////////////////////////////////////////////////////////////////////////////////////////////////
    */

    public abstract class Singleton<T> : Singleton where T : MonoBehaviour
    {
        #region  Fields
        [CanBeNull]
        private static T _instance;

        [NotNull]
        // ReSharper disable once StaticMemberInGenericType
        private static readonly object Lock = new object();

        [SerializeField]
        private bool _persistent = false;
        #endregion

        #region  Properties
        [NotNull]
        public static T Instance
        {
            get
            {
                if (Quitting)
                {
                    Debug.LogWarning($"[{nameof(Singleton)}<{typeof(T)}>] Instance will not be returned because the application is quitting.");
                    // ReSharper disable once AssignNullToNotNullAttribute
                    return null;
                }
                lock (Lock)
                {
                    if (_instance != null)
                        return _instance;
                    var instances = FindObjectsOfType<T>();
                    var count = instances.Length;
                    if (count > 0)
                    {
                        if (count == 1)
                            return _instance = instances[0];
                        Debug.LogWarning($"[{nameof(Singleton)}<{typeof(T)}>] There should never be more than one {nameof(Singleton)} of type {typeof(T)} in the scene, but {count} were found. The first instance found will be used, and all others will be destroyed.");
                        for (var i = 1; i < instances.Length; i++)
                            Destroy(instances[i]);
                        return _instance = instances[0];
                    }

                    Debug.Log($"[{nameof(Singleton)}<{typeof(T)}>] An instance is needed in the scene and no existing instances were found, so a new instance will be created.");
                    return _instance = new GameObject($"({nameof(Singleton)}){typeof(T)}")
                               .AddComponent<T>();
                }
            }
        }
        #endregion

        #region  Methods
        private async Task Awake()
        {
            if (_persistent)
                DontDestroyOnLoad(gameObject);
            await OnAwake();
        }

        protected virtual async Task OnAwake() { }
        #endregion
    }

    public abstract class Singleton : MonoBehaviour
    {
        #region  Properties
        public static bool Quitting { get; private set; }
        #endregion

        #region  Methods
        private void OnApplicationQuit()
        {
            Quitting = true;
        }
        #endregion
    }
}
