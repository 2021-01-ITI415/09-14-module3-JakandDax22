using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager manager;

    public AudioSource mainMusic;
    public AudioSource auxMusic;
    public AudioSource ambMusic;
    public AudioSource eventMusic;


    private void Awake()
    {
        manager = this;
    }

    public AudioMixerSnapshot eventSnap;
    public AudioMixerSnapshot idleSnapshot;
    public AudioMixerSnapshot currentAudioMixerSnapshot;

    public bool eventRunning;
    public bool auxIn;
    public IEnumerator PlayEventMusic()
    {
        eventRunning = true;
        eventSnap.TransitionTo(0.25f);
        currentAudioMixerSnapshot = eventSnap;

        yield return new WaitForSeconds(0.3f);
        eventMusic.Play();
        while (eventMusic.isPlaying)
        {
            yield return null;
        }
        eventRunning = false;
        //idleSnapshot.TransitionTo(0.5f);
        yield break;
    }
}
