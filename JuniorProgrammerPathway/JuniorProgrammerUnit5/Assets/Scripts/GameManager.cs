using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Game Objects")]
    [SerializeField] private Blade blade;
    [Space(10)]

    [Header("Prefabs")]
    [SerializeField] private List<GameObject> targets;
    [Space(10)]

    [Header("UI")]
    [Header("In-Game UI")]
    [SerializeField] private GameObject InGameCanvas;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;
    [SerializeField] private TextMeshProUGUI livesLostText;
    [Space(5)]

    [Header("Game Over UI")]
    [SerializeField] private GameObject GameOverCanvas;
    [SerializeField] private GameObject NewHighScoreText;
    [SerializeField] private TextMeshProUGUI FinalScoreText;
    [Space(5)]

    [Header("Menu UI")]
    [SerializeField] private GameObject MenuCanvas;
    [SerializeField] private TextMeshProUGUI menuHighScoreText;

    [Space(5)]

    [Header("Health Colors")]
    [SerializeField] private Color FullHealthColor;
    [SerializeField] private Color MediumHealthColor;
    [SerializeField] private Color LowHealthColor;
    [Space(10)]

    [Header("Selected Color")]
    private Vector3 selectedColorVector3;
    private Color selectedColor;

    [Space(5)]

    [Header("Blade Color Options")]
    public Color White;
    public Color Red;
    public Color Blue;
    public Color Green;
    public Color Yellow;
    public Color Purple;
    public Color Pink;
    public Color Orange;

    private float spawnRate = 1.0f;
    private int lives;
    private int score;
    private int highScore;
    private float difficulty;
    public bool isPlaying;

    public float Difficulty { get => difficulty; set => difficulty = value; } // ENCAPSULATION

    private void Awake()
    {
        if (GameManager.instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            GameManager.instance = this;
        }

        if (PlayerPrefs.HasKey("highScore"))
        {
            highScore = PlayerPrefs.GetInt("highScore");
        }
        else
        {
            PlayerPrefs.SetInt("highScore", 0);
            highScore = 0;
        }

        // Janky way of storing color in player prefs
        if (PlayerPrefs.HasKey("bladeColorR"))
        {
            selectedColorVector3.x = PlayerPrefs.GetFloat("bladeColorR");
        }
        else
        {
            PlayerPrefs.SetFloat("bladeColorR", 1);
            selectedColorVector3.x = 1;
        }

        if (PlayerPrefs.HasKey("bladeColorG"))
        {
            selectedColorVector3.y = PlayerPrefs.GetFloat("bladeColorG");
        }
        else
        {
            PlayerPrefs.SetFloat("bladeColorG", 1);
            selectedColorVector3.y = 1;
        }

        if (PlayerPrefs.HasKey("bladeColorB"))
        {
            selectedColorVector3.z = PlayerPrefs.GetFloat("bladeColorB");
        }
        else
        {
            PlayerPrefs.SetFloat("bladeColorB", 1);
            selectedColorVector3.z = 1;
        }
    }
    private void Start()
    {
        LoadMenuUI();
        SetColor(new Color(selectedColorVector3.x, selectedColorVector3.y, selectedColorVector3.z, 1));
        menuHighScoreText.text = "High Score: " + highScore.ToString();
    }
    private IEnumerator SpawnTarget()
    {
        while (isPlaying)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
        }
    }
    public void UpdateScore(int targetScore = 0)
    {
        if (isPlaying)
        {
            score += targetScore;
            scoreText.text = score.ToString();
            FinalScoreText.text = score.ToString();
        }
    }
    public void UpdateLives(int livesTaken = 0)
    {
        lives -= livesTaken;

        livesLostText.text = "";
        for (int i = 0; i < this.lives; i++) { livesLostText.text += "I"; }
        if (lives == 2)
        {
            livesLostText.color = MediumHealthColor;
        }
        else if (lives == 1)
        {
            livesLostText.color = LowHealthColor;
        }
        else if (lives == 0)
        {
            EndGame();
        }
    }
    private void EndGame()
    {
        isPlaying = false;
        StopAllCoroutines();

        if(score > highScore)
        {
            UpdateHighScore(score);
        }
        InGameCanvas.SetActive(false);
        GameOverCanvas.SetActive(true);
        foreach ( var t in FindObjectsOfType<Target>())
        {
            Destroy(t.gameObject); 
        }
    }
    public void StartGame()
    {
        spawnRate = difficulty;
        ResetGame();
        isPlaying = true;
        StartCoroutine(SpawnTarget());
    }
    private void ResetGame()
    {
        livesLostText.color = FullHealthColor;
        lives = 3;
        score = 0;
        UpdateLives();
        UpdateScore();
        highScoreText.text = "Best: " + highScore.ToString();
        menuHighScoreText.text = "Best: " + highScore.ToString();
        NewHighScoreText.SetActive(false);
        MenuCanvas.SetActive(false);
        GameOverCanvas.SetActive(false);
        InGameCanvas.SetActive(true);

    }
    private void UpdateHighScore(int score)
    {
        highScore = score;
        highScoreText.text = "Best: " + highScore.ToString();
        menuHighScoreText.text = "Best: " + highScore.ToString();
        NewHighScoreText.SetActive(true);
        PlayerPrefs.SetInt("highScore", highScore);
    }

    public void LoadMenuUI()
    {
        InGameCanvas.SetActive(false);
        GameOverCanvas.SetActive(false);
        MenuCanvas.SetActive(true);
    }

    public void SetColor(Color color)
    {
        selectedColor = color;
        PlayerPrefs.SetFloat("bladeColorR", color.r);
        PlayerPrefs.SetFloat("bladeColorG", color.g);
        PlayerPrefs.SetFloat("bladeColorB", color.b);
        blade.ChangeBladeColor(selectedColor);
    }

    public void SetDifficulty(float difficulty = 0)
    {
        this.difficulty = difficulty;
    }
}
