using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    const string ANIM_MOVE_SPEED = "MoveSpeed";
    const string ANIM_IS_GROUNDED = "IsGrounded";
    const string ANIM_IS_DEAD = "IsDead";
    [SerializeField] public GameObject alivePlayer;
    [SerializeField] public GameObject deadPlayer;
    Animator _aliveAnimator;
    Animator _deadAnimator;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] Transform groundPoint;
    [SerializeField] float moveSpeed;
    [SerializeField] float jumpForce;
    Rigidbody2D _rigidBody;
    SpriteRenderer _spriteRenderer;
    float _inputMovement;
    bool _isGrounded;
    bool _isJumping;
    bool _isDead = false;
    bool _canMove = true;
    // Start is called before the first frame update
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _aliveAnimator = alivePlayer.GetComponent<Animator>();
        _deadAnimator = deadPlayer.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if(!_canMove) return;
        _isGrounded = Physics2D.OverlapCircle(groundPoint.position, 0.2f, groundLayer);
        if (_isGrounded) {
            _isJumping = false;
        }
        _rigidBody.velocity = new Vector2(_inputMovement * moveSpeed, _rigidBody.velocity.y);
        FacePlayerTowardsMovement();
        SetMovementAnimation();
        SetJumpAnimation();
    }

    private void SetJumpAnimation()
    {
        if (_isDead)
        {
            _deadAnimator.SetBool(ANIM_IS_GROUNDED, _isGrounded);
        }
        else
        {
            _aliveAnimator.SetBool(ANIM_IS_GROUNDED, _isGrounded);
        }
    }

    private void SetMovementAnimation()
    {
        if (_isDead)
        {
            _deadAnimator.SetFloat(ANIM_MOVE_SPEED, Mathf.Abs(_inputMovement));
        }
        else
        {
            _aliveAnimator.SetFloat(ANIM_MOVE_SPEED, Mathf.Abs(_inputMovement));
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        _inputMovement = context.ReadValue<Vector2>().x;
    }

    private void FacePlayerTowardsMovement()
    {
        if (_rigidBody.velocity.x < 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (_rigidBody.velocity.x > 0)
        {
            transform.localScale = Vector3.one;
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if(_isGrounded && context.performed)
        {
            _isJumping = true;
            _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, jumpForce);
        }
    }

    public void Death()
    {
        StartCoroutine(TransitionToDead());
        _isDead = true;
    }

    IEnumerator TransitionToDead()
    {
        _canMove = false;
        _aliveAnimator.SetBool(ANIM_IS_DEAD, true);
        yield return new WaitForSeconds(1f);
        alivePlayer.SetActive(false);
        deadPlayer.SetActive(true);
        _canMove = true;
    }

    public void IsFalling()
    {
        _canMove = false;
        _aliveAnimator.SetBool(ANIM_IS_GROUNDED, false); //set jump animation
        _rigidBody.velocity = Vector2.zero; //stop player from moving
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Platform"))
        {
            transform.parent = other.transform;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Platform"))
        {
            transform.parent = null;
        }
    }
}
