using UnityEngine;
using System.Collections;

public class GameObjectDestroyInGivenTime : MonoBehaviour
{
    [SerializeField] float timeInSeconds;
    private void Start()
    {
        StartCoroutine(die());
    }
    private IEnumerator die()
    {
        yield return new WaitForSeconds(timeInSeconds);
        Destroy(gameObject);
    }
}
