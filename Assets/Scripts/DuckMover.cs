using UnityEngine;

public class DuckMover : MonoBehaviour
{
    public float moveDistance = 0.5f;
    public float moveSpeed = 2f;

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.localPosition;
    }

    void Update()
    {
        float offset = Mathf.Sin(Time.time * moveSpeed) * moveDistance;
        transform.localPosition = startPosition + new Vector3(offset, 0f, 0f);
    }
}