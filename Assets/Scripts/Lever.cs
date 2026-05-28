using UnityEngine;

public class Lever : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private ConfigurableJoint joint;
    [SerializeField] private float threshold = 0.9f; // 90% of the way to limit = "reached"
    [SerializeField] private LeverState correctState;
    [SerializeField] private AudioClip audioClip;

    private AudioSource audioSource;
    public enum LeverState { Up, Down, Middle }
    public LeverState CurrentState { get; private set; } = LeverState.Middle;
    public bool IsCorrect => CurrentState == correctState;


    private float limit;
    private Vector3 anchorWorldPos;
    private LeverState lastState = LeverState.Middle;

    void Start()
    {
        limit = joint.linearLimit.limit;
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        Vector3 anchorWorldPos = joint.transform.TransformPoint(joint.anchor);
        float offset = anchorWorldPos.y - transform.position.y;

        if (offset >= limit * threshold)
            SetState(LeverState.Up);
        else if (offset <= -limit * threshold)
            SetState(LeverState.Down);
        else
            SetState(LeverState.Middle);
    }

    private void SetState(LeverState newState)
    {
        if (newState == lastState) return; // only fire once when first reached

        lastState = newState;
        CurrentState = newState;

        audioSource.PlayOneShot(audioClip);
        LeverController.instance.OnLeverStateChanged();

    }
}

