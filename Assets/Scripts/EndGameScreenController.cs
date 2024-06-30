using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGameScreenController : MonoBehaviour
{
    [SerializeField] private GameObject successGameScreen;
    [SerializeField] private GameObject failGameScreen;
    [SerializeField] private Button successButton;
    [SerializeField] private Button failButton;

    public static EndGameScreenController Instance;

    private void Awake()
    {
        Instance = this;

        successButton.onClick.AddListener(OnSuccessClick);
        failButton.onClick.AddListener(OnFailClick);
    }
    private void OnSuccessClick()
    {
        GameManager.Instance.ReloadScene(true);
    }
    private void OnFailClick()
    {
        GameManager.Instance.ReloadScene(false);
    }

    public void ShowSuccessScreen()
    {
        successGameScreen.SetActive(true);
    }
    public void ShowFailScreen()
    {
        failGameScreen.SetActive(true);
    }
}
