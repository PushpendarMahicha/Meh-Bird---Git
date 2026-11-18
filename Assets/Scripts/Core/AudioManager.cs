using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioSource bgMusicSource;
    [SerializeField] private AudioClip flapClip;
    [SerializeField] private AudioClip scoreClip;
    [SerializeField] private AudioClip gameOverClip;
    [SerializeField] private AudioClip laserClip;
    [SerializeField] private AudioClip bgMusicClip;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(this);
    }
    private void Start()
    {
        if (bgMusicClip == null || bgMusicSource==null)
        {
            Debug.LogWarning("GameOver sound not added." + this);
            return;
        }
        bgMusicSource.clip = bgMusicClip;
        bgMusicSource.loop = true;
        bgMusicSource.Play();
    }
    public void SetMasterVolume(float volume)
    {
        AudioListener.volume = Mathf.Clamp01(volume);
    }
    public void PlayFlap()
    {
        if(flapClip==null)
        {
            Debug.LogWarning("Flap Sound Not added." + this);
            return;
        }
        sfxSource.PlayOneShot(flapClip);
    }
    public void PlayScoreClip()
    {
        if(scoreClip==null)
        {
            Debug.LogWarning("Score sound not added." + this);
            return;
        }
        sfxSource.PlayOneShot(scoreClip);
    }
    public void PlayLaserClip()
    {
        sfxSource.PlayOneShot(laserClip);
    }
    public void PlayGameOverClip()
    {
        if (gameOverClip == null)
        {
            Debug.LogWarning("GameOver sound not added." + this);
            return;
        }
        sfxSource.PlayOneShot(gameOverClip);
    }

}
