using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class game_manager : MonoBehaviour {

	public int lives;
	public int score;
	public int waves;
	public int cash;

	public Text livesText;
	public Text scoreText;
	public Text wavesText;
	public Text goScoreText;
	public Text goWavesText;
	public Text cashText;
	public Text countdownText;
	public int difficulty;
	private GameObject gameOverText;
	public int victory = 0;
	public int lastWaveStarted = 0;

	public enemy_spawning spawner;

	public bool newHighScore;
	public int slimeBabiesAlive;

	private int optionMenu;

	public GameObject lightningBolt;

	// Audio Stuff
	private AudioSource audSource;
	public AudioClip clickSound;
	public AudioClip constructionSound;
	public AudioClip upgradeSound;
	public AudioClip finalUpgradeSound;
	public AudioClip gameOverSound;
	public AudioClip waveCompleteSound;
	public AudioClip victorySound;
	public AudioClip thunderSound;


	// Use this for initialization
	void Start () {
		AudioSource musicAudio = GameObject.Find ("Music_Control").GetComponent<AudioSource> ();
		musicAudio.volume = PlayerPrefs.GetInt ("Music") / 100f;

		SetOptionsSliders ();
		gameOverText = GameObject.Find ("GameOver_Text");

		gameOverText.SetActive (false);
		audSource = GameObject.Find ("Main Camera").GetComponent<AudioSource> ();

		//lives = 10;
		//score = 0;
		//waves = 10;
		//cash = 1000;
		slimeBabiesAlive = 0;
		newHighScore = false;

		StartCountDown ();
		getDifficulty ();
	}
	
	// Update is called once per frame
	void Update () {

	}
	public void getDifficulty(){
		difficulty = PlayerPrefs.GetInt ("diff",2);
	}
	public void AddCash(int amt) {
		cash += amt;
		cashText.text = "Cash: " + System.Convert.ToString (cash);
	}

    public void SubCash(int amt)
    {
        if (amt <= cash) {
            cash -= amt;
            cashText.text = "Cash: " + System.Convert.ToString(cash);
        }
	}

	public void AddScore(int amt) {
		score += amt;
		scoreText.text = "Score: " + System.Convert.ToString (score);
		goScoreText.text = scoreText.text;
	}

	public bool CheckEnemiesAlive(int type) {
		int alive = GameObject.FindGameObjectsWithTag ("Enemy").Length;

		GameObject[] enemies = GameObject.FindGameObjectsWithTag ("Enemy");
		foreach (GameObject enem in enemies) {
			if (enem.GetComponent<enemy_movement> ().isDying) {
				alive -= 1;
			}
		}

		if (waves != 5) {
			if (alive == 0 && spawner.allEnemiesSpawned) {
				if (waves == 15) {
					victory = 1;
					GameOver ();
				}
				return false;
			} else {
				return true;
			}
		} else {
			if (spawner.allEnemiesSpawned && slimeBabiesAlive == 0) {
				return false;
			} else {
				return true;
			}
		}
	}
	public void setEasy(){
		difficulty = 8;
	}
	public void setMed(){

	}
	public void setHard(){

	}
	public void StartCountDown() {
		if (lives > 0) {
			GameObject[] bullets = GameObject.FindGameObjectsWithTag ("Bullet");
			foreach (GameObject blt in bullets) {
				Destroy (blt);
			}

			if (waves != 0) {
				playWaveCompleteSound ();
				AddCash (30);
			}

			countdownText.enabled = true;
			StartCoroutine (WaitForWave (1));
		}
	}

	private IEnumerator WaitForWave(float time) {
		for (int i = 5; i > 0; i--) {
			countdownText.text = System.Convert.ToString (i);
			yield return new WaitForSeconds (time);
		}
		countdownText.enabled = false;
		waves += 1;
		wavesText.text = "- Wave " + System.Convert.ToString (waves) + " -";
		goWavesText.text = "Waves: " + System.Convert.ToString (waves);
		wavesText.gameObject.GetComponent<Animation> ().Play ();
		yield return new WaitForSeconds (time);
		spawner.allEnemiesSpawned = false;
		spawner.enemiesToSpawn = 10;
	}

	public void GameOver() {

		gameOverText.SetActive (true);
		if (victory == 1) {
			playVictorySound ();
			GameObject.Find ("go_text").GetComponent<Text> ().text = "Victory";
		} else {
			playGameOverSound ();
		}
		GameObject.Find ("GameOver_Text").GetComponent<Animation> ().Play ();
		GameObject.Find ("Score_Back").GetComponent<RawImage> ().enabled = false;
		GameObject.Find ("Lives_Cash_Back").GetComponent<RawImage> ().enabled = false;
		if (GameObject.Find("Heart") != null)
		GameObject.Find ("Heart").SetActive (false);
		if (GameObject.Find("Money") != null)
		GameObject.Find ("Money").SetActive (false);

		scoreText.enabled = false;
		livesText.enabled = false;
		cashText.enabled = false;
		spawner.allEnemiesSpawned = true;

		GameObject[] enemies = GameObject.FindGameObjectsWithTag ("Enemy");
		foreach (GameObject enem in enemies) {
			enem.GetComponent<enemy_movement> ().enabled = false;
		}

		GameObject[] towers = GameObject.FindGameObjectsWithTag ("Tower");
		foreach (GameObject twr in towers) {
			Destroy (twr);
		}

		GameObject[] Bullets = GameObject.FindGameObjectsWithTag ("Bullet");
		foreach (GameObject twr in towers) {
			Destroy (twr);
		}
			
		//StartCoroutine (GetComponent<game_over> ().getHsFromDb ());
		GetComponent<game_over> ().getHsFromPlayerPrefs ();
	}
	public void Retry() {
		SceneManager.LoadScene (SceneManager.GetActiveScene ().name);

		if (newHighScore && !GameObject.Find("Options_Canvas").GetComponent<Canvas>().enabled) {
			//StartCoroutine (GetComponent <game_over> ().addHsToDb ());
			GetComponent<game_over>().addHsToPlayerPrefs();
		}
	}

	public void BackToMain() {
		SceneManager.LoadScene ("menu");

		if (newHighScore && !GameObject.Find("Options_Canvas").GetComponent<Canvas>().enabled) {
			//StartCoroutine (GetComponent <game_over> ().addHsToDb ());
			GetComponent<game_over>().addHsToPlayerPrefs();
		}
	}

	public void playConstructionSound() {
		audSource.PlayOneShot (constructionSound, PlayerPrefs.GetInt("Sound")/100f);
	}

	public void playGameOverSound() {
		audSource.PlayOneShot (gameOverSound, PlayerPrefs.GetInt("Sound")/100f);
	}
	public void playUpgradeSound()
	{
		audSource.PlayOneShot (upgradeSound, PlayerPrefs.GetInt("Sound")/100f);
	}
	public void playFinalUpgradeSound()
	{
		audSource.PlayOneShot (finalUpgradeSound, PlayerPrefs.GetInt("Sound")/100f);
	}
	public void playVictorySound() {
		audSource.PlayOneShot (victorySound, PlayerPrefs.GetInt("Sound")/100f);
	}

	public void playWaveCompleteSound() {
		audSource.PlayOneShot (waveCompleteSound, PlayerPrefs.GetInt("Sound")/100f);
	}

	private void SetOptionsSliders() {
		int music = PlayerPrefs.GetInt ("Music");
		GameObject.Find ("Music_Slider").GetComponent<Slider> ().value = music;
		GameObject.Find ("Music_Percent").GetComponent<Text> ().text = System.Convert.ToString (music) + "%";

		int sound = PlayerPrefs.GetInt ("Sound");
		GameObject.Find ("Sound_Slider").GetComponent<Slider> ().value = sound;
		GameObject.Find ("Sound_Percent").GetComponent<Text> ().text = System.Convert.ToString (sound) + "%";
	}

	public void ShowCloseOptions(int btn) {
		audSource.PlayOneShot (clickSound, PlayerPrefs.GetInt("Sound")/100f);
		if (btn == 1) {
			GameObject.Find ("Options_Canvas").GetComponent<Canvas> ().enabled = true;
		} else {
			GameObject.Find ("Options_Canvas").GetComponent<Canvas> ().enabled = false;
		}
	}

	public void OptionsSliderChange(int slider) {
		// 1 = Music
		if (slider == 1) {
			int nxtMusic = System.Convert.ToInt32 (GameObject.Find ("Music_Slider").GetComponent<Slider> ().value);
			PlayerPrefs.SetInt ("Music", nxtMusic);
			GameObject.Find ("Music_Percent").GetComponent<Text> ().text = System.Convert.ToString (nxtMusic) + "%";

			AudioSource musicAudio = GameObject.Find ("Music_Control").GetComponent<AudioSource> ();
			musicAudio.volume = PlayerPrefs.GetInt ("Music") / 100f;
		}
		// 2 = Sound
		else if (slider == 2) {
			int nxtSound = System.Convert.ToInt32 (GameObject.Find ("Sound_Slider").GetComponent<Slider> ().value);
			PlayerPrefs.SetInt ("Sound", nxtSound);
			GameObject.Find ("Sound_Percent").GetComponent<Text> ().text = System.Convert.ToString (nxtSound) + "%";
		}
	}

	public void OptionsRestartQuit(int btn) {
		audSource.PlayOneShot (clickSound, PlayerPrefs.GetInt("Sound")/100f);

		// Restart
		if (btn == 1) {
			GameObject.Find ("Options_Text").GetComponent<Text> ().text = "Are you sure you want to RESTART the game?";
			optionMenu = 1;
		} 
		// Quit
		else {
			GameObject.Find ("Options_Text").GetComponent<Text> ().text = "Are you sure you want to QUIT the game?";
			optionMenu = 2;
		} 

		GameObject.Find ("Options_Text").GetComponent<Text> ().enabled = true;
		GameObject.Find ("Yes_Button").GetComponent<Image> ().enabled = true;
		GameObject.Find ("Yes_Button").GetComponent<Button>().interactable = true;
		GameObject.Find ("No_Button").GetComponent<Image> ().enabled = true;
		GameObject.Find ("No_Button").GetComponent<Button>().interactable = true;
		GameObject.Find ("Yes_Text").GetComponent<Text> ().enabled = true;
		GameObject.Find ("No_Text").GetComponent<Text>().enabled = true;
		GameObject.Find ("Music_Slider").GetComponent<Slider> ().interactable = false;
		GameObject.Find ("Music_Slider").GetComponent<Slider> ().enabled = false;
		GameObject.Find ("Music_Text").GetComponent<Text> ().enabled = false;
		GameObject.Find ("Music_Back").GetComponent<Image> ().enabled = false;
		GameObject.Find ("Music_Percent").GetComponent<Text> ().enabled = false;
		GameObject.Find ("Sound_Slider").GetComponent<Slider> ().interactable = false;
		GameObject.Find ("Sound_Slider").GetComponent<Slider> ().enabled = false;
		GameObject.Find ("Sound_Text").GetComponent<Text> ().enabled = false;
		GameObject.Find ("Sound_Back").GetComponent<Image> ().enabled = false;
		GameObject.Find ("Sound_Percent").GetComponent<Text> ().enabled = false;
		GameObject.Find ("Close_Button").GetComponent<Image> ().enabled = false;
		GameObject.Find ("Close_Button").GetComponent<Button> ().interactable = false;
		GameObject.Find ("Close_Text").GetComponent<Text> ().enabled = false;
	}

	public void OptionsYesNo(int btn) {
		audSource.PlayOneShot (clickSound, PlayerPrefs.GetInt("Sound")/100f);

		// Yes
		if (btn == 1) {
			if (optionMenu == 1) {
				Retry ();
			} else {
				BackToMain ();
			}
		} 
		// No
		else if (btn == 2) {
			GameObject.Find ("Options_Text").GetComponent<Text> ().enabled = false;
			GameObject.Find ("Yes_Button").GetComponent<Image> ().enabled = false;
			GameObject.Find ("Yes_Button").GetComponent<Button>().interactable = false;
			GameObject.Find ("No_Button").GetComponent<Image> ().enabled = false;
			GameObject.Find ("No_Button").GetComponent<Button>().interactable = false;
			GameObject.Find ("Yes_Text").GetComponent<Text> ().enabled = false;
			GameObject.Find ("No_Text").GetComponent<Text>().enabled = false;
			GameObject.Find ("Music_Slider").GetComponent<Slider> ().interactable = true;
			GameObject.Find ("Music_Slider").GetComponent<Slider> ().enabled = true;
			GameObject.Find ("Music_Text").GetComponent<Text> ().enabled = true;
			GameObject.Find ("Music_Back").GetComponent<Image> ().enabled = true;
			GameObject.Find ("Music_Percent").GetComponent<Text> ().enabled = true;
			GameObject.Find ("Sound_Slider").GetComponent<Slider> ().interactable = true;
			GameObject.Find ("Sound_Slider").GetComponent<Slider> ().enabled = true;
			GameObject.Find ("Sound_Text").GetComponent<Text> ().enabled = true;
			GameObject.Find ("Sound_Back").GetComponent<Image> ().enabled = true;
			GameObject.Find ("Sound_Percent").GetComponent<Text> ().enabled = true;
			GameObject.Find ("Close_Button").GetComponent<Image> ().enabled = true;
			GameObject.Find ("Close_Button").GetComponent<Button> ().interactable = true;
			GameObject.Find ("Close_Text").GetComponent<Text> ().enabled = true;
			optionMenu = 0;
		}
	}

	public void lightning() {
		if (lives > 0) {
			audSource.PlayOneShot (thunderSound, PlayerPrefs.GetInt ("Sound") / 100f);

			GameObject[] enemies = GameObject.FindGameObjectsWithTag ("Enemy");
			foreach (GameObject enem in enemies) {
				GameObject l = Instantiate (lightningBolt, enem.transform.position, Quaternion.identity, enem.transform);
				enem.GetComponentInChildren<HealthBar> ().SetHealth (enem.GetComponentInChildren<HealthBar> ().GetHealth () / 1.25f);
				Destroy (l, 0.3f);
			}

			GameObject.Find ("Lightning").GetComponent<Animation> ().Play ("lightning");
		}
	}
}
