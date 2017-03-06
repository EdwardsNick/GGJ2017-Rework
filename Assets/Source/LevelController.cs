using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour {

	public static int level = 0;

	public RandomPlanet planetGen;
	public List<GameObject> planets;

	public float LEVEL_HEIGHT = 500f;
	public float LEVEL_WIDTH = 5000f;
	public float LEVEL_LENGTH = 10000f;

	// Use this for initialization
	void Start () {
		planets = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
