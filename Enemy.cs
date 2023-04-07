using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    private Transform       _target;
    private Rigidbody2D     _rigidBody;
    private Animator        _animatorController;
    private Vector2         _velocity;
    private float           _speed = 8.0f;
    private float           _maxSpeed = 15.0f;
    private bool            _facingRight = true;
    private IEnemyState     _currentState;

    public float Speed
    {
        get { return _speed; }
        set { _speed = value; }
    }

    public Rigidbody2D RigidBody
    {
        get { return _rigidBody; }
        set { _rigidBody = value; }
    }

    public Animator AnimatorController
    {
        get { return _animatorController; }
        set { _animatorController = value; }
    }

    public float MaxSpeed
    {
        get { return _maxSpeed; }
        set { _maxSpeed = value; }
    }

    public Vector2 Velocity
    {
        get { return _velocity; }
        set { _velocity = value; }
    }

    public Transform Target
    {
        get { return _target; }
        set { _target = value; }
    }

    void Awake()
    {
        ChangeState(new IdleState());
    }

    void Start()
    {
        _animatorController = GetComponent<Animator>();
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (_target)
        {
            SetDirection();
        }
        _currentState.Update();
    }

    private void SetDirection()
    {
        if (_target.position.x > transform.position.x && !_facingRight)
        {
            Flip();
        }

        if (_target.position.x < transform.position.x && _facingRight)
        {
            Flip();
        }
    }

    private void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
        _facingRight = !_facingRight;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            SceneManager.LoadScene(2);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Target = collision.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
           Target = null;
        }
    }

    public void ChangeState(IEnemyState newState)
    {
        if (_currentState != null)
        {
            _currentState.Exit();
        }

        _currentState = newState;
        _currentState.Enter(this);
    }
}
