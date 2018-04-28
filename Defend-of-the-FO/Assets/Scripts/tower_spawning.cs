using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class tower_spawning : MonoBehaviour {

    //Tower Prefabs
    public GameObject wizard_towerPrefab;
    private GameObject wizard_tower;
    public GameObject splash_towerPrefab;
    private GameObject splash_tower;
    public GameObject archer_towerPrefab;
    private GameObject archer_tower;

    //Game Manager
    private game_manager gm;

    //Tower Type Selection
    public Button archer_button;
    public Button wizard_button;
    public Button splash_button;
    public Button exit_button; 

    private Button archer_select;
    private Button wizard_select;
    private Button splash_select;
    private Button exit_select;

    private GameObject tower_select;
	public AudioClip upgradeSound;
	public AudioClip finalUpgradeSound;

    // Use this for initialization

    void Awake () {
        gm = GameObject.Find("GameManager").GetComponent<game_manager>();
    }
	
	// Update is called once per frame
	void Update () {
    }
    private bool canPlaceTower()
    {
		return archer_tower == null && splash_tower == null && wizard_tower == null && gm.cash >= 30 && !GameObject.Find ("Options_Canvas").GetComponent<Canvas> ().enabled;
    }

    private bool needUI()
    {
        return archer_select == null && splash_select == null && wizard_select == null && exit_select == null;
    }
    void OnMouseUp()
    {
           //2
        if (canPlaceTower() && needUI())
        {
            //3
			
            archer_select = Instantiate(archer_button, new Vector2(-2,0), Quaternion.identity);
            wizard_select = Instantiate(wizard_button, new Vector2(2, 0), Quaternion.identity);
            splash_select = Instantiate(splash_button, new Vector2(0, 3), Quaternion.identity);
            exit_select = Instantiate(exit_button, new Vector2(0, -3), Quaternion.identity);
            tower_select = GameObject.Find("Tower_Canvas");
            archer_select.transform.SetParent(tower_select.transform);
            wizard_select.transform.SetParent(tower_select.transform);
            splash_select.transform.SetParent(tower_select.transform);
            exit_select.transform.SetParent(tower_select.transform);
            archer_select.onClick.AddListener(placeArcherTower);
            wizard_select.onClick.AddListener(placeWizardTower);
            splash_select.onClick.AddListener(placeSplashTower);
            exit_select.onClick.AddListener(exitSelect);

        }
        else if (canUpgradeArcherTower())
        {
            archer_tower.GetComponent<TowerData>().increaseLevel();
            if (archer_tower.GetComponent<TowerData>().levels.IndexOf(archer_tower.GetComponent<TowerData>().CurrentLevel) == 1)
            {
				gm.playUpgradeSound ();
				gm.SubCash(60);
            }
            else if (archer_tower.GetComponent<TowerData>().levels.IndexOf(archer_tower.GetComponent<TowerData>().CurrentLevel) == 2)
            {
				gm.playFinalUpgradeSound ();
				gm.SubCash(90);
            }
            // TODO: Deduct gold
        }
        else if (canUpgradeWizardTower())
        {
			
            wizard_tower.GetComponent<TowerData>().increaseLevel();
            if (wizard_tower.GetComponent<TowerData>().levels.IndexOf(wizard_tower.GetComponent<TowerData>().CurrentLevel) == 1)
            {
				gm.playUpgradeSound ();
				gm.SubCash(90);
            }
            else if (wizard_tower.GetComponent<TowerData>().levels.IndexOf(wizard_tower.GetComponent<TowerData>().CurrentLevel) == 2)
            {
				gm.playFinalUpgradeSound ();
				gm.SubCash(150);
            }
            // TODO: Deduct gold
        }
        else if (canUpgradeSplashTower())
        {

            splash_tower.GetComponent<TowerData>().increaseLevel();
            if (splash_tower.GetComponent<TowerData>().levels.IndexOf(splash_tower.GetComponent<TowerData>().CurrentLevel) == 1)
            {
                gm.playUpgradeSound();
                gm.SubCash(100);
            }
            else if (splash_tower.GetComponent<TowerData>().levels.IndexOf(splash_tower.GetComponent<TowerData>().CurrentLevel) == 2)
            {
                gm.playFinalUpgradeSound();
                gm.SubCash(170);
            }
            // TODO: Deduct gold
        }
    }
    private void exitSelect()
    {
        archer_select.GetComponentInChildren<Button>().enabled = false;
        archer_select.GetComponentInChildren<Image>().enabled = false;
        archer_select.GetComponentInChildren<Text>().enabled = false;
        wizard_select.GetComponentInChildren<Button>().enabled = false;
        wizard_select.GetComponentInChildren<Image>().enabled = false;
        wizard_select.GetComponentInChildren<Text>().enabled = false;
        splash_select.GetComponentInChildren<Button>().enabled = false;
        splash_select.GetComponentInChildren<Image>().enabled = false;
        splash_select.GetComponentInChildren<Text>().enabled = false;
        exit_select.GetComponentInChildren<Button>().enabled = false;
        exit_select.GetComponentInChildren<Image>().enabled = false;
        exit_select.GetComponentInChildren<Text>().enabled = false;
        archer_select = null;
        wizard_select = null;
        splash_select = null;
        exit_select = null;
    }
    private void placeWizardTower()
    {
		gm.playConstructionSound ();
        //3
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        wizard_tower = (GameObject)
          Instantiate(wizard_towerPrefab, transform.position, Quaternion.identity);
        //4
        gm.SubCash(50);
        archer_select.GetComponentInChildren<Button>().enabled = false;
        archer_select.GetComponentInChildren<Image>().enabled = false;
        archer_select.GetComponentInChildren<Text>().enabled = false;
        wizard_select.GetComponentInChildren<Button>().enabled = false;
        wizard_select.GetComponentInChildren<Image>().enabled = false;
        wizard_select.GetComponentInChildren<Text>().enabled = false;
        splash_select.GetComponentInChildren<Button>().enabled = false;
        splash_select.GetComponentInChildren<Image>().enabled = false;
        splash_select.GetComponentInChildren<Text>().enabled = false;
        exit_select.GetComponentInChildren<Button>().enabled = false;
        exit_select.GetComponentInChildren<Image>().enabled = false;
        exit_select.GetComponentInChildren<Text>().enabled = false;
        archer_select = null;
        wizard_select = null;
        splash_select = null;
        exit_select = null;
    }
    private void placeArcherTower()
    {
		gm.playConstructionSound ();
        //3
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        archer_tower = (GameObject)
          Instantiate(archer_towerPrefab, transform.position, Quaternion.identity);
        //4
        gm.SubCash(50);
        archer_select.GetComponentInChildren<Button>().enabled = false;
        archer_select.GetComponentInChildren<Image>().enabled = false;
        archer_select.GetComponentInChildren<Text>().enabled = false;
        wizard_select.GetComponentInChildren<Button>().enabled = false;
        wizard_select.GetComponentInChildren<Image>().enabled = false;
        wizard_select.GetComponentInChildren<Text>().enabled = false;
        splash_select.GetComponentInChildren<Button>().enabled = false;
        splash_select.GetComponentInChildren<Image>().enabled = false;
        splash_select.GetComponentInChildren<Text>().enabled = false;
        exit_select.GetComponentInChildren<Button>().enabled = false;
        exit_select.GetComponentInChildren<Image>().enabled = false;
        exit_select.GetComponentInChildren<Text>().enabled = false;
        archer_select = null;
        wizard_select = null;
        splash_select = null;
        exit_select = null;
    }

    private void placeSplashTower()
    {
        gm.playConstructionSound();
        //3
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        splash_tower = (GameObject)
          Instantiate(splash_towerPrefab, transform.position, Quaternion.identity);
        //4
        gm.SubCash(50);
        archer_select.GetComponentInChildren<Button>().enabled = false;
        archer_select.GetComponentInChildren<Image>().enabled = false;
        archer_select.GetComponentInChildren<Text>().enabled = false;
        wizard_select.GetComponentInChildren<Button>().enabled = false;
        wizard_select.GetComponentInChildren<Image>().enabled = false;
        wizard_select.GetComponentInChildren<Text>().enabled = false;
        splash_select.GetComponentInChildren<Button>().enabled = false;
        splash_select.GetComponentInChildren<Image>().enabled = false;
        splash_select.GetComponentInChildren<Text>().enabled = false;
        exit_select.GetComponentInChildren<Button>().enabled = false;
        exit_select.GetComponentInChildren<Image>().enabled = false;
        exit_select.GetComponentInChildren<Text>().enabled = false;
        archer_select = null;
        wizard_select = null;
        splash_select = null;
        exit_select = null;
    }



    private bool canUpgradeWizardTower()
    {
        if(wizard_tower != null)
        {
            TowerData towerData = wizard_tower.GetComponent<TowerData>();
            TowerLevel nextLevel = towerData.getNextLevel();
            if (nextLevel != null)
            {
                if (wizard_tower.GetComponent<TowerData>().levels.IndexOf(nextLevel) == 1 && gm.cash >= 90)
                {
                    return true;
                }
                else if (wizard_tower.GetComponent<TowerData>().levels.IndexOf(nextLevel) == 2 && gm.cash >= 150)
                {
                    return true;
                }
            }
        }
        return false;
    }
    private bool canUpgradeArcherTower()
    {
        if (archer_tower != null)
        {
            TowerData towerData = archer_tower.GetComponent<TowerData>();
            TowerLevel nextLevel = towerData.getNextLevel();
            if (nextLevel != null)
            {
                if (archer_tower.GetComponent<TowerData>().levels.IndexOf(nextLevel) == 1 && gm.cash >= 60)
                {
                    return true;
                }
                else if (archer_tower.GetComponent<TowerData>().levels.IndexOf(nextLevel) == 2 && gm.cash >= 90)
                {
                    return true;
                }
            }
        }
        
        return false;
    }

    private bool canUpgradeSplashTower()
    {
        if (splash_tower != null)
        {
            TowerData towerData = splash_tower.GetComponent<TowerData>();
            TowerLevel nextLevel = towerData.getNextLevel();
            if (nextLevel != null)
            {
                if (splash_tower.GetComponent<TowerData>().levels.IndexOf(nextLevel) == 1 && gm.cash >= 100)
                {
                    return true;
                }
                else if (splash_tower.GetComponent<TowerData>().levels.IndexOf(nextLevel) == 2 && gm.cash >= 170)
                {
                    return true;
                }
            }
        }

        return false;
    }

}
