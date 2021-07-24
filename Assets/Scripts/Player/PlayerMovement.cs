using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    public float speed = 3;
    [SerializeField]
    public float jumpHeight = 10;
    [SerializeField]
    private LayerMask jumpableGround;

    private bool _wasDoubleJump = false;
    private bool _isFreeze = false;

    private SpriteRenderer _sprite;
    private Rigidbody2D _rigidBody2D;
    private BoxCollider2D _boxCollider2D;
    private Animator _animator;

    private enum MovementState { Idle, Running, Jumping, Falling }

    // Start is called before the first frame update
    void Start()
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _sprite = GetComponent<SpriteRenderer>();
        _boxCollider2D = GetComponent<BoxCollider2D>();        
    }

    // Update is called once per frame
    void Update()
    {
        float inputX = Input.GetAxisRaw("Horizontal");

        ProcessAnimationChange(inputX);
        CalculateMovementAndJumping(inputX);
        
    }

    public void FreezePlayer(bool isFreeze)
    {
        this._isFreeze = isFreeze;
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

        if(!IsGrounded())
        {
            state = MovementState.Jumping;
         
        }

        if(_rigidBody2D.velocity.y < -.1f)
        {
            state = MovementState.Falling;
        }

        _animator.SetInteger("MovementState", (int)state);
    }

    private void CalculateMovementAndJumping(float inputX)
    {
        if(!_isFreeze)
        {
            transform.Translate(new Vector2(speed * inputX * Time.deltaTime, 0));
        }
        //_rigidBody2D.velocity = new Vector2(speed * inputX, _rigidBody2D.velocity.y);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Space enter!");

            if (!IsGrounded() && !_wasDoubleJump)
            {
                _rigidBody2D.AddForce(new Vector2(0, jumpHeight), ForceMode2D.Impulse);
                _wasDoubleJump = true;
                _animator.SetTrigger("DoubleJump");
            }

            if (IsGrounded())
            {
                _rigidBody2D.AddForce(new Vector2(0, jumpHeight), ForceMode2D.Impulse);
                _wasDoubleJump = false;
            }
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(_boxCollider2D.bounds.center, _boxCollider2D.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }


    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.tag == "Terrain") // GameObject is a type, gameObject is the property
    //    {
    //        Debug.Log("collision with terrain!");
    //        _isJumping = false;
    //        _wasDoubleJump = false;
    //    }
    //}
}
