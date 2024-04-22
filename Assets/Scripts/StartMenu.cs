using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour 
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    { 

    }

    public void OnMouseDown()
    {

        if (this.gameObject.tag == "Start")
        {
            StartGame();
        }
        else if (this.gameObject.tag == "Instructions")
        {
            Instructions();
        }
        else if (this.gameObject.tag == "Exit")
        {
            GameExit();
        }
        else if (this.gameObject.tag == "Return")
        {
            Return();
        }
    }
    public void StartGame()
    {
        Debug.Log("Start game clicked!");
        SceneManager.LoadScene("CategoryScreen");
    }
    public void Instructions()
    {
        Debug.Log("Instructions clicked!");
        //Fixa till antingen en ny scene eller bara text, Borde vara ny scene med valet att gå tillbaka till StartMenyn.
        SceneManager.LoadScene("InstructionScreen");
    }
    public void GameExit()
    {
        Debug.Log("Exit game clicked!");
        Application.Quit();
    }
    public void Return()
    {
        Debug.Log("Return Clicked!");
        SceneManager.LoadScene("StartScreen");
    }
}
