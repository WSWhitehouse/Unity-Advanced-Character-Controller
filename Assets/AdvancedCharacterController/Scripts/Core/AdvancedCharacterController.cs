using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AdvancedCharacterController : MonoBehaviour
{
    // Components
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

    #region Components

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
        SetColliderValues();
    }

    private void GetRigidbody()
    {
        _rigidbodies.Clear();
        _rigidbodies = gameObject.GetComponents<Rigidbody>().ToList();

        if (_rigidbodies.Count == 0)
        {
            Debug.Log("rb");
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
        SetRigidbodyValues();
    }

    public void SetColliderValues()
    {
        // TODO Set collider values
    }

    public void SetRigidbodyValues()
    {
        // TODO Set rigidbody values
    }

    #endregion

    private void Awake()
    {
        GetCollider();
        GetRigidbody();
    }
}