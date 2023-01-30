using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumping : MonoBehaviour
{
    // Components
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private Animator anim;

    // Basic jump parameters
    [SerializeField] float jumpVel = 5.0f;

    // State manager
    private StateManager stateManager;

    // Ground detection parameters
    [SerializeField] private LayerMask jumpableGround;
    
    // Audio parameters
    [SerializeField] private AudioSource jumpSoundEffect;

    private enum JumpState {
        // TODO: UPDATE Jump STATES;
        jumping = 2, falling = 3
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        stateManager = StateManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if(!stateManager.canJump)
        {
            return;
        }
        
        if(Input.GetButtonDown("Jump") && IsGrounded()) 
        {
            Jump();
            UpdateAnimation();
        }

    }

    private void UpdateAnimation()
    {
        JumpState state = JumpState.jumping;
        // TODO: UPDATE THIS PART ACCORDING TO THE NEW STATE MACHINE
        // TODO: SET EXECUTION SEQUENCE OF DIFFERENT MOVEMENT SCRPITS
        if(rb.velocity.y > .1f)
        {
            state = JumpState.jumping;
        }
        else if (rb.velocity.y < -.1f)
        {
            state = JumpState.falling;
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
