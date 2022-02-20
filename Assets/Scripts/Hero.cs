using System;
using System.Collections;
using System.Collections.Generic;
using Components;
using UnityEngine;
using UnityEngine.InputSystem;

public class Hero : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpImpulse;
    [SerializeField] private float _damageJumpImpulse;
    [SerializeField] private float _setRadiusForInteract;
    [SerializeField] private LayerCheck _layerCheck;
    [SerializeField] private LayerMask _layerInteract;

    [SerializeField] private CreateDustParticles _createParticles;
    [SerializeField] private ParticleSystem _particleCoins;

    private Vector2 _derection;
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
        float directionX = _derection.x * _speed;
        float directionY = SetDirectionY();
        _rigidbody2D.velocity = new Vector2(directionX,directionY);
        
        _animator.SetBool(isRunning, _derection.x != 0);
        _animator.SetBool(isGround,_layerCheck.IsGround);
        _animator.SetFloat(verticalVelocity,_rigidbody2D.velocity.y);
        
        SetDirectionHero();
    }

    private float SetDirectionY()
    {
        float yVelocity = _rigidbody2D.velocity.y;
        
        if (_derection.y > 0)
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
             yVelocity += _jumpImpulse;
        }
        else if (isDoubleJump && !_layerCheck.IsGround && _rigidbody2D.velocity.y < 0f)
        {
            isDoubleJump = false;
            yVelocity = _jumpImpulse;
        }

        return yVelocity;
    }


    private void SetDirectionHero()
    {
        if (_derection.x > 0)
        {
            transform.localScale = Vector3.one;
        }
        else if(_derection.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
    private void OnHorizontalVertical(InputValue context)
    {
        _derection = context.Get<Vector2>();
    }

    public void OnPressF(InputValue context)
    {
        if (context.isPressed)
        {
            var countobj = Physics2D.OverlapCircleNonAlloc(transform.position, _setRadiusForInteract,
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
        _rigidbody2D.velocity = Vector2.up * _damageJumpImpulse;
        _animator.SetTrigger(isHit);
        if (MoneyData.Money >= 5)
        {
            _particleCoins.gameObject.SetActive(true);
            _particleCoins.Play();
        }
    }

    public void SpawnDust()
    {
        _createParticles.SpawnDust();
    }
}
