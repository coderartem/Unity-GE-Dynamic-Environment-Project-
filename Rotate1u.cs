﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate1u : MonoBehaviour
{
	public float speed = 15f;

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		transform.Rotate(-speed, 0, 0);
	}
}