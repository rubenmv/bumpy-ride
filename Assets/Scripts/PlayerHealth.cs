using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour {

	public int hp = 1;
	
	public AudioClip deathSound;
	
	bool gameOver;
	GameManager gameManager;
	ObjectManager objectManager;

	public bool invincible = false;
	
	void Start() {
		gameManager = GameManager.Instance;
		objectManager = gameManager.getObjectManager();

		Random.seed = (int)System.DateTime.Now.Ticks;
	}

	public void damage(int damageCount) {
		hp -= damageCount;
		
		if(hp <= 0) {
			gameManager.gameOver = true;
			audio.clip = deathSound;
			audio.Play();
			Destroy(gameObject.GetComponent<SpriteRenderer>());
			Destroy(gameObject.GetComponent<Rigidbody2D>());
			Destroy(gameObject.GetComponent<PlayerMovement>());
			objectManager.makeExplosion(gameObject.transform.position, Color.red);
		}
	}
}
