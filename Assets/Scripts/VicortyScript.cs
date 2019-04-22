using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VicortyScript : MonoBehaviour {

    public GameObject Vpnl;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == GameManager.instance.player.tag)
        {
            VictoryScene();
            Debug.Log("Congrats You WON ");
        }
    }



    private void VictoryScene()
    {
        Vpnl.SetActive(true);
        //GameManager.instance.player.GetComponent<PlayerMovementControlles>().enabled = false;
        GameManager.instance.StopBoxSpawner();

    }
}
