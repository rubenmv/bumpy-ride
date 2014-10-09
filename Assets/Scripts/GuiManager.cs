using UnityEngine;
using System.Collections;

public class GuiManager : MonoBehaviour {

	Vector2 _windowSize;	
	Vector2 _buttonSize;
	Vector2 _buttonPosition;
	Vector2 _titlePosition;

	GameManager _gameManager;
	AudioManager _audioManager;

	public GUISkin guiSkin;
	public Texture titleTexture;

	// Use this for initialization
	void Start() {
		_gameManager = GameManager.Instance;
		_audioManager = _gameManager.getAudioManager();
		_windowSize = new Vector2(Screen.width, Screen.height);

		_buttonSize = new Vector2(170, 50);
		_buttonPosition.x = (_windowSize.x / 2) - (_buttonSize.x / 2);
		_buttonPosition.y = Screen.height / 2; // A partir de aqui
		_titlePosition.x = (_windowSize.x / 2) - (titleTexture.width / 2);
		_titlePosition.y = 50;
	}

	void OnGUI() {
		// El GUI funciona como una state machine
		switch(_gameManager.currentScene) {
			case 0: // Menu inicio
				GUI.DrawTexture(new Rect(_titlePosition.x, _titlePosition.y, titleTexture.width, titleTexture.height), titleTexture, ScaleMode.ScaleToFit);
				if(GUI.Button(new Rect(_buttonPosition.x, _buttonPosition.y, _buttonSize.x, _buttonSize.y), "New Game", guiSkin.button)) {
					_gameManager.loadLevel(1);
				}
				if(GUI.Button(new Rect(_buttonPosition.x, _buttonPosition.y + _buttonSize.y + 10, _buttonSize.x, _buttonSize.y), "Quit", guiSkin.button)) {
					_gameManager.quit();
				}
				GUI.Label(new Rect(50, _windowSize.y - 50, 200, 50), "http://rubenmv.github.io/");
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
				if(GUI.Button(new Rect(_buttonPosition.x, _buttonPosition.y + _buttonSize.y + 10, _buttonSize.x, _buttonSize.y), "Quit", guiSkin.button)) {
					_gameManager.quit();
				}
				break;
		}	
	}
}
