using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Audio Sources")]
    [SerializeField] private AudioSource MusicSource;    // For playing background music
    [SerializeField] private AudioSource SFXSource;      // For playing sound effects

    [Header("Audio Clips")]
    [SerializeField] private AudioClip backgroundMusic;  

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keep AudioManager persistent across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        PlayMusic(); // Start background music when game begins
    }

    public void PlaySFX(AudioClip clip, float minPitch = 1f, float maxPitch = 1f)
    {
        SFXSource.pitch = Random.Range(minPitch, maxPitch);
        SFXSource.PlayOneShot(clip);
    }

    public void PlayMusic()
    {
        Debug.Log("Trying to play music...");

        if (MusicSource == null)
        {
            Debug.LogError("No AudioSource found!");
            return;
        }

        if (backgroundMusic == null)
        {
            Debug.LogError("No background music assigned!");
            return;
        }

        MusicSource.clip = backgroundMusic;
        MusicSource.loop = true;

        Debug.Log("MusicSource.isPlaying: " + MusicSource.isPlaying);

        if (!MusicSource.isPlaying)
        {
            Debug.Log("Music played");
            MusicSource.Play();
        }
        else
        {
            Debug.Log("Music was already playing.");
        }
    }
}