using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    [SerializeField] private GameObject colorOptions;

    private void Start()
    {
        colorOptions.SetActive(false);
    }
    public void ToggleColorOptions()
    {
        colorOptions.SetActive(!colorOptions.activeInHierarchy);
    }
    public void ChangeColorToOrange()
    {
        GameManager.instance.SetColor(GameManager.instance.Orange);
    }
    public void ChangeColorToGreen()
    {
        GameManager.instance.SetColor(GameManager.instance.Green);
    }
    public void ChangeColorToPink()
    {
        GameManager.instance.SetColor(GameManager.instance.Pink);
    }
    public void ChangeColorToWhite()
    {
        GameManager.instance.SetColor(GameManager.instance.White);
    }
    public void ChangeColorToRed()
    {
        GameManager.instance.SetColor(GameManager.instance.Red);
    }
    public void ChangeColorToBlue()
    {
        GameManager.instance.SetColor(GameManager.instance.Blue);
    }
    public void ChangeColorToPurple()
    {
        GameManager.instance.SetColor(GameManager.instance.Purple);
    }
    public void ChangeColorToYellow()
    {
        GameManager.instance.SetColor(GameManager.instance.Yellow);
    }
}
