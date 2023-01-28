using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{   
    // singleton
    public static StateManager Instance{get; private set; }

    // Status
    private enum State {
        ICE = -1, WATER = 0, GAS = 1
    }

    // State-based parameters
    private int stateNumber = 0;
    public bool canJump = true;
    public bool canSquat = true;
    public bool canSmash = false;
    public bool canSlide = false;
    public bool canPassFilter = true;
    private Rigidbody2D rb; // for changing gravity scale: rb.gravityScale;

    private void Awake() 
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start() 
    {
        rb = GetComponent<Rigidbody2D>();    
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

    // Update state parameters based on state number
    private void UpdateState() {
        if (stateNumber == (int)State.WATER)
        {
            canJump = true;
            canSquat = true;
            canSmash = false;
            canSlide = false;
            canPassFilter = true;
            rb.gravityScale = 1;
        } else if (stateNumber == (int)State.GAS)
        {
            canJump = false;
            canSquat = false;
            canSmash = false;
            canSlide = false;
            canPassFilter = true;
            rb.gravityScale = -0.6f;

        } else if (stateNumber == (int)State.ICE)
        {
            canJump = true;
            canSquat = false;
            canSmash = true;
            canSlide = true;
            canPassFilter = false;
            rb.gravityScale = 1;
        }

        Debug.Log("canJump: " + canJump);
        Debug.Log("canSquat: " + canSquat);
        Debug.Log("canSmash: " + canSmash);
        Debug.Log("canSlide: " + canSlide);
        Debug.Log("gravity: " + rb.gravityScale);
    }

    

}
