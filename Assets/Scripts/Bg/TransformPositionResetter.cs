using UnityEngine;

public class TransformPositionResetter : MonoBehaviour
{
    [SerializeField] private Transform Object;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float startingXPosition;
    [SerializeField] private float resetPositionWhenXPositionValueisBelow;

    private void Update()
    {
        if (!GameManager.Instance || !GameManager.Instance.isGameStarted || GameManager.Instance.isGameOver)
        {
            return;
        }
        if(Object.position.x<=resetPositionWhenXPositionValueisBelow)
        {
            Object.position = new Vector3(startingXPosition, Object.position.y, Object.position.z);
        }
        Object.position += new Vector3(-1 * moveSpeed * Time.deltaTime,0,0);
    }
    //startingXPosition=8.72f;
    //resetPositionWhenXPositionValueisBelow=-10.08f;

}
