using UnityEngine;
using System.Collections;

public class PlayerState : MonoBehaviour {
	
	float itemClock = 0f;
	float itemTime = 5f;
	bool gameOver;
	GameManager gameManager;
	ObjectManager objectManager;
	int activeItem; // 0 = invencible, 1 = gravity
	float alpha = 1f;
	bool alphaDown = true;
	SpriteRenderer spriteRenderer;
	PlayerHealth playerHealth;

	//TextMesh pointsText;

	// Use this for initialization
	void Start() {
		gameManager = GameManager.Instance;
		objectManager = gameManager.getObjectManager();
		spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

		playerHealth = (PlayerHealth)GetComponent("PlayerHealth");

		//GameObject go = GameObject.Find("3DTextPoints");
		//pointsText = (TextMesh)go.GetComponent(typeof(TextMesh));

		Random.seed = (int)System.DateTime.Now.Ticks;
	}
	
	// Update is called once per frame
	void Update() {
		if(!gameManager.getGameOver()) {
			// Item activo?
			if(activeItem > -1) {
				if(alphaDown) {
					alpha -= 0.1f;
					if(alpha < 0.5f) {
						alphaDown = false;
					}
				} else {
					alpha += 0.1f;
					if(alpha > 0.9f) {
						alphaDown = true;
					}
				}
				
				switch(activeItem) {
					case 0: // Invincibility
						spriteRenderer.color = new Color(1f, 1f, 1f, alpha);
						break;
					case 1: // Gravity
						spriteRenderer.color = new Color(1f, 0.6f, 0.6f, alpha);
						break;
				}
				
				// Removes item effect
				itemClock -= Time.deltaTime;
				if(itemClock <= 0f) {
					activeItem = -1;
					
					gameObject.rigidbody2D.gravityScale = 0;
					// Restores opacity
					spriteRenderer.color = new Color(1, 1, 1, 1);
				}
			}
		}
	}

	void OnCollisionEnter2D(Collision2D collision) {
		if(collision.gameObject.tag == "Asteroid") {
			if(activeItem == 0) { // Invencible
				// Punto para el jugador
				gameManager.points++;
				//pointsText.text = "PUNTOS: " + gameManager.points;
				
				Destroy(collision.gameObject);
				objectManager.makeExplosion(collision.gameObject.transform.position, Color.red);
			} else {
				playerHealth.damage(1);
			}
		} else if(collision.gameObject.tag == "Item") {
			
			activeItem = Random.Range(0, 2);
			
			if(activeItem == 0) {
				// invencible
				playerHealth.invincible = true;
			} else {
				if(Random.Range(0, 2) == 1) {
					gameObject.rigidbody2D.gravityScale = 1;
				} else {
					gameObject.rigidbody2D.gravityScale = -1;
				}
			}
			
			itemClock = itemTime;
			//Disable item gameobject
			Destroy(collision.gameObject);
		}
	}
}
