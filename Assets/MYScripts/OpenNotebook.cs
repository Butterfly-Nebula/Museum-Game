using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class OpenNotebook : MonoBehaviour
{
    public GameObject buttonNbook;
    public GameObject Nbook;
    public GameObject crosshair; 

    public bool Pause = false;

   // GameObject mainCamera;
   // float distance = 1;

    private void Start()
    {
        
    }
    private void Update()
    {
        if (Input.GetKeyDown("q"))
        {
            if (!Pause)
            {
                Nbook.SetActive(true); // journal visible
                buttonNbook.SetActive(false); // button invisible
                                              //transform.position = MainCamera.position + Camera.forward * distance;
                crosshair.SetActive(false);

                //Nbook.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, Camera.main.nearClipPlane)); // set the journal in the center of the screen

                Time.timeScale = 0; // freeze everything

                Cursor.lockState = CursorLockMode.None; // not stuck in the middle DUUHHH
                Cursor.visible = true;

                GameObject varGameObject = GameObject.FindWithTag("Player");
                varGameObject.GetComponent<MovementPlayer>().enabled = false;

                Pause = true;
            }
            else
            {
                if (Input.GetKeyDown("q"))
                {
                    Nbook.SetActive(false);
                    buttonNbook.SetActive(true);
                    crosshair.SetActive(true);

                    Time.timeScale = 1f; // un-freeze everything

                    Cursor.lockState = CursorLockMode.Locked; 
                    Cursor.visible = false;

                    GameObject varGameObject = GameObject.FindWithTag("Player");
                    varGameObject.GetComponent<MovementPlayer>().enabled = true;

                    Pause = false;
                }
            }

        } 
    }
}
