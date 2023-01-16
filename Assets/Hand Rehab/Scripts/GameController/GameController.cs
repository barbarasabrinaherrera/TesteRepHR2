using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private GameObject levelOne;
    private GameObject levelTwo;
    private GameObject levelThree;
    private GameObject levelFour;
    private GameObject player;

    // UI GameObjects
    public GameObject textLevel;
    public GameObject textSecondsLeft;
    public GameObject canvasGameOn;
    public GameObject canvasGameOver;
    public GameObject canvasMenu;
    public GameObject textGameStatus;
    public GameObject textLifeRemaining;
    public GameObject tutorial1;
    public GameObject tutorial2;
    public GameObject tutorial3;
    public GameObject tutorial4;

    // Game Variables
    private int secondsLeft; //Seconds to survive on current level
    private bool canUseThumbUp;
  

    // Text Components
    TextMeshProUGUI levelText;
    TextMeshProUGUI secondsLeftText;
    TextMeshProUGUI gameStatusText;
    TextMeshProUGUI lifeRemainingText;

    // Effect GameObjects
    public GameObject endLevelEffect;
    public GameObject winEffect;
    public GameObject gameOverEffect;

    AudioSource endLevelAudio;

    bool initialMenuOn;
    bool tutorialIsOn;
    bool gameIsOver;
    int _currentLevel = 1;
    float levelDuration = 15f; // Level duration default

    [SerializeField]
    private EnemyCreator _enemyCreator;

    //[SerializeField]
    //private TutorialManager _tutorialManager;

    // Start is called before the first frame update
    void Start()
    {
        gameIsOver = false;

        levelOne = GameObject.Find("LevelOne");
        levelOne.SetActive(false);

        levelTwo = GameObject.Find("LevelTwo");
        levelTwo.SetActive(false);

        levelThree = GameObject.Find("LevelThree");
        levelThree.SetActive(false);

        levelFour = GameObject.Find("LevelFour");
        levelFour.SetActive(false);

        player = GameObject.Find("Player");

        endLevelAudio = GetComponent<AudioSource>();

        // Text Mesh Pro initialization - Level - Seconds Left - Game Status
        levelText = textLevel.GetComponent<TextMeshProUGUI>();
        secondsLeftText = textSecondsLeft.GetComponent<TextMeshProUGUI>();
        gameStatusText = textGameStatus.GetComponent<TextMeshProUGUI>();
        lifeRemainingText = textLifeRemaining.GetComponent<TextMeshProUGUI>();
        
        //_enemyCreator = GameObject.Find("EnemyCreator")?.GetComponent<EnemyCreator>();

        _enemyCreator = GameObject.Find("EnemyCreator")?.GetComponent<EnemyCreator>();
        if (_enemyCreator == null)
            Debug.LogError("EnemyCreator is null");

        endLevelEffect = GameObject.Instantiate(endLevelEffect, new Vector3(0f, 0f, 0f), new Quaternion(0,0,0,0));
        endLevelEffect.SetActive(false);

        winEffect = GameObject.Instantiate(winEffect, canvasGameOn.transform.position, canvasGameOn.transform.rotation);
        winEffect.SetActive(false);

        gameOverEffect = GameObject.Instantiate(gameOverEffect, canvasGameOn.transform.position, canvasGameOn.transform.rotation);
        gameOverEffect.SetActive(false);

        canvasGameOn.SetActive(false);
        canvasGameOver.SetActive(false);
        tutorial1.SetActive(false);
        tutorial2.SetActive(false);
        tutorial3.SetActive(false);
        tutorial4.SetActive(false);

        _currentLevel = 0;

        canvasMenu.SetActive(true);
        initialMenuOn = true;
        canUseThumbUp = true;

        //StartInfiniteStage();
        /* _tutorialManager = GameObject.Find("FlatScreenTV")?.GetComponent<TutorialManager>();
         if (_enemyCreator == null)
             Debug.LogError("TutorialManager is null");*/
    }

    public void DetectThumbsUp()
    {
        if(initialMenuOn && canUseThumbUp)
        {
            canUseThumbUp = false;
            StartTutorialOne();
            initialMenuOn = false;
        }
        else if(canUseThumbUp)
        {
            canUseThumbUp = false;
            LoadNextLevel();
        }
        StartCoroutine(SetThumbsUpTrue());
    }

    IEnumerator SetThumbsUpTrue()
    {
        yield return new WaitForSeconds(2);
        canUseThumbUp = true;
    }

    private void ActivateLevelOne()
    {
        levelOne.SetActive(true);
        levelTwo.SetActive(false);
        levelThree.SetActive(false);
        levelFour.SetActive(false);
    }

    private void ActivateLevelTwo()
    {
        GameObject.Find("AbilityController").GetComponent<AbilityManager>()?.ShieldDeactivation();
        levelOne.SetActive(false);
        levelTwo.SetActive(true);
        levelThree.SetActive(false);
        levelFour.SetActive(false);
    }

    private void ActivateLevelThree()
    {
        levelOne.SetActive(false);
        levelTwo.SetActive(false);
        levelThree.SetActive(true);
        levelFour.SetActive(false);
    }

    private void ActivateLevelFour()
    {
        levelOne.SetActive(false);
        levelTwo.SetActive(false);
        levelThree.SetActive(false);
        levelFour.SetActive(true);
    }
    private void ActivateTutorialOne()
    {
        tutorialIsOn = true;
        tutorial1.SetActive(true);
        canvasGameOn.SetActive(false);
        ActivateLevelOne();
        //ativar Canvas E VideoPlayer
        //canvasTutorial.GetComponent<Canvas>().enabled = true;
        //videoExample.GetComponent<VideoPlayer>().enabled = true;
        //videoExample.GetComponent<VideoPlayer>().Play();
    }

    private void ActivateTutorialTwo()
    {//mudar os videos para cada fase
        tutorialIsOn = true;
        tutorial2.SetActive(true);
        canvasGameOn.SetActive(false);
        ActivateLevelTwo();
    }

    private void ActivateTutorialThree()
    {
        tutorialIsOn = true;
        tutorial3.SetActive(true);
        canvasGameOn.SetActive(false);
        ActivateLevelThree();
    }
    private void ActivateTutorialFour()
    {
        tutorialIsOn = true;
        tutorial4.SetActive(true);
        canvasGameOn.SetActive(false);
        ActivateLevelFour();
    }
    public void StartTutorialOne()
    {
        canvasMenu.SetActive(false);
        ActivateTutorialOne();
    }
    private void StartTutorialTwo()
    {
        ActivateTutorialTwo();
    }
    private void StartTutorialThree()
    {
        ActivateTutorialThree();
    }
    private void StartTutorialFour()
    {
        ActivateTutorialFour();
    }
    private void StartLevelOne()
    {
        levelDuration = 15f;//15f;
        _currentLevel = 1;
        _levelTime = 0;
        ActivateLevelOne();
        CreateEnemiesWithDelay(2, 5);
    }

    private void StartLevelTwo()
    {
        levelDuration = 30f;//30f;
        _currentLevel = 2;
        _levelTime = 0;
        ActivateLevelTwo();
        CreateEnemiesWithDelay(3, 9);
    }

    private void StartLevelThree()
    {
        levelDuration = 45;//45f;
        _currentLevel = 3;
        _levelTime = 0;
        ActivateLevelThree();
        CreateEnemiesWithDelay(4, 9);
    }

    private void StartLevelFour()
    {
        levelDuration = 60f;//60f;
        _currentLevel = 4;
        _levelTime = 0;
        ActivateLevelFour();
        CreateEnemiesWithDelay(5, 10);//alterar tempo em que sao criados.
    }

    // Update is called once per frame
    private float _levelTime = 0;
    private int lifePlayer;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            QuitGame();
        }
        if(!gameIsOver && !tutorialIsOn && !initialMenuOn)
        {
            _levelTime += Time.deltaTime;
        }       
        secondsLeft = (int)(levelDuration + 1 - _levelTime);//TODO
        // Text Mesh Pro for Canvas: Current Level, Time Remaining
        levelText.text = _currentLevel.ToString();
        secondsLeftText.text = secondsLeft.ToString(); //Convert to String 'cause it's a float number
        lifeRemainingText.text = player.GetComponent<Character>().hp.ToString();
        CheckEndLevel();
    }

    private void TurnOffAllMagicEffects()
    {
        GameObject.Find("AbilityController").GetComponent<AbilityManager>()?.TurnOffAllMAgics();
    }

    private void CheckEndLevel()
    {
        //Checagem da vida do jogador está sendo feita no Character Script do Player
        if(_levelTime >= levelDuration)
        {
            FinishCurrentLevel();
            LoadNextTutorial();
        }
    }

    private void FinishCurrentLevel()
    {
        _levelTime = 0;
        Enemy.DestroyAllEnemies();
        FinishLevelEffect();
        TurnOffAllMagicEffects();
    }

    private void LoadNextTutorial()
    {
        switch(_currentLevel)
        {
            case 1:
                StartTutorialTwo();
                break;
            case 2:
                StartTutorialThree(); 
                break;
            case 3:
                StartTutorialFour();
                break;
            case 4:
                WinGame();
                break;
            default:
                return;
        }
    }
    public void LoadNextLevel()
    {
        if (tutorialIsOn)
        {
            TurnOffAllMagicEffects();
            tutorialIsOn = false;
            canvasGameOn.SetActive(true);
            switch (_currentLevel)
            {
                case 0:
                    tutorial1.SetActive(false);
                    StartLevelOne();
                    break;
                case 1:
                    tutorial2.SetActive(false);
                    StartLevelTwo();
                    break;
                case 2:
                    tutorial3.SetActive(false);
                    StartLevelThree();
                    break;
                case 3:
                    tutorial4.SetActive(false);
                    StartLevelFour();
                    break;
                case 4:
                    WinGame();
                    break;
                default:
                    throw new System.Exception("Unknown level: " + _currentLevel);
            }
        }
    }

    private void FinishLevelEffect()
    {
        endLevelEffect.transform.position = new Vector3(player.transform.position.x, player.transform.position.y+1.5f, player.transform.position.z);
        endLevelEffect.transform.rotation = player.transform.rotation;
        endLevelEffect.transform.localScale *= 10f;
        endLevelEffect.SetActive(true);
        endLevelAudio.Play();
        StartCoroutine(SetEndLevelEffectInactive());

    }

    IEnumerator SetEndLevelEffectInactive()
    {
        yield return new WaitForSeconds(1);
        endLevelEffect.SetActive(false);
    }

    private void WinGame()
    {
        //TODO: mostrar tela de vitoria
        gameStatusText.enableAutoSizing = true;
        gameStatusText.text = "Congratulations! \n You win!";
        Enemy.DestroyAllEnemies();
        canvasGameOn.SetActive(false);
        canvasGameOver.SetActive(true);
        winEffect.SetActive(true);
        gameIsOver = true;
    }

    /*void StartInfiniteStage()
    {
        AddAllAvailableMagics();
        for (int i = 0; i < enemiesHordeSize; i++)
        {
            _enemyCreator.SpawnEnemies((Element)Random.Range(0, 4), 5 * i);
        }
        enemiesHordeSize++;
    }*/

    /*void CheckEndStage()
    {
        if (gameIsOver) return;
        requireOk = Enemy.numberOfEnemies == 0;
        if (!stageIsTimeBased && requireOk)
        {
            if (infiniteStage)
            {
                ExerciseDetector.availableMagics.Clear();
                _uiManager.RequireOK();
            }
            else
            {
                ExerciseDetector.availableMagics.Clear();
                _uiManager.RequireOK();
                _tutorialManager.NextVideo();
                ExerciseDetector.availableMagics.Add((ExerciseType)stageNumber);
            }
            canCheckEndStage = !requireOk;
        }
    }*/

    /*void EndTimeBasedStage()
    {
        if (gameIsOver) return;
        Enemy.DestroyAllEnemies();

        stageIsTimeBased = false;
        requireOk = true;
        _tutorialManager.NextVideo();

        _uiManager.RequireOK();
        ExerciseDetector.availableMagics.Clear();
        ExerciseDetector.availableMagics.Add((ExerciseType)stageNumber);
    }*/

    /*void AddAllAvailableMagics()
    {
        ExerciseDetector.availableMagics.Add(ExerciseType.FINGER_CURL);
        ExerciseDetector.availableMagics.Add(ExerciseType.FIST);
        ExerciseDetector.availableMagics.Add(ExerciseType.ROTATION);
        ExerciseDetector.availableMagics.Add(ExerciseType.WRIST_CURL);
    }*/

    /*public void GameOver()
    {
        Enemy.DestroyAllEnemies();
        requireOk = true;
        gameIsOver = true;
        _uiManager.GameOver();
    }*/

    void CreateEnemiesWithDelay(int numberOfEnemies, int delayBetweenEnemies)
    {
        for(int i = 0; i < numberOfEnemies; i++)
        {
            _enemyCreator.SpawnEnemies(Element.NORMAL, i * delayBetweenEnemies);
        }
    }

    public void RestartGame()
    {
        if (gameIsOver)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void GameOver()
    {
        canvasGameOn.SetActive(false);
        canvasGameOver.SetActive(true);
        gameOverEffect.SetActive(true);
        gameIsOver = true;
    }

    public void QuitGame()
    {
        if(gameIsOver)
        {// Save any Game Data here
#if UNITY_EDITOR
            // Application.Quit() does not work in the editor so
            // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
            UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
        }
    }

    public EnemyCreator EnemyCreator
    {
        get => default;
        set
        {
        }
    }

    public AbilityManager AbilityManager
    {
        get => default;
        set
        {
        }
    }
}
