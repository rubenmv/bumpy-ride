using UnityEngine;
using System.Collections;

public class GuiManager : MonoBehaviour
{

	private Vector2 _windowSize;
	private Vector2 _buttonSize;
	private Vector2 _buttonPosition;
	private Vector2 _titlePosition;
	private GameManager _gameManager;
	private AudioManager _audioManager;
	private PlayerState _playerState;
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
					_playerState = null;
					_gameManager.loadLevel(1);
				}
				if (GUI.Button(new Rect(_buttonPosition.x, _buttonPosition.y + _buttonSize.y + 10, _buttonSize.x, _buttonSize.y), "Quit", guiSkin.button))
				{
					_gameManager.quit();
				}
                break;
            case 1: // In Game
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
				// Active item info
				if (_playerState == null)
				{
					GameObject player = GameObject.Find("Player");
					if (player != null)
					{
						_playerState = player.GetComponent<PlayerState>();
					}
				}
				if (_playerState != null)
				{
					string message = "";

					switch (_playerState.getActiveItem())
					{
						case 0:
							message = "Invincibility!";
							break;
						case 1:
							message = "Gravity Pull!";
							break;
					}

					GUI.Label(new Rect(_windowSize.x / 2 - 150, 10, 200, 50), message, guiSkin.GetStyle("LabelItemMessage"));
				}

				break;
			case 2: // Game Over
				if (GUI.Button(new Rect(_buttonPosition.x, _buttonPosition.y, _buttonSize.x, _buttonSize.y), "Restart", guiSkin.button))
				{
					_playerState = null;
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
