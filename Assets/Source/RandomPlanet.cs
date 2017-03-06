using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPlanet : MonoBehaviour {

	public float MIN_ROTATION_SPEED = 10f;
	public float MAX_ROTATION_SPEED = 90f;

	public float GAS_CHANCE = 0.55f;
	public float ROCKY_CHANCE = 0.4f;
	public float STAR_CHANCE = 0.05f;

	public float RING_CHANCE = 0.5f;

	public float MIN_SCALE = 15f;
	public float MAX_SCALE = 25f;

	public float MIN_VELOCITY = 5f;
	public float MAX_VELOCITY = 15f;

	public List<GameObject> rings;
	public List<GameObject> gasPlanets;
	public List<GameObject> rockyPlanets;
	public GameObject star;

	public GameObject Anchor;

	// Use this for initialization
	void Start() {

	}

	// Update is called once per frame
	void Update() {

	}

	public GameObject createNewPlanet(Vector3 location) {
		GameObject anchor = Instantiate(Anchor, location, Quaternion.identity);
		PlanetAnchor anchorScript = anchor.GetComponent<PlanetAnchor>();

		if (location.y > 0) {
			anchorScript.velocity = new Vector3(Random.value, -Random.value, Random.value);
			anchorScript.velocity *= Random.Range(MIN_VELOCITY, MAX_VELOCITY);
		}
		else {
			anchorScript.velocity = new Vector3(Random.value, Random.value, Random.value);
			anchorScript.velocity *= Random.Range(MIN_VELOCITY, MAX_VELOCITY);
		}

		anchor.transform.localScale *= Random.Range(MIN_SCALE, MAX_SCALE);

		float planetType = Random.value;
		if (planetType < STAR_CHANCE) {
			GameObject sun = Instantiate(star, location, Quaternion.identity, anchor.transform);
			PlanetRotator rotate = sun.AddComponent<PlanetRotator>();
			rotate.rotationSpeed = Random.Range(MIN_ROTATION_SPEED, MAX_ROTATION_SPEED);
		}
		else if (planetType < STAR_CHANCE + ROCKY_CHANCE) {
			GameObject planet = Instantiate(rockyPlanets[Random.Range(0, rockyPlanets.Count)], location, Random.rotation, anchor.transform);
			PlanetRotator rotate = planet.AddComponent<PlanetRotator>();
			rotate.rotationSpeed = Random.Range(MIN_ROTATION_SPEED, MAX_ROTATION_SPEED);		}
		else {
			GameObject gas = Instantiate(gasPlanets[Random.Range(0, gasPlanets.Count)], location, Random.rotation, anchor.transform);
			if (Random.value < RING_CHANCE) Instantiate(rings[Random.Range(0, rings.Count)], location, gas.transform.rotation, gas.transform);
			PlanetRotator rotate = gas.AddComponent<PlanetRotator>();
			rotate.rotationSpeed = Random.Range(MIN_ROTATION_SPEED, MAX_ROTATION_SPEED);
		}

		return anchor;
	}
}
