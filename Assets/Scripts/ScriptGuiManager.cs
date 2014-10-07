using UnityEngine;
using System.Collections;

public class GUIManager : MonoBehaviour {

	GameManagerScript gameManager;
	Vector2 windowSize;	
	Vector2 buttonSize;
	Vector2 buttonPosition;
	public bool mute = false;

	public GUISkin guiSkin;

	// Use this for initialization
	void Start () {
		gameManager = GameManagerScript.Instance;
		windowSize = new Vector2(Screen.width, Screen.height);

		buttonSize = new Vector2(170, 50);
		buttonPosition.x = (windowSize.x / 2) - (buttonSize.x / 2);
		buttonPosition.y = Screen.height / 2; // A partir de aqui

		mute = false;
	}
	
	void OnGUI() {
		// El GUI funciona como una state machine
		switch(gameManager.currentScene) {
			case 0: // Menu inicio
			if (GUI.Button(new Rect(buttonPosition.x, buttonPosition.y, buttonSize.x, buttonSize.y), "Nueva Partida", guiSkin.button)) {
				GameManagerScript.Instance.objectManager.enabled = true;
				gameManager.objectManager.init();
				GameManagerScript.Instance.init();
				Application.LoadLevel(1);
			}
			if (GUI.Button(new Rect(buttonPosition.x, buttonPosition.y + buttonSize.y + 10, buttonSize.x, buttonSize.y), "Salir", guiSkin.button)) {
				Application.Quit();
			}
			break;
			/*
		case 1:
			if (GUI.Button(new Rect(buttonPosition.x + 100, 0, buttonSize.x, buttonSize.y / 2), "Silenciar", guiSkin.button)) {
				mute = !mute;
				if (mute) {
					audio.volume = 0f;
				}
				else {
					audio.volume = 0.6f;
				}

			}
			break;
			*/
			case 2: // Fin de juego
			if (GUI.Button(new Rect(buttonPosition.x, buttonPosition.y, buttonSize.x, buttonSize.y), "Reiniciar", guiSkin.button)) {
				gameManager.objectManager.enabled = true;
				gameManager.objectManager.init();
				gameManager.init();
				Application.LoadLevel(1);
			}
			break;
		}		
	}
}
