using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BulletBehaviour : MonoBehaviour {
    public float speed = 10;
    public int damage;
    public bool slow;
    public bool splash;
    public GameObject target;
	public Vector3 startPosition;
	public Vector3 targetPosition;
    private float distance;
    private float startTime;
	private game_manager gm;
    public List<GameObject> enemiesInRange;

    // Use this for initialization
    void Start () {
		startTime = Time.time;
        distance = Vector3.Distance(startPosition, targetPosition);

		if (!SceneManager.GetActiveScene ().name.Equals ("menu")) {
			gm = GameObject.Find ("GameManager").GetComponent<game_manager> ();
		}
    }

    private void Update()
    {
        float timeInterval = Time.time - startTime;
        gameObject.transform.position = Vector3.Lerp(startPosition, targetPosition, timeInterval * speed / distance);

        // 2 
		if (gameObject.transform.position.Equals(targetPosition))
        {
            if (target != null)
            {
				if (!SceneManager.GetActiveScene ().name.Equals ("menu")) {
					Transform healthBarTransform = target.transform.Find ("HealthBar");
					HealthBar healthBar = healthBarTransform.gameObject.GetComponent<HealthBar> ();
					healthBar.Damage (damage);
					gm.AddScore (7);
				    if (slow == true)
                    {
                        if (target.GetComponent<enemy_movement>().speed >= 1)
                        {
                            target.GetComponent<enemy_movement>().speed = target.GetComponent<enemy_movement>().speed - (float)0.5;
                        }
                        else
                        {
                            target.GetComponent<enemy_movement>().speed = 1;
						}
                    }
                    if (splash == true)
                    {
                        int i = 0;
                        while (i < enemiesInRange.Count)
                        {
                            Transform healthTransform = enemiesInRange[i].transform.Find("HealthBar");
                            HealthBar health = healthTransform.gameObject.GetComponent<HealthBar>();
                            health.Damage(damage);
                            if (health.GetHealth() <= 0 && enemiesInRange[i].GetComponent<enemy_movement>().isDying)
                            {
                                health.SetHealth(0);
                                enemiesInRange[i].GetComponent<BoxCollider2D>().isTrigger = false;
                                enemiesInRange[i].GetComponent<enemy_movement>().stopMove = 1;
                                enemiesInRange[i].GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                                enemiesInRange[i].GetComponent<Animator>().SetInteger("state", 2);
                                Destroy(enemiesInRange[i], enemiesInRange[i].GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
                                gm.AddCash(5);

                                if (enemiesInRange[i].name.Equals("enemy6(Clone)"))
                                {
                                    gm.slimeBabiesAlive -= 1;
                                }

								if (!gm.CheckEnemiesAlive(1) && gm.lastWaveStarted == 0){
									gm.StartCountDown();
									if (gm.waves == 14) {
										gm.lastWaveStarted = 1;
									}
								}
                            }
                            i++;
                        }
                    }
                    Debug.Log(target.GetComponent<enemy_movement>().isDying);
					if (healthBar.GetHealth () <= 0 && !target.GetComponent<enemy_movement>().isDying) {
						healthBar.SetHealth (0);
						target.GetComponent<BoxCollider2D> ().isTrigger = false;
						target.GetComponent<enemy_movement> ().stopMove = 1;
						target.GetComponent<enemy_movement> ().isDying = true;
						target.GetComponent<Rigidbody2D> ().velocity = Vector3.zero;
						target.GetComponent<Animator> ().SetInteger ("state", 2);
						Destroy (target, target.GetComponent<Animator> ().GetCurrentAnimatorStateInfo (0).length);

						gm.AddCash (5);

						if (target.name.Equals ("enemy6(Clone)")) {
							gm.slimeBabiesAlive -= 1;
						}
							
						if (!SceneManager.GetActiveScene ().name.Equals ("menu")) {
							if (!gm.CheckEnemiesAlive (2) && gm.lastWaveStarted == 0) {
								gm.StartCountDown();
								if (gm.waves == 14) {
									gm.lastWaveStarted = 1;
								}
							}
						}
					}
				}
            }
			Destroy(gameObject);
        }
    }

    // 1 
    void OnTriggerEnter2D(Collider2D other)
    {
        // 2
        if (other.gameObject.tag.Equals("Enemy"))
        {
            enemiesInRange.Add(other.gameObject);
        }
    }
    // 3
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Enemy"))
        {
            enemiesInRange.Remove(other.gameObject);
        }
    }

    // Update is called once per frame
    /*void FixedUpdate () {
        float timeInterval = Time.time - startTime;
		gameObject.transform.position = Vector2.MoveTowards (startPosition, targetPosition, timeInterval * speed / distance);
        
		if (gameObject.transform.position.x == targetPosition.x) 
        {
            if (target != null)
            {
            }
            Destroy(gameObject);
        }
    }*/
}
