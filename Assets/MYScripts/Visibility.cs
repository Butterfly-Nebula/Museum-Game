using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Visibility : MonoBehaviour
{
    public GameObject objectToShow;
    public GameObject objectToHide;
    bool active = false;

    // Start is called before the first frame update
    public void ShowObject()
    {
        objectToShow.SetActive(true);
        active = true;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && active)
        {
            HideObject();
        }
    }

    public void HideObject()
    {
        objectToHide.SetActive(false);
        active = false;
    }
}

