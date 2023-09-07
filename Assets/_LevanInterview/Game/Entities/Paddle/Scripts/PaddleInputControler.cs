using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;


#if UNITY_EDITOR
using UnityEditor;
#endif

public class PaddleInputControler : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    //////////////////////////////////////// Members

    private Vector3   pointerPosition;
    private bool      isInputDown;
    private Vector3   initialPosition;

    private GameObject movementTarget;
    private FixedJoint joint;

    private Rigidbody  Rigidbody;
    
    //////////////////////////////////////// Lifecycle Events

    private void Awake()
    {
        this.Rigidbody       = GetComponent<Rigidbody>();
        this.initialPosition = transform.position;
    }

    private void OnDisable()
    {
        isInputDown = false;
    }

    private void OnEnable()
    {
        transform.position = initialPosition;
        Rigidbody.velocity = Vector3.zero;

        if (movementTarget == null)
        {
            CreateMovementTarget();
        }
        else
        {
            RefreshConnectionToPhysicsJoint();
        }
    }

    private void OnDestroy()
    {
        DestroyMovementTarget();
    }

    //////////////////////////////////////// Pointer Drag Events

    public void OnBeginDrag(PointerEventData eventData)
    {
        isInputDown = true;

        if (movementTarget == null)
            CreateMovementTarget();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!isInputDown) return;

        // make sure mouse is in bounds
        var screenRect = new Rect(0,0,Screen.width, Screen.height);

        if (!screenRect.Contains(eventData.position))
            return;

        // calculate movement target position
        var worldPosition    = eventData.pointerCurrentRaycast.worldPosition;
        this.pointerPosition = new Vector3(worldPosition.x, transform.position.y, worldPosition.z);

        // set position in physics joint - it will get us there correctly taking all physics into account
        if (this.joint != null)
        {
            this.joint.transform.position = pointerPosition;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isInputDown = false;

        if (this.movementTarget != null)
            this.movementTarget.transform.position = this.transform.position;
    }

    //////////////////////////////////////// Methods
    
    public void CreateMovementTarget()
    {
        if (movementTarget != null)
            return;

        // Create and cacheGameobject
        movementTarget                     = new GameObject($"{this.gameObject.name}_movement_target");
        movementTarget.transform.parent    = transform.parent;
        movementTarget.transform.position  = transform.position;
        
        // setup physics behaviour
        var targetRB                       = movementTarget.AddComponent<Rigidbody>();
        targetRB.isKinematic               = true;
        targetRB.constraints               = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
        targetRB.useGravity                = false;

        // Setup the physics joint that will do the work
        joint                              = movementTarget.AddComponent<FixedJoint>();
        joint.connectedBody                = this.Rigidbody;
    }

    public void DestroyMovementTarget()
    {
        if (this.movementTarget != null)
        {
            Destroy(this.movementTarget);
            this.movementTarget = null;
        }
    }

    public void RefreshConnectionToPhysicsJoint()
    {
        this.movementTarget.transform.position = this.transform.position;
        this.movementTarget.GetComponent<Rigidbody>().WakeUp();
        this.movementTarget.GetComponent<FixedJoint>().connectedBody = this.GetComponent<Rigidbody>();
    }

    //////////////////////////////////////// Editor Debug helper

    #if UNITY_EDITOR

    /// <summary>
    /// Debug helper - draws a line in the scene view representing our movement target
    /// </summary>
    [CustomEditor(typeof(PaddleInputControler))]
    public class PaddleInputControlerEditor : Editor
    {
        public void OnSceneGUI()
        {
            var t          = target as PaddleInputControler;
            var moveTarget = t.movementTarget;

            if (moveTarget != null)
            {
                Handles.color = Color.white;

                // Physics joint position
                Handles.DrawWireDisc(moveTarget.transform.position, moveTarget.transform.up, 2);
            }
            
            // Pointer position
            Handles.DrawLine(t.pointerPosition, t.pointerPosition + Vector3.up * 5, 20);
        }
    }

    #endif
}
