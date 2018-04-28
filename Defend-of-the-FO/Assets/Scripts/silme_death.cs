using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class silme_death : MonoBehaviour {

	// Use this for initialization
	public GameObject enemy6;
	Vector2 whereToSpawn;
	public int spawned = 0;
	int moveState;
	int animeState;

	void Start () {
		GameObject.Find ("GameManager").GetComponent<game_manager> ().slimeBabiesAlive += 1;
	}
	
	// Update is called once per frame
	void Update () {
		Transform healthBarTransform = transform.Find ("HealthBar");
		HealthBar healthBar = healthBarTransform.gameObject.GetComponent<HealthBar> ();
		if (healthBar.GetHealth () <= 0) {
			if (spawned == 0) {
				moveState = GetComponent<enemy_movement>().nextMove;
				animeState = GetComponent<enemy_animation_noInvert> ().nextAnime;
				enemy6.gameObject.GetComponent<enemy_movement> ().nextMove = moveState;
				enemy6.gameObject.GetComponent<enemy_animation_noInvert> ().whereToMove = animeState;
				Instantiate (enemy6, gameObject.transform.position, Quaternion.identity);
				spawned = 1;
			}
		}
	}
}
