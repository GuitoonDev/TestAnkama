using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; set; }

    [Header("Tracks")]
    [SerializeField] private AudioSource sfxTrack = null;
    [SerializeField] private AudioSource musicTrack = null;

    [Header("Playable Musics")]
    [SerializeField] private AudioClip gameMusic = null;


    private void Awake() {
        if (Instance == null) {
            Instance = this;
        }
        else if (Instance != this) {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Start() {
        if (gameMusic != null) {
            PlayMusic(gameMusic);
        }
    }

    private void PlayMusic(AudioClip _playableMusic) {
        if (musicTrack.clip != _playableMusic) {
            musicTrack.Stop();
            musicTrack.clip = _playableMusic;
            musicTrack.Play();
        }
    }

    public void PlaySound(AudioClip _audioClip) {
        sfxTrack.PlayOneShot(_audioClip);
    }
}
