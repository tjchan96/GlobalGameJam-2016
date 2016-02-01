using UnityEngine;
using System.Collections;

public class Head_Selector : MonoBehaviour {

	public string resourceName;
	public int index = 0;

	// Update is called once per frame
	void LateUpdate () {

		var newSprite = Resources.LoadAll<Sprite> ("Sprites/" + resourceName);
		GetComponent<SpriteRenderer> ().sprite = newSprite[index];
	}
}
