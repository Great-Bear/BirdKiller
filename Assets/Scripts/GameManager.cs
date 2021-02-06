using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject[] lineHeath = new GameObject[3];
    public ParticleSystem killedBirdsPartial;

    public Text scoreShellText;
    public Text scoreGameText;
    public Text topScore;

    private SpawnManager spawnManager;
    private MenuManager menuManager;

    static GameManager This;
    static short countKilledBirds;
    static private int reward = 5;

    static private float hueMin = 0;
    static private float hueMax = 1;
    static private float ValueMin = 1;
    static private float ValueMax = 1;
    static private float saturationMin = 1;
    static private float saturationMax = 1;

    private int scoreGame;
    private int scoreShell;    
    private short activeLife = 3;
    private float complexity = 0.9f;

    private float stepComplexity = 0.1f;
    public const float maxComplexity = 2f;
    public const float startComplicity = 0.9f;

    private short timeWaitIncreaseScore = 1;
    private short timeWaitIncreaseComplexity = 10;
    private short timeWaitIncreaseShellCount = 2;

    public static bool IsGame { get; set; }
    public static int TimeGame { set; get; }
    public int ScoreGame 
    {
        set 
        {
            scoreGame = value;

            scoreGameText.text = $"Score:{scoreGame}";
            if (PlayerPrefs.GetInt("TopScore") < scoreGame)
            {
                PlayerPrefs.SetInt("TopScore", scoreGame);
                topScore.text = $"Top:{scoreGame}";
            }
        }
        get => scoreGame;
    }
    public int ScoreShell 
    {
        set 
        {
            scoreShell = value;
            scoreShellText.text = $"Shell:{scoreShell}";
        }
        get => scoreShell;
    }

    public static short CountKilledBirds
    {
        set
        {
            countKilledBirds = value;
            This.ScoreGame += reward; 
        }
        get => countKilledBirds;
    }

    public static void RecallPause()
    {
        if (This.activeLife == 0)
            This.PlayAgain();
    }

    private void Awake()
    {
        IsGame = false;
    }

    void Start()
    {   spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
        menuManager = GameObject.Find("MenuManager").GetComponent<MenuManager>();

        This = this;
        menuManager.replay += PlayAgain;

        StartCoroutine(increaseComplexity());
        StartCoroutine(increaseScore());
        StartCoroutine(IncreaseShellCount());

       

        if (PlayerPrefs.HasKey("FirstGame") == false) 
        {          
            menuManager.CallInstruction();
            PlayerPrefs.SetInt("FirstGame", 1);
        }
        else 
            menuManager.CallMenu();
        
        if (PlayerPrefs.HasKey("TopScore"))
        {
            topScore.text = $"Top:{PlayerPrefs.GetInt("TopScore")}";
        }
    }

    public void RemoveLife(short count) 
    {
        if (activeLife == 0)
            return;

        activeLife -= count;
        lineHeath[activeLife].SetActive(false);

        if(activeLife == 0) 
        {
            IsGame = false;

            ScoreGame = 0;
            ScoreShell = 0;

            menuManager.CallReplayMenu();
        }

    }


    IEnumerator increaseScore() 
    {
        while (true)
        {
            if (IsGame)
            {
                ScoreGame++;
                TimeGame++;
            }
            yield return new WaitForSeconds(timeWaitIncreaseScore);
        }
    }

    IEnumerator increaseComplexity()
    {
        while (true)
        {
            if (complexity >= maxComplexity)
            {
                ControllBirds.UpdateTimeSpawn(complexity);             
            }
            else if (IsGame)
            {
                complexity += stepComplexity;           
                spawnManager.UpdateTimeSpawn(complexity);
            }           
            yield return new WaitForSeconds(timeWaitIncreaseComplexity);
        }
    }

    IEnumerator IncreaseShellCount() 
    {
        while (true) 
        {
            if (IsGame)
                ScoreShell++;         
            yield return new WaitForSeconds(timeWaitIncreaseShellCount);
        }
    } 
    static public void StartKilledBirdEffect(Vector2 position) 
    {
        This.killedBirdsPartial.transform.position = new Vector3(position.x,position.y,-3);


        ParticleSystem.MainModule main = This.killedBirdsPartial.
                            GetComponent<ParticleSystem>().main;
        main.startColor = UnityEngine.Random.ColorHSV(hueMin,hueMax,
                                                        saturationMin,saturationMax,
                                                          ValueMin, ValueMax); 

        This.killedBirdsPartial.Play();      
    }


    void PlayAgain() 
    {
        complexity = startComplicity;
        ControllBirds.UpdateTimeSpawn(complexity);
        spawnManager.UpdateTimeSpawn(complexity);

        IsGame = true;

        activeLife = 3;
        for (int i = 0; i < lineHeath.Length; i++)
        {
            lineHeath[i].SetActive(true);
        }
    }
}

