using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Attach to Player object
public class PlayerMovement : MonoBehaviour
{
    // Components
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Animator anim;

    // Basic movement parameters
    private float inputX = 0.0f;
    [SerializeField] float runSpeed = 10.0f;

    // Game manager
    private GameManager gameManager;

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
        gameManager = GameManager.Instance;
    }

    private void Update()
    {
        if(gameManager.isInputDisabled)
        {
            return;
        }

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
