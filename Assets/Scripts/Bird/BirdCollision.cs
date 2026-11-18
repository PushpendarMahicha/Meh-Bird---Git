using UnityEngine;

public class BirdCollision : MonoBehaviour
{
    private Rigidbody2D rigidBody2D;
    private void Awake()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (GameManager.Instance == null || GameManager.Instance.isGameOver)
        {
            return;
        }
        GameManager.Instance.GameOver();
        rigidBody2D.simulated = false;
        Debug.Log("Bird Collided with :" + collision.gameObject.name);
    }
}
