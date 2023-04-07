using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(BoxCollider2D))]
public class PlayerPhysics : MonoBehaviour
{
    private BoxCollider2D   _collider;
    private Vector3         _size;
    private Vector3         _center;
    private Vector3         _originalSize;
    private Vector3         _originalCenter;
    private float           _colliderScale;

    private int             _collisionDivisionsX = 3;
    private int             _collisionDivisionsY = 10;

    private Ray2D           _ray;
    private RaycastHit2D    _hit;

    [SerializeField]
    private LayerMask       _collisionMask = ~0;

    private float skin      = .005f; // Creates a small space between player and ground

    public bool IsGrounded          { get; private set; }
    public bool HasMovementStopped  { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        _collider = GetComponent<BoxCollider2D>();
        _colliderScale = transform.localScale.x;
        _originalSize = _collider.size;
        _originalCenter = _collider.offset;
        SetCollider(_originalSize, _originalCenter);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Move(Vector2 moveAmount)
    {
        float deltaX = moveAmount.x;
        float deltaY = moveAmount.y;

        Vector2 playerPosition = transform.position;
        
        //Check collisions above and below
        IsGrounded = false;

        for (int i = 0; i < _collisionDivisionsX; i++)
        {
            float direction = Mathf.Sign(deltaY);
            float x = (playerPosition.x + _center.x - _size.x / 2) + _size.x / (_collisionDivisionsX - 1) * i; // Left, center then rightmost point of collider
            float y = playerPosition.y + _center.y + _size.y / 2 * direction; // Bottom of collider
      
            _ray = new Ray2D(new Vector2(x, y), new Vector2(0.0f, direction));
            _hit = Physics2D.Raycast(_ray.origin, _ray.direction, Mathf.Abs(deltaY) + skin, _collisionMask);
            Debug.DrawRay(_ray.origin, _ray.direction, Color.blue);

            if (_hit && _hit.collider)
            {
                //Get distance between player and ground
                float distance = Vector3.Distance(_ray.origin, _hit.point);

                //Stop players downwards movement after coming within skin width of a collider
                if (distance > skin)
                {
                    deltaY = distance * direction - skin * direction;
                }
                else
                {
                    deltaY = 0.0f; // If already within skin width, don't move player at all
                }
                IsGrounded = true;
                break; // If raycast hit the ground then break
            }
        }

        //Check collisions left and right
        HasMovementStopped = false;
        for (int i = 0; i < _collisionDivisionsY; i++)
        {
            float direction = Mathf.Sign(deltaX);
            float x = playerPosition.x + _center.x + _size.x / 2 * direction; // Left, center then rightmost point of collider
            float y = playerPosition.y + _center.y - _size.y / 2 + _size.y / (_collisionDivisionsY-1) * i; // Bottom of collider

            _ray = new Ray2D(new Vector2(x, y), new Vector2(direction, 0.0f));
            _hit = Physics2D.Raycast(_ray.origin, _ray.direction, Mathf.Abs(deltaX) + skin, _collisionMask);
            Debug.DrawRay(_ray.origin, _ray.direction, Color.red);

            if (_hit && _hit.collider)
            {
                //Get distance between player and ground
                float distance = Vector3.Distance(_ray.origin, _hit.point);

                //Stop players downwards movement after coming within skin width of a collider
                if (distance > skin)
                {
                    deltaX = distance * direction - skin * direction;
                }
                else
                {
                    deltaX = 0.0f; // If already within skin width, don't move player at all
                }
                HasMovementStopped = true;
                break; // If raycast hit the ground then break
            }
        }

        // Check for diagonal collisions
        if (!IsGrounded && !HasMovementStopped)
        {
            Vector3 playerDirection = new Vector3(deltaX, deltaY);
            Vector3 origin = new Vector3(playerPosition.x + _center.x + _size.x / 2 * Mathf.Sign(deltaX), playerPosition.y + _center.y + _size.y / 2 * Mathf.Sign(deltaY));

            _ray = new Ray2D(origin, playerDirection.normalized);
            _hit = Physics2D.Raycast(_ray.origin, _ray.direction, Mathf.Sqrt(deltaX * deltaX + deltaY * deltaY), _collisionMask);
            if (_hit && _hit.collider)
            {
                IsGrounded = true;
                deltaY = 0.0f;
            }
            Debug.DrawRay(origin, playerDirection.normalized, Color.cyan);
        }
        
        Vector2 finalTransform = new Vector2(deltaX, deltaY);
        transform.Translate(finalTransform, Space.World);
    }

    private void SetCollider (Vector3 size, Vector3 center)
    {
        _collider.size = size;
        _collider.offset = center;

        _size = size * _colliderScale;
        _center = center * _colliderScale;
    }

}
