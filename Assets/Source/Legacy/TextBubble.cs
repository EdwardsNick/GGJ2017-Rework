using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextBubble : MonoBehaviour {

	public bool isDone;
	public string toPrint;

	Text text;

	// Use this for initialization
	void Start() {
		isDone = false;
		text = transform.Find("Text").GetComponent<Text>();
		text.text = "";
		StartCoroutine(texter());
	}

	// Update is called once per frame
	void Update() {

	}

	public IEnumerator texter() {
		foreach (string str in toPrint.Split(' ')) {
			foreach (char ch in str) {
				text.text += ch;
				yield return new WaitForSeconds(0.05f);
			}
			text.text += " ";
		}
		isDone = true;
		yield return new WaitForSeconds(5f);
		Destroy(gameObject);
	}
}
