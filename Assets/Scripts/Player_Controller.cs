using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class Player_Controller : MonoBehaviour {

	private Animator anim;
	private Rigidbody2D body;

	public GameObject golemPart;
	public GameObject satanCircle;
	private bool satanSpawned = false;
	private int dropped = 0;

	public float maxSpeed = 1f;
	public int busy = 0;

	public int maxHealth = 100;
	public int health;
	public bool isDead = false;

	public int rockCount = 0;
	public int twigCount = 0;

	private float dx;
	private float dy;

	void Awake () {
		body = GetComponent<Rigidbody2D> ();

		anim = GetComponent<Animator> ();

		health = maxHealth;
	}

	void FixedUpdate () {
		dx = Input.GetAxisRaw ("Horizontal");
		dy = Input.GetAxisRaw ("Vertical");
		bool bsy = isBusy ();
		if (bsy)
			body.velocity = new Vector2 (0f, 0f);
		else
			body.velocity = new Vector2 (dx*maxSpeed, dy*maxSpeed);

		UpdateAnimation (dx, dy, bsy);
	}

	void UpdateAnimation (float dx, float dy, bool busy) {
		if ((dx == 0 && dy == 0))
			anim.SetBool ("stopped", true);
		else
			anim.SetBool ("stopped", false);
		anim.SetBool ("Channel", busy);
		anim.SetInteger ("dx", (int)(999 * dx));
		anim.SetInteger ("dy", (int)(999 * dy));
	}

	void Update () {
		if (Input.GetKeyUp ("space"))
			SummonSatan ();
		if (Input.GetKeyDown ("f"))
			DropRock ();
		else if (Input.GetKeyDown ("r"))
			DropTwig ();

		if (isDead && anim.GetCurrentAnimatorStateInfo(0).IsName("PlayerDeathGarbage"))
			SceneManager.LoadScene ("GameOverScene");
	}

	private void SummonSatan () {
		if (!satanSpawned) {
			Vector2 pos = new Vector2(transform.position.x, transform.position.y - 0.3f);
			GameObject satanicThing = Instantiate (satanCircle, pos, Quaternion.identity) as GameObject;
			satanSpawned = true;
			dropped = 0;
		}
	}

	private void DropRock () {
		if (rockCount > 0 && dropped < 6 && satanSpawned) {
			GameObject part = Instantiate (golemPart, transform.position, Quaternion.identity) as GameObject;
			Resource_Selector resource_selector = part.GetComponent<Resource_Selector> () as Resource_Selector;
			resource_selector.index = Random.Range (2, 5);
			resource_selector.resourceName = "Rocks";
			resource_selector.damage = 0.2f;
			resource_selector.health = 0.2f;
			rockCount--;
			dropped++;
			satanSpawned = !(dropped == 6);
		}
	}

	private void DropTwig () {
		if (twigCount > 0 && dropped < 6 && satanSpawned) {
			GameObject part = Instantiate (golemPart, transform.position, Quaternion.identity) as GameObject;
			Resource_Selector resource_selector = part.GetComponent<Resource_Selector> () as Resource_Selector;
			resource_selector.index = Random.Range (2, 4);
			resource_selector.resourceName = "Twigs";
			resource_selector.damage = 0.1f;
			resource_selector.health = 0.1f;
			twigCount--;
			dropped++;
			satanSpawned = !(dropped == 6);
		}
	}

	public void TakeDamage (int damage) {
		health -= damage;

		if (health <= 0 && !isDead)
			Die ();
			
	}

	void IncreaseRock() {
		rockCount++;
		busy--;
	}

	void IncreaseTwig() {
		twigCount++;
		busy--;
	}

	void IncreaseBusy() {
		busy++;
	}

	bool isBusy() {
		return busy > 0 || Input.GetMouseButton (0);
	}

	void Die () {
		isDead = true;

		anim.SetTrigger ("die");
		busy += 9999;
	}

}
