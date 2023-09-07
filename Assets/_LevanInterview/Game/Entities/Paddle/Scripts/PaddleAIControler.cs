using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using LevanInterview.Models;
using UnityEngine;
using System;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace LevanInterview
{
    public class PaddleAIControler : MonoBehaviour
    {
        //////////////////// Types

        enum AIState
        {
            Rest,
            Defence,
            Attack
        }

        //////////////////// Members

        [Header("Defence")]
        public Transform DefencePosition;
        public float     DefenceMovementSpeed     = 8;

        [Header("Attack")]
        public float AttackMovementSpeed          = 10;
        public float AttackCoolDownSeconds        = 0.5f;
        public float NewRoundCoolDownSeconds      = 1.5f;
        public float AttackRadious                = 10;

        [Header("target")]
        public Transform Puck;

        // private
        [SerializeField] private AIState State;

        // cache
        Vector3     target;
        Rigidbody   rb;
        Vector3     InitialPosition;
        float       timeOfLastEnable;
        float       timeOfLastAttack;

    
        //////////////////// Lifecycle Events

        private void Awake()
        {
            this.rb              = GetComponent<Rigidbody>();
            this.InitialPosition = transform.position;
        }

    
        private void OnDisable()
        {
            // restore initial values
            this.transform.position = this.InitialPosition;
            this.rb.velocity        = Vector3.zero;
        }

        private void OnEnable()
        {
            State = AIState.Rest;
            this.timeOfLastEnable = Time.realtimeSinceStartup;
        }

        private void FixedUpdate()
        {
            // Did we just start a new round ?
            if (Time.realtimeSinceStartup - timeOfLastEnable < NewRoundCoolDownSeconds)
                return;
            else if (State == AIState.Rest)
                State = AIState.Attack;

            var distanceToTarget = (Puck.position - transform.position).magnitude;

            if (State ==  AIState.Attack
            &&  distanceToTarget < AttackRadious)
            {
                this.target          = Puck.position;
                Vector3 pathToTarget = (target - transform.position);
                rb.velocity          = AttackMovementSpeed * pathToTarget;
            
            }
            else if (State == AIState.Defence)
            {
                this.target          = DefencePosition.position;
                Vector3 pathToTarget = (target - transform.position);
                rb.velocity          = DefenceMovementSpeed * pathToTarget;
                
                // Did we just finished recovering after out attack ?
                if (Time.realtimeSinceStartup - timeOfLastAttack > AttackCoolDownSeconds)
                {
                    Logger.Trace("AI is done waiting. Attack !");
                    State = AIState.Attack;
                }
            }
        }

        //////////////////// Physics Events
    
        public void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag(Puck.tag))
            {
                // we attacked, now go defend
                State = AIState.Defence;
                timeOfLastAttack = Time.realtimeSinceStartup;
            }
        }

        //////////////////// Editor Debug helper

        #if UNITY_EDITOR

        [CustomEditor(typeof(PaddleAIControler))]
        public class ExampleEditor : Editor
        {
            public void OnSceneGUI()
            {
                var t = target as PaddleAIControler;

                var color = Color.red;
                Handles.color = color;
                Handles.DrawWireDisc(t.transform.position, t.transform.up, t.AttackRadious, 1);
            }
        }

        #endif
    }

}
