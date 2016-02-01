using UnityEngine;
using System.Collections;

public class Resource_Move : MonoBehaviour {

	private Rigidbody2D body;

	private GameObject target;

	public float maxSpeed = 2.0f;
	public float angleVariation = 0.1f;
	public string type;

	void Start() {
		target = GameObject.FindWithTag("Player");
		target.SendMessage ("IncreaseBusy");


		body = GetComponent<Rigidbody2D> ();
		Vector2 vel = body.position - (Vector2)(target.transform.position);

		body.velocity = vel * maxSpeed * 3.0f;
	}

	void FixedUpdate () {
		Vector2 vel = (Vector2)(target.transform.position) - body.position;

		body.velocity = body.velocity * 0.9f + (vel * maxSpeed / vel.magnitude) * 0.5f;
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.tag == "Player") {
			other.SendMessage ("Increase" + type);
			Destroy (this.gameObject);
		}
	}
}
