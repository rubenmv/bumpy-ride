﻿using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	private static GameManager _instance;
		
	private ObjectManager _objectManager;
	private GuiManager _guiManager;
	private AudioManager _audioManager;
	private Vector2 _windowSize;
	
	private bool _isGameOver;
	public int points = 0;
	public int currentScene = 0;

	public void setGameOver(bool isOver) {
		_isGameOver = isOver;
	}
	public bool getGameOver() {
		return _isGameOver;
	}

	// Managers
	public ObjectManager getObjectManager() {
		return _objectManager;
	}
	public AudioManager getAudioManager() {
		return _audioManager;
	}
	public GuiManager getGuiManager() {
		return _guiManager;
	}

	// Persistent Singleton
	public static GameManager Instance {
		get {
			if(_instance == null) {
				_instance = GameObject.FindObjectOfType<GameManager>();
				
				//Tell unity not to destroy this object when loading a new scene!
				DontDestroyOnLoad(_instance.gameObject);
			}
			return _instance;
		}
	}
	void Awake() {
		if(_instance == null) {
			//If I am the first instance, make me the Singleton
			_instance = this;
			DontDestroyOnLoad(this);
		} else {
			//If a Singleton already exists and you find
			//another reference in scene, destroy it!
			if(this != _instance) {
				Destroy(this.gameObject);
			}
		}
	}

	void Start() {
		// Get managers
		_objectManager = gameObject.GetComponent<ObjectManager>();
		_objectManager.enabled = false;
		_guiManager = gameObject.GetComponent<GuiManager>();
		_audioManager = gameObject.GetComponent<AudioManager>();

		currentScene = 0;

		_windowSize = new Vector2(Screen.width, Screen.height);
	}

	public Vector2 getScreenSize() {
		return _windowSize;
	}
	
	private void init() {
		points = 0;
		_isGameOver = false;
	}

	void Update() {
		if(Input.GetKey("escape")) {
			Application.Quit();
		}
		if(_isGameOver) {
			StartCoroutine(waitForGameOver(3f));
		}
	}
	//Coroutine
	IEnumerator waitForGameOver(float seconds) {
		// Make sure to trigger only one coroutine (they are independent)
		_isGameOver = false;
		yield return new WaitForSeconds(seconds);
		Application.LoadLevel("SceneGameOver");
	}

	// Load new level
	public void loadLevel(int level) {
		Application.LoadLevel(level);
		init();
	}

	public void quit() {
		Application.Quit();
	}
	
	private void OnLevelWasLoaded(int level) {
		if(level == 1) { // In Game
			_objectManager.enabled = true;
			_audioManager.init();
		} else {
			_objectManager.enabled = false;
		}
		
		currentScene = level;
	}
}
