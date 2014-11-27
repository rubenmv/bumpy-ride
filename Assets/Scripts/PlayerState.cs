using UnityEngine;
using System.Collections;

public class PlayerState : MonoBehaviour
{
	// References
	//
	private GameManager gameManager;
	private ObjectManager objectManager;
	private PlayerHealth playerHealth;
	private CameraShake cameraShaker;

	// Item effect
	//
	private float itemClock = 0f;
	private float itemTime = 5f;
	private int activeItem = -1; // 0 = invencible, 1 = gravity
    private int gravityDirection = 0;

	// Main sprite
	private SpriteRenderer spriteRenderer;

	// Visual effects
	//
    private SpriteRenderer spriteRendererEffects;
	// alpha component for color effect
	private float alphaSprite = 1f;
    private float alphaSpriteEffect = 1f;
	private bool alphaDown = true;

	// Getters
	//
	public int getActiveItem()
	{
		return activeItem;
	}

	// Use this for initialization
	void Start()
	{
		gameManager = GameManager.Instance;
		objectManager = gameManager.getObjectManager();
		spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRendererEffects = GameObject.Find("SpriteEffects").GetComponent<SpriteRenderer>();
		// Game sprite renderer from children game object
		playerHealth = (PlayerHealth)GetComponent("PlayerHealth");
		cameraShaker = GameObject.Find("Camera-Background").GetComponent<CameraShake>();

		Random.seed = (int)System.DateTime.Now.Ticks;
	}

    private void resetEffect()
    {
        alphaSpriteEffect = 1f;
        spriteRendererEffects.transform.localScale = new Vector3(1f, 1f, 1f);
        spriteRendererEffects.transform.localPosition = new Vector3(0f, 0f, 0f);
    }

	// Update is called once per frame
	void Update()
	{
		if (!gameManager.isGameOver())
		{
			// Item effect is active
			if (activeItem > -1)
			{
				if (alphaDown)
				{
                    alphaSprite -= 0.01f;
                    if (alphaSprite < 0.5f)
					{
						alphaDown = false;
					}
				}
				else
				{
                    alphaSprite += 0.01f;
                    if (alphaSprite > 0.9f)
					{
						alphaDown = true;
					}
				}

                // Aux sprite alpha
                alphaSpriteEffect -= 0.02f;
                // Set color to sprites
				switch (activeItem)
				{
					case 0: // Invincibility
						spriteRenderer.color = new Color(1f, 1f, 1f, alphaSprite); // White

                        // Cool Effect
                        spriteRendererEffects.transform.localScale += new Vector3(0.01f, 0.01f, 0f);
                        if (alphaSpriteEffect <= 0f) // reset
                        {
                            resetEffect();
                        }
                        spriteRendererEffects.color = new Color(1f, 1f, 1f, alphaSpriteEffect); // White
						break;
					case 1: // Gravity
						spriteRenderer.color = new Color(1f, 0.6f, 0.6f, alphaSprite); // Red

                        // Cool Effect
                        spriteRendererEffects.transform.localPosition += new Vector3(gravityDirection * 0.02f, 0f, 0f);
                        if (alphaSpriteEffect <= 0f) // reset
                        {
                            resetEffect();
                        }
                        spriteRendererEffects.color = new Color(1f, 0.6f, 0.6f, alphaSpriteEffect); // Red
						break;
				}

				// Removes item effect
				itemClock -= Time.deltaTime;
				if (itemClock <= 0f)
				{
                    activeItem = -1;
                    gameObject.rigidbody2D.gravityScale = 0;
                    // Restores opacity
                    spriteRenderer.color = new Color(1, 1, 1, 1);
                    resetEffect();
				}
			}
		}
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Asteroid")
		{
			if (activeItem == 0)
			{ // Invencible
				cameraShaker.shake(0.8f);
				// Punto para el jugador
				gameManager.points++;
				//pointsText.text = "PUNTOS: " + gameManager.points;
				
				Destroy(collision.gameObject);
				objectManager.makeExplosion(collision.gameObject.transform.position);
			}
			else
			{
				cameraShaker.shake(1.5f);
				playerHealth.damage(1);
			}
		}
		else if (collision.gameObject.tag == "Item")
		{
            // Reset effect sprite and main object
            resetEffect(); // if some effect was active
            gameObject.rigidbody2D.gravityScale = 0;
            // Restores opacity
            spriteRenderer.color = new Color(1, 1, 1, 1);

			activeItem = Random.Range(0, 2);
            			
			if (activeItem == 0)
			{
				// Invincible
				playerHealth.invincible = true;
			}
			else
			{
				if (Random.Range(0, 2) == 1)
				{
					gameObject.rigidbody2D.gravityScale = 1;
                    gravityDirection = 1;
				}
				else
				{
					gameObject.rigidbody2D.gravityScale = -1;
                    gravityDirection = -1;
				}
			}
			
			itemClock = itemTime;
			// Destroy item gameobject
			Destroy(collision.gameObject);
		}
	}
}
