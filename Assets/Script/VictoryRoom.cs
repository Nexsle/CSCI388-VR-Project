using UnityEngine;

public class VictoryRoom : MonoBehaviour
{
    [SerializeField] AudioClip victoryAudio;

    [SerializeField] private AudioSource audioSource;

    private void Start()
    {
        audioSource.clip = victoryAudio;
    }

    
}
