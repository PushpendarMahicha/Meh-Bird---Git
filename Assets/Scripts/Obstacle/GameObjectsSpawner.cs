using UnityEngine;

public class GameObjectsSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] gameObjects;
    [SerializeField] private float minY = -2f;
    [SerializeField] private float maxY = 2f;
    [SerializeField] private float spawnInterval = 2f;
    private float timer = 0;
    private void Update()
    {
        if (GameManager.Instance == null || GameManager.Instance.isGameOver || !GameManager.Instance.isGameStarted)
        {
            return;
        }
        
        if (Time.time>timer)
        {
            int randomGameObject = Random.Range(0,gameObjects.Length);
            Debug.Log(gameObjects.Length);
            Spawn(gameObjects[randomGameObject]);
            timer = spawnInterval + Time.time;
        }
     
    }
    private void Spawn(GameObject gameObject)
    {
        float randomY = Random.Range(minY, maxY);
        Vector3 spawnPosition = new Vector3(transform.position.x, randomY, 0f);
        Instantiate(gameObject, spawnPosition, Quaternion.identity);
    }

  }
