using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleSlot : MonoBehaviour
{
    public GameObject correctPiece; // Reference to the correct puzzle piece for this slot

    private GameObject currentPiece; // Reference to the puzzle piece currently in the slot

    private SlotRenderer slotRenderer; // Reference to the SlotRenderer component

    private void Awake()
    {
        slotRenderer = GetComponent<SlotRenderer>();
    }

    // Check if the given puzzle piece is correct for this slot
    public bool IsCorrectPiece(GameObject piece)
    {
        return piece == correctPiece;
    }

    // Place the puzzle piece in the slot
    public void PlacePiece(GameObject piece)
    {
        currentPiece = piece;
        // for the pieces to be seen above the slots
        MeshRenderer pieceRenderer = piece.GetComponent<MeshRenderer>();
        int newSortingOrder = slotRenderer.GetSortingOrder() - 1;
        if (pieceRenderer != null)
        {
            pieceRenderer.sortingOrder = newSortingOrder;
        }

        // Set the sorting order of the slot
        slotRenderer.SetSortingOrder(newSortingOrder);

        piece.transform.position = transform.position;
        piece.transform.rotation = transform.rotation;
        piece.transform.SetParent(transform, false); // Attach the puzzle piece to the slot

        // Check if the placed piece is correct
        if (IsCorrectPiece(piece))
        {
            Debug.Log("Correct piece placed!");
            // Perform any additional actions for correct placement

            // Disable the ability to move the puzzle piece
            //CoolName coolNameScript = piece.GetComponent<CoolName>();
            //if (coolNameScript != null)
            //{
            //    coolNameScript.enabled = false;
            //}

            /* Check if all pieces have been placed correctly
            if (transform.parent.childCount == transform.GetSiblingIndex() + 1)
            {
                Debug.Log("Puzzle completed!");
            }
            */
        }
        else
        {
            Debug.Log("Incorrect piece placed!");
        }
        RemovePiece();
    }

    // Remove the puzzle piece from the slot
    public void RemovePiece()
    {
        currentPiece = null;
    }

    public void ResetPiecePosition(GameObject piece)
    {
        piece.transform.position = transform.position;
    }
}



