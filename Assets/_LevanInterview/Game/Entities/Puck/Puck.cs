using System.Collections;
using System.Collections.Generic;
using LevanInterview;
using UnibusEvent;
using UnityEngine;

namespace LevanInterview
{
    [SelectionBase]
    public class Puck : MonoBehaviour
    {
        //////////////////// Consts

        public const string TAG = "puck";

        //////////////////// Members

        public float     maxSpeed = 5f;
        public Transform CenterPoint;
        public AudioClip PuckHitSound;
        public float     smallWindForce = 0.01f;

        // cache
        Rigidbody   rigidbody;
        AudioSource audioSource;
        Vector3     InitialPosition;

        //////////////////// Lifecycle Events

        private void Awake()
        {
            rigidbody       = GetComponent<Rigidbody>();
            audioSource     = GetComponentInChildren<AudioSource>(includeInactive:true);
            InitialPosition = transform.position;
        }

        private void OnDisable()
        {
            this.GetComponentInChildren<TrailRenderer>().Clear();
        }

        public void OnEnable()
        {
            rigidbody.velocity = Vector3.zero;
            this.transform.position = InitialPosition;
        }

        private void FixedUpdate()
        {
            rigidbody.velocity = Vector3.ClampMagnitude(rigidbody.velocity, maxSpeed);

            // Air hockey tables have very small wind that moves the puck.
            // as a bonus - we always apply a very small force thwards the center to help the puck not get stuck.
            rigidbody.AddForce((CenterPoint.position - this.transform.position) * smallWindForce);
        }

        //////////////////// Physics Events

        private void OnCollisionEnter(Collision collision)
        {
            audioSource.clip = PuckHitSound;
            audioSource.Play();
        }
    }
}
