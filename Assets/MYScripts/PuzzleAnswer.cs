using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleAnswer : MonoBehaviour
{
    public bool isCorrect = false;
    
    private Button button;

    private void Start()
    {
        button = GetComponent<Button>();

        // Adds an OnClick listener, so that when the button is clicked, it checks for the answer.
        button.onClick.AddListener(CheckAnswer);

        ColorBlock cb = button.colors;
        
        // Change the inactive button colour depending on isCorrect.
        if (isCorrect)
        {
            // Change inactive colour to a green tint
            cb.disabledColor = PuzzleManager.Instance.correctColor;
            button.colors = cb;
        }
        else if (!isCorrect)
        {
            // Change inactive colour to a red tint
            cb.disabledColor = PuzzleManager.Instance.wrongColor;
            button.colors = cb;
        }
    }

    private void CheckAnswer()
    {
        GetComponent<Button>().interactable = false;
        PuzzleManager.Instance.CheckAnswer(isCorrect);
    }
}
