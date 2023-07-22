using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DifficultyChanger : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI easyText;
    [SerializeField] private TextMeshProUGUI mediumText;
    [SerializeField] private TextMeshProUGUI hardText;

    private void Start()
    {
        SetEasy();
    }
    public void SetEasy()
    {
        GameManager.instance.SetDifficulty(1f);
        easyText.color = Color.white;
        mediumText.color = Color.black;
        hardText.color = Color.black;
    }
    public void SetMedium()
    {
        GameManager.instance.SetDifficulty(0.5f);
        easyText.color = Color.black;
        mediumText.color = Color.white;
        hardText.color = Color.black;
    }
    public void SetHard()
    {
        GameManager.instance.SetDifficulty(0.25f);
        easyText.color = Color.black;
        mediumText.color = Color.black;
        hardText.color = Color.white;
    }
}
