using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeBox : MonoBehaviour {


    public PlayerManager playerRef;



    // Use this for initialization
    void Start ()
    {
        playerRef = GameManager.instance.player.GetComponent<PlayerManager>();

        if (playerRef == null)
        {
            Debug.LogError("NO PLAYER TO DMG CANT FIND PLAYER ");
        }
	}
	


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == playerRef.gameObject.tag)
        {
            playerRef.PlayerTakeDMG();
        }
    }
}
