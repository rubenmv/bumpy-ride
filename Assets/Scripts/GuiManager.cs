using UnityEngine;
using System.Collections;

public class GuiManager : MonoBehaviour {

	Vector2 _windowSize;	
	Vector2 _buttonSize;
	Vector2 _buttonPosition;

	GameManager _gameManager;
	AudioManager _audioManager;

	public GUISkin guiSkin;

	// Use this for initialization
	void Start() {
		_gameManager = GameManager.Instance;
		_audioManager = _gameManager.getAudioManager();
		_windowSize = new Vector2(Screen.width, Screen.height);

		_buttonSize = new Vector2(170, 50);
		_buttonPosition.x = (_windowSize.x / 2) - (_buttonSize.x / 2);
		_buttonPosition.y = Screen.height / 2; // A partir de aqui
	}

	void OnGUI() {
		// El GUI funciona como una state machine
		switch(_gameManager.currentScene) {
			case 0: // Menu inicio
				if(GUI.Button(new Rect(_buttonPosition.x, _buttonPosition.y, _buttonSize.x, _buttonSize.y), "Nueva Partida", guiSkin.button)) {
					_gameManager.loadLevel(1);
				}
				if(GUI.Button(new Rect(_buttonPosition.x, _buttonPosition.y + _buttonSize.y + 10, _buttonSize.x, _buttonSize.y), "Salir", guiSkin.button)) {
					_gameManager.quit();
				}
				break;
			case 1:
				if(GUI.Button(new Rect(_buttonPosition.x + 100, 0, _buttonSize.x, _buttonSize.y / 2), "Silenciar", guiSkin.button)) {
					// Toggle muted
					_audioManager.setMuted(!_audioManager.isMuted());
				}
				break;
			case 2: // Fin de juego
				if(GUI.Button(new Rect(_buttonPosition.x, _buttonPosition.y, _buttonSize.x, _buttonSize.y), "Reiniciar", guiSkin.button)) {
					_gameManager.loadLevel(1);
				}
				break;
		}	
	}
}
