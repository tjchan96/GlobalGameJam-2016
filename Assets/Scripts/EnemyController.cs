using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

	private Animator anim;
	private Rigidbody2D body;

	private const int BOBBING = 1;
	private const int BOUNCE_MELEE = 1;

	private GameObject player;

	private System.Random rng;

	public float maxSpeed = 1.2f;

	public int maxHealth = 10;
	public int health;
	public bool isBorn = false;
	public bool isDead = false;

	void Awake () {
		body = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();

		rng = new System.Random ();

		player = GameObject.FindGameObjectWithTag ("Player");

		health = maxHealth;
	}

	void FixedUpdate () {
		if (!isBorn) {
			if (anim.GetCurrentAnimatorStateInfo (0).IsName ("Enemy"))
				isBorn = true;
		}
		else {
			if (isDead && anim.GetCurrentAnimatorStateInfo (0).IsName ("EnemyGarbage")) {
				Spawner_Controller.decreaseGlobalSpawnCount (1);
				Destroy (this.gameObject);
			} else
				MoveEnemy (BOBBING);
		}
	}

	public void TakeDamage (int damage) {
		health -= damage;

		if (health <= 0 && !isDead)
			Die ();
	}

	void Die () {
		isDead = true;
		anim.SetTrigger ("Death");
	}

	void MoveEnemy (int choice) {
		switch (choice) {
			case BOBBING: MoveEnemy1(); break;
			default: break;
		}
	}

	void AttackPlayer (int choice) {
		switch (choice) {
			case BOUNCE_MELEE: AttackPlayer1(); break;
			default: break;
		}
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.tag == "Player")
			AttackPlayer (BOUNCE_MELEE);
	}

	void MoveEnemy1 () {
		Vector2 vel = (Vector2)(player.transform.position) - body.position;
		if (vel.magnitude > 1000)
			vel *= 0.6f;			
		body.velocity = body.velocity * 0.9f + (vel * maxSpeed / vel.magnitude) * 0.1f;
	}

	void AttackPlayer1 () {
		Player_Controller pc = player.GetComponent<Player_Controller> ();
		pc.TakeDamage (1);
		Vector2 vel = body.position - (Vector2)(player.transform.position);
		Vector2 offset = new Vector2 ((float)(rng.NextDouble () - 0.5) * 4, (float)(rng.NextDouble () - 0.5) * 4);
		body.velocity = vel / vel.magnitude * 8 + offset;

		TakeDamage (5);
	}

}