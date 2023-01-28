using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class PassingFilter : MonoBehaviour
{
    // State manager
    private StateManager stateManager;

    private void Start() 
    {
        stateManager = StateManager.Instance;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!stateManager.canPassFilter)
        {
            return;
        }
        if (collision.CompareTag("Filter"))
        {
            collision.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(!stateManager.canPassFilter)
        {
            return;
        }

        if (collision.CompareTag("Filter"))
        {
            collision.GetComponent<BoxCollider2D>().enabled = true;
        }
    }
}
