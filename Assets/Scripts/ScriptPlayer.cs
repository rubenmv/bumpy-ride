using UnityEngine;
using System.Collections;

public class ScriptPlayer : MonoBehaviour {

	public int hp = 1;
	
	public AudioClip deathSound;
	
	bool gameOver;
	GameManagerScript gameManager;

	SpriteRenderer spriteRenderer;
	float alpha = 1f;
	bool alphaDown = true;

	TextMesh pointsText;
	// Explosion particle effect
	public GameObject explosionPrefab;
	
	float itemClock = 0f;
	float itemTime = 5f;

	public bool invincible = false;
	int itemType; // 0 = invencible, 1 = gravity

	void Start() {
		gameManager = GameManagerScript.Instance;
		spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

		GameObject go = GameObject.Find("3DTextPoints");
		pointsText = (TextMesh)go.GetComponent(typeof(TextMesh));

		Random.seed = (int)System.DateTime.Now.Ticks;
	}
	
	void Update() {
		if(gameManager.gameOver && !audio.isPlaying) {
			Application.LoadLevel(2);
		}

		if (!gameManager.gameOver) {
			// Item activo?
			if (itemType > -1) {

				if (alphaDown) {
					alpha-=0.1f;
					if(alpha < 0.5f) {
						alphaDown = false;
					}
				}
				else {
					alpha+=0.1f;
					if (alpha > 0.9f) {
						alphaDown = true;
					}
				}
				
				switch(itemType) {
				case 0: // Invencible
					spriteRenderer.color = new Color(1f, 1f, 1f, alpha);
					break;
				case 1: // Gravedad
					spriteRenderer.color = new Color(1f, 0.6f, 0.6f, alpha);
					break;
				}
				
				// Comprueba si termina el efecto
				itemClock -= Time.deltaTime;
				if (itemClock <= 0f) {
					itemType = -1;
					
					gameObject.rigidbody2D.gravityScale = 0;
					// Restaura la opacidad
					spriteRenderer.color = new Color(1, 1, 1, 1);
				}
			}
		}
	}

	public void Damage(int damageCount)	{
		hp -= damageCount;
		
		if (hp <= 0) {
			gameManager.gameOver = true;
			audio.clip = deathSound;
			audio.Play();
			Destroy(gameObject.GetComponent<SpriteRenderer>());
			Destroy(gameObject.GetComponent<Rigidbody2D>());
			Destroy(gameObject.GetComponent<TouchControl>());
			makeExplosion(gameObject.transform.position);
		}
	}
	
	void OnCollisionEnter2D(Collision2D collision) {
		if (collision.gameObject.tag == "Meteor") {
			if (itemType == 0) { // Invencible
				// Punto para el jugador
				gameManager.points++;
				pointsText.text = "PUNTOS: " + gameManager.points;

				Destroy(collision.gameObject);
				makeExplosion(collision.gameObject.transform.position);
			}
			else {
				Damage(1);
			}
		}
		else if (collision.gameObject.tag == "Item") {

			itemType = Random.Range(0, 2);

			if (itemType == 0) {
				// invencible
				invincible = true;
			}
			else {
				if (Random.Range(0, 2) == 1) {
					gameObject.rigidbody2D.gravityScale = 1;
				}
				else {
					gameObject.rigidbody2D.gravityScale = -1;
				}

			}

			itemClock = itemTime;
			// Y destruyo el gameobject del item
			Destroy(collision.gameObject);
		}
	}
	
	void makeExplosion(Vector3 position) {
		GameObject obj =  (GameObject) Instantiate(explosionPrefab, position, Quaternion.identity);
		obj.particleSystem.startColor = Color.red;
		Destroy(obj, obj.particleSystem.duration);
	}
}
