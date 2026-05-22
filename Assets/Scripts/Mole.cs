using System.Collections;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class Mole : MonoBehaviour
{
    public float hiddenYOffset = -0.35f;
    public float exposedYOffset = 0.35f;
    public float moveSpeed = 8f;
    public float exposedTime = 1.5f;
    public float stunnedTime = 2f;

    public Color normalColor = Color.gray;
    public Color stunnedColor = Color.red;

    private bool isExposed = false;
    private bool isStunned = false;

    private Vector3 baseHolePosition;
    private Vector3 hiddenPos;
    private Vector3 exposedPos;

    private Renderer moleRenderer;

    public AudioSource audioSource;
    public AudioClip hitSound;

    void Start()
    {
        moleRenderer = GetComponent<Renderer>();

        if (moleRenderer != null)
        {
            moleRenderer.material.color = normalColor;
        }

        SetHolePosition(transform.position);
    }

    void Update()
    {
        if (isExposed)
        {
            transform.position = Vector3.Lerp(
                transform.position,
                exposedPos,
                Time.deltaTime * moveSpeed
            );
        }
        else
        {
            transform.position = Vector3.Lerp(
                transform.position,
                hiddenPos,
                Time.deltaTime * moveSpeed
            );
        }
    }

    public void SetHolePosition(Vector3 holePosition)
    {
        baseHolePosition = holePosition;
        hiddenPos = baseHolePosition + new Vector3(0f, hiddenYOffset, 0f);
        exposedPos = baseHolePosition + new Vector3(0f, exposedYOffset, 0f);

        transform.position = hiddenPos;
    }

    public void PopUp()
    {
        if (isStunned) return;

        isExposed = true;
        CancelInvoke(nameof(Hide));
        Invoke(nameof(Hide), exposedTime);
    }

    public void Hide()
    {
        if (isStunned) return;

        isExposed = false;
    }

    public void Hit()
    {

        if (!isExposed || isStunned) return;

        if (audioSource  != null && hitSound != null)
        {
            audioSource.PlayOneShot(hitSound);
        }

        StartCoroutine(StunRoutine());
    }

    IEnumerator StunRoutine()
    {
        isStunned = true;
        isExposed = true;

        CancelInvoke(nameof(Hide));

        if (moleRenderer != null)
        {
            moleRenderer.material.color = stunnedColor;
        }

        yield return new WaitForSeconds(stunnedTime);

        if (moleRenderer != null)
        {
            moleRenderer.material.color = normalColor;
        }

        isStunned = false;
        isExposed = false;
    }

    public bool CanBeHit()
    {
        return isExposed && !isStunned;
    }

    public bool IsStunned()
    {
        return isStunned;
    }
}