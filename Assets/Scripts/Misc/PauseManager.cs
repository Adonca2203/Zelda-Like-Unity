using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{

    private bool isPaused;
    public GameObject pausePanel;
    public GameObject inventoryPanel;
    public string mainMenu;
    private bool useInventory;

    // Start is called before the first frame update
    void Start()
    {
        
        isPaused = false;
        useInventory = false;
        pausePanel.SetActive(false);
        inventoryPanel.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        
        if(Input.GetButtonDown("pause"))
        {

            ChangePause();


        }

        if(Input.GetButtonDown("inventory"))
        {

            ChangePause();
            SwitchPanels();

        }

    }

    public void ChangePause()
    {

        isPaused = !isPaused;
        if (isPaused)
        {

            pausePanel.SetActive(true);
            Time.timeScale = 0f;
            useInventory = true;

        }

        else
        {

            inventoryPanel.SetActive(false);
            pausePanel.SetActive(false);
            Time.timeScale = 1f;
            useInventory = false;

        }

    }

    public void QuitToMain()
    {

        SceneManager.LoadScene(mainMenu);
        Time.timeScale = 1f;

    }

    public void SwitchPanels()
    {

        if(useInventory)
        {

            pausePanel.SetActive(false);
            inventoryPanel.SetActive(true);

        }

    }

}
