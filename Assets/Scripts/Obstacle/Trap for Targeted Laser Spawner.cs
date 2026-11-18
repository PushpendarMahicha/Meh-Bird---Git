using UnityEngine;

public class TrapforTargetedLaserSpawner : MonoBehaviour
{
    [SerializeField] private GameObject TrapforTargetedLaserPrefab;
    [SerializeField] private float minY = -2f;
    [SerializeField] private float maxY = 2f;
    [SerializeField] private float spawnX = 10f;
    [SerializeField] private float spawnInterval = 2f;
    private float timer;
    private void Update()
    {
        if (GameManager.Instance == null || GameManager.Instance.isGameOver || !GameManager.Instance.isGameStarted)
        {
            return;
        }
        timer += Time.deltaTime;
        if (timer < spawnInterval)
        {
            return;
        }
        timer = 0;
        SpawnTrapforTargetedLaserPrefab();
    }
    private void SpawnTrapforTargetedLaserPrefab()
    {
        float randomY = Random.Range(minY, maxY);
        Vector3 spawnPosition = new Vector3(spawnX, randomY, 0f);
        Instantiate(TrapforTargetedLaserPrefab, spawnPosition, Quaternion.identity);
    }
}
