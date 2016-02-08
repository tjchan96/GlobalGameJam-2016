using UnityEngine;
using System.Collections;

public class Resource_Producer : MonoBehaviour {

	public GameObject resource;
	public int collectSpeed = 30;

	private int count;

	private Vector3 oldPosition;
	private float radius;
	private bool spawn = false;

	void Start()
	{
		CircleCollider2D collider = GetComponent<CircleCollider2D> ();
		radius = collider.radius * Mathf.Min(transform.localScale.x, transform.localScale.y);
		oldPosition = transform.position;
		count = collectSpeed;
	}

	void OnMouseOver()
	{
		spawn = Input.GetMouseButton (0);
	}

	void FixedUpdate() {
		if (spawn && Input.GetMouseButtonUp (0))
			spawn = false;
		if (spawn) {
			if (count == 10) {
				float randomRadius = Random.Range (0, radius);
				float randomAngle = Random.Range (0, 2 * Mathf.PI);
				Vector2 rand = new Vector2 (randomRadius * Mathf.Cos (randomAngle), randomRadius * Mathf.Sin (randomAngle));
				Vector3 point = new Vector3 (rand.x + transform.position.x, rand.y + transform.position.y, oldPosition.z - 1);
				GameObject resourceCopy = Instantiate (resource, point, Quaternion.identity) as GameObject;
				Resource_Selector resource_selector = resourceCopy.GetComponent<Resource_Selector> () as Resource_Selector;
				resource_selector.index = Random.Range (2, resource_selector.index);
				transform.position = oldPosition;
			}
			if (count == 0)
			{
				count = collectSpeed;
			}
			if (count % (count / 5 + 1) == 0) {
				float moveRange = (float) count / collectSpeed / 10f;
				Vector3 position = new Vector3 (oldPosition.x + Random.Range (-moveRange, moveRange), oldPosition.y + Random.Range (-moveRange, moveRange), oldPosition.z);
				transform.position = position;
			}
			count--;
		} else {
			count = collectSpeed;
			transform.position = oldPosition;
		}
	}
}