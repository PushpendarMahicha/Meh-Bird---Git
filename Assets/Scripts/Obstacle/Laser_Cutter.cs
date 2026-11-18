using UnityEngine;

public class Laser_cutter : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.GameOver();
        }
    }
}
