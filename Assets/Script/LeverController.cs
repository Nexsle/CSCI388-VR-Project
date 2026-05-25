using UnityEngine;

public class LeverController : MonoBehaviour
{
    [SerializeField] private Animator chestAnimator;

    public static LeverController instance;

    private Lever[] levers;

    private void Awake()
    {
        instance = this;
        levers = GetComponentsInChildren<Lever>();
    }

    public void OnLeverStateChanged()
    {
        foreach (var lever in levers)
        {
            if (!lever.IsCorrect) return;
        }
        CompletedTask();
    }

    private void CompletedTask()
    {
        chestAnimator.SetBool("OpenChest", true);
    }
}
