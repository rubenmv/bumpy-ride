using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
	public AudioClip clickSound;

	private Vector2 direction;
	private CameraShake cameraShaker;
	private float friction = 0.98f;
	private float speed = 3;
	private Vector2 windowSize;
	private float leftLimit;
	private float rightLimit;
	private AudioManager _audioManager;
	
	void Start()
	{
		windowSize = GameManager.Instance.getScreenSize();
		_audioManager = GameManager.Instance.getAudioManager();

		float margin = windowSize.x / 10;
    
		leftLimit = Camera.main.ScreenToWorldPoint(new Vector3(margin, 0, 0)).x;
		rightLimit = Camera.main.ScreenToWorldPoint(new Vector3(windowSize.x - margin, 0, 0)).x;

		cameraShaker = GameObject.Find("Camera-Background").GetComponent<CameraShake>();
	}

	// Update is called once per frame
	void Update()
	{
		Vector2 playerPosition = new Vector2(transform.position.x, transform.position.y);
        
		if (Input.GetMouseButtonDown(0))
		{
			// Posicion de raton en el mundo
			Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Vector2 mousePosition = new Vector2(mouseWorld.x, mouseWorld.y);
            
			direction = (playerPosition - mousePosition) * speed;
			direction = new Vector2(Mathf.Clamp(direction.x, -12, 12), Mathf.Clamp(direction.y, -12, 12));

			applyForce(direction);

			// Audio
			_audioManager.playClip(clickSound, 0.2f);

			// Camera shake
			cameraShaker.shake(0.3f);
		}

		// Screen limits
		if (playerPosition.x > rightLimit)
		{
			direction = playerPosition - (new Vector2(playerPosition.x + 1, playerPosition.y));
			applyForce(direction);
		}
		else if (playerPosition.x < leftLimit)
		{
			direction = playerPosition - (new Vector2(playerPosition.x - 1, playerPosition.y));
			applyForce(direction);
		}

		rigidbody2D.velocity *= friction;

		// Rotation when moving
//		if (rigidbody2D.velocity != new Vector2(0, 0))
//		{
//			if (Mathf.Abs(transform.rotation.y) < 0.3)
//			{
//				float rotation = rigidbody2D.velocity.x;
//				if (direction.x < 0)
//				{
//					rotation *= -1;
//				}
//				transform.Rotate(0, rotation, 0);
//			}
//		}
	}

	void applyForce(Vector2 direction)
	{
		rigidbody2D.velocity *= 0;
		rigidbody2D.AddForce(direction * 50);
	}
}
