using UnityEngine;

public class LaserBeam : MonoBehaviour
{
    [SerializeField] private Collider2D damageCollider;
    public bool isActive;
    private void Awake()
    {
        if (damageCollider == null)
        {
            damageCollider = GetComponent<Collider2D>();
        }
        SetBeamActive(false);
    }
    public void SetBeamActive(bool active)
{
    isActive = active;
    if (damageCollider != null)
    {
        damageCollider.enabled = true;
    }
}

private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
        {
            return;
        }
        GameManager.Instance.GameOver();
    }
}
