using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    [SerializeField] int timeToEnd;
    bool gamePaused = false;
    bool endGame = false;
    bool win = false;
    public int points = 0;

    public int redKey = 0;
    public int greenKey = 0;
    public int goldKey = 0;

    AudioSource audioSource;
    public AudioClip resumeClip;
    public AudioClip pauseClip;
    public AudioClip winClip;
    public AudioClip loseClip;
    public AudioClip pickClip;

    MusicScript musicScript;
    bool lessTime = false;

    public Text timeText;
    public Text goldKeyText;
    public Text greenKeyText;
    public Text redKeyText;
    public Text crystalText;
    public Image snowFlake;

    public GameObject infoPanel;
    public Text pauseEnd;
    public Text reloadInfo;
    public Text useInfo;

    // Start is called before the first frame update
    void Start()
    {
        if (gameManager == null) 
        {
            gameManager = this;
        }

        if (timeToEnd <= 0) 
        {
            timeToEnd = 100;
        }

        audioSource = GetComponent<AudioSource>();
        InvokeRepeating("Stopper", 2, 1);
        snowFlake.enabled = false; // <----
        timeText.text = timeToEnd.ToString(); // <----
        infoPanel.SetActive(true); // <----
        pauseEnd.text = "Pause"; // <----
        reloadInfo.text = ""; // <----
        SetUseInfo("");  // <---- PRZERWA DO 12:46

    }

    // Update is called once per frame
    void Update()
    {
        PauseCheck();
        PickUpCheck();
    }

    public void PlayClip(AudioClip playClip)
    {
        audioSource.clip = playClip;
        audioSource.Play();
    }

    void Stopper() 
    {
        timeToEnd --;
        Debug.Log("Time: " + timeToEnd + " s");
        timeText.text = timeToEnd.ToString(); // <-----
        snowFlake.enabled = false;  // <-----

        if (timeToEnd <= 0)
        {
            timeToEnd = 0;
            endGame = true;
        }
        if (endGame) 
        {
            EndGame();
        }

        if (timeToEnd < 20 && !lessTime) 
        {
            LessTimeOn();
            lessTime = true;
        }
        if (timeToEnd > 20 && lessTime)
        {
            LessTimeOff();
            lessTime = false;
        }
    }

    public void PauseGame() 
    {
        PlayClip(pauseClip);
        infoPanel.SetActive(true); // <-----
        musicScript.OnPauseGame();
        Debug.Log("Pause Game");
        Time.timeScale = 0f;
        gamePaused = true;
    }

    public void ResumeGame() 
    {
        PlayClip(resumeClip);
        infoPanel.SetActive(false); // <-----
        musicScript.OnResumeGame();
        Debug.Log("Resume Game");
        Time.timeScale = 1f;
        gamePaused = false;
    }

    void PauseCheck() 
    {
        if (Input.GetKeyDown(KeyCode.P)) 
        {
            if (gamePaused) 
            {
                ResumeGame();
            }
            else 
            {
                PauseGame();
            }
        }
    }

    public void EndGame() 
    {
        CancelInvoke("Stopper");
        infoPanel.SetActive(true); // <-----
        if (win)
        {
            PlayClip(winClip);
            Debug.Log("You win!!! Reload?");
            pauseEnd.text = "You Win!!!"; // <-----
            reloadInfo.text = "Reload? Y/N"; ; // <-----

        }
        else 
        {   
            PlayClip(loseClip);
            Debug.Log("You lose!!! Reload?");
            pauseEnd.text = "You Lose!!!"; // <-----
            reloadInfo.text = "Reload? Y/N"; ; // <-----
        }
    }

    public void SetUseInfo(string info)
    {
        useInfo.text = info;
    }

    public void AddPoints(int point) 
    {
        points += point;
        Debug.Log("ADD POINTS");
        crystalText.text = points.ToString(); // <-----
    }

    public void AddTime(int addTime) 
    {
        timeToEnd += addTime;
        Debug.Log("ADD TIME");
        timeText.text = timeToEnd.ToString(); // <-----
    }

    public void FreezTime(int freez) 
    {
        CancelInvoke("Stopper");
        InvokeRepeating("Stopper", freez, 1);
        Debug.Log("FREEZE TIME");
        snowFlake.enabled = true; // <-----
    }

    public void AddKey(KeyColor color)
    {
        if (color == KeyColor.Gold)
        {
            goldKey++;
            Debug.Log("Added gold key");
            goldKeyText.text = goldKey.ToString(); // <-----
        } else if (color == KeyColor.Green)
        {
            greenKey++;
            Debug.Log("Added green key");
            greenKeyText.text = greenKey.ToString(); // <-----
        } else if (color == KeyColor.Red)
        {
            redKey++;
            Debug.Log("Added red key");
            redKeyText.text = redKey.ToString(); // <-----
        }
    }

    void PickUpCheck()
    {
        if(Input.GetKeyDown(KeyCode.L)) 
        {
            Debug.Log("Actual Time: " + timeToEnd);
            Debug.Log("Key red: " + redKey + " green: " + greenKey + " gold: " + goldKey);
            Debug.Log("Points: " + points);
        }
    }

    public void LessTimeOn() 
    {
        musicScript.PitchThis(1.58f);
    }

    public void LessTimeOff() 
    {
        musicScript.PitchThis(1f);
    }

}
