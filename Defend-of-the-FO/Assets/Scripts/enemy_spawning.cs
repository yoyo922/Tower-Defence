using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_spawning : MonoBehaviour {

	public GameObject enemy1;
	public GameObject enemy2;
	public GameObject enemy3;
	public GameObject enemy4;
	public GameObject enemy5;
	public GameObject enemy6;
	public GameObject enemy7;
	public GameObject enemy8;
	public GameObject enemy9;
	public GameObject enemy10;
	public GameObject enemy11;
	public GameObject enemy12;
	public GameObject enemy13;
	public GameObject enemy14;
	public GameObject enemy15;

	Vector2 whereToSpawn;
	public float spawnRate = 2f;
	float nextSpwan = 0.0f;

	public int enemiesToSpawn;

	public bool allEnemiesSpawned;

	private game_manager gm;

	// Use this for initialization
	void Start () {
		allEnemiesSpawned = false;
		gm = GameObject.Find ("GameManager").GetComponent<game_manager> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time > nextSpwan && enemiesToSpawn > 0 && !allEnemiesSpawned) {
			nextSpwan = Time.time + spawnRate;
			whereToSpawn = gameObject.transform.position;
			if (gm.waves == 1) {
				Instantiate (enemy1, whereToSpawn, Quaternion.identity);
			} else if (gm.waves == 2) {
				Instantiate (enemy2, whereToSpawn, Quaternion.identity);
			} else if (gm.waves == 3) {
				Instantiate (enemy3, whereToSpawn, Quaternion.identity);
			} else if (gm.waves == 4) {
				Instantiate (enemy4, whereToSpawn, Quaternion.identity);
			} else if (gm.waves == 5) {
				Instantiate (enemy5, whereToSpawn, Quaternion.identity);
			} else if (gm.waves == 6) {
				Instantiate (enemy6, whereToSpawn, Quaternion.identity);
			} else if (gm.waves == 7) {
				Instantiate (enemy7, whereToSpawn, Quaternion.identity);
			} else if (gm.waves == 8) {
				Instantiate (enemy8, whereToSpawn, Quaternion.identity);
			} else if (gm.waves == 9) {
				Instantiate (enemy9, whereToSpawn, Quaternion.identity);
			}else if (gm.waves == 10) {
				Instantiate (enemy10, whereToSpawn, Quaternion.identity);
			}else if (gm.waves == 11) {
				Instantiate (enemy11, whereToSpawn, Quaternion.identity);
			}else if (gm.waves == 12) {
				Instantiate (enemy12, whereToSpawn, Quaternion.identity);
			}else if (gm.waves == 13) {
				Instantiate (enemy13, whereToSpawn, Quaternion.identity);
			}else if (gm.waves == 14) {
				Instantiate (enemy14, whereToSpawn, Quaternion.identity);
			}else if (gm.waves == 15) {
				Debug.Log ("WTF?");
				Instantiate (enemy15, whereToSpawn, Quaternion.identity);
			}

			/*if (gm.waves % 2 == 0) {
				Instantiate (enemy2, whereToSpawn, Quaternion.identity);
			} else {
				Instantiate (enemy1, whereToSpawn, Quaternion.identity);
			}*/
			enemiesToSpawn -= 1;

			if (enemiesToSpawn == 0) {
				allEnemiesSpawned = true;
			}
		}
	}
}

