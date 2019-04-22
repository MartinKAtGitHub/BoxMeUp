using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

    public int playerHP;

    private bool invulnerableState;

    private Rigidbody2D rb;
    private PlayerMovementControlles playerMovementController;

    [SerializeField]
    private float invulnerabilityDuration;
    private float invulnerabilityTimer;

    // Use this for initialization
    void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
        playerMovementController = GetComponent<PlayerMovementControlles>();
        invulnerabilityTimer = invulnerabilityDuration;

    }
	
	// Update is called once per frame
	void Update ()
    {
        if(invulnerableState)
        {
            CoundownInvulnerableState();
        }
        
		if(playerHP <= 0)
        {
            GameManager.instance. GameOver();
        }
	}



    public void PlayerTakeDMG()
    {
        // set no DMG state
        if(invulnerableState)
        {
            return;
        }
        else
        {
            playerHP--;
            Debug.Log("TAKING DMG");
            invulnerableState = true;
        }


    }

    private void CoundownInvulnerableState()
    {
        invulnerabilityTimer -= Time.deltaTime;
       // Debug.Log("CountDown" + invulnerabilityTimer);
        if (invulnerabilityTimer < 0)
        {
            Debug.Log(" invulnerableState Done");
            invulnerableState = false;
            invulnerabilityTimer = invulnerabilityDuration;
        }
        // tick 
    }

    private void GameOver()// should be on GameManager but my mind is broken
    {
        

        Debug.LogError("YOU ARE DEAD RESTART OR EXIT GAME U NOOB");
    }
}
