using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class enemy_movement : MonoBehaviour {

	public float speed = 5f;

	public Vector2[] movePositions = new Vector2[12];
	public Vector2[] movePositions2 = new Vector2[5];
	public Vector2[] movePositions3 = new Vector2[5];

    private int currentWaypoint = 0;
	public int nextMove = 0;
    private game_manager gm;
	public int stopMove = 0;
	public int moveState = 0;

	public float deathTimer;

	public bool isDying = false;

	Animator anim;

	// Use this for initialization
	void Start () {
		if (!SceneManager.GetActiveScene ().name.Equals ("menu")) {
			gm = GameObject.Find ("GameManager").GetComponent<game_manager> ();
		}

		anim = GetComponent<Animator> ();
	}

	// Update is called once per frame

	void FixedUpdate () {
		if (SceneManager.GetActiveScene ().name.Equals ("map1_master") || SceneManager.GetActiveScene().name.Equals("menu")) {
			Vector2 nextPosition;
			string name = gameObject.name;
			if (name == "enemy6(Clone)") {
				nextPosition = movePositions [nextMove];
			} else {
				nextPosition = movePositions [0];
			}
			if (stopMove == 0) {
				transform.position = Vector2.MoveTowards (transform.position, nextPosition, speed * Time.deltaTime);
				if (transform.position.x == nextPosition.x && transform.position.y == nextPosition.y){
					if (name != "enemy6(Clone)") {
						nextMove++;
					}
					for (int i = 0; i < movePositions.Length - 1; i++) {
						movePositions [i] = movePositions [i + 1];
						currentWaypoint++;
					}
				}
			}
		} else if (SceneManager.GetActiveScene ().name.Equals ("map2_master")) {
			Vector2 nextPosition = movePositions2 [0];
			string name = gameObject.name;
			if (name == "enemy6(Clone)") {
				nextPosition = movePositions2 [nextMove];
			} else {
				nextPosition = movePositions2 [0];
			}
			if (stopMove == 0) {
				transform.position = Vector2.MoveTowards (transform.position, nextPosition, speed * Time.deltaTime);
				if (transform.position.x == nextPosition.x && transform.position.y == nextPosition.y) {
					if (name != "enemy6(Clone)") {
						nextMove++;
					}
					for (int i = 0; i < movePositions2.Length - 1; i++) {
						movePositions2 [i] = movePositions2 [i + 1];
						currentWaypoint++;

					}
				}
			}
		} else if (SceneManager.GetActiveScene ().name.Equals ("map3_master")) {
			Vector2 nextPosition = movePositions3 [0];
			string name = gameObject.name;
			if (name == "enemy6(Clone)") {
				nextPosition = movePositions3 [nextMove];
			} else {
				nextPosition = movePositions3 [0];
			}
			if (stopMove == 0) {
				transform.position = Vector2.MoveTowards (transform.position, nextPosition, speed * Time.deltaTime);
				if (transform.position.x == nextPosition.x && transform.position.y == nextPosition.y) {
					if (name != "enemy6(Clone)") {
						nextMove++;
					}
					for (int i = 0; i < movePositions3.Length - 1; i++) {
						movePositions3 [i] = movePositions3 [i + 1];
						currentWaypoint++;

					}
				}
			}
		}
	}

	void OnTriggerEnter2D(Collider2D col) {
		if (!SceneManager.GetActiveScene ().name.Equals ("menu")) {
			if (col.gameObject.tag.Equals ("End")) {
				if (gm.lives > 0) {
					isDying = true;

					GetComponent<BoxCollider2D> ().enabled = false;
					GetComponent<CircleCollider2D> ().enabled = false;
					if (gm.waves == 5) {
						gm.slimeBabiesAlive -= 1;
					}
					gm.lives -= 1;
					gm.livesText.text = "Lives: " + System.Convert.ToString (gm.lives);

					//StartCoroutine (waitForDeath (deathTimer));
					if (!SceneManager.GetActiveScene ().name.Equals ("menu")) {
						if (!gm.CheckEnemiesAlive (1)) {
							gm.StartCountDown();
						}
					}
					if (gm.lives == 0) {
						gm.GameOver ();
					}
				}
			}
		}
	}

	/*public IEnumerator waitForDeath(float time) {
		Debug.Log ("1");
		Debug.Log (time);
		yield return new WaitForSeconds (time);
		Debug.Log ("1.5");
		if (!SceneManager.GetActiveScene ().name.Equals ("menu")) {
			if (!gm.CheckEnemiesAlive (2)) {
				gm.StartCountDown();
			}
		}
	}*/

    public float distanceToGoal()
    {
        float distance = 10;
        /*distance += Vector3.Distance(
            gameObject.transform.position,
            movePositions[currentWaypoint]);
        for (int i = currentWaypoint; i < movePositions.Length - 1; i++)
        {
            Vector3 startPosition = movePositions[i];
            Vector3 endPosition = movePositions[i + 1];
            distance += Vector3.Distance(startPosition, endPosition);
        }*/
        return distance;
    }
}
