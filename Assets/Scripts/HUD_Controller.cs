using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HUD_Controller : MonoBehaviour {

	public Player_Controller player;

	public RectTransform healthBar;
	private Text rocksText;
	private Text twigsText;
	private float maxXVal;

	// Use this for initialization
	void Start () {
		//healthBar = (RectTransform)(transform.GetChild (0).GetChild (0).GetChild (0).GetChild (0).GetChild (0));
		rocksText = gameObject.GetComponentsInChildren<Text> ()[0];
		twigsText = gameObject.GetComponentsInChildren<Text> ()[1];
		maxXVal = healthBar.position.x;
	}
	
	// Update is called once per frame
	void Update () {
		UpdateHealth ();
		UpdateRocks ();
		UpdateTwigs ();
	}

	void UpdateHealth () {
		float newX = maxXVal - (player.maxHealth - player.health) * (healthBar.rect.width) * 0.4f / player.maxHealth;

		healthBar.position = new Vector2 (newX, healthBar.position.y);
	}

	void UpdateRocks () {
		rocksText.text = "" + player.rockCount;
	}

	void UpdateTwigs () {
		twigsText.text = "" + player.twigCount;
	}
		
}
