using UnityEngine;
using System.Collections;

public class GameManagerScript : MonoBehaviour {
	
	public bool gameOver;
	public int points = 0;
	public int currentScene = 0;
	
	public GameObjectManagerScript objectManager;
	public GUIManager guiManager;

	public AudioClip music;

	Vector2 windowSize;
	
	// Static singleton property
	public static GameManagerScript Instance { get; private set; }
	
	void Awake() {
		//Application.targetFrameRate = 30;
		Instance = this;
		// Esto lo mantiene entre escenas
		DontDestroyOnLoad(this.gameObject);
	}

	void Start () {
		objectManager = (GameObjectManagerScript)gameObject.GetComponent<GameObjectManagerScript>();
		guiManager = (GUIManager)gameObject.GetComponent<GUIManager>();
		objectManager.enabled = false;
		currentScene = 0;

		windowSize = new Vector2(Screen.width, Screen.height);
	}

	public Vector2 getScreenSize() {
		return windowSize;
	}
	
	public void init() {
		points = 0;
		gameOver = false;

		if (!guiManager.mute) {
			audio.clip = music;
			audio.Play();
			audio.loop = true;
		}
	}
	
	void Update() {
		if(Input.GetKey("escape")) {
			Application.Quit();
		}
	}
	
	void OnLevelWasLoaded(int level) {
		if (level == 1) { // In Game
			objectManager.enabled = true;
		}
		else {
			objectManager.enabled = false;
		}
		
		currentScene = level;
	}
}
