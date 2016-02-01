using UnityEngine;
using System.Collections;

public class Satan_Controller : MonoBehaviour {

	public GameObject golem;

	private Animator anim;
	private GameObject[] res;
	private int count;



	// Use this for initialization
	void Awake () {
		anim = GetComponent<Animator> ();

		res = new GameObject[6];
		count = 0;
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.tag == "GolemPart") {
			res [count++] = other.gameObject;
			anim.SetInteger ("Pieces", count);	
		}
			
	}

	// Update is called once per frame
	void FixedUpdate () {
		if (count >= 6) {
			GameObject golemn = Instantiate (golem, transform.position, Quaternion.identity) as GameObject;
			golemn.BroadcastMessage("init", res);
			for (int i = 0; i < count; i++)
				Destroy (res [i]);
			count = 0;
		}
	}
}
