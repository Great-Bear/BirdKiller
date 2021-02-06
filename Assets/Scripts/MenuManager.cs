using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public GameObject menu;
    public GameObject pauseButton;
    public GameObject instruction;
    public GameObject replayMenu;

    public Text CountKiledBirds;
    public Text TimeGame;

   public delegate void Replay();
   public event Replay replay;

    public void CallMenu() 
    {
        menu.SetActive(true);
        GameManager.IsGame = false;
        pauseButton.SetActive(false);
        replayMenu.SetActive(false);
    }

    public void RecallMenu() 
    {
        menu.SetActive(false);
        GameManager.IsGame = true;
        GameManager.RecallPause();
        pauseButton.SetActive(true);
    }

    public void QuitApplication()
    {
        Application.Quit();
    }

    public void CallInstruction() 
    {
        instruction.SetActive(true);
        menu.SetActive(false);
    }

    public void RecallInstruction()
    {
        instruction.SetActive(false);
        menu.SetActive(true);
    }

    public void CallReplayMenu() 
    {
        replayMenu.SetActive(true);

        CountKiledBirds.text = $"You Killed:\n{GameManager.CountKilledBirds} birds";
        TimeGame.text = $"You play:\n{GameManager.TimeGame} sc";
        GameManager.TimeGame = 0;

        pauseButton.SetActive(false);
    }

    public void PlayAgain() 
    {
        replayMenu.SetActive(false);
        replay?.Invoke();
        pauseButton.SetActive(true);
    }
}
