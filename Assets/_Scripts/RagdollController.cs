using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollController : MonoBehaviour
{
    [SerializeField]private bool isRagdollActive = true;

    private Rigidbody[] _rigidbodies;
    private Collider[] _colliders;

    private void Start()
    {
        GetAllRigidbodies();
        GetAllColliders();

    }

    private void Update()
    {
        ActivateRagdoll(isRagdollActive);

    }

    private void GetAllRigidbodies()
    {
        _rigidbodies = GetComponentsInChildren<Rigidbody>(true);
    }
    private void GetAllColliders()
    {
        _colliders = GetComponentsInChildren<Collider>(true);
    }
    public void ActivateRagdoll(bool status)
    {
        isRagdollActive = status;
        if (status)
        {
            for (int i = 0; i < _rigidbodies.Length; i++)
                _rigidbodies[i].isKinematic = false;

            for (int i = 0; i < _colliders.Length; i++)
                _colliders[i].enabled = true;
        }
        else
        {
            for (int i = 0; i < _rigidbodies.Length; i++)
                _rigidbodies[i].isKinematic = true;

            for (int i = 0; i < _colliders.Length; i++)
                _colliders[i].enabled = false;
        }
    }


}
