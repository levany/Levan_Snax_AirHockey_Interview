using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using LevanInterview.Controllers;
using LevanInterview.Models;
using LevanInterview.Services;
using LevanInterview.Views;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LevanInterview
{
	public class Link : MonoBehaviour
	{
        // Singleton

        public static Link              Instance { get; private set; }

        // Members

		public List<Controller>         Controllers = new List<Controller>();
		public List<View>               Views       = new List<View>();
        public List<Service>            Services    = new List<Service>();

        public AppGlobals               AppGlobals;

        public Dictionary<Type, object> Index;

        // Services

        [NonSerialized] public DataService dataService;
        
        // Lifecycle

        public Link()
        {
            Instance = this;   
            Index    = new Dictionary<Type, object>();
        }

        private void Awake()
        {
            this.AppGlobals = GetComponent<AppGlobals>();
        }

        public IEnumerable<GameObject>    AllGameObjects => this.gameObject.scene.GetRootGameObjects();
        public IEnumerable<MonoBehaviour> AllComponents  => AllGameObjects.SelectMany(go => go.GetComponentsInChildren<MonoBehaviour>(includeInactive:true));

        /// <summary>
        /// Collects all Services, Controllers, and views
        /// </summary>
        public void Initialize(AirHockeyApp app)
        {
            this.Controllers.AddRange(AllComponents.OfType<Controller>()); // Collect Conrollers
            foreach (var c in Controllers) {Index.Add(c.GetType(), c); }   // Update index

            this.Views.AddRange(AllComponents.OfType<View>());             // Collect Views
            foreach (var v in Views) {Index.Add(v.GetType(), v); }         // Update Index

            this.Services.AddRange(AllComponents.OfType<Service>());       // Collect Views
            foreach (var s in Services) {Index.Add(s.GetType(), s); }      // Update Index


            // Services are global and comon to app domains not bo buisness domain. they earned a shortcut :)
            dataService = Index[typeof(DataService)] as DataService;
        }

        // API
            
        public static T GetController<T>() where T : Controller
        {
            var item = Instance.Index[typeof(T)] as T;

            if (item == null)
                Logger.LogError($"Link : no Controller of type '{typeof(T).Name}' found");

            return item;
        }

        public static T GetView<T>() where T : View
        {
            var item = Instance.Index[typeof(T)] as T;

            if (item == null)
                Logger.LogError($"Link : no View of type '{typeof(T).Name}' found");

            return item;
        }

        public static T GetData<T>() where T : Model
        {
            var item = Instance.dataService.GetModel<T>();

            if (item == null)
                Logger.LogError($"Link : no Model of type '{typeof(T).Name}' found");

            return item;
        }

        // GLobals

        public static AppSettings AppSettings => Instance.AppGlobals.AppSettings;

        // Services API

        public static DataService Data => Instance.dataService;
    } 
}
