using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Attach to Player object
public class Sliding : MonoBehaviour
{
    // sliding state for animation
    private const int SLIDING = 4;

    private Rigidbody2D rb;
    private Animator anim;
    // store the velocity entering the sliding area
    private Vector2 vel;

    // sliding speed
    private float slidingSpeed = 3.5f;

    // Game manager
    private GameManager gameManager;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        gameManager = GameManager.Instance;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("SlideTrigger"))
        {
            vel = rb.velocity;
            rb.velocity = vel * slidingSpeed;
            gameManager.isInputDisabled = true;
            anim.SetInteger("state", SLIDING);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.CompareTag("SlideTrigger"))
        {
            rb.velocity = vel;
            gameManager.isInputDisabled = false;
        }
    }
}
