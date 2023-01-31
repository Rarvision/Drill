using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Attach to Player object
public class Transforming : MonoBehaviour
{
    // Game manager
    private GameManager gameManager;

    void Start()
    {
        gameManager = GameManager.Instance;
    }

    // Update state number (+1 or -1) baesd on the trigger type
    // TODO: Need to find a way to prevent repeating update
    private void OnTriggerEnter2D(Collider2D other) {

        if(other.gameObject.CompareTag("Heater")) {
            gameManager.UpdateState(1);
        } else if (other.gameObject.CompareTag("Cooler")) {
            gameManager.UpdateState(-1);
        }
    }
}
