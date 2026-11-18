using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    [SerializeField] private GameObject PipePrefab;
    [SerializeField] private float minY = -2f;
    [SerializeField] private float maxY = 2f;
    [SerializeField] private float spawnX = 10f;
    [SerializeField] private float spawnInterval = 2f;
    private float timer;
    private void Update()
    {
        if(GameManager.Instance==null || GameManager.Instance.isGameOver || !GameManager.Instance.isGameStarted)
        {
            return;
        }
        timer += Time.deltaTime;
        if(timer<spawnInterval)
        {
            return;
        }
        timer = 0;
        SpawnPipe();
    }
    private void SpawnPipe()
    {
        float randomY = Random.Range(minY, maxY);
        Vector3 spawnPosition = new Vector3(spawnX, randomY, 0f);
        Instantiate(PipePrefab, spawnPosition, Quaternion.identity);
    }
}
