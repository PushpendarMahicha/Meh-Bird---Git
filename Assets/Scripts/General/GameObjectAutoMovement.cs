using UnityEngine;

public class GameObjectAutoMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] direction chooseDirection;
    private void Update()
    {
        if (!GameManager.Instance || GameManager.Instance.isGameOver || !GameManager.Instance.isGameStarted)
        {
            return;
        }

        if (chooseDirection == direction.left)
        {
            transform.position += Vector3.left * Time.deltaTime * moveSpeed;
        }
        if (chooseDirection == direction.right)
        {
            transform.position += Vector3.right * Time.deltaTime * moveSpeed;
        }
    }
    public enum direction
    {
        left,
        right,
    }
}
