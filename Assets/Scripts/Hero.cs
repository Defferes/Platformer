using System;
using System.Collections;
using System.Collections.Generic;
using Components;
using UnityEngine;
using UnityEngine.InputSystem;

public class Hero : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    [SerializeField] private float JumpImpulse = 12f;
    [SerializeField] private float damageJumpImpulse;
    [SerializeField] private float setRadiusForInteract;
    [SerializeField] private LayerCheck _layerCheck;
    [SerializeField] private LayerMask _layerInteract;

    private Vector2 direction;
    private Collider2D[] _collider2DForInteract = new Collider2D[1];
    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private bool isDoubleJump = true;

    private static readonly int isGround = Animator.StringToHash("isGround");
    private static readonly int isRunning = Animator.StringToHash("isRunning");
    private static readonly int verticalVelocity = Animator.StringToHash("verticalVelocity");
    private static readonly int isHit = Animator.StringToHash("isHit");

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }
    
    private void FixedUpdate()
    {
        float directionX = direction.x * speed;
        float directionY = SetDirectionY();
        _rigidbody2D.velocity = new Vector2(directionX,directionY);
        
        _animator.SetBool(isRunning, direction.x != 0);
        _animator.SetBool(isGround,_layerCheck.IsGround);
        _animator.SetFloat(verticalVelocity,_rigidbody2D.velocity.y);
        
        SetDirectionHero();
    }

    private float SetDirectionY()
    {
        float yVelocity = _rigidbody2D.velocity.y;
        
        if (direction.y > 0)
        {
            yVelocity = Jumping(ref yVelocity);
        }
        else if (_rigidbody2D.velocity.y > 0)
        {
             yVelocity *= 0.5f;
        }
        return yVelocity;
    }

    private float Jumping(ref float yVelocity)
    {
        if (_layerCheck.IsGround) isDoubleJump = true;
        if (_layerCheck.IsGround && _rigidbody2D.velocity.y <= 0f)
        {
             yVelocity += JumpImpulse;
        }
        else if (isDoubleJump && !_layerCheck.IsGround && _rigidbody2D.velocity.y < 0f)
        {
            isDoubleJump = false;
            yVelocity = JumpImpulse;
        }

        return yVelocity;
    }


    private void SetDirectionHero()
    {
        if (direction.x > 0)
        {
            _spriteRenderer.flipX = false;
        }
        else if(direction.x < 0)
        {
            _spriteRenderer.flipX = true;
        }
    }
    private void OnHorizontalVertical(InputValue context)
    {
        direction = context.Get<Vector2>();
    }

    public void OnPressF(InputValue context)
    {
        if (context.isPressed)
        {
            var countobj = Physics2D.OverlapCircleNonAlloc(transform.position, setRadiusForInteract,
                _collider2DForInteract, _layerInteract);
            for (int i = 0; i < countobj; i++)
            {
                var interactabl = _collider2DForInteract[i].GetComponent<InteractionComponent>();
                if (interactabl != null)
                {
                    interactabl.Interact();
                }
            }
        }
    }

    public void ApplyDamage()
    {
        _rigidbody2D.velocity = Vector2.up * damageJumpImpulse;
        _animator.SetTrigger(isHit);
    }
}
