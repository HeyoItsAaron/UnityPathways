using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using static UnityEngine.EventSystems.EventTrigger;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Colors")]
    [SerializeField] private Color yellowColor;
    [SerializeField] private Color whiteColor;
    [Space(10)]

    [Header("Spawned GameObjects")]
    [SerializeField] private GameObject player;
    [SerializeField] private List<Enemy> enemies;
    [SerializeField] private GameObject powerup;
    [Space(10)]

    [Header("Factories")]
    [SerializeField] private PlayerFactory playerFactory;
    [SerializeField] private EnemyFactory enemyFactory;
    [SerializeField] private PowerUpFactory powerupFactory;
    [SerializeField] public PowerUpIndicatorFactory PowerupIndicatorFactory;
    [Space(10)]

    private float timePassed;

    private int enemiesDefeated;
    private int currentScore;
    private int highScore;
    private int difficulty;

    public bool gamePlaying;

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(this); }
        else { Instance = this; }

        if(PlayerPrefs.HasKey("HighScore")) { highScore = PlayerPrefs.GetInt("HighScore"); }
        else { highScore = 0; }
    }

    private void Start()
    {
        UIManager.Instance.ShowStartMenuUI(highScore);
        difficulty = 1;
        gamePlaying = false;
    }

    private void Update()
    {
        if (gamePlaying)
        {
            timePassed += Time.deltaTime;
            currentScore = (int)((timePassed) + (10 * enemiesDefeated));

            UIManager.Instance.UpdateInGameUI(timePassed, enemiesDefeated, currentScore);
            
            if (currentScore > highScore)
            {
                UIManager.Instance.UpdateScoreColor();
            }
        }
    }

    private IEnumerator StartWave(int difficulty)
    {
        yield return new WaitForSeconds(1f);

        if(difficulty / 2 == 0) { SpawnPowerUp(); }
        
        for(int i = 0; i < difficulty; i++)
        {
            enemies.Add(enemyFactory.MakeGameObject().GetComponent<Enemy>());
        }
    }
    private void SpawnPowerUp()
    {
        if(powerup != null) { return; }
        powerup = powerupFactory.MakeGameObject();
    }
    
    private void Reset()
    {
        
        if (powerup != null) { Destroy(powerup); }
        foreach (var enemy in enemies)
        {
            Destroy(enemy.gameObject);
        }
        Destroy(player);
        NullCheck();
        timePassed = 0;
        enemiesDefeated = 0;
        difficulty = 1;
    }
    public void DefeatedEnemy(Enemy enemy)
    {
        Destroy(enemy.gameObject);
        enemies.Remove(enemy);

        enemiesDefeated++;
        UIManager.Instance.UpdateEnemiesDefeatedText(enemiesDefeated);

        TryUpdateDifficulty();

        if (enemies.Count == 0 && gamePlaying)
        {
            StartCoroutine(StartWave(difficulty));
        }
    }
    public void StartGame()
    {
        Reset();

        UIManager.Instance.SwitchToInGameUI();

        player = playerFactory.MakeGameObject();

        gamePlaying = true;

        StartCoroutine(StartWave(difficulty));
    }
    public void EndGame()
    {
        StopAllCoroutines();
        gamePlaying = false;

        currentScore = (int)((timePassed) + (10 * enemiesDefeated));
        bool newRecord = false;
        if (currentScore > highScore)
        {
            highScore = currentScore;
            PlayerPrefs.SetInt("HighScore", highScore);
            newRecord = true;
        }
        UIManager.Instance.UpdateTimeText(timePassed);
        UIManager.Instance.UpdateEnemiesDefeatedText(enemiesDefeated);
        UIManager.Instance.UpdateFinalScoreText(currentScore);

        StartCoroutine(EndRoutine(newRecord));
    }
    private void NullCheck()
    {
        if(playerFactory == null) { playerFactory = FindObjectOfType<PlayerFactory>(); }
        if(enemyFactory == null) { enemyFactory = FindObjectOfType<EnemyFactory>(); }
        if(powerupFactory == null) { powerupFactory = FindObjectOfType<PowerUpFactory>(); }

        player = null;
        enemies = new List<Enemy>(); ;
        powerup = null;
    }

    private void TryUpdateDifficulty()
    {
        if(enemiesDefeated > 5)
        {
            difficulty = (enemiesDefeated + 1) / 2;
        }
        else
        {
            difficulty = 1;
        }
    }
    private IEnumerator EndRoutine(bool newRecord)
    {
        yield return new WaitForSeconds(2f);
        UIManager.Instance.SwitchToRetryUI(newRecord);
    }
}
