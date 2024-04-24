using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class DialogueSystem1 : MonoBehaviour
{
    [SerializeField] private GameObject buttons_UI;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;

    public GameObject toChatText;
    public Transform dialogueCanvas;

    public float letterDelay = 0.1f;
    public float letterMultiplier = 0.5f;
    public int currentDialogueIndex;

    public KeyCode DialogueInput = KeyCode.F;

    public string Names;

    public string[] dialogueLines;

    public bool letterIsMultiplied = false;
    public bool dialogueActive = false;
    public bool dialogueEnded = false;
    public bool outOfRange = true;
    public bool option = true;

    void Start()
    {
        dialogueText.text = "";
        
    }

    void Update()
    {
    
    }

    public void EnterRangeOfNPC()
    {
        outOfRange = false;
        toChatText.SetActive(true);
        if (dialogueActive == true)
        {
            toChatText.SetActive(false);
        }
    }

    public void NPCName()
    {
        outOfRange = false;
        dialogueCanvas.GetComponent<Canvas>().enabled = true;
        nameText.text = Names;
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!dialogueActive)
            {
                dialogueActive = true;
                StartCoroutine(StartDialogue());
            }
        }
        StartDialogue();
    }

    public IEnumerator StartDialogue()
    {
        if (outOfRange == false)
        {
            int dialogueLength = dialogueLines.Length;
            int currentDialogueIndex = 0;

            while (currentDialogueIndex < dialogueLength || !letterIsMultiplied)
            {
                if (!letterIsMultiplied && option)
                {
                    letterIsMultiplied = true;
                    StartCoroutine(DisplayString(dialogueLines[currentDialogueIndex++]));

                    if (currentDialogueIndex >= dialogueLength)
                    {
                        dialogueEnded = true;
                        buttons_UI.SetActive(false);
                    }
                }
                yield return 0;
            }

            while (true)
            {
                if (Input.GetKeyDown(DialogueInput) && dialogueEnded == false)
                {
                    break;
                }
                yield return 0;
            }
            dialogueEnded = false;
            dialogueActive = false;
            DropDialogue();
            buttons_UI.SetActive(false);
        }
    }


    private IEnumerator DisplayString(string stringToDisplay)
    {
        if (outOfRange == false)
        {
            int stringLength = stringToDisplay.Length;
            int currentCharacterIndex = 0;

            dialogueText.text = "";

            while (currentCharacterIndex < stringLength)
            {
                dialogueText.text += stringToDisplay[currentCharacterIndex];
                currentCharacterIndex++;

                if (currentCharacterIndex < stringLength)
                {
                    if (Input.GetKey(DialogueInput))
                    {
                        yield return new WaitForSeconds(letterDelay * letterMultiplier);
                    }
                    else
                    {
                        yield return new WaitForSeconds(letterDelay);
                    }
                }
                else
                {
                    dialogueEnded = false;
                    break;
                }
            }
            while (true)
            {
                if (Input.GetKeyDown(DialogueInput))
                {
                    break;
                }
                yield return 0;
            }
            dialogueEnded = false;
            letterIsMultiplied = false;
            dialogueText.text = "";
        }
    }

    public IEnumerator SelectDialogue1(string stringToDisplay)
    {
        option = false;
        if (outOfRange == false)
        {
            int stringLength = stringToDisplay.Length;
            int currentCharacterIndex = 1;

            dialogueText.text = "";

            while (currentCharacterIndex < stringLength)
            {
                dialogueText.text += stringToDisplay[currentCharacterIndex];
                currentCharacterIndex++;

                if (currentCharacterIndex < stringLength)
                {
                    if (Input.GetKey(DialogueInput))
                    {
                        yield return new WaitForSeconds(letterDelay * letterMultiplier);
                    }
                    else
                    {
                        yield return new WaitForSeconds(letterDelay);
                    }
                }
                else
                {
                    dialogueEnded = true;

                    break;
                }
            }
            while (true)
            {
                if (Input.GetKeyDown(DialogueInput))
                {
                    DropDialogue();
                    buttons_UI.SetActive(false);
                    option = true;
                    break;
                }
                yield return 0;
            }
            dialogueEnded = false;
            letterIsMultiplied = false;
            dialogueText.text = "";
        }
    }

    public void DropDialogue()
    {
        Debug.Log("Drop dialogue.");
        toChatText.SetActive(false);
        dialogueCanvas.GetComponent<Canvas>().enabled = false;
    }

    public void OutOfRange()
    {
        Debug.Log("Out of range.");
        letterIsMultiplied = false;
        dialogueActive = false;
        StopAllCoroutines();
        toChatText.SetActive(false);
        dialogueCanvas.GetComponent<Canvas>().enabled = false;

        /*
        outOfRange = true;
        if (outOfRange == true)
        {
            letterIsMultiplied = false;
            dialogueActive = false;
            StopAllCoroutines();
            dialogueGUI.SetActive(false);
            dialogueBoxGUI.gameObject.SetActive(false);
        }
        */
    }
}
