using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Categories : MonoBehaviour
{
    PauseMenu pauseMenu;
    Timer timer;
    public Fiction fiction;
    public MoviesSeries movieSerie;
    public Animals animals;

    // Start is called before the first frame update
    void Start()
    {
        pauseMenu = FindObjectOfType<PauseMenu>();
        timer = FindObjectOfType<Timer>();        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMouseDown()
    {
        CategorySelection();

        if (this.gameObject.tag == "Return")
        {
            Return();
        }
    }
    public void CategorySelection()
    {


      switch (this.gameObject.tag)
        {
            case "Cat1":
                Debug.Log("Cat1 Initiated");
                SceneManager.LoadScene("Cat1");
                break;
            case "Cat2":
                Debug.Log("Cat2 Initiated");
                SceneManager.LoadScene("Cat2");
                break;
            case "Cat3":
                Debug.Log("Cat3 Initiated");
                SceneManager.LoadScene("Cat3");
                break;
            case "Categories":
                SceneManager.LoadScene("CategoryScreen");
                break;
            case "Main Menu":
                SceneManager.LoadScene("StartScreen");
                break;
            case "FBeginner": //Specifikt för fictional characters
                Debug.Log("Beginner difficulty initiated!");
                SceneManager.LoadScene("Cat1 Beginner");
                break;
            case "FIntermediate": //Specifikt för fictional characters
                Debug.Log("Intermediate difficulty initiated!");
                SceneManager.LoadScene("Cat1 Intermediate");
                break;
            case "FAdvanced": //Specifikt för fictional characters
                Debug.Log("Advanced difficulty initiated!");
                SceneManager.LoadScene("Cat1 Advanced");
                break;
            case "MSBeginner": //Specifikt för fictional characters
                Debug.Log("Beginner difficulty initiated!");
                SceneManager.LoadScene("Cat2 Beginner");
                break;
            case "MSIntermediate": //Specifikt för fictional characters
                Debug.Log("Beginner difficulty initiated!");
                SceneManager.LoadScene("Cat2 Intermediate");
                break;
            case "MSAdvanced": //Specifikt för fictional characters
                Debug.Log("Beginner difficulty initiated!");
                SceneManager.LoadScene("Cat2 Advanced");
                break;
            case "ABeginner": //Specifikt för fictional characters
                Debug.Log("Beginner difficulty initiated!");
                SceneManager.LoadScene("Cat3 Beginner");
                break;
            case "AIntermediate": //Specifikt för fictional characters
                Debug.Log("Beginner difficulty initiated!");
                SceneManager.LoadScene("Cat3 Intermediate");
                break;
            case "AAdvanced": //Specifikt för fictional characters
                Debug.Log("Beginner difficulty initiated!");
                SceneManager.LoadScene("Cat3 Advanced");
                break;
            case "Resume":
                pauseMenu.pausedMenu.SetActive(false);
                timer.timerStopped = true;
                timer.boxParent.gameObject.SetActive(true);
                break;
            case "HintButton":
                //gör en setactive variabel för objectet inom hint.
                string currentScene = SceneManager.GetActiveScene().name;
                switch (currentScene)
                {
                    case "Cat1 Beginner":
                    case "Cat1 Intermediate":
                    case "Cat1 Advanced":
                        fiction.hintPopup.SetActive(true);
                        break;
                    case "Cat2 Beginner":
                    case "Cat2 Intermediate":
                    case "Cat2 Advanced":
                        movieSerie.hintPopup.SetActive(true);
                        break;
                    case "Cat3 Beginner":
                    case "Cat3 Intermediate":
                    case "Cat3 Advanced":
                        animals.hintPopup.SetActive(true);
                        break;
                }
                break;
            case "Popup":
                //gör en setactive false variabel för objectet inom hint.
                string currentScene2 = SceneManager.GetActiveScene().name;
                switch (currentScene2)
                {
                    case "Cat1 Beginner":
                    case "Cat1 Intermediate":
                    case "Cat1 Advanced":
                        fiction.hintPopup.SetActive(false);
                        break;
                    case "Cat2 Beginner":
                    case "Cat2 Intermediate":
                    case "Cat2 Advanced":
                        movieSerie.hintPopup.SetActive(false);
                        break;
                    case "Cat3 Beginner":
                    case "Cat3 Intermediate":
                    case "Cat3 Advanced":
                        animals.hintPopup.SetActive(false);
                        break;
                }
                break;
            case "InGameInfo":
                pauseMenu.instructions.SetActive(true);
                break;
            case "InGameInfo2":
                pauseMenu.instructions.SetActive(false);
                break;







        }
    }
    public void Return()
    {
        Debug.Log("Return Clicked!");
        SceneManager.LoadScene("StartScreen");
    }
}
