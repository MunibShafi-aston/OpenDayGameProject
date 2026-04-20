using UnityEngine;
using System.Collections.Generic;

public class soundManager : MonoBehaviour
{
    public static soundManager Instance;

    [Header("Audio Sources")]
    public AudioSource musicSource;
    public AudioSource sfxSource;

    [Header("Sound Library")]
    public List<Sound> sounds = new List<Sound>();

    private Dictionary<string, AudioClip> soundDict;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        soundDict = new Dictionary<string, AudioClip>();

        foreach (var sound in sounds)
        {
            soundDict[sound.name] = sound.clip;
        }
    }

    public void PlaySFX(string name)
    {
        if (soundDict.TryGetValue(name, out AudioClip clip))
        {
            sfxSource.pitch = Random.Range(0.9f, 1.1f);
            sfxSource.PlayOneShot(clip, 0.8f);
            sfxSource.pitch = 1f;
        }
        else
        {
            Debug.LogWarning("Sound not found: " + name);
        }
    }
    public void StopAllSFX()
    {
        sfxSource.Stop();
    }
    public void PlayMusic(string name)
    {
        if (soundDict.TryGetValue(name, out AudioClip clip))
        {
            if (musicSource.clip == clip && musicSource.isPlaying)
                return;

            musicSource.Stop();
            musicSource.clip = clip;
            musicSource.loop = true;
            musicSource.volume = 0.3f;
            musicSource.Play();
        }
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }
}