using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceSleep : MonoBehaviour {
    Rigidbody2D rb;
	void Start () {
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update ()
    {
       
	    if(Input.GetKey(KeyCode.S))
        {
            Debug.Log("FREEZ BOX");

            rb.isKinematic = true;
            rb.angularVelocity = 0;
            rb.velocity = Vector2.zero;
            //rb.Sleep();
        }
	}




    private void OutOfViewFeezBOX()
    {
        // so i need to calulcate the cam size or use the method camoutofview and freez the boxes / delete them baucse performance lel
        rb.isKinematic = true;
        rb.angularVelocity = 0;
        rb.velocity = Vector2.zero;
        //rb.Sleep();
    }
}
