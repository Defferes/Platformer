using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerCheck : MonoBehaviour
{
    [SerializeField] private LayerMask _layerMask;
    private Collider2D _collider2D;
    public bool IsGround;

    private void Start()
    {
        _collider2D = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        IsGround = _collider2D.IsTouchingLayers(_layerMask);
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        IsGround = _collider2D.IsTouchingLayers(_layerMask);
    }
}
