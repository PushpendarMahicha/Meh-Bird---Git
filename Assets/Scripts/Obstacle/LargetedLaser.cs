using UnityEngine;
using System.Collections;

public class LargetedLaser : MonoBehaviour
{
    [SerializeField] private Transform player, Circle;
    [SerializeField] private SpriteRenderer CircleInnerColorRenderer, CircleOutlineColorRenderer;
    [SerializeField] private GameObject Laser;
    [SerializeField] private float circleShrinkInSecond;
    private float elapsedTime;
    [SerializeField] private float circleStartSize, circleLaserInitiationSize, circleWarningSize, circleEndSize;
    [SerializeField] private Color innerCircleWarningColor, outerCircleWarningColor, innerCircleStartingColor, outerCircleStartingColor, innerCircleLaserInitilizeColor, outerCircleLaserInitilizeColor;
    private bool isfirelaser = true;
    [SerializeField] private float laserStayForSeconds, laserShowInSeconds;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        Circle.gameObject.SetActive(true);
        Circle.localScale = new Vector3(circleStartSize, circleStartSize, 0);
    }
    private void Update()
    {
            if (!GameManager.Instance || GameManager.Instance.isGameOver || !GameManager.Instance.isGameStarted)
            {
                return;
            }
        if (isfirelaser)
        {
            elapsedTime += Time.deltaTime;
            float progress = Mathf.Clamp01(elapsedTime / circleShrinkInSecond);
            float currentSize = Mathf.Lerp(circleStartSize, circleEndSize, progress);
            Circle.transform.localScale = Vector3.one * currentSize;
            transform.position = player.position;

            float currentCircleSize = Circle.localScale.x;
            if (currentCircleSize < circleWarningSize && currentCircleSize > circleLaserInitiationSize)
            {
                CircleInnerColorRenderer.color = innerCircleWarningColor;
                CircleOutlineColorRenderer.color = outerCircleWarningColor;
            }
            if (currentCircleSize < circleStartSize && currentCircleSize > circleWarningSize)
            {
                CircleInnerColorRenderer.color = innerCircleStartingColor;
                CircleOutlineColorRenderer.color = outerCircleStartingColor;
            }
            if (currentCircleSize < circleLaserInitiationSize)
            {
                CircleInnerColorRenderer.color = innerCircleLaserInitilizeColor;
                CircleOutlineColorRenderer.color = outerCircleLaserInitilizeColor;
                StartCoroutine(LaserDmgActive());
                Laser.transform.position = player.position;
                isfirelaser = false;
            }
        }

        

    }
    private IEnumerator LaserDmgActive()
    {
        yield return new WaitForSeconds(laserShowInSeconds);
        Laser.SetActive(true);
        AudioManager.Instance.PlayLaserClip();
        yield return new WaitForSeconds(laserStayForSeconds);
        Laser.SetActive(false);
        Destroy(this.gameObject);
    }
    
}
