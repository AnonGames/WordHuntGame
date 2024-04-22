using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Timer  : MonoBehaviour
{
    public Fiction fictionalCharacters;
    public MoviesSeries moviesSeries;
    public Animals animals;
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] float remainingTime;
    public int maxAttempts = 0;
    public Text menuText;
    public GameObject endMenu;
    public bool timerStopped = false;
    public Transform boxParent;
    public Text yourGuess;
    public Text theAnswer;
    



    void Start()
    {
        SetDifficulty();
        
        
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            timerStopped = true;
        }
        TimeRun();
    }

    public int SetDifficulty()
    {        
        string sceneName = SceneManager.GetActiveScene().name;
        switch (sceneName)
        {
            case "Cat1 Beginner":
                maxAttempts = GetMaxAttemptsBeginner();
                Debug.Log(" Beginner The Attempts are: " + maxAttempts);
                return maxAttempts;
            case "Cat1 Intermediate":
                maxAttempts = GetMaxAttemptsIntermediate();
                Debug.Log(" Intermediate The Attempts are: " + maxAttempts);
                return maxAttempts;
            case "Cat1 Advanced":
                maxAttempts = GetMaxAttemptsAdvanced();
                Debug.Log("Advanced The Attempts are: " + maxAttempts);
                return maxAttempts;
            case "Cat2 Beginner":
                maxAttempts = GetMaxAttemptsBeginner();
                Debug.Log(" Beginner The Attempts are: " + maxAttempts);
                break;
            case "Cat2 Intermediate":
                maxAttempts = GetMaxAttemptsIntermediate();
                Debug.Log(" Intermediate The Attempts are: " + maxAttempts);
                break;
            case "Cat2 Advanced":
                maxAttempts = GetMaxAttemptsAdvanced();
                Debug.Log("Advanced The Attempts are: " + maxAttempts);
                break;
            case "Cat3 Beginner":
                maxAttempts = GetMaxAttemptsBeginner();
                Debug.Log(" Beginner The Attempts are: " + maxAttempts);
                break;
            case "Cat3 Intermediate":
                maxAttempts = GetMaxAttemptsIntermediate();
                Debug.Log(" Intermediate The Attempts are: " + maxAttempts);
                break;
            case "Cat3 Advanced":
                maxAttempts = GetMaxAttemptsAdvanced();
                Debug.Log("Advanced The Attempts are: " + maxAttempts);
                break;
            default:
                Debug.LogError("Unkown difficulty level: ");
                break;
        }
        Debug.LogError("Bad output: The Attempts are: " + maxAttempts);
        return maxAttempts;
        

    }
    public int GetMaxAttemptsBeginner()
    {
        return 10;        
    }

    public int GetMaxAttemptsIntermediate()
    {
        return 5;
    }

    public int GetMaxAttemptsAdvanced()
    {
        return 3;
    }
    
    public void GameOver(bool won, bool lost) //Kalla denna method i fictionalcharacters script där bool variabel är lika med outcome och om den är sann händer saker i denna.
    {
        
        endMenu.SetActive(true);
        if (won)
        {
            menuText.text = "You Won!";
            boxParent.gameObject.SetActive(false);
            timerStopped = false;
            
        }
        else if (lost)
        {
            menuText.text = "You lost!";
            boxParent.gameObject.SetActive(false);
            timerStopped = false;
        }
    }

    public void TimeRun()
    {
        
        if (timerStopped)
        {
            remainingTime -= Time.deltaTime;
            int minutes = Mathf.FloorToInt(remainingTime / 60);
            int seconds = Mathf.FloorToInt(remainingTime % 60);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

            if (remainingTime <= 0)
            {
                bool gameWon = true;
                timerStopped = false;
                timerText.text = string.Format("00:00");
                GameOver(timerStopped, gameWon);

            }
        }
    }
    
}
