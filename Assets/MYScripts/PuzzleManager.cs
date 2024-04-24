using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class PuzzleManager : MonoBehaviour
{
    public static PuzzleManager Instance;

    [Header("General References")]
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform deskTransform;
    [SerializeField] private GameObject canvasHolder;
    [SerializeField] private TextMeshProUGUI winText;
    [SerializeField] private TextMeshProUGUI loseText;

    [Header("Note Settings")]
    [SerializeField] private int notesPickedUp = 0;
    private int maxNotesPickedUp;
    [SerializeField] private GameObject[] notes;

    [Header("Puzzle Settings")]
    public int correctAnswers = 0;
    public int wrongAnswers = 0;
    [SerializeField] private int maxCorrectAnswers = 3;
    [SerializeField] private int maxWrongAnswers = 3;
    public Color correctColor;
    public Color wrongColor;
    [SerializeField] private Button[] answerButtons;

    private void Awake()
    {
        // Ensures there's only one Instance of PuzzleManager. If there are multiple PuzzleManagers, the others will be ignored.
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        maxNotesPickedUp = notes.Length;
        Debug.Log($"Amount of notes to be picked up: {maxNotesPickedUp}.");
    }

    public void CheckNotesAmount()
    {
        notesPickedUp++;

        Debug.Log($"CheckNotesAmount() called, amount: {notesPickedUp}.");
        
        if (notesPickedUp >= maxNotesPickedUp)
        {
            foreach (GameObject note in notes)
            {
                note.SetActive(false); // Remove the fcking notes :D
            }

            StartPuzzle();
        }
    }

    public void StartPuzzle()
    {
        Debug.Log("StartPuzzle() called.");
        canvasHolder.SetActive(true);
        DisablePlayerMovement();
    }

    public void ResetPuzzle()
    {
        Debug.Log("ResetPuzzle() called.");
        StartCoroutine(DoResetPuzzle(2f)); // wait a few secs       
    }

    private IEnumerator DoResetPuzzle(float time)
    {
        // Disable puzzle
        EndPuzzle();

        // Show lose text
        loseText.gameObject.SetActive(true);

        // Reset stats of puzzle
        for (int i = 0; i < answerButtons.Length; i++)
        {
            answerButtons[i].interactable = true;
        }

        correctAnswers = 0;
        wrongAnswers = 0;

        yield return new WaitForSeconds(time);

        // Hide lose text
        loseText.gameObject.SetActive(false);

        // Make puzzle reappear
        StartPuzzle();
    }

    public void EndPuzzle()
    {
        Debug.Log("EndPuzzle() called.");
        canvasHolder.SetActive(false);
        EnablePlayerMovement();
    }

    public void CheckAnswer(bool isCorrect)
    {
        if (isCorrect)
            correctAnswers++;
        else if (!isCorrect)
            wrongAnswers++;

        Debug.Log($"CheckNotesAmount() called, correct amount: {correctAnswers}, wrong amount: {wrongAnswers}.");

        if (correctAnswers >= maxCorrectAnswers)
        {
            EndPuzzle();
            StartCoroutine(DoShowWinText());
        }

        if (wrongAnswers >= maxWrongAnswers)
        {
            ResetPuzzle();
        }
    }

    private IEnumerator DoShowWinText()
    {
        winText.gameObject.SetActive(true);
        yield return new WaitForSeconds(4f);
        winText.gameObject.SetActive(false);
    }

    public void DisablePlayerMovement()
    {
        Debug.Log("DisablePlayerMovement() called.");

        GameObject varGameObject = GameObject.FindWithTag("Player");
        varGameObject.gameObject.GetComponent<MovementPlayer>().enabled = false;
        Cursor.lockState = CursorLockMode.None; // not stuck in the middle DUUHHH
        Cursor.visible = true;
    }
    
    public void EnablePlayerMovement()
    {
        Debug.Log("EnablePlayerMovement() called.");

        GameObject varGameObject = GameObject.FindWithTag("Player");
        varGameObject.gameObject.GetComponent<MovementPlayer>().enabled = true;
        Cursor.lockState = CursorLockMode.Locked; // not stuck in the middle DUUHHH
        Cursor.visible = false;
    }
}
