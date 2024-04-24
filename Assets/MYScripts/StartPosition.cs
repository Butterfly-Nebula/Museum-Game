using UnityEngine;

public class StartPosition : MonoBehaviour
{
    public Vector3 initialPosition; // Initial position of the puzzle piece

    private void Awake()
    {
        initialPosition = transform.position; // Set the initial position to the current position of the puzzle piece
    }
}
