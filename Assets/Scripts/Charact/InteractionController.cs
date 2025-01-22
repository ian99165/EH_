using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionController : MonoBehaviour
{
    [Header("Interaction Settings")]
    [SerializeField] private float _maxDistance = 10f;
    [SerializeField] private float _visionRadius = 1f;
    [SerializeField] private LayerMask _layerMask;
    
    [Header("Throw Setting")]
    [SerializeField]private Transform _target;

    public bool IsPickup { get; set; }
    
    private Vector3 _origin;
    private Vector3 _direction;
    private RaycastHit _hit;
    private Transform _item;

    private void Throw()
    {
        _item.position = _target.position;
        _item.GetComponent<Rigidbody>().useGravity = true;
        _item.gameObject.SetActive(true);
        _item = null;
    }

    private void Dropoff()
    {
        _item.GetComponent<Rigidbody>().useGravity = true;
        _item = null;
    }

    private void FixedUpdate()
    {
        float moveSpeed = 10f;
        if (_item != null)
        {
            Vector3 newPos = Vector3.Lerp(_item.position, _target.position,Time.fixedDeltaTime * moveSpeed);
            _item.GetComponent<Rigidbody>().MovePosition(newPos);
        }
    }

    private void Update()
    {
        _origin = transform.position;
        _direction = transform.forward;

        if(!IsPickup && _item != null) Dropoff();
        
        if (Physics.SphereCast(_origin, _visionRadius, _direction, out _hit, _maxDistance, _layerMask))
        {
            if (_hit.transform.TryGetComponent(out IInteractable item) && IsPickup)
            {
                _item = _hit.transform;
                item.Interact();
            }
        }
    }
    
    //debug
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(_origin, _direction * _hit.distance);
        Gizmos.DrawWireSphere(_origin + _direction * _hit.distance , _visionRadius);
    }
}
