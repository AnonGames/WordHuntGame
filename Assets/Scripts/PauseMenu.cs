using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{    
    public Transform boxParent;
    public GameObject pausedMenu;
    public Timer timer;
    public Text startText;
    public Categories categories;
    public GameObject instructions;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Pause()
    {
        if (timer.timerStopped)
        {
            timer.timerStopped = false;
            pausedMenu.SetActive(true);
            boxParent.gameObject.SetActive(false);
            startText.gameObject.SetActive(false);
            categories.CategorySelection();
        }
        else
        {
            return;
        }
    }
    public void OnMouseDown()
    {
        if (this.gameObject.tag == "Pause")
        {
            Pause();
        }
    }
}
