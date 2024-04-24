using UnityEngine;
using UnityEngine.UI;

public class ButtonHighlight : MonoBehaviour
{
    private Button button;
    private Color originalColor;
    public Color highlightColor;

    private void Start()
    {
        button = GetComponent<Button>();
        originalColor = button.colors.normalColor;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ResetButtonColor();
        }
    }

    public void OnPointerEnter()
    {
        var colors = button.colors;
        colors.normalColor = highlightColor;
        button.colors = colors;
    }

    public void OnPointerExit()
    {
        ResetButtonColor();
    }

    private void ResetButtonColor()
    {
        var colors = button.colors;
        colors.normalColor = originalColor;
        button.colors = colors;
    }
}


