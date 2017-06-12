using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCall : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        GetComponent<Rigidbody>().AddForce(transform.forward);
    }
}
