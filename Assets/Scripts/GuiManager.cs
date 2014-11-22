using UnityEngine;
using System.Collections;

public class GuiManager : MonoBehaviour
{

	Vector2 _windowSize;	
	Vector2 _buttonSize;
	Vector2 _buttonPosition;
	Vector2 _titlePosition;

	GameManager _gameManager;
	AudioManager _audioManager;

	public GUISkin guiSkin;
	public Texture titleTexture;

	// Use this for initialization
	void Start()
	{
		_gameManager = GameManager.Instance;

		_windowSize = new Vector2(Screen.width, Screen.height);

		_buttonSize = new Vector2(170, 50);
		_buttonPosition.x = (_windowSize.x / 2) - (_buttonSize.x / 2);
		_buttonPosition.y = Screen.height / 2; // A partir de aqui
		_titlePosition.x = (_windowSize.x / 2) - (titleTexture.width / 2);
		_titlePosition.y = 50;
	}

	void OnGUI()
	{
		// GUI works like a state machine
		switch (_gameManager.currentScene)
		{
			case 0: // Star menu
				GUI.DrawTexture(new Rect(_titlePosition.x, _titlePosition.y, titleTexture.width, titleTexture.height), titleTexture, ScaleMode.ScaleToFit);
				if (GUI.Button(new Rect(_buttonPosition.x, _buttonPosition.y, _buttonSize.x, _buttonSize.y), "New Game", guiSkin.button))
				{
					_gameManager.loadLevel(1);
				}
				if (GUI.Button(new Rect(_buttonPosition.x, _buttonPosition.y + _buttonSize.y + 10, _buttonSize.x, _buttonSize.y), "Quit", guiSkin.button))
				{
					_gameManager.quit();
				}
				GUI.Label(new Rect(50, _windowSize.y - 50, 200, 50), "http://rubenmv.github.io/");
				break;
			case 1: // In game
				if (GUI.Button(new Rect(_windowSize.x - _buttonSize.x - 5, 5, _buttonSize.x / 2, _buttonSize.y / 1.5f), "Mute", guiSkin.button))
				{
					if (_audioManager == null)
					{
						_audioManager = _gameManager.getAudioManager();
					}
					// Toggle muted
					_audioManager.setMuted(!_audioManager.isMuted());
				}
				// Score
				GUI.Label(new Rect(50, 10, 200, 50), "SCORE: " + _gameManager.points, guiSkin.label);
				break;
			case 2: // Game Over
				if (GUI.Button(new Rect(_buttonPosition.x, _buttonPosition.y, _buttonSize.x, _buttonSize.y), "Restart", guiSkin.button))
				{
					_gameManager.loadLevel(1);
				}
//				if (GUI.Button(new Rect(_buttonPosition.x, _buttonPosition.y + _buttonSize.y + 10, _buttonSize.x, _buttonSize.y), "Quit", guiSkin.button))
//				{
//					_gameManager.quit();
//				}
				// Score
				GUI.Label(new Rect(_windowSize.x / 2 - 130, 100, 200, 50), "TOTAL SCORE: " + _gameManager.points, guiSkin.GetStyle("LabelGameOverScore"));
				break;
		}	
	}
}
