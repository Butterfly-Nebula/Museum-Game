using UnityEngine;

public class SlotRenderer : MonoBehaviour
{
    private MeshRenderer slotRenderer;

    private void Awake()
    {
        slotRenderer = GetComponent<MeshRenderer>();
    }

    public int GetSortingOrder()
    {
        return slotRenderer.sortingOrder;
    }

    public void SetSortingOrder(int sortingOrder)
    {
        slotRenderer.sortingOrder = sortingOrder;
    }
}
