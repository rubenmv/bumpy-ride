using UnityEngine;
using System.Collections;

public class ScrollScript : MonoBehaviour {
	
	public float speed = 0f;
	GameObject player = null;
	float positionX = 0f;
	float positionY = 0f;
	
	void Start () {
		player = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update () {
		if (player == null) {
			player = GameObject.Find("Player");
		}
		
		if (player == null) {
			positionX += 0.02f;
		}
		else {
			positionX = player.transform.position.x;
			positionY = player.transform.position.y;
		}
		
		renderer.material.mainTextureOffset = new Vector2(positionX * speed * Time.fixedDeltaTime, positionY * speed * Time.fixedDeltaTime);
	}
}
