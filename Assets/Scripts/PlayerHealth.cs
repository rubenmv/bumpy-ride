using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour {

	public int hp = 1;
	public AudioClip deathSound;

	private GameManager _gameManager;
	private ObjectManager _objectManager;

	public bool invincible = false;
	
	void Start() {
		_gameManager = GameManager.Instance;
		_objectManager = _gameManager.getObjectManager();

		Random.seed = (int)System.DateTime.Now.Ticks;
	}

	public void damage(int damageCount) {
		hp -= damageCount;
		
		if(hp <= 0) {
			audio.clip = deathSound;
			audio.Play();
			_objectManager.makeExplosion(gameObject.transform.position, Color.red);
			_gameManager.setGameOver(true);
			Destroy(this.gameObject);
			//this.gameObject.SetActive(false);
		}
	}
}
