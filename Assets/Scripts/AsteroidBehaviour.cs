using UnityEngine;
using System.Collections;

public class AsteroidBehaviour : MonoBehaviour
{
	float range = 15; // Sera el doble de esto
	
	//TextMesh pointsText;
	
	private GameManager _gameManager;
	private GuiManager _guiManager;

	// Use this for initialization
	void Start()
	{
		_gameManager = GameManager.Instance;
		_guiManager = _gameManager.getGuiManager();

		transform.position = new Vector2(20f, Random.Range(-3f, 3f));

		float scale = Random.Range(1f, 1.5f);

		transform.localScale = new Vector2(scale, scale);
		
		// Impulso inicial
		float yVelocity = Random.Range(-range, range);
		float xVelocity = -20f;
		
		if (Mathf.Abs(yVelocity) < 5f)
		{
			xVelocity = -10f;
		}

		rigidbody2D.velocity = new Vector2(xVelocity, yVelocity);

		// Se autodestruyen despues de x segundos
		Destroy(gameObject, 80f);
	}
	
	void FixedUpdate()
	{
		// Comprueba si se ha salido de la pantalla, en su caso se destruye
		Vector2 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
		
		if (screenPosition.x < -5f)
		{
			// Punto para el jugador
			if (!_gameManager.isWaiting()) // Player dead. Waiting for game over screen
			{
				_gameManager.points++;
			}

			Destroy(gameObject);
		}
	}

	
}
