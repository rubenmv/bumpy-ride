using UnityEngine;
using System.Collections;

public class FinalScore : MonoBehaviour {

	// Use this for initialization
	void Start () {
		TextMesh text = gameObject.GetComponent<TextMesh>();
		text.text = "Puntos: " + GameManager.Instance.points;
	}
}
