using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LTAUnityBase.Base.DesignPattern;
using TMPro;
using UnityEngine.SceneManagement;

public class TOPICNAME
{
    public const string ENEMY_DIE = "Enemy_Die";
    public const string PAUSE = "Pause";
    //public const string ENEMY_SPAWNED = "Enemy_Spawned";
}

public class GameController : MonoBehaviour
{
    public static GameController instance { get; private set; }
    [SerializeField]
    TextMeshProUGUI textScore;
    [SerializeField]
    TextMeshProUGUI textWave;
    public GameStates gameState;
    public WaveManager waveManager;
    public GameObject canvasMenu;
    public GameObject canvasPlaying;
    public LevelManager levelManager;
    public SummaryController summary;
    int score = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            DataManager.Instance.LoadData();
        }
        else
        {
            Destroy(gameObject);
            //DestroyImmediate(gameObject);
        }
    }
    void Start()
    {
        gameState = GameStates.Playing;
        Observer.Instance.AddObserver(TOPICNAME.ENEMY_DIE, OnEnemyDie);
        score = 0;
        //Sound.Instance.PlayMusic("BackgroundMusic");
    }
    private void OnDestroy()
    {
        Observer.Instance.RemoveObserver(TOPICNAME.ENEMY_DIE, OnEnemyDie);
    }
    void Update()
    {
        
    }
    void OnEnemyDie(object data)
    {
        score++;
        textScore.text = "Score: " + score.ToString();
    }
    public void DisplayWave(int wave)
    {
        textWave.text = "Wave: " + wave.ToString();
    }
    
    public void Pause()
    {
        if (gameState == GameStates.Pause)
        {
            gameState = GameStates.Playing;
        }
        else
        {
            gameState = GameStates.Pause;
        }
        Observer.Instance.Notify(TOPICNAME.PAUSE, this);
    }
    public void Stats()
    {
        summary.circle.text = WaveManager.totalDamageCircle.ToString();
        summary.normal.text = WaveManager.totalDamageNormal.ToString();
        summary.fire.text = WaveManager.totalDamageFire.ToString();
        summary.plants.text = WaveManager.totalDamagePlants.ToString();
        summary.lightText.text = WaveManager.totalDamageLight.ToString();
        summary.dark.text = WaveManager.totalDamageDark.ToString();
    }
    public void YouWin()
    {
        levelManager.LevelCurrent++;
        levelManager.DisPlayLevelCurrent();
        PlayerPrefs.SetInt(Constants.LEVEL_CURRENT, levelManager.LevelCurrent);
        
        gameState = GameStates.Pause;
        Observer.Instance.Notify(TOPICNAME.PAUSE, this);

        summary.title.text = "YouWin";
        Stats();
        summary.gameObject.SetActive(true);
    }
    public void YouLose()
    {
        gameState = GameStates.Pause;
        Observer.Instance.Notify(TOPICNAME.PAUSE, this);
        summary.title.text = "YouLose";
        Stats();
        summary.gameObject.SetActive(true);
    }
    public void BackToMenu()
    {
        CreateGameController.Instance.summary.gameObject.SetActive(false);
        CreateGameController.Instance.summary.gameObject.SetActive(false);
        LoadLevelAsync(0);
    }
    public bool IsPause()
    {
        return gameState == GameStates.Pause ? true : false;
    }
    public void LoadLevelAsync(int level)
    {
        if (level == 0)
        {
            canvasMenu.SetActive(true);
            canvasPlaying.SetActive(false);
        }
        else
        {
            canvasMenu.SetActive(false);
            canvasPlaying.SetActive(true);
        }
        score = 0;
        textScore.text = "Score: " + score.ToString();
        PoolingObject.ResetStaticVariable();
        AsyncOperation async = SceneManager.LoadSceneAsync(level);
    }
}
public class CreateGameController : SingletonMonoBehaviour<GameController>
{
}
