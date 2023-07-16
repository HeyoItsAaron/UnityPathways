using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using static UnityEngine.EventSystems.EventTrigger;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private float timeLimit = 60.0f;
    [SerializeField] private TextMeshProUGUI timeRemainingText;
    [SerializeField] private TextMeshProUGUI bonusTimeText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;
    [SerializeField] private TextMeshProUGUI highScoreWordText;
    [SerializeField] private TextMeshProUGUI newRecordText;
    [SerializeField] private GameObject retryButton;
    [SerializeField] private GameObject startButton;
    [SerializeField] private GameObject MenuOverlay;
    [SerializeField] private GameObject scoreWordText;
    
    [SerializeField] private GameObject playerPrefab;

    [SerializeField] private Color yellowColor;
    [SerializeField] private Color whiteColor;
    
    private GameObject player;
    private List<GameObject> enemies = new List<GameObject>();
    private EnemyFactory enemyFactory;

    private float timeRemaining;
    private float bonusTime;

    private bool gamePlaying;
    private bool isBonusTime;

    private int currentScore;
    private int highScore;

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(this); }
        else { Instance = this; }

        if(PlayerPrefs.HasKey("HighScore")) { highScore = PlayerPrefs.GetInt("HighScore"); }
        else { highScore = 0; }
    }

    private void Start()
    {
        enemyFactory = FindObjectOfType<EnemyFactory>();

        gamePlaying = false;
        timeRemainingText.text = "";
        bonusTimeText.text = "";
        scoreText.text = "";
        highScoreText.text = highScore.ToString();
        highScoreText.color = yellowColor;
        highScoreWordText.color = yellowColor;


        retryButton.SetActive(false);
        bonusTimeText.gameObject.SetActive(false);
        scoreWordText.SetActive(false);
        newRecordText.gameObject.SetActive(false);

        MenuOverlay.SetActive(true);
        startButton.SetActive(true);
        highScoreWordText.gameObject.SetActive(true);
        highScoreText.gameObject.SetActive(true);
        
    }

    private void Update()
    {
        if (gamePlaying)
        {
            if(timeRemaining >= 0) { CountDownRemainingTime(); }
            else if (timeRemaining <= 0 && !isBonusTime) { StartBonusTime(); }
            else if(timeRemaining <= 0) { CountUpBonusTime(); }
        }
    }

    private void CountDownRemainingTime()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
        }
        else if (timeRemaining <= 0)
        {
            timeRemaining = 0;
        }
        timeRemainingText.text = timeRemaining.ToString("0");
    }
    private void CountUpBonusTime()
    {
        bonusTime += Time.deltaTime;
        bonusTimeText.text = bonusTime.ToString("0");
    }
    private int CalculateDifficulty()
    {
        // Just some jotted math to figure out how to calculate the difficulty.
            // 60 = 1   60 / 10 = 6 - 7 = -1
            // 50 = 2   50 / 10 = 5 - 7 = -2
            // 40 = 3   40 / 10 = 4 - 7 = -3
            // 30 = 4   30 / 10 = 3 - 7 = -4
            // 20 = 5   20 / 10 = 2 - 7 = -5
            // 10 = 6   10 / 10 = 1 - 7 = -6
        if(timeRemaining >= 10) { return (int)Mathf.Abs((timeRemaining / 10) - 7); }
        else { return 6 + (int)(bonusTime / 10); }
    }
    private void SpawnEnemies()
    {
        int difficultylevel = CalculateDifficulty();
        for(int i = 0; i < difficultylevel; i++)
        {
            //enemies.Add(enemyFactory.MakeEnemy());
            //var enemy = enemyFactory.MakeEnemy();
            // enemies.Add(enemy);
        }
    }
    public void StartGame()
    {
        Time.timeScale = 1;
        if(player != null) { ResetSpheres(); }

        timeRemaining = timeLimit;
        bonusTime = 0f;

        retryButton.SetActive(false);
        bonusTimeText.gameObject.SetActive(false);
        scoreWordText.SetActive(false);
        scoreText.gameObject.SetActive(false);
        newRecordText.gameObject.SetActive(false);
        startButton.SetActive(false);
        MenuOverlay.SetActive(false);
        highScoreWordText.gameObject.SetActive(false);
        highScoreText.gameObject.SetActive(false);

        timeRemainingText.text = timeRemaining.ToString("0");
        timeRemainingText.gameObject.SetActive(true);

        player = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);

        InvokeRepeating("SpawnEnemies", 0f, 10f);
        
        gamePlaying = true;
    }
    private void StartBonusTime()
    {
        bonusTimeText.text = bonusTime.ToString("0");
        bonusTimeText.gameObject.SetActive(true);
        isBonusTime = true;
    }
    public void EndGame()
    {
        Time.timeScale = 0;
        CancelInvoke("SpawnEnemies");
        gamePlaying = false;
        if(timeRemaining < 0) { timeRemaining = 0; }

        currentScore = (int)((60-timeRemaining) + bonusTime);
        if(currentScore > highScore)
        { 
            highScore = currentScore;
            PlayerPrefs.SetInt("HighScore", highScore);
            newRecordText.gameObject.SetActive(true);
            newRecordText.gameObject.SetActive(true);
        }
        else
        {
            highScoreText.color = whiteColor;
            highScoreWordText.color = whiteColor;
            highScoreWordText.gameObject.SetActive(true);
            highScoreText.gameObject.SetActive(true);
        }

        scoreText.text = currentScore.ToString();
        
        timeRemainingText.gameObject.SetActive(false);
        bonusTimeText.gameObject.SetActive(false);

        MenuOverlay.SetActive(true);
        retryButton.SetActive(true);
        scoreText.gameObject.SetActive(true);
        scoreWordText.SetActive(true);
    }

    private void ResetSpheres()
    {
        foreach (var e in enemies)
        {
            Destroy(e);
        }
        Destroy(player);
    }
}
