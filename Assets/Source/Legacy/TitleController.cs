using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleController : MonoBehaviour {

	public GameObject zackaryLocation;
	public GameObject lucienLocation;
	public GameObject captainLocation;
	public GameObject bubbles;
	public List<string> texts;

	public Canvas origin;

	// Use this for initialization
	void Start() {
		StartCoroutine(dialogue());
	}

	// Update is called once per frame
	void Update() {

	}

	public IEnumerator dialogue() {
		foreach (string text in texts) {
			Vector3 loc;
			if (text.Split(' ')[0] == "Lucian:") {
				loc = zackaryLocation.transform.position;
			}
			else if (text.Split(' ')[0] == "Zackery:") {
				loc = zackaryLocation.transform.position;
			}
			else if (text.Split(' ')[0] == "Cpt.") {
				loc = captainLocation.transform.position;
			}
			else {
				loc = new Vector3(400, 350, 0);
			}
			GameObject temp = Instantiate(bubbles, origin.transform, false);
			temp.GetComponent<RectTransform>().position = loc;
			TextBubble bubble = temp.GetComponent<TextBubble>();
			bubble.toPrint = text;
			while (!bubble.isDone) yield return null;
		}
	}
}
