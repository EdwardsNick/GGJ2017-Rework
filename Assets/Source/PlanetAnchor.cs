using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetAnchor : MonoBehaviour {

	public Vector3 velocity;

	// Use this for initialization
	void Start () {
		if (velocity == null) {
			velocity = Vector3.zero;
		}
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(velocity * Time.deltaTime);
	}
}
