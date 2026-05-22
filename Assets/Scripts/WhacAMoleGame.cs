using UnityEngine;

public class WhacAMoleGame : MonoBehaviour
{
    public GameObject molePrefab;
    public Transform[] holePoints;

    public float spawnInterval = 1.5f;

    private GameObject currentMole;
    private int lastHole = -1;

    void Start()
    {
        SpawnMole();
        InvokeRepeating(nameof(SpawnMole), spawnInterval, spawnInterval);
    }

    void SpawnMole()
    {
        if (currentMole == null)
        {
            CreateMoleAtRandomHole();
            return;
        }

        Mole moleScript = currentMole.GetComponent<Mole>();
        if (moleScript == null) return;

        // do not move the mole while stunned
        if (moleScript.IsStunned())
        {
            return;
        }

        MoveMoleToRandomHole();
        moleScript.PopUp();
    }

    void CreateMoleAtRandomHole()
    {
        int index = Random.Range(0, holePoints.Length);
        lastHole = index;

        currentMole = Instantiate(
            molePrefab,
            holePoints[index].position,
            Quaternion.identity
        );

        Mole moleScript = currentMole.GetComponent<Mole>();
        if (moleScript != null)
        {
            moleScript.SetHolePosition(holePoints[index].position);
            moleScript.PopUp();
        }
    }

    void MoveMoleToRandomHole()
    {
        int index = Random.Range(0, holePoints.Length);

        if (holePoints.Length > 1)
        {
            while (index == lastHole)
            {
                index = Random.Range(0, holePoints.Length);
            }
        }

        lastHole = index;

        Mole moleScript = currentMole.GetComponent<Mole>();
        if (moleScript != null)
        {
            moleScript.SetHolePosition(holePoints[index].position);
        }
    }
}