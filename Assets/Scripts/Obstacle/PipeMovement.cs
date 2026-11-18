using UnityEngine;

public class PipeMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float destroyX = -12f;
    private void Update()
    {
        if(transform.position.x<destroyX)
        {
            Destroy(gameObject);
            return;
        }
        if (GameManager.Instance.isGameOver || GameManager.Instance==null || !GameManager.Instance.isGameStarted)
        {
            return;
        }
        transform.position += Vector3.left * moveSpeed * Time.deltaTime;
    }
}
