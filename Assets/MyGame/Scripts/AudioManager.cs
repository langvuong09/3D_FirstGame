using UnityEngine;
[AddComponentMenu("TienCuong/AudioManager")]
public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;
    public static AudioManager Instance
    {
        get => instance;
    }
    [Header("Audio Clip")]
    public AudioSource musicSource;
    public AudioSource sfxSourcePlayer;
    public AudioSource sfxSourceEnemy;
    [Header("Audio Clip background")]
    public AudioClip backGroundMusic;
    private void Awake()
    {
        if (instance != null)
        {
            DestroyImmediate(gameObject);
            return;
        }
        instance = this;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayerMusic(backGroundMusic);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlaysfxPlayer(AudioClip clip)
    {
        sfxSourcePlayer.PlayOneShot(clip);
    }
    public void PlaysfxEnemy(AudioClip clip)
    {
        sfxSourceEnemy.PlayOneShot(clip);
    }

    public void PlayerMusic(AudioClip clip)
    {
        musicSource.clip = clip;
        musicSource.loop = true;
        musicSource.Play();
    }
}
