using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class enemy_animation_noInvert: MonoBehaviour {

	public Vector2[] movePositions = new Vector2[5];
	public Vector2 movePositions2;
	public Vector2[] movePositions3 = new Vector2[2];
	Animator anim;
	public int nextAnime = 0;
	public int whereToMove = 0;

	private game_manager gm;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		if (!SceneManager.GetActiveScene ().name.Equals ("menu")) {
			gm = GameObject.Find ("GameManager").GetComponent<game_manager> ();
			Vector3 theScale = transform.localScale;
			string name = gameObject.name;
			if (name == "enemy6(Clone)" && ((whereToMove == 2 ) || (whereToMove == 4))) {
				theScale.x = theScale.x * -1;
			}
			theScale.x = theScale.x * -1;
			transform.localScale = theScale;
			if (gm.waves == 1 || gm.waves == 2) {
				theScale.x = theScale.x * -1;
				transform.localScale = theScale;
			}
			GetComponent<enemy_movement> ().deathTimer = anim.GetCurrentAnimatorStateInfo (0).length;
		}
	}

	// Update is called once per frame
	void FixedUpdate () {
		if (SceneManager.GetActiveScene ().name.Equals ("map1_master")) {
			if ((transform.position.x == movePositions [0].x &&
				transform.position.y == movePositions [0].y) || whereToMove == 1) {
				Vector3 theScale = transform.localScale;
				theScale.x = theScale.x * -1;
				transform.localScale = theScale;
				nextAnime = 1;

			}
			if ((transform.position.x == movePositions [1].x &&
				transform.position.y == movePositions [1].y) || whereToMove == 2) {
				Vector3 theScale = transform.localScale;
				theScale.x = theScale.x * -1;
				transform.localScale = theScale;
				nextAnime = 2;
			}
			if (transform.position.x == movePositions [2].x &&
				transform.position.y == movePositions [2].y || whereToMove == 3) {
				Vector3 theScale = transform.localScale;
				theScale.x = theScale.x * -1;
				transform.localScale = theScale;
				nextAnime = 3;
			}
			if (transform.position.x == movePositions [3].x &&
				transform.position.y == movePositions [3].y || whereToMove == 4) {
				Vector3 theScale = transform.localScale;
				theScale.x = theScale.x * -1;
				transform.localScale = theScale;
				nextAnime = 4;
			}
			if (transform.position.x == movePositions [4].x &&
				transform.position.y == movePositions [4].y && anim.GetInteger("state") != 2) {
				anim.SetInteger ("state", 2);
				Destroy (this.gameObject, anim.GetCurrentAnimatorStateInfo (0).length);
			}
			whereToMove = 0;
		} else if (SceneManager.GetActiveScene ().name.Equals ("map2_master")) {
			if (transform.position.x == movePositions2.x &&
				transform.position.y == movePositions2.y && anim.GetInteger("state") != 2) {
				anim.SetInteger ("state", 2);
				Destroy (this.gameObject, anim.GetCurrentAnimatorStateInfo (0).length);
			}
		} else if (SceneManager.GetActiveScene ().name.Equals ("map3_master")) {
			if ((transform.position.x == movePositions3 [0].x &&
				transform.position.y == movePositions3 [0].y) || whereToMove == 1) {
				Vector3 theScale = transform.localScale;
				theScale.x = theScale.x * -1;
				transform.localScale = theScale;
				nextAnime = 1;
			} else if (transform.position.x == movePositions3[1].x &&
				transform.position.y == movePositions3[1].y && anim.GetInteger("state") != 2) {
				anim.SetInteger ("state", 2);
				Destroy (this.gameObject, anim.GetCurrentAnimatorStateInfo (0).length);
			}
		}
	}
}
