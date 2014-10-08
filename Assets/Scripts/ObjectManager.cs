using UnityEngine;
using System.Collections;

public class ObjectManager : MonoBehaviour {

	// Types of objects(prefabs) to instantiate
	public Object player;
	public Object asteroid;
	public Object item;
	// Explosion particle effect
	public Object explosion;

	// Tiempo entre la creacion de meteoros
	float meteorTime = 2.0f;
	float meteorTimer = 2.0f;
	// Tiempo para aumentar la frecuencia de meteoros
	float nextLevelTime = 10.0f; // 10 seg
	float nextLevelTimer = 10.0f; // 10 seg

	public void init() {
		player = Instantiate(player);

		// Tiempo entre la creacion de meteoros
		meteorTime = 2.0f;
		meteorTimer = 2.0f;
		// Tiempo para aumentar la frecuencia de meteoros
		nextLevelTime = 10.0f; // 10 seg
		nextLevelTimer = 10.0f; // 10 seg
	}
	
	// Update is called once per frame
	void Update() {
		meteorTimer -= Time.deltaTime;
		nextLevelTimer -= Time.deltaTime;

		if(meteorTimer < 0) {
			meteorTimer = meteorTime;
			Instantiate(asteroid);
		}

		// Sube el nivel de dificultad
		if(nextLevelTimer < 0) {
			// Se reduce el tiempo entre spawn
			if(meteorTime > 0.6f) {
				meteorTime -= 0.1f;
			}
			nextLevelTimer = nextLevelTime;
			// Lanza un item
			Instantiate(item);
		}
	}

	// Creates an explosion effect on a position and with a color
	public void makeExplosion(Vector3 position, Color color) {
		GameObject obj = (GameObject)Instantiate(explosion, position, Quaternion.identity);
		obj.particleSystem.startColor = color;
		Destroy(obj, obj.particleSystem.duration);
	}

	private void OnLevelWasLoaded(int level) {
		if(level == 1) { // In Game
			init();
		}
	}
}
