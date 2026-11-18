using UnityEngine;
using System.Collections;

public class Targeted_Laser_Initilization_TriggerObject : MonoBehaviour
{
    [SerializeField] private GameObject TargetedLaserPrefab, Player;
    [SerializeField] private int score;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Instantiate(TargetedLaserPrefab, Player.transform.position, Quaternion.identity);
            GameManager.Instance.AddScore(score);        
            Destroy(this.gameObject);
        }
    }
}
