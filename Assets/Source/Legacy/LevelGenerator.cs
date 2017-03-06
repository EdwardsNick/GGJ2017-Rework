/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour {

	public Transform player;

	public enum mode { START, INSYSTEM, VOID };

	public mode state;

	public GameObject planet;

	SolarSystem currentSystem;

	public GameObject waves;
	public GameObject endSolar;

	public int MIN_WAVE_TIME = 2;
	public int MAX_WAVE_TIME = 4;

	public float MAX_WAVE_DISTANCE = 1200f;
	public float MIN_WAVE_DISTANCE = 600f;

	public float WAVE_X_OFFSET = 400;

	public List<GameObject> planets;

	public List<GameObject> planetsAvailable;

	public GameObject deathWave;

	public Material mat;


	// Use this for initialization
	void Start() {
		state = mode.START;
		StartCoroutine(generateWaves());
		StartCoroutine(checkVoid());
	}

	// Update is called once per frame
	void Update() {
		switch (state) {
			case mode.START:
				planets = new List<GameObject>();
				currentSystem = new SolarSystem();
				foreach (PlanetaryBody body in currentSystem.system) {
					//Debug.Log(body.zPos);
					GameObject temp = Instantiate(planetsAvailable[(int)(Random.value * planetsAvailable.Count)], transform.position + (transform.forward * (body.zPos)) + (transform.right * body.xPos), transform.rotation);
					temp.transform.localScale *= body.scaleFactor;
					RotationScript rot = temp.AddComponent<RotationScript>();
					rot.rotation = body.rotationSpeed;
					rot.planetRadius = temp.transform.localScale.magnitude * temp.GetComponent<SphereCollider>().radius;
					rot.gravityRadius = temp.transform.Find("Gravity").transform.localScale.magnitude * body.scaleFactor;
					body.body = temp;
					planets.Add(temp);
					if (body.star) {
						temp.GetComponent<MeshRenderer>().material = mat;
					}
				}
				Instantiate(endSolar, currentSystem.system[currentSystem.system.Count - 1].body.transform.position + (player.transform.forward * 3000), transform.rotation);
				state = mode.INSYSTEM;
				break;
			case mode.INSYSTEM:
				break;
			case mode.VOID:
				destroyPlanets();
				planets = new List<GameObject>();
				currentSystem = new SolarSystem();
				foreach (PlanetaryBody body in currentSystem.system) {
					GameObject temp = Instantiate(planetsAvailable[(int)(Random.value * planetsAvailable.Count)], player.transform.position + (player.transform.forward * (body.zPos + 15000)) + (player.transform.right * body.xPos), transform.rotation);
					temp.transform.localScale *= body.scaleFactor;
					RotationScript rot = temp.AddComponent<RotationScript>();
					rot.rotation = body.rotationSpeed;
					rot.planetRadius = temp.transform.localScale.magnitude * body.scaleFactor;
					rot.gravityRadius = temp.transform.Find("Gravity").transform.localScale.magnitude * body.scaleFactor;
					body.body = temp;
					planets.Add(temp);
				}
				Instantiate(endSolar, currentSystem.system[currentSystem.system.Count - 1].body.transform.position + (player.transform.forward * 3000), transform.rotation);
				deathWave.GetComponent<DeathWave>().increaseSpeed();
				state = mode.INSYSTEM;
				break;
		}
	}

	public IEnumerator generateWaves() {
		while (true) {
			yield return new WaitForSeconds(Random.Range(MIN_WAVE_TIME, MAX_WAVE_TIME));
			float x = Random.value;
			GameObject wave = Instantiate(waves, player.transform.position +
													(player.transform.right * (x < 0.5f ? WAVE_X_OFFSET : -WAVE_X_OFFSET)) +
													(player.transform.forward * (Random.Range(MIN_WAVE_DISTANCE, MAX_WAVE_DISTANCE))),
													Quaternion.identity);
			wave.GetComponent<GravWave>().dir = x < 0.5 ? -player.transform.right : player.transform.right;
		}
	}

	public void destroyPlanets() {
		foreach (GameObject obj in planets) {
			Destroy(obj);
		}
	}

	public IEnumerator checkVoid() {
		LevelChecker control = player.Find("Corridor").GetComponent<LevelChecker>();
		yield return new WaitForSeconds(3f);
		while (true) {
			control.nextCheck = false;
			yield return new WaitForSeconds(3f);
			if (!control.nextCheck) {
				state = mode.VOID;
				yield return new WaitForSeconds(15f);
			}
		}
	}
}
*/
