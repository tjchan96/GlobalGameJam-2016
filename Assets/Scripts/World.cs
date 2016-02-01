using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class World : MonoBehaviour {

	public Dictionary<string, Color[]> colorKey;

	[Range(1, 10)]
	public float scale;

	public int tileDim = 64;
	public int width = 10048;
	public int height = 10048;
	private float offset = 0.5f;
	private float seed = 0;

	public GameObject grassTile;
	public GameObject grass2Tile;
	public GameObject grass3Tile;
	public GameObject dirtTile;
	public GameObject dirt2Tile;
	public GameObject boulder;
	public float spawnBoulderProbability = 0;
	public GameObject tree;
	public float spawnTreeProbability = 0;
	public GameObject spawner;
	public float spawnSpawnerProbability = 0;

	private float pixelsToUnits;
	private string[,] coloredMap;
	private float[,] noiseMap;

	void Start() {
		Sprite defaultSprite = GetComponent<SpriteRenderer> ().sprite;
		pixelsToUnits = defaultSprite.rect.width / defaultSprite.bounds.size.x;
		transform.position = new Vector3 (0, 0, height / pixelsToUnits);
		GenerateMap ();
		DrawMap ();
	}

	void GenerateMap () {
		coloredMap = new string[width / tileDim, height / tileDim];
		noiseMap = new float[width / tileDim, height / tileDim];
		seed = Random.value * width;

		for (int x = 0; x < width / tileDim; x++) {
			for (int y = 0; y < height / tileDim; y++) {
				float xCoord = ((float)x * tileDim * scale / width);
				float yCoord = ((float)y * tileDim * scale / height);
				float noise = Mathf.PerlinNoise (xCoord + seed, yCoord + seed);
				noiseMap [x, y] = noise;
//				if (noise > 0.72) {
//					coloredMap [x, y] = "dirt1";
//				} else if (noise > 0.67) {
//					coloredMap [x, y] = "dirt2";
//				} else if (noise > 0.5) {
//					coloredMap [x, y] = "grass5";
//				} else if (noise > 0.3) {
//					coloredMap [x, y] = "grass4";
//				} else {
//					coloredMap [x, y] = "grass2";
//				}
				if (noise > 0.65) {
					if (shouldSpawnBoulder ()) {
						spawnGameObject (x, y, boulder);
					}
				} else if (noise < 0.3) {
					if (shouldSpawnTree ()) {
						spawnGameObject (x, y, tree);
					}
				}
				if (noise < 0.5){
					if (shouldSpawnSpawner ()) {
						spawnGameObject (x, y, spawner);
					}
				}
			}
		}
	}

	bool shouldSpawnBoulder () {
		return Random.value < spawnBoulderProbability;
	}

	bool shouldSpawnTree () {
		return Random.value < spawnTreeProbability;
	}

	bool shouldSpawnSpawner () {
		return Random.value < spawnSpawnerProbability;
	}

	void spawnGameObject (int x, int y, GameObject gameObject){
		Instantiate (gameObject, new Vector3((x * tileDim - width / 2) / pixelsToUnits, (y * tileDim - height / 2) / pixelsToUnits, y * tileDim / pixelsToUnits), Quaternion.identity);
	}

	void DrawMap () {
		for (int x = 0; x < width / tileDim; x++) {
			for (int y = 0; y < height / tileDim; y++) {
				GameObject tile;
				if (noiseMap [x, y] > 0.75f) {
					tile = dirtTile;
				} else if (noiseMap [x, y] > 0.65f) {
					tile = dirt2Tile;
				} else if (noiseMap [x, y] > 0.5f) {
					tile = grass3Tile;
				} else if (noiseMap [x, y] > 0.3f) {
					tile = grass2Tile;
				} else {
					tile = grassTile;
				}
				Instantiate (tile, new Vector3 ((x * tileDim - width / 2) / pixelsToUnits, (y * tileDim - height / 2) / pixelsToUnits, transform.position.z), Quaternion.identity);
			}
		}
	}
}