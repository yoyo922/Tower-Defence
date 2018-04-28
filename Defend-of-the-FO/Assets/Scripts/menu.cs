using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class menu : MonoBehaviour {
	public GameObject enemy;
	Vector2 whereToSpawn;
	public float spawnRate = 2f;
	float nextSpwan = 0.0f;

	private string[] names;
	private int[] scores;

	private int selectedMap;
	private AudioSource audSource;

	public AudioClip clickSound;
	private string hs_dif;

	// Use this for initialization
	void Start () {
		selectedMap = 0;
		audSource = GameObject.Find ("Main Camera").GetComponent<AudioSource> ();
		hs_dif = "Easy";

		if (!PlayerPrefs.HasKey ("Sound")) {
			PlayerPrefs.SetInt ("Sound", 100);
		} 

		if (!PlayerPrefs.HasKey ("Music")) {
			PlayerPrefs.SetInt ("Music", 100);
		} 

		SetOptionsSliders ();
	}
	
	// Update is called once per frame
	void Update () {
		// Spawn enemies
		if (Time.time > nextSpwan) {
			nextSpwan = Time.time + spawnRate;
			whereToSpawn = gameObject.transform.position;
			Instantiate (enemy, whereToSpawn, Quaternion.identity);
		}
	}

	public void MapSelect() {
		audSource.PlayOneShot (clickSound, PlayerPrefs.GetInt("Sound")/100f);

		GameObject.Find("Main_Canvas").GetComponent<Animation>().Play("main_canvas_out");
		GameObject.Find("Map_Canvas").GetComponent<Animation>().Play("map_canvas_in");
	}

	public void HighScores() {
		audSource.PlayOneShot (clickSound, PlayerPrefs.GetInt("Sound")/100f);

		GameObject.Find("Hs_Canvas").GetComponent<Animation>().Play("hs_canvas_in");
		GameObject.Find("Main_Canvas").GetComponent<Animation>().Play("main_canvas_out");

		//StartCoroutine (getHsFromDb ());
		getHsFromPlayerPrefs(hs_dif);
	}

	public void Options() {
		audSource.PlayOneShot (clickSound, PlayerPrefs.GetInt("Sound")/100f);

		GameObject.Find("Options_Canvas").GetComponent<Animation>().Play("options_canvas_in");
		GameObject.Find("Main_Canvas").GetComponent<Animation>().Play("main_canvas_out");
	}

	private void SetOptionsSliders() {
		int music = PlayerPrefs.GetInt ("Music");
		GameObject.Find ("Music_Slider").GetComponent<Slider> ().value = music;
		GameObject.Find ("Music_Percent").GetComponent<Text> ().text = System.Convert.ToString (music) + "%";

		int sound = PlayerPrefs.GetInt ("Sound");
		GameObject.Find ("Sound_Slider").GetComponent<Slider> ().value = sound;
		GameObject.Find ("Sound_Percent").GetComponent<Text> ().text = System.Convert.ToString (sound) + "%";
	}

	public void OptionsSliderChange(int slider) {
		// 1 = Music
		if (slider == 1) {
			int nxtMusic = System.Convert.ToInt32 (GameObject.Find ("Music_Slider").GetComponent<Slider> ().value);
			PlayerPrefs.SetInt ("Music", nxtMusic);
			GameObject.Find ("Music_Percent").GetComponent<Text> ().text = System.Convert.ToString (nxtMusic) + "%";
		}
		// 2 = Sound
		else if (slider == 2) {
			int nxtSound = System.Convert.ToInt32 (GameObject.Find ("Sound_Slider").GetComponent<Slider> ().value);
			PlayerPrefs.SetInt ("Sound", nxtSound);
			GameObject.Find ("Sound_Percent").GetComponent<Text> ().text = System.Convert.ToString (nxtSound) + "%";
		}
	}

	IEnumerator getHsFromDb()
	{
		string[] users;
		string[] fields = {"Name: ", "Score: "};
		string address;

		/*#if UNITY_EDITOR
		//address = "http://192.168.0.10/dotf/geths.php";
			address = "https://scrubbylol.github.io/Randy-Nguyen-Website/php/connection.php";
		#else
			address = "http://192.168.0.10/dotf/geths.php";
		#endif

		WWW con = new WWW (address);

		yield return con; */

		#if UNITY_EDITOR
		address = "http://192.168.0.10/dotf/geths.php";
		//address = "https://scrubbylol.github.io/Randy-Nguyen-Website/geths.php";
		#else
		address = "http://192.168.0.10/dotf/geths.php";
		#endif

		WWW hs = new WWW (address);

		yield return hs;

		//Debug.Log (con.text);
		Debug.Log (hs.text);

		if (hs.error == null) {
			Debug.Log(hs.error);
		}

		string hsString = hs.text;
		//Debug.Log (hsString);

		// Dynamically place strings in array separated by ";" from PHP data file
		users = hsString.Split (';');
		names = new string[users.Length];
		scores = new int[users.Length];

		for (int i = 0; i < 10; i++) {
			scores [i] = 0;
		}

		// Loop for each user, for each field
		for (int k = 0; k < users.Length - 1; k++) {
			for (int i = 0; i < fields.Length; i++) {
				string value = getDataValue (users [k], fields [i]);
				//Debug.Log (value);

				if (i == 0) {
					names [k] = value;
				} else if (i == 1) {
					scores [k] = System.Convert.ToInt32 (value);
				}
			}
			//Debug.Log ("------------");
		}

		sort ();

		GameObject.Find("Name_Body").GetComponent<Text> ().text =  "";
		GameObject.Find("Score_Body").GetComponent<Text> ().text =  "";

		for (int i = 0; i < 10; i++) {
			if (names [i] != null) {
				GameObject.Find ("Name_Body").GetComponent<Text> ().text += names [i] + ((i!=9) ? "\n" : "");
			} else {
				GameObject.Find ("Name_Body").GetComponent<Text> ().text += "xxx" + ((i!=9) ? "\n" : "");
			}

			if (i != 10) {

			}

			GameObject.Find ("Score_Body").GetComponent<Text> ().text += scores[i] + ((i!=9) ? "\n" : "");
		}
	}

	public void getHsFromPlayerPrefs (string dif) {
		ArrayList tmpScores = new ArrayList ();
		ArrayList tmpNames = new ArrayList ();
		ArrayList tmpDiffculties = new ArrayList ();
		int i = 1;

		while (PlayerPrefs.HasKey("hs_" + i + "_name")) {
			if (PlayerPrefs.GetString ("hs_" + i + "_difficulty").Equals (dif)) {
				tmpScores.Add (PlayerPrefs.GetInt ("hs_" + i + "_score"));
				tmpNames.Add (PlayerPrefs.GetString ("hs_" + i + "_name"));
			}
			i++;
		}

		scores = new int[tmpScores.Count];
		names = new string[tmpNames.Count];

		for (int j =0;j<scores.Length;j++) {
			scores [j] = System.Convert.ToInt32 (tmpScores [j]);
			names [j] = System.Convert.ToString (tmpNames [j]);
		}

		sort ();

		GameObject.Find("Name_Body").GetComponent<Text> ().text =  "";
		GameObject.Find("Score_Body").GetComponent<Text> ().text =  "";

		for (int k = 0; k < names.Length; k++) {
			GameObject.Find ("Name_Body").GetComponent<Text> ().text += names [k] + "\n";
			GameObject.Find ("Score_Body").GetComponent<Text> ().text += scores[k] + "\n";
		}

		if (names.Length < 10) {
			int len = 10 - names.Length;
			for (int l = 0; l < len; l++) {
				GameObject.Find ("Name_Body").GetComponent<Text> ().text += "xxx" + ((l!=len-1) ? "\n" : "");
				GameObject.Find ("Score_Body").GetComponent<Text> ().text += "0" + ((l!=len-1) ? "\n" : "");
			}
		}
	}

	// Bubble sort
	private void sort() {
		for(int x = 0; x < scores.Length; x++)
		{
			for(int y = 0; y<scores.Length - 1; y++)
			{
				if (scores[y] < scores[y+1])
				{
					int temp = scores[y+1];
					string temp2 = names[y+1];
					scores[y+1] = scores[y];
					scores[y] = temp;

					names [y+1] = names [y];
					names [y] = temp2;
				}
			}
		}

		for (int z = 0; z < scores.Length; z++) {
			//Debug.Log (scores [z]);
			//Debug.Log (names [z]);
		}
	}

	// Return data values from Users fields
	private string getDataValue(string data, string index) {
		string value = data.Substring (data.IndexOf (index) + index.Length);
		if (value.Contains("|"))
			value = value.Remove(value.IndexOf("|"));
		return value;
	}

	public void ClickMap(int map) {
		audSource.PlayOneShot (clickSound, PlayerPrefs.GetInt("Sound")/100f);

		if (map == 1) {
			GameObject.Find ("Map1_Select").GetComponent<RawImage> ().enabled = true;
			GameObject.Find ("Map2_Select").GetComponent<RawImage> ().enabled = false;
			GameObject.Find ("Map3_Select").GetComponent<RawImage> ().enabled = false;
		} else if (map == 2) {
			GameObject.Find ("Map1_Select").GetComponent<RawImage> ().enabled = false;
			GameObject.Find ("Map2_Select").GetComponent<RawImage> ().enabled = true;
			GameObject.Find ("Map3_Select").GetComponent<RawImage> ().enabled = false;
		} else if (map == 3) {
			GameObject.Find ("Map1_Select").GetComponent<RawImage> ().enabled = false;
			GameObject.Find ("Map2_Select").GetComponent<RawImage> ().enabled = false;
			GameObject.Find ("Map3_Select").GetComponent<RawImage> ().enabled = true;
		}

		selectedMap = map;
	}

	public void Back(int back) {
		audSource.PlayOneShot (clickSound, PlayerPrefs.GetInt("Sound")/100f);

		if (back == 1) {
			GameObject.Find("Map_Canvas").GetComponent<Animation>().Play("map_canvas_out");
			GameObject.Find("Main_Canvas").GetComponent<Animation>().Play("main_canvas_in");
		} else if (back == 2) {
			GameObject.Find("Hs_Canvas").GetComponent<Animation>().Play("hs_canvas_out");
			GameObject.Find("Main_Canvas").GetComponent<Animation>().Play("main_canvas_in");
		} else if (back == 3) {
			GameObject.Find("Options_Canvas").GetComponent<Animation>().Play("options_canvas_out");
			GameObject.Find("Main_Canvas").GetComponent<Animation>().Play("main_canvas_in");
		}
	}

	public void StartGame() {
		audSource.PlayOneShot (clickSound, PlayerPrefs.GetInt("Sound")/100f);

		if (selectedMap == 1) {
			SceneManager.LoadScene ("map1_master");
		} else if (selectedMap == 2) {
			SceneManager.LoadScene ("map2_master");
		} else if (selectedMap == 3) {
			SceneManager.LoadScene ("map3_master");
		}
	}

	public void EndGame() {
		audSource.PlayOneShot (clickSound, PlayerPrefs.GetInt("Sound")/100f);

		#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
		#else
		Application.Quit();
		#endif
	}

	public void SetDif (int mode) {
		audSource.PlayOneShot (clickSound, PlayerPrefs.GetInt("Sound")/100f);
		PlayerPrefs.SetInt ("diff", mode);
	}

	public void Hs_Dif(int dir) {
		string dif = GameObject.Find ("Hs_Difficulty_Text").GetComponent<Text> ().text;

		// Right
		if (dir == 1) {
			if (dif.Equals ("Easy")) {
				GameObject.Find ("Hs_Difficulty_Text").GetComponent<Text> ().text = "Medium";
				audSource.PlayOneShot (clickSound, PlayerPrefs.GetInt("Sound")/100f);
				hs_dif = "Medium";
				getHsFromPlayerPrefs (hs_dif);
			} else if (dif.Equals ("Medium")) {
				GameObject.Find ("Hs_Difficulty_Text").GetComponent<Text> ().text = "Hard";
				audSource.PlayOneShot (clickSound, PlayerPrefs.GetInt("Sound")/100f);
				hs_dif = "Hard";
				getHsFromPlayerPrefs (hs_dif);
			}
		} 
		// Left
		else if (dir == 2) {
			if (dif.Equals ("Medium")) {
				GameObject.Find ("Hs_Difficulty_Text").GetComponent<Text> ().text = "Easy";
				audSource.PlayOneShot (clickSound, PlayerPrefs.GetInt("Sound")/100f);
				hs_dif = "Easy";
				getHsFromPlayerPrefs (hs_dif);
			} else if (dif.Equals ("Hard")) {
				GameObject.Find ("Hs_Difficulty_Text").GetComponent<Text> ().text = "Medium";
				audSource.PlayOneShot (clickSound, PlayerPrefs.GetInt("Sound")/100f);
				hs_dif = "Medium";
				getHsFromPlayerPrefs (hs_dif);
			}
		}
	}

}
