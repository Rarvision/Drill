using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayerMovement : MonoBehaviour
{
    // Components
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private SpriteRenderer sprite;
    private Animator anim;

    // Basic movement parameters
    private float inputX = 0.0f;
    [SerializeField] float runSpeed = 10.0f;
    [SerializeField] float jumpVel = 5.0f;

    // Status
    private enum State {
        WATER, GAS, ICE
    }

    // State-based parameters
    private int stateNumber = 0;
    private bool canJump = true;
    private bool canSquat = true;
    private bool canSmash = false;
    private bool canSlide = false;
    // gravityScale -- rb.gravityScale;

    // Ground detection parameters
    [SerializeField] private LayerMask jumpableGround;
    
    // Audio parameters
    [SerializeField] private AudioSource jumpSoundEffect;

    // MovementStatus
    private enum MovementState {
        // TODO: UPDATE MOVEMENT STATES;
        idle, running, jumping, falling
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        inputX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(inputX * runSpeed, rb.velocity.y);

        if(Input.GetButtonDown("Jump") && IsGrounded() && canJump) 
        {
            Debug.Log("!");
            Jump();
        }

        UpdateAnimation();
    }

    // Update state number (+1 or -1) baesd on the trigger type
    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log(other.gameObject.name);
        if(other.gameObject.CompareTag("Heater")) {
            stateNumber++;
        } else if (other.gameObject.CompareTag("Cooler")) {
            stateNumber--;
        }
        UpdateState();
        Destroy(other.gameObject);
    }

    private void UpdateState() {
        if (stateNumber == (int)State.WATER)
        {
            canJump = true;
            canSquat = true;
            canSmash = false;
            canSlide = false;
            rb.gravityScale = 1;
        } else if (stateNumber == (int)State.GAS)
        {
            canJump = false;
            canSquat = false;
            canSmash = false;
            canSlide = false;
            rb.gravityScale = -0.6f;

        } else if (stateNumber == (int)State.ICE)
        {
            canJump = true;
            canSquat = false;
            canSmash = true;
            canSlide = true;
            rb.gravityScale = 1;
        }

        Debug.Log("canJump: " + canJump);
        Debug.Log("canSquat: " + canSquat);
        Debug.Log("canSmash: " + canSmash);
        Debug.Log("canSlide: " + canSlide);
        Debug.Log("gravity: " + rb.gravityScale);
    }

    private void UpdateAnimation() 
    {
        MovementState state;

        // TODO: UPDATE THIS PART ACCORDING TO THE NEW STATE MACHINE
        if(inputX < 0) {
            state = MovementState.running;
            sprite.flipX = true;
        } else if(inputX > 0) {
            state = MovementState.running;
            sprite.flipX = false;
        } else {
            state = MovementState.idle;
        }

        // TODO: UPDATE THIS PART ACCORDING TO THE NEW STATE MACHINE
        if(rb.velocity.y > .1f)
        {
            state = MovementState.jumping;
        }
        else if (rb.velocity.y < -.1f)
        {
            state = MovementState.falling;
        }

        // TODO: UPDATE THIS PART ACCORDING TO THE NEW STATE MACHINE
        anim.SetInteger("state", (int)state);
    }

    private bool IsGrounded()
    {
        // TODO: CHECK THE NEW LAYER SETTING
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }

    private void Jump()
    {
        jumpSoundEffect.Play();
        rb.AddForce(transform.up * jumpVel, ForceMode2D.Impulse);
    }
}
