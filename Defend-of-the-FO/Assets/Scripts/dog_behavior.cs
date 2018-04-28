using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dog_behavior : MonoBehaviour {

	// Use this for initialization
	private int isDamaged = 0;
	int Time = 0;
	private Animator anim;
	private HealthBar healthBar;
	void Start () {
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		Transform healthBarTransform = transform.Find ("HealthBar");
		healthBar = healthBarTransform.gameObject.GetComponent<HealthBar> ();
		if (isDamaged == 0) {
			if (healthBar.maxHealth/healthBar.currentHealth != 1 ) {
				gameObject.GetComponent<enemy_movement> ().speed = 4;
				anim.SetInteger ("state", 1);
				Time = Time + 1;
				if (Time > 75) {
					anim.SetInteger ("state", 0);
					gameObject.GetComponent<enemy_movement> ().speed = 2;
					isDamaged = 1;
				}
			}
		}
	}
}
