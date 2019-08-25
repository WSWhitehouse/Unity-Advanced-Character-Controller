using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AdvancedCharacterController : MonoBehaviour
{
    #region Components

    private List<CapsuleCollider> _capsuleColliders = new List<CapsuleCollider>();
    private List<Rigidbody> _rigidbodies = new List<Rigidbody>();

    public CapsuleCollider Collider
    {
        get
        {
            if (_capsuleColliders.Count <= 0)
            {
                GetCollider();
            }

            return _capsuleColliders[0];
        }
    }

    public Rigidbody Rigidbody
    {
        get
        {
            if (_rigidbodies.Count <= 0)
            {
                GetRigidbody();
            }

            return _rigidbodies[0];
        }
    }

    private void GetCollider()
    {
        _capsuleColliders.Clear();

        _capsuleColliders = gameObject.GetComponents<CapsuleCollider>().ToList();

        if (_capsuleColliders.Count == 0)
        {
            var collider = gameObject.AddComponent<CapsuleCollider>();
            _capsuleColliders.Add(collider);
        }

        if (_capsuleColliders.Count > 1)
        {
            Debug.LogError(
                $"[AdvancedCharacterController] Cannot have any colliders on {gameObject.name}, please remove them!");
        }

        _capsuleColliders[0].hideFlags = HideFlags.HideInInspector;
        UpdateColliderValues();
    }

    private void GetRigidbody()
    {
        _rigidbodies.Clear();
        _rigidbodies = gameObject.GetComponents<Rigidbody>().ToList();

        if (_rigidbodies.Count == 0)
        {
            var rb = gameObject.AddComponent<Rigidbody>();
            _rigidbodies.Add(rb);
        }

        if (_rigidbodies.Count > 1)
        {
            for (int i = _rigidbodies.Count - 1; i > 1; i--)
            {
                Destroy(_rigidbodies[i]);
                _rigidbodies.RemoveAt(i);
            }
        }

        _rigidbodies[0].hideFlags = HideFlags.HideInInspector;
        UpdateRigidbodyValues();
    }

    #endregion

    #region ColliderValues

    public float radius = 0.5f;
    public float height = 2f;
    public Vector3 center = Vector3.up;

    public Vector3 ColliderMidPos =>
        new Vector3(transform.position.x, transform.position.y + height / 2f, transform.position.z);

    public void SetColliderRadius(float radius)
    {
        if (radius == this.radius)
        {
            return;
        }

        this.radius = radius;
        UpdateColliderValues();
    }

    public void SetColliderHeight(float height)
    {
        if (height == this.height)
        {
            return;
        }

        this.height = height;
        UpdateColliderValues();
    }

    public void SetColliderCenter(Vector3 center)
    {
        if (center.Equals(this.center))
        {
            return;
        }

        this.center = center;
        UpdateColliderValues();
    }

    private void UpdateColliderValues()
    {
        if (!Application.isPlaying) return;

        Collider.radius = radius;
        Collider.height = height;
        Collider.center = center;

        if (Collider.material == null)
        {
            Collider.material = SetUpPhysicMaterial();
        }

        if (!ignoreColliders.Contains(Collider))
        {
            ignoreColliders.Add(Collider);
        }
    }

    private PhysicMaterial SetUpPhysicMaterial()
    {
        return new PhysicMaterial("No Friction") {dynamicFriction = 0, staticFriction = 0, bounciness = 0};
    }

    #endregion

    #region RigidbodyValues

    public float mass = 1f;
    public float drag = 0f;
    public float angularDrag = 0.05f;
    public bool autoApplyGravity = true;
    public float gravityForce = 30f;
    public float stickToGroundForce = 10f;

    public void SetRigidbodyMass(float mass)
    {
        if (mass == this.mass)
        {
            return;
        }

        this.mass = mass;
        UpdateRigidbodyValues();
    }

    public void SetRigidbodyDrag(float drag)
    {
        if (drag == this.drag)
        {
            return;
        }

        this.drag = drag;
        UpdateRigidbodyValues();
    }

    public void SetRigidbodyAngularDrag(float angularDrag)
    {
        if (angularDrag == this.angularDrag)
        {
            return;
        }

        this.angularDrag = angularDrag;
        UpdateRigidbodyValues();
    }

    private void UpdateRigidbodyValues()
    {
        if (!Application.isPlaying) return;

        Rigidbody.mass = mass;
        Rigidbody.drag = drag;
        Rigidbody.angularDrag = angularDrag;
        Rigidbody.useGravity = false;
        Rigidbody.isKinematic = false;
        Rigidbody.freezeRotation = true;
    }

    #endregion

    #region GroundedProperties

    public float sphereCastRadius = 0.5f;
    [Range(0f, 1f)] public float sphereCastDepth = 0.03f;
    public LayerMask layerMask = Physics.AllLayers;
    public List<Collider> ignoreColliders = new List<Collider>();

    public Vector3 SphereCastPos =>
        new Vector3(ColliderMidPos.x, ColliderMidPos.y - SphereCastDistance, ColliderMidPos.z);

    private float SphereCastDistance => height / 2f + sphereCastRadius - (1f - sphereCastDepth);

    private RaycastHit _groundHit;
    public bool IsGrounded { get; private set; }

    public bool OnSlope { get; private set; }

    private void CheckForGrounded()
    {
        bool hasDetectedHit = Physics.SphereCast(ColliderMidPos, sphereCastRadius, -transform.up, out _groundHit,
            SphereCastDistance, layerMask, QueryTriggerInteraction.Ignore);

        if (!hasDetectedHit)
        {
            IsGrounded = false;
            return;
        }

        if (ignoreColliders.Contains(_groundHit.collider))
        {
            IsGrounded = false;
            return;
        }

        if (Vector3.Angle(_groundHit.normal, transform.up) < slopeLimit)
        {
            OnSlope = true;
        }

        IsGrounded = true;
    }

    #endregion

    #region Move

    [Range(0, 90)] public float slopeLimit = 30f;

    private Vector3 _moveVector = Vector3.zero;

    /// <summary>
    /// Move the character controller in the direction of the "moveVector"
    /// </summary>
    /// <param name="moveVector"></param>
    public void Move(Vector3 moveVector)
    {
        _moveVector = moveVector;
    }

    private void FixedUpdate()
    {
        CheckForGrounded();

        UpdateMoveVector();

        Rigidbody.velocity = _moveVector;
    }

    private void UpdateMoveVector()
    {
        if (IsGrounded)
        {
            _moveVector.y += -stickToGroundForce;
        }
        else
        {
            _moveVector.y += gravityForce * Time.fixedDeltaTime * Physics.gravity.y;
        }

        if (OnSlope)
        {
            // TODO Add code to manipulate character controller when on slope 
        }
    }

    #endregion

    private void Awake()
    {
        GetCollider();
        GetRigidbody();
    }
}