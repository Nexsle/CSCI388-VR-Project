using UnityEngine;

public class CheatMode : MonoBehaviour
{
    [SerializeField] private Outline paintingOutline;
    [SerializeField] private Color highlightColor = Color.orange;
    [SerializeField] private float outlineWidth = 10f;

    void Start()
    {
        paintingOutline.enabled = false;
        paintingOutline.OutlineColor = highlightColor;
        paintingOutline.OutlineWidth = outlineWidth;
    }

    public void ToggleCheats()
    {
        paintingOutline.enabled = !paintingOutline.enabled;
    }
}