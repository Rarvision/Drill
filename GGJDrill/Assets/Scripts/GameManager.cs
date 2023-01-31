using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Attach to GameManager object
public class GameManager : MonoBehaviour
{   
    // singleton
    private static GameManager instance;
    public static GameManager Instance{ get {return instance; }}

    // Input state
    public bool isInputDisabled = false;

    // Status
    private enum State {
        ICE = -1, WATER = 0, GAS = 1
    }

    private int stateNumber = (int)State.WATER;

    // Player reference
    private GameObject player;

    // Prefabs reference
    public GameObject iceMan;
    public GameObject waterMan;
    public GameObject gasMan;

    private void Awake() 
    {
        if(instance == null || instance == this)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start() 
    {
        player = GameObject.FindGameObjectWithTag("Player");  
    }

    // Update state parameters based on state number
    public void UpdateState(int update) {
        if(stateNumber + update < -1 || stateNumber + update > 1)
        {
            return;
        }

        stateNumber += update;
        Transform currentTrans = player.transform;
        Removing removePlayerScript = player.GetComponent<Removing>();
        
        removePlayerScript.removeItself();

        if (stateNumber == (int)State.WATER)
        {
            player = Instantiate(waterMan, transform.position, transform.rotation);
        } 
        else if (stateNumber == (int)State.GAS)
        {
            player = Instantiate(gasMan, transform.position, transform.rotation);
        } 
        else if (stateNumber == (int)State.ICE)
        {
            player = Instantiate(iceMan, transform.position, transform.rotation);
        }

    }

    

}
