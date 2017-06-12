using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinmove : MonoBehaviour {
	public float speed, rotSpeed;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += Vector3.left * speed;
		transform.Rotate(0, rotSpeed, 0);
	}
}
