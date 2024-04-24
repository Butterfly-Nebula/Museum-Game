using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeskBehaviour : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Notes")
        {
            PuzzleManager.Instance.CheckNotesAmount();
        }
    }
}
