using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGameState : MonoBehaviour
{
    public GameObject loseScreen;
    public GameObject winScreen;

    public void HideScreens()
    {
        loseScreen.SetActive(false);
        winScreen.SetActive(false);
    }
    private void ShowLoseScreen()
    {
        loseScreen.SetActive(true);
    }
    
    private void ShowWinScreen()
    {
        winScreen.SetActive(true);
    }

    private void OnEnable()
    {
        Health.OnPlayerDeath += ShowLoseScreen;
        Spawner.WonGame += ShowWinScreen;
    }

    private void OnDisable()
    {
        Health.OnPlayerDeath -= ShowLoseScreen;
        Spawner.WonGame -= ShowWinScreen;
    }
}
