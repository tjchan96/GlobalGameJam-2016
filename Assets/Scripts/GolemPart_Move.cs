using UnityEngine;
using System.Collections;

public class GolemPart_Move : MonoBehaviour {

	private Rigidbody2D body;

	private GameObject target;
	private bool triggered;

	public float maxSpeed = 2.0f;
	public float angleVariation = 0.1f;
	public string type;

	void Start() {
		target = GameObject.FindWithTag("Satan");
		triggered = false;

		body = GetComponent<Rigidbody2D> ();
		Vector2 vel = body.position - (Vector2)(target.transform.position);

		body.velocity = vel * maxSpeed;
	}

	void FixedUpdate () {
		target = GameObject.FindWithTag("Satan");
		if (!triggered && target != null) {
			Vector2 vel = (Vector2)(target.transform.position) - body.position;
			body.velocity = body.velocity * 0.9f + (vel * maxSpeed / vel.magnitude) * 0.5f;
		} else
			body.velocity = new Vector2 (0f, 0f);
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.tag == "Satan") {
			triggered = true;
			GetComponent<SpriteRenderer> ().sprite = null;
		}
	}
}
