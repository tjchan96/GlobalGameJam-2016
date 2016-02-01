using UnityEngine;
using System.Collections;

public class Spawner_Controller : MonoBehaviour {
	 
	private static int globalSpwnMax = 500;
	private static int globalSpwnCount = 0;

	public GameObject monster;
	private GameObject player;

	public int minFrequency = 100;
	private int frequency;
	private int time;

	public int maxDistance = 50;

	private System.Random rng;

	// Use this for initialization
	void Start () {
		rng = new System.Random ();

		player = GameObject.FindGameObjectWithTag ("Player");

		frequency = minFrequency;
		time = 0;
	}
	
	// Update is called once per frame
	void Update () {
		//print ("Spawn Count: " + globalSpwnCount);
		if (time >= frequency && globalSpwnCount < globalSpwnMax) {
			globalSpwnCount++;
			time = 0;
			frequency = minFrequency + (int)(rng.NextDouble () * minFrequency);
			Vector3 pos = new Vector3 ((float)(rng.NextDouble() - .5), (float)(rng.NextDouble() - .5), 0);
			GameObject newMonster = Instantiate (monster, transform.position + pos, Quaternion.identity) as GameObject;
		}
		if (Vector2.Distance((Vector2)(transform.position), (Vector2)(player.transform.position)) < maxDistance)
			time++;
	}
	public static void decreaseGlobalSpawnCount(int i) {
		globalSpwnCount -= i;
	}
}
