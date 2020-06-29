using UnityEngine;

public class AlgorithmRunner : MonoBehaviour
{
    Grid currentGrid;

    private void Awake()
    {
        currentGrid = FindObjectOfType<Grid>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            TryRunAlgorithm();
    }

    void TryRunAlgorithm()
    {
        AstarAlgorithm.Run(currentGrid);
    }
}
