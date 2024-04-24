using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class JournalOpen : MonoBehaviour
{
    public string mainMenuScene;
    public GameObject pauseMenu;
    public GameObject buttonNbook;
    public GameObject questLine;

    public bool isPaused = false;

    //[Header("Volume")]
    //public float volume; // slider
    //public AudioMixer mixer; // audio source

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("q"))
        {
            if (!isPaused) // it is paused
            {
                isPaused = true;

                pauseMenu.SetActive(true);

                buttonNbook.SetActive(false);

                questLine.SetActive(false);

                Time.timeScale = 0; // freeze everything

                Cursor.lockState = CursorLockMode.None; // not stuck in the middle DUUHHH
                Cursor.visible = true;

                GameObject varGameObject = GameObject.FindWithTag("Player");
                varGameObject.GetComponent<MovementPlayer>().enabled = false;

            } else // it is NOT paused
            {
                isPaused = false;

                pauseMenu.SetActive(false);

                buttonNbook.SetActive(true);

                questLine.SetActive(true);

                Time.timeScale = 1f; // un-freeze everything

                Cursor.lockState = CursorLockMode.Locked; // not stuck in the middle DUUHHH
                Cursor.visible = false;

                GameObject varGameObject = GameObject.FindWithTag("Player");
                varGameObject.GetComponent<MovementPlayer>().enabled = true;
            }
        }
    }

    public void SetVolume()
    {
        
    }

    public void ReturnToMain()
    {
        SceneManager.LoadScene(mainMenuScene);
    }
}
