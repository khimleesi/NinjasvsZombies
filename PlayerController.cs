using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(PlayerPhysics), typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    private float _gravity       = 20.0f;
    private float _walkSpeed     = 8.0f;
    private float _runSpeed      = 12.0f;
    private float _acceleration  = 30.0f;
    private float _jumpHeight    = 12.0f;
    private float _deadZone      = -10.0f;
    private float _winZone       = 145.0f;

    private float           _currentSpeed;
    private float           _targetSpeed;
    private Vector2         _moveAmount;
    private PlayerPhysics   _physics;
    private Animator        _animator;
    private float           _animationSpeed;
    private bool            _isJumping;

    // Start is called before the first frame update
    void Start()
    {
        _physics    = GetComponent<PlayerPhysics>();
        _animator   = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Reset acceleration upon collision
        if (_physics.HasMovementStopped)
        {
            _targetSpeed = 0.0f;
            _currentSpeed = 0.0f;
        }
    
        // If player is touching the ground
        if (_physics.IsGrounded)
        {
            _moveAmount.y = 0.0f;
            _isJumping = false;
            _animator.SetBool("IsJumping", _isJumping);
          
            // Jump Input
            if (Input.GetButtonDown("Jump"))
            {
                _moveAmount.y = _jumpHeight;
                _isJumping = true;
                _animator.SetBool("IsJumping", _isJumping);
            }
        }

        Animate();
        GetInput();
        Move();
        SetDirection();
    }

    private void Animate()
    {
        // Animation
        _animationSpeed = Tools.IncrementTowards(_animationSpeed, Mathf.Abs(_targetSpeed), _acceleration);
        _animator.SetFloat("Speed", _animationSpeed);
    }

    private void Move()
    {
        // Set amount to move
        _moveAmount.x = _currentSpeed;
        _moveAmount.y -= _gravity * Time.deltaTime;
        _physics.Move(_moveAmount * Time.deltaTime);
        
        if (transform.position.y <= _deadZone)
        {
            SceneManager.LoadScene(2);
        }

        if (transform.position.x >= _winZone)
        {
            SceneManager.LoadScene(4);
        }
    }

    private void GetInput()
    {
        // Input
        float speed = (Input.GetButton("Run")) ? _runSpeed : _walkSpeed;
        _targetSpeed = Input.GetAxisRaw("Horizontal") * speed;
       
        // Increment current speed of player towards target speed
        _currentSpeed = Tools.IncrementTowards(_currentSpeed, _targetSpeed, _acceleration);

    }

    private void SetDirection()
    {
        // Face direction
        float direction = Input.GetAxisRaw("Horizontal");
        if (direction != 0)
        {
            transform.eulerAngles = (direction > 0) ? Vector3.zero : Vector3.up * 180;
        }
    }

  
}

