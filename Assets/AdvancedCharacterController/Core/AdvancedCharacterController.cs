using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AdvancedCharacterController.Core
{
    /// <summary>
    /// This is a more advanced and capable character controller for Unity3D. It is designed to replace the current character controller that is packaged in Unity.
    /// </summary>
    public class AdvancedCharacterController : MonoBehaviour
    {
        // Components that are apart of the AdvancedCharacterController - Capsule Collider and Rigidbody

        #region Components
        private List<CapsuleCollider> _capsuleColliders = new List<CapsuleCollider>();
        private List<Rigidbody> _rigidbodies = new List<Rigidbody>();

        /// <summary>
        /// The main collider for the AdvancedCharacterController
        /// </summary>
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

        /// <summary>
        /// The Rigidbody for the AdvancedCharacterController
        /// </summary>
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

        /// <summary>
        /// Get the Collider from this GameObject, or add it if one doesn't exist
        /// </summary>
        private void GetCollider()
        {
            _capsuleColliders.Clear();

            _capsuleColliders = gameObject.GetComponents<CapsuleCollider>().ToList();

            if (_capsuleColliders.Count == 0)
            {
                CapsuleCollider collider = gameObject.AddComponent<CapsuleCollider>();
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

        /// <summary>
        /// Get the Rigidbody from this GameObject, or add it if one doesn't exist
        /// </summary>
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

        // Values to control the Collider of the AdvancedCharacterController

        #region ColliderValues
        /// <summary>
        /// Radius of AdvancedCharacterController Collider
        /// </summary>
        public float Radius { get; private set; } = 0.5f;

        /// <summary>
        /// Height of AdvancedCharacterController Collider
        /// </summary>
        public float Height { get; private set; } = 2f;

        /// <summary>
        /// Center of AdvancedCharacterController Collider
        /// </summary>
        public Vector3 Center { get; private set; } = Vector3.up;

        /// <summary>
        /// Returns the mid-point position of the collider
        /// </summary>
        public Vector3 ColliderMidPos =>
            new Vector3(transform.position.x, transform.position.y + Height / 2f, transform.position.z);

        /// <summary>
        /// Set the Radius of the AdvancedCharacterController Collider
        /// </summary>
        /// <param name="radius"></param>
        public void SetColliderRadius(float radius)
        {
            if (Height <= 0)
            {
                radius = 0;
            }
            else
            {
                radius = Mathf.Clamp(radius, 0, Height / 2f);
            }

            if (radius.Equals(Radius))
            {
                return;
            }

            Radius = radius;
            UpdateColliderValues();
        }

        /// <summary>
        /// Set the Height of the AdvancedCharacterController Collider
        /// </summary>
        /// <param name="height"></param>
        public void SetColliderHeight(float height)
        {
            height = Mathf.Clamp(height, 0, Mathf.Infinity);

            if (height.Equals(Height))
            {
                return;
            }

            Height = height;
            UpdateColliderValues();
        }

        /// <summary>
        /// Set the Center of the AdvancedCharacterController Collider
        /// </summary>
        /// <param name="center"></param>
        public void SetColliderCenter(Vector3 center)
        {
            if (center.Equals(Center))
            {
                return;
            }

            Center = center;
            UpdateColliderValues();
        }

        private void UpdateColliderValues()
        {
            if (!Application.isPlaying) return;

            Collider.radius = Radius;
            Collider.height = Height;
            Collider.center = Center;

            if (Collider.material == null)
            {
                Collider.material = NoFrictionPhysicMaterial();
            }

            if (!ignoreColliders.Contains(Collider))
            {
                ignoreColliders.Add(Collider);
            }
        }

        private PhysicMaterial NoFrictionPhysicMaterial()
        {
            return new PhysicMaterial("No Friction") {dynamicFriction = 0, staticFriction = 0, bounciness = 0};
        }
        #endregion

        // Values to control the Rigidbody of the AdvancedCharacterController

        #region RigidbodyValues
        /// <summary>
        /// Mass of the AdvancedCharacterController
        /// </summary>
        public float Mass { get; private set; } = 1f;

        /// <summary>
        /// Drag of the AdvancedCharacterController
        /// </summary>
        public float Drag { get; private set; } = 0f;

        /// <summary>
        /// Angular Drag of the AdvancedCharacterController
        /// </summary>
        public float AngularDrag { get; private set; } = 0.05f;

        public bool autoApplyGravity { get; set; } = true;
        public float gravityForce = 30f;
        public float stickToGroundForce = 10f;

        /// <summary>
        /// Set the Mass of the AdvancedCharacterController Rigidbody
        /// </summary>
        /// <param name="mass"></param>
        public void SetRigidbodyMass(float mass)
        {
            mass = Mathf.Clamp(mass, 0, Mathf.Infinity);

            if (mass.Equals(Mass))
            {
                return;
            }

            Mass = mass;
            UpdateRigidbodyValues();
        }

        /// <summary>
        /// Set the Drag of the AdvancedCharacterController Rigidbody
        /// </summary>
        /// <param name="drag"></param>
        public void SetRigidbodyDrag(float drag)
        {
            drag = Mathf.Clamp(drag, 0, Mathf.Infinity);

            if (drag.Equals(Drag))
            {
                return;
            }

            Drag = drag;
            UpdateRigidbodyValues();
        }

        /// <summary>
        /// Set the AngularDrag of the AdvancedCharacterController Rigidbody
        /// </summary>
        /// <param name="angularDrag"></param>
        public void SetRigidbodyAngularDrag(float angularDrag)
        {
            angularDrag = Mathf.Clamp(angularDrag, 0, Mathf.Infinity);

            if (angularDrag.Equals(AngularDrag))
            {
                return;
            }

            AngularDrag = angularDrag;
            UpdateRigidbodyValues();
        }

        private void UpdateRigidbodyValues()
        {
            if (!Application.isPlaying) return;

            Rigidbody.mass = Mass;
            Rigidbody.drag = Drag;
            Rigidbody.angularDrag = AngularDrag;
            Rigidbody.useGravity = false;
            Rigidbody.isKinematic = false;
            Rigidbody.freezeRotation = true;
        }
        #endregion

        // Values for checking for the ground

        #region GroundedProperties
        public float sphereCastRadius = 0.5f;
        [Range(0f, 1f)] public float sphereCastDepth = 0.03f;
        public LayerMask layerMask = Physics.AllLayers;
        public List<Collider> ignoreColliders = new List<Collider>();

        public Vector3 SphereCastPos =>
            new Vector3(ColliderMidPos.x, ColliderMidPos.y - SphereCastDistance, ColliderMidPos.z);

        private float SphereCastDistance => Height / 2f + sphereCastRadius - (1f - sphereCastDepth);

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

        // Moving the AdvancedCharacterController

        #region Move
        [Range(0, 90)] public float slopeLimit = 30f;

        private Vector3 _moveVector = Vector3.zero;

        /// <summary>
        /// Move the character controller in the direction of the "moveVector". Call from FixedUpdate()
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
            if (!autoApplyGravity) return;

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
}