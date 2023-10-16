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
    public GameObject canvasPlaying;
    public JoyStickController joyStick;
    public SelectSkillsController selectSkills;
    public SummaryController summary;
    public StatsController statsController;

    int levelUnlock;
    int levelCurrent;
    public const int MAX_LEVEL = 5;

    public int LevelUnlock
    {
        set
        {
            if (value < 0)
            {
                levelUnlock = 0;
            }
            else if (value > MAX_LEVEL)
            {
                levelUnlock = MAX_LEVEL - 1;
            }
            else
            {
                levelUnlock = value;
            }
        }
        get
        {
            return levelUnlock;
        }
    }
    public int LevelCurrent
    {
        set
        {
            if (value < 0)
            {
                levelCurrent = 0;
            }
            else if (value > levelUnlock)
            {
                levelCurrent = levelUnlock;
            }
            else
            {
                levelCurrent = value;
            }
        }
        get
        {
            return levelCurrent;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            DataManager.Instance.LoadData();
        }
    }
    void Start()
    {
        gameState = GameStates.Menu;
        //PlayerPrefs.SetInt(Constants.LEVEL_UNLOCK, 0);
        LevelUnlock = PlayerPrefs.GetInt(Constants.LEVEL_UNLOCK, 0);
        LevelCurrent = LevelUnlock;
        LoadLevelAsync(0);
        //Sound.Instance.PlayMusic("BackgroundMusic");
    }
    void Update()
    {
        
    }
    public void UpdateScore(int _score)
    {
        textScore.text = "Score: " + _score.ToString();
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
    public void YouWin()
    {
        Pause();
        UnlockNextLevel();

        summary.title.text = "YouWin";
        summary.gameObject.SetActive(true);
        statsController.UpdateStats(WaveManager.totalDamages);
    }
    public void YouLose()
    {
        Pause();

        summary.title.text = "YouLose";
        summary.gameObject.SetActive(true);
        statsController.UpdateStats(WaveManager.totalDamages);
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
            canvasPlaying.SetActive(false);
        }
        else
        {
            canvasPlaying.SetActive(true);
        }
        PoolingObject.ResetStaticVariable();
        level++;
        AsyncOperation async = SceneManager.LoadSceneAsync(level);
    }
    public void LoadLevelAsync()
    {
        LoadLevelAsync(LevelCurrent + 1);
    }
    public void UnlockNextLevel()
    {
        if (LevelCurrent == LevelUnlock)
        {
            LevelUnlock++;
            LevelCurrent++;
            PlayerPrefs.SetInt(Constants.LEVEL_UNLOCK, LevelUnlock);
        }
    }
}
public class CreateGameController : SingletonMonoBehaviour<GameController>
{
}
