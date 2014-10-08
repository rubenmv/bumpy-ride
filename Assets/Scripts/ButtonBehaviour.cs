using UnityEngine;
using System.Collections;

public class ButtonBehaviour : MonoBehaviour {
	
	public string buttonTag;
	public Sprite normalSprite;
	public Sprite activeSprite;

	SpriteRenderer spriteRenderer;
	
	void Start() {
		spriteRenderer = (SpriteRenderer)transform.Find("ButtonSprite").gameObject.GetComponent<SpriteRenderer>();
		spriteRenderer.sprite = normalSprite;
	}
	
	void OnMouseUp() {
		gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
		spriteRenderer.sprite = normalSprite;
		
		switch(buttonTag) {
			case "quit":
				Application.Quit();
				break;
			case "new_game":
			case "restart":
				GameManager.Instance.loadLevel(1);
				break;
		}
	}
	
	void OnMouseDown() {
		spriteRenderer.gameObject.transform.localScale -= new Vector3(0.2f, 0.2f, 1f);
		spriteRenderer.sprite = activeSprite;
	}
}
