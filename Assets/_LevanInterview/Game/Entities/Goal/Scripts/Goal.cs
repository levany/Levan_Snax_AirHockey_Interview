using System.Collections;
using System.Collections.Generic;
using UnibusEvent;
using UnityEngine;

namespace LevanInterview
{
    public class Goal : MonoBehaviour
    {
        public string      GoalOwnerPlayerID;
        public BoxCollider Collider;

        public void Awake()
        {
            Collider = GetComponent<BoxCollider>();
        }

        public void OnTriggerEnter(Collider other)
        {
            Logger.Trace($"object : {other.attachedRigidbody.gameObject.name} Collided with Goal:{this.gameObject.name}");

            // if we collided with the puck
            if (other.CompareTag(Puck.TAG))
            {
                // Goal !!!
                Unibus.Dispatch(new Events.Game.Goal() { GoalOwnerPlayerID = this.GoalOwnerPlayerID } );
            }
        }
    }
}
