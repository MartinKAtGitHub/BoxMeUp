using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;

    [SerializeField]
    private GameObject ESCPnl;
    [SerializeField]
    private GameObject GameOverPnl;

    public GameObject player;

    [SerializeField]
    private Transform spawnPoint;

    [SerializeField]
    private BoxSpawner boxSpawner;

    private Rigidbody2D prb2d;

    private Coroutine stdSpawner;
    private Coroutine burstSpawner;

    private bool ActivCanavs;
    private void Awake()
    {
        SingeltonPatter();
    }
    
    // Use this for initialization
    void Start ()
    {
        ActivCanavs = false;
        ESCPnl.SetActive(ActivCanavs);
        if (boxSpawner == null)
        {
            Debug.LogError("cant find box spawner Scaning scene to find it");
            var Bs = GameObject.FindGameObjectWithTag("BoxSpawner");
            boxSpawner = Bs.GetComponent<BoxSpawner>();
        }
        SpawnPlayer();

        // 3 Sek wait before start 
        Invoke("StartBoxSpawner", 2f);
        //StartBoxSpawner();
        
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(Input.GetKey(KeyCode.R))
        {
            RestartLevel();
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGameESCMenu();
        }
	}

    public void RestartLevel()
    {
        SceneManager.LoadScene("MainScene");
    }


    private void SingeltonPatter()
    {
        // A simple singolton pattron used in one of Unitys own tutorial --> https://unity3d.com/learn/tutorials/projects/2d-roguelike-tutorial/writing-game-manager
        // This is not a Safe way, should use better Singolton pattern but for this game i think its ok and i dont want to think anymore this shoudl be fine
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

       // DontDestroyOnLoad(gameObject); // since we only have 1 scene this is atuly going to break ouer scene unless we handle the fact that GM will be around and set up again
    }

    public void PauseGameESCMenu()
    {
        ActivCanavs = !ActivCanavs;
        ESCPnl.SetActive(ActivCanavs);

        if(ActivCanavs)
        {
            Time.timeScale = 0f;
        }
        else if(!ActivCanavs)
        {
            Time.timeScale = 1f;
        }
    }
    public void GameOver()
    {
        // Freez player 
        prb2d.isKinematic = true;
        prb2d.velocity = Vector2.zero;
        prb2d.angularVelocity = 0;
        player.GetComponent<PlayerMovementControlles>().enabled = false;
        StopBoxSpawner();
        // death Anim


        GameOverPnl.SetActive(true);
        Debug.LogError("GAMEMANGER == GAME OVER");
    }

    public void SpawnPlayer()
    {
        player = Instantiate(player, spawnPoint.position, Quaternion.identity);
        prb2d = player.GetComponent<Rigidbody2D>();
    }


    private void StartBoxSpawner()
    {
       stdSpawner = StartCoroutine(boxSpawner.Spawner());
       burstSpawner =  StartCoroutine(boxSpawner.BurstSpawnSpawner());
    }

    public void StopBoxSpawner()
    {
        StopCoroutine(stdSpawner);
        StopCoroutine(burstSpawner);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
