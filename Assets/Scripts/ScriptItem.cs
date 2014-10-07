using UnityEngine;
using System.Collections;

public class ItemScript : MonoBehaviour {

	float range = 10; // Sera el doble de esto

	// Use this for initialization
	void Start () {
		transform.Rotate(new Vector3(0, 0, 45));
		transform.position = new Vector2(20f, Random.Range(-5f, 5f));

		// Impulso inicial
		float yVelocity = Random.Range(-range, range);
		float xVelocity = -10f;
		
		rigidbody2D.velocity = new Vector2(xVelocity, yVelocity);

		// Se autodestruyen despues de x segundos
		Destroy(gameObject, 10f);
	}
}
