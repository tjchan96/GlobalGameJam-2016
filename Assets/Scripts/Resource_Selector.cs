using UnityEngine;
using System.Collections;

public class Resource_Selector : MonoBehaviour {

	public string resourceName;
	public int index = 2;
	public float health = 0;
	public int range = 0;
	public float damage = 0;
	
	// Update is called once per frame
	void Start () {
		
		var newSprite = Resources.LoadAll<Sprite> ("Sprites/" + resourceName);
		GetComponent<SpriteRenderer> ().sprite = newSprite[index];
	}
}
