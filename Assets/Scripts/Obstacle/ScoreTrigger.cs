using UnityEngine;

public class ScoreTrigger : MonoBehaviour
{
    private bool hasScored;
    [SerializeField] int amount=5;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (hasScored || !collision.CompareTag("Player"))
        {
            return;
        }
        if (GameManager.Instance == null || GameManager.Instance.isGameOver || !GameManager.Instance.isGameStarted)
        {
            return;
        }
        hasScored = true;
        GameManager.Instance.AddScore(amount);
    }
}
