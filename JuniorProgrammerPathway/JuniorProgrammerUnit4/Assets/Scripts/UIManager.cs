using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("Joystick")]
    [SerializeField] private GameObject joystick;
    [Space(10)]

    [Header("Runtime-modified text")]
    [SerializeField] private TextMeshProUGUI timeUI;
    [SerializeField] private TextMeshProUGUI enemiesDefeatedUI;
    [SerializeField] private TextMeshProUGUI inGameScoreUI;
    [SerializeField] private TextMeshProUGUI finalScoreUI;
    [SerializeField] private TextMeshProUGUI highScoreUI;
    [SerializeField] private TextMeshProUGUI finalTimeUI;
    [SerializeField] private TextMeshProUGUI finalEnemyUI;
    [Space(10)]

    [Header("Buttons")]
    [SerializeField] private GameObject retryButton;
    [SerializeField] private GameObject startButton;
    [Space(10)]

    [Header("Static text")]
    [SerializeField] private TextMeshProUGUI highScoreText;
    [SerializeField] private TextMeshProUGUI finalScoreText;
    [SerializeField] private GameObject newRecordText;
    [SerializeField] private GameObject finalTimeText;
    [SerializeField] private GameObject finalEnemyText;
    [Space(10)]

    [Header("Colors")]
    [SerializeField] private Color goldColor = new Color(1f, 0.9882353f, 0.4196078f, 1f);
    [SerializeField] private Color whiteColor = new Color(1f, 1f, 1f, 1f);

    [Header("Other UI")]
    [SerializeField] private GameObject MenuOverlay;

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(this); }
        else { Instance = this; }

        goldColor = new Color(1f, 0.9882353f, 0.4196078f, 1f);
        whiteColor = new Color(1f, 1f, 1f, 1f);
    }
    
    public void ShowStartMenuUI(int highScore)
    {
        ResetColorsAndText(highScore);

        if (joystick != null) { joystick.SetActive(false); }
        retryButton.SetActive(false);
        finalScoreText.gameObject.SetActive(false);
        finalEnemyText.SetActive(false);
        finalTimeText.SetActive(false);
        finalEnemyUI.gameObject.SetActive(false);
        finalTimeUI.gameObject.SetActive(false);
        enemiesDefeatedUI.gameObject.SetActive(false);
        newRecordText.SetActive(false);
        timeUI.gameObject.SetActive(false);
        
        MenuOverlay.SetActive(true);
        startButton.SetActive(true);
        highScoreText.gameObject.SetActive(true);
        highScoreUI.gameObject.SetActive(true);
    }

    public void SwitchToInGameUI()
    {
        if (joystick != null) { joystick?.SetActive(true); }
        finalEnemyText.SetActive(false);
        finalTimeText.SetActive(false);
        finalEnemyUI.gameObject.SetActive(false);
        finalTimeUI.gameObject.SetActive(false);
        finalScoreText.gameObject.SetActive(false);
        finalScoreUI.gameObject.SetActive(false);
        newRecordText.SetActive(false);
        
        MenuOverlay.SetActive(false);
        highScoreText.gameObject.SetActive(false);
        highScoreUI.gameObject.SetActive(false);

        startButton.SetActive(false);
        retryButton.SetActive(false);

        timeUI.gameObject.SetActive(true);
        enemiesDefeatedUI.gameObject.SetActive(true);
        inGameScoreUI.gameObject.SetActive(true);
    }

    public void SwitchToRetryUI(bool newRecord)
    {
        if (joystick != null) { joystick.SetActive(false); }

        if (newRecord)
        {
            newRecordText.SetActive(newRecord);
            finalScoreText.color = goldColor;
            finalScoreUI.color = goldColor;
        }
        else
        {
            finalScoreUI.color = whiteColor;
            finalScoreText.color = whiteColor;
            highScoreUI.color = goldColor;
            highScoreText.color = goldColor;
            highScoreUI.gameObject.SetActive(true);
            highScoreText.gameObject.SetActive(true);
        }

        inGameScoreUI.gameObject.SetActive(false);
        enemiesDefeatedUI.gameObject.SetActive(false);
        timeUI.gameObject.SetActive(false);

        finalEnemyText.SetActive(true);
        finalTimeText.SetActive(true);
        finalEnemyUI.gameObject.SetActive(true);
        finalTimeUI.gameObject.SetActive(true);
        finalScoreUI.gameObject.SetActive(true);
        finalScoreText.gameObject.SetActive(true);

        retryButton.SetActive(true);
    }

    public void UpdateInGameUI(float time, int enemiesDefeated, int score)
    {
        UpdateTimeText(time);
        UpdateEnemiesDefeatedText(enemiesDefeated);
        UpdateScoreText(score);
    }
    public void UpdateTimeText(float time)
    {
        timeUI.text = time.ToString("0");
        finalTimeUI.text = time.ToString("0");
    }
    public void UpdateEnemiesDefeatedText(int enemiesDefeated)
    {
        enemiesDefeatedUI.text = enemiesDefeated.ToString("0");
        finalEnemyUI.text = enemiesDefeated.ToString("0");
    }
    public void UpdateScoreText(int score)
    {
        inGameScoreUI.text = score.ToString();
    }
    public void UpdateFinalScoreText(int score)
    {
        finalScoreUI.text = score.ToString();
    }
    public void UpdateScoreColor()
    {
        inGameScoreUI.color = goldColor;
        finalScoreUI.color = goldColor;
        finalScoreText.color = goldColor;
    }

    public void ResetColorsAndText(int highScore)
    {
        timeUI.text = "";
        enemiesDefeatedUI.text = "";
        finalScoreUI.text = "";
        inGameScoreUI.text = "";
        highScoreUI.text = "";
        inGameScoreUI.color = whiteColor;
        finalScoreUI.color = whiteColor;
        finalScoreText.color = whiteColor;
        highScoreUI.color = goldColor;
        highScoreText.color = goldColor;
        highScoreUI.text = highScore.ToString();
    }
}
