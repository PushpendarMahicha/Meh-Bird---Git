using UnityEngine;
using System.Collections;

public class TargetedLaserAttack : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform player;
    [SerializeField] private Transform targetCircle;
    [SerializeField] private LaserBeam LaserBeam;
    [SerializeField] private SpriteRenderer innerLine;
    [SerializeField] private SpriteRenderer outerLine;

    [Header("Timing")]
    [SerializeField] private float aimDuration = 2f;
    [SerializeField] private float warningDuration = 0.2f;
    [SerializeField] private float beamDuration = 1f;

    [Header("Target Circle")]
    [SerializeField] private float startScale = 3f;
    [SerializeField] private float endScale = 0.1f;

    [Header("Beam Colors")]
    [SerializeField] private Color warningColor = Color.green;
    [SerializeField] private Color activeColor = Color.red;

    private bool isRunning;
    private void Awake()
    {
        targetCircle.gameObject.SetActive(false);
        LaserBeam.gameObject.SetActive(false);
    }
    private void Start()
    {
        StartAttack();
    }
    public void StartAttack()
    {
        if(isRunning)
        {
            return;
        }
        StartCoroutine(AttackSequence());
    }
    private IEnumerator AttackSequence()
    {
        isRunning = true;
        targetCircle.gameObject.SetActive(true);
        targetCircle.localScale = Vector3.one * startScale;
        float elapsed = 0f;
        while(elapsed<aimDuration)
        {
            if(player==null)
            {
                break;
            }
            targetCircle.position = player.position;
            float progress = elapsed / aimDuration;

            float scale = Mathf.Lerp(startScale, endScale, progress);
            targetCircle.localScale = Vector3.one * scale;

            elapsed += Time.deltaTime;
            yield return null;

        }
        if(player==null)
        {
            targetCircle.gameObject.SetActive(false);
            isRunning = false;
            yield break;
        }
        Vector3 lockedPosition = targetCircle.position;
        targetCircle.gameObject.SetActive(false);
        LaserBeam.transform.position = new Vector3(LaserBeam.transform.position.x,lockedPosition.y,LaserBeam.transform.position.z);

        outerLine.color = warningColor;
        innerLine.color = warningColor;

        LaserBeam.gameObject.SetActive(true);

        yield return new WaitForSeconds(warningDuration);

        outerLine.color = activeColor;
        outerLine.color = activeColor;

        LaserBeam.SetBeamActive(false);
        LaserBeam.gameObject.SetActive(false);

        isRunning = false;
    }

}
