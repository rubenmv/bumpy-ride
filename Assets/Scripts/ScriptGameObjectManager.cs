using UnityEngine;
using System.Collections;

public class GameObjectManagerScript : MonoBehaviour {

	public GameObject meteorPrefab;
	public GameObject itemPrefab;

	// Tiempo entre la creacion de meteoros
	float meteorTime = 2.0f;
	float meteorTimer = 2.0f;
	// Tiempo para aumentar la frecuencia de meteoros
	float nextLevelTime = 10.0f; // 10 seg
	float nextLevelTimer = 10.0f; // 10 seg

	// Use this for initialization
	void Start () {
	
	}
	
	public void init() {
		// Tiempo entre la creacion de meteoros
		meteorTime = 2.0f;
		meteorTimer = 2.0f;
		// Tiempo para aumentar la frecuencia de meteoros
		nextLevelTime = 10.0f; // 10 seg
		nextLevelTimer = 10.0f; // 10 seg
	}
	
	// Update is called once per frame
	void Update () {
		meteorTimer -= Time.deltaTime;
		nextLevelTimer -= Time.deltaTime;
		
		if (meteorTimer < 0) {
			meteorTimer = meteorTime;
			Instantiate(meteorPrefab);
		}

		// Sube el nivel de dificultad
		if (nextLevelTimer < 0) {
			// Se reduce el tiempo entre spawn
			if (meteorTime > 0.6f) {
				meteorTime -= 0.1f;
			}
			nextLevelTimer = nextLevelTime;
			// Lanza un item
			Instantiate(itemPrefab);
		}
	}
}
