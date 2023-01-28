using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayerMovement : MonoBehaviour
{
    // Components
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Animator anim;

    // Basic movement parameters
    private float inputX = 0.0f;
    [SerializeField] float runSpeed = 10.0f;

    // State manager
    private StateManager stateManager;
        // int stateNumber = 0;
        // bool canJump = true;
        // bool canSquat = true;
        // bool canSmash = false;
        // bool canSlide = false;
        // gravityScale -- rb.gravityScale;

    // MovementStatus
    private enum MovementState {
        // TODO: UPDATE MOVEMENT STATES;
        idle = 0, running = 1
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        stateManager = StateManager.Instance;
    }

    private void Update()
    {
        inputX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(inputX * runSpeed, rb.velocity.y);

        UpdateAnimation();
    }

    private void UpdateAnimation() 
    {
        MovementState state = MovementState.idle;

        // TODO: UPDATE THIS PART ACCORDING TO THE NEW STATE MACHINE
        // TODO: SET EXECUTION SEQUENCE OF DIFFERENT MOVEMENT SCRPITS
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
        anim.SetInteger("state", (int)state);
    }
}
