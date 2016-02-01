using UnityEngine;
using System.Collections;

public class Golem_Controller : MonoBehaviour {

	private Animator anim;
	private Rigidbody2D body;
	private CircleCollider2D trigCol, physCol;
	private Resource_Selector[] resSelChild;
	private GameObject player;
	private GameObject satanCirc;

	public float maxSpeed = 2f;

	public int baseHealth = 100;
	public int baseDamage = 10;
	public bool isDead = false;
	public bool backHead = false;
	public float stopFollowRange = 1.5f;
	public float cowardRange = 6.0f;

	private int health;
	private float healthScale = 1;
	private int maxHealth;
	private int damage;
	private float damageScale = 1;
	private int range = 0;

	private bool initiated = false;
	private bool alive = false;
	private int mode = 0;
	private GameObject target;
	private float dx;
	private float dy;

	// Handled once
	void Awake () {
		body = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
		foreach (CircleCollider2D col in GetComponents<CircleCollider2D> ()) {
			if (col.isTrigger)
				trigCol = col;
			else
				physCol = col;
		}

		player = GameObject.FindGameObjectWithTag ("Player");
		satanCirc = GameObject.FindGameObjectWithTag ("Satan");
	}

	public void init(GameObject[] mats) {
		resSelChild = GetComponentsInChildren<Resource_Selector> ();
		Head_Selector[] headres = GetComponentsInChildren<Head_Selector> ();
		int count = 0;
		bool noHead = true;
		if (mats != null && mats.Length == 6) {
			foreach (Resource_Selector res in resSelChild) {
				Resource_Selector mat = mats [count++].GetComponent<Resource_Selector> ();
				res.resourceName = mat.resourceName;
				res.index = mat.GetComponent<Resource_Selector> ().index;

				if (noHead && Random.Range (0, 10) > 3 || count == 6) {
					noHead = false;
					headres[0].resourceName = mat.resourceName;
					headres[0].index = 0;

					healthScale += mat.health;
					damageScale += mat.damage;
					range += mat.range;
				}
			}
		}
			
		health = maxHealth = (int) (healthScale * baseHealth);
		damage = (int) (baseDamage * damageScale);
		physCol.enabled = false;

	}

	// Update is called once per frame
	void FixedUpdate () {
		if (initiated)
			act ();
	}

	void Update () {
		if (!initiated && Input.GetMouseButtonUp (0)) {
			initiated = true;
			satanCirc.tag = "SatanDone";
			anim.SetTrigger ("Create");
		}
	}

	void act () {

		if (isDead && anim.GetCurrentAnimatorStateInfo(0).IsName("GarbageCollect")) {			
			Destroy (this.gameObject);
			return;
		}
		else if (!alive && anim.GetCurrentAnimatorStateInfo(0).IsName("Idle")) {
			Destroy(satanCirc);
			body.constraints = RigidbodyConstraints2D.FreezeRotation;
			physCol.enabled = true;
			alive = true;
		}
			
		findMode();
		if (alive) {
			switch (mode) {
			case 0:
				GolemFollow ();
				break;
			case 1:
				GolemAttack ();
				break;

			default:
				break;
			}
		}
		else
			body.velocity = Vector2.zero;
		handleHead ();

		UpdateAnimation (body.velocity.x, body.velocity.y, false);
	}



	void findMode() {
		Vector2 vel = (Vector2)(player.transform.position) - body.position;
		if (target != null && vel.magnitude < cowardRange) {
			if (Vector2.Distance (body.position, (Vector2)(target.transform.position)) > cowardRange) {
				target = null;
				mode = 0;
			}
			mode = 1;
		} else {
			mode = 0;
			target = null;
		}

	}

	void GolemFollow () {
		Vector2 vel = (Vector2)(player.transform.position) - body.position;
		if (vel.magnitude < stopFollowRange)
			body.velocity = Vector2.zero;
		else
			body.velocity = body.velocity * 0.9f + (vel * maxSpeed / vel.magnitude) * 0.1f;
	}

	void GolemAttack() {
		Vector2 vel = (Vector2)(target.transform.position) - body.position;
		body.velocity = body.velocity * 0.9f + (vel * maxSpeed / vel.magnitude) * 0.1f;
	}

	void UpdateAnimation (float dx, float dy, bool atk) {
		if (atk) anim.SetTrigger ("Attack");
		if (Mathf.Abs (dy) > Mathf.Abs (dx))
			dx = 0.0f;
		else
			dy = 0.0f;
		anim.SetFloat ("dx", dx);
		anim.SetFloat ("dy", dy);
	}

	public void TakeDamage (int damage) {
		
		health -= damage;

		if (health <= 0 && !isDead)
			Die ();
	}

	void OnTriggerStay2D (Collider2D other) {
		if (target == null && other.tag == "Enemy")
			target = other.gameObject;
	}

	void OnCollisionEnter2D (Collision2D other) {
		if (other.gameObject == target) {
			anim.SetTrigger ("Attack");
			target.SendMessage ("TakeDamage", damage);
			Rigidbody2D targetBody = target.GetComponent<Rigidbody2D> (); 
			Vector2 vel = targetBody.position - body.position;
			targetBody.velocity = targetBody.velocity * 0.2f + vel / vel.magnitude * 8 * 0.8f;
		}
	}

	void Die () {
		isDead = true;
		anim.SetTrigger ("Death");
	}

	// READ BELOW ONLY IF NECESSARY

	private void handleHead() {
		if (backHead)
			setHead (1);
		else
			setHead (0);
	}
	private void setHead(int index) {
		Head_Selector[] headres = GetComponentsInChildren<Head_Selector> ();
		headres [0].index = index;
	}
}
