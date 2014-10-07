using UnityEngine;
using System.Collections;

public class TouchControl : MonoBehaviour {
	Vector2 direction;
	float friction = 0.98f;
	float speed = 3;

	public GameObject explosionPrefab;

	Vector2 windowSize;

	float leftLimit;
	float rightLimit;

	public AudioClip clickSound;

	void Start() {
		windowSize = GameManagerScript.Instance.getScreenSize();

		float margin = windowSize.x / 10;
	
		leftLimit = Camera.main.ScreenToWorldPoint(new Vector3(margin, 0, 0)).x;
		rightLimit = Camera.main.ScreenToWorldPoint(new Vector3(windowSize.x-margin, 0, 0)).x;
	}

	// Update is called once per frame
	void Update () {
		Vector2 playerPosition = new Vector2(transform.position.x, transform.position.y);
		
		if ( Input.GetMouseButtonDown(0) ) {
			// Posicion de raton en el mundo
			Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Vector2 mousePosition = new Vector2(mouseWorld.x, mouseWorld.y);
			
			direction = (playerPosition - mousePosition) * speed;
			direction = new Vector2( Mathf.Clamp(direction.x, -12, 12), Mathf.Clamp(direction.y, -12, 12) );

			applyForce(direction);
			makeExplosion(mouseWorld);

			// Audio
			audio.volume = 0.2f;
			audio.PlayOneShot(clickSound);
		}

		if ( playerPosition.x > rightLimit ) {
			direction = playerPosition - (new Vector2(playerPosition.x + 1, playerPosition.y));
			applyForce(direction);
		}
		else if ( playerPosition.x < leftLimit ) {
			direction = playerPosition - (new Vector2(playerPosition.x - 1, playerPosition.y));
			applyForce(direction);
		}
		
		rigidbody2D.velocity *= friction;
	}

	void applyForce(Vector2 direction) {
		rigidbody2D.velocity *= 0;
		rigidbody2D.AddForce(direction * 50);
	}

	void makeExplosion(Vector3 mouseWorld) {
		GameObject obj =  (GameObject) Instantiate(explosionPrefab, mouseWorld, Quaternion.identity);
		Destroy(obj, obj.particleSystem.duration);
	}
}
