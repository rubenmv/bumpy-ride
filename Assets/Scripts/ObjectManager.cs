using UnityEngine;
using System.Collections;

public class ObjectManager : MonoBehaviour {

	// Types of objects(prefabs) to instantiate
	public Object prefabPlayer;
	public Object prefabAsteroid;
	public Object prefabItem;
	public Object prefabExplosion; //Explosion particle system

	// Tiempo entre la creacion de meteoros
	float _spawnTime = 2.0f;
	float _spawnTimer = 2.0f;
	// Tiempo para aumentar la frecuencia de meteoros
	float _nextLevelTime = 10.0f; // 10 seg
	float _nextLevelTimer = 10.0f; // 10 seg

	public void init() {
		Instantiate(prefabPlayer);
		// Tiempo entre la creacion de meteoros
		_spawnTime = 2.0f;
		_spawnTimer = 2.0f;
		// Tiempo para aumentar la frecuencia de meteoros
		_nextLevelTime = 10.0f; // 10 seg
		_nextLevelTimer = 10.0f; // 10 seg
	}
	
	// Update is called once per frame
	void Update() {
		_spawnTimer -= Time.deltaTime;
		_nextLevelTimer -= Time.deltaTime;

		if(_spawnTimer < 0) {
			_spawnTimer = _spawnTime;
			Instantiate(prefabAsteroid);
		}

		// Sube el nivel de dificultad
		if(_nextLevelTimer < 0) {
			// Se reduce el tiempo entre spawn
			if(_spawnTime > 0.6f) {
				_spawnTime -= 0.1f;
			}
			_nextLevelTimer = _nextLevelTime;
			// Lanza un item
			Instantiate(prefabItem);
		}
	}

	// Creates an explosion effect on a position and with a color
	public void makeExplosion(Vector3 position, Color color) {
		/*
		GameObject obj = (GameObject)Instantiate(prefabExplosion, position, Quaternion.identity);
		obj.particleSystem.startColor = color;
		Destroy(obj, obj.particleSystem.duration);
		*/
	}

	private void OnLevelWasLoaded(int level) {
		if(level == 1) { // In Game
			init();
		}
	}
}
