using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{

	public int hp = 1;
	public AudioClip deathSound;

	private GameManager _gameManager;
	private ObjectManager _objectManager;
	private AudioManager _audioManager;

	public bool invincible = false;
	
	void Start()
	{
		_gameManager = GameManager.Instance;
		_objectManager = _gameManager.getObjectManager();
		_audioManager = _gameManager.getAudioManager();
		Random.seed = (int)System.DateTime.Now.Ticks;
	}

	public void damage(int damageCount)
	{
		hp -= damageCount;
		
		if (hp <= 0)
		{
			_audioManager.playClip(deathSound, 0.6f);
			_objectManager.makeExplosion(gameObject.transform.position);
			_gameManager.setGameOver(true);
			Destroy(this.gameObject);
		}
	}
}
