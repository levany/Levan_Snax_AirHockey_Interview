using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class Boundaries : MonoBehaviour
{
    public GameObject  Target;
    public BoxCollider Box;

    public void Awake()
    {
        this.Box = GetComponent<BoxCollider>();
    }

    private void LateUpdate()
    {
        if (Target == null)
            return;

        var poision = new Vector3(Mathf.Clamp(Target.transform.position.x, Box.bounds.min.x, Box.bounds.max.x)
                                 ,Target.transform.position.y
                                 ,Mathf.Clamp(Target.transform.position.z, Box.bounds.min.z, Box.bounds.max.z));

        Target.transform.position = poision;
        

    }
}
