using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    public float speed = 3;
    [SerializeField]
    public float jumpHeight = 10;

    private bool _isJumping = false; 
    private bool _wasDoubleJump = false; 

    private SpriteRenderer _sprite;
    private Rigidbody2D _rigidBody2D;
    private Animator _animator;

    private enum MovementState { Idle, Running, Jumping, Falling }

    // Start is called before the first frame update
    void Start()
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float inputX = Input.GetAxisRaw("Horizontal");

        ProcessAnimationChange(inputX);
        CalculateMovementAndJumping(inputX);
        
    }

    private void ProcessAnimationChange(float inputX)
    {
        MovementState state;

        if (inputX > 0f )
        {
            _sprite.flipX = false;
            state = MovementState.Running;
        } 
        else if (inputX < 0f) 
        {
            _sprite.flipX = true;
            state = MovementState.Running;
        }
        else
        {
            state = MovementState.Idle;
        }

        if(_isJumping)
        {
            state = MovementState.Jumping;
         
        }

        _animator.SetInteger("MovementState", (int)state);
    }

    private void CalculateMovementAndJumping(float inputX)
    {
        transform.Translate(new Vector2(speed * inputX * Time.deltaTime, 0));

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Space enter!");

            if (_isJumping && !_wasDoubleJump)
            {
                _rigidBody2D.AddForce(new Vector2(0, jumpHeight), ForceMode2D.Impulse);
                _wasDoubleJump = true;
                _animator.SetTrigger("DoubleJump");
            }

            if (!_isJumping)
            {
                _rigidBody2D.AddForce(new Vector2(0, jumpHeight), ForceMode2D.Impulse);
                _isJumping = true;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Terrain") // GameObject is a type, gameObject is the property
        {
            Debug.Log("collision with terrain!");
            _isJumping = false;
            _wasDoubleJump = false;
        }
    }
}
