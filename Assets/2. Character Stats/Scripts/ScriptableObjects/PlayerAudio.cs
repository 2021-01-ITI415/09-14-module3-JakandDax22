using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerAudio : MonoBehaviour
{
    public AudioClip splashSound;
    public AudioSource audioS;
    public AudioMixerSnapshot idleSnapshot;
    public AudioMixerSnapshot auxInSnapshot;
    public AudioMixerSnapshot ambidleSnapshot;
    public AudioMixerSnapshot ambInSnapshot;
    public LayerMask enemyMask;
    bool enemyNear;

    public void Update()
    {
        RaycastHit[] hits = Physics.SphereCastAll(transform.position, 5f, transform.forward, 0f, enemyMask);
        
        //New code starts here
        if (hits.Length > 0)
        {
            enemyNear = true;
        }
        else
        {
            enemyNear = false;
        }
        if (!AudioManager.manager.eventRunning)
        {

            if (enemyNear)
            {
                if (AudioManager.manager.auxIn)
                {
                    auxInSnapshot.TransitionTo(0.5f);
                    AudioManager.manager.currentAudioMixerSnapshot = auxInSnapshot;
                    AudioManager.manager.auxIn = true;
                }
                else
                {
                    if (AudioManager.manager.currentAudioMixerSnapshot == AudioManager.manager.eventSnap)
                    {
                        auxInSnapshot.TransitionTo(0.5f);
                        AudioManager.manager.currentAudioMixerSnapshot = auxInSnapshot;
                        AudioManager.manager.auxIn = true;
                    }
                }
            }
            else
            {
                if (AudioManager.manager.auxIn)
                {
                    idleSnapshot.TransitionTo(0.5f);
                    AudioManager.manager.currentAudioMixerSnapshot = idleSnapshot;
                    AudioManager.manager.auxIn = false;
                }
                else
                {
                    if (AudioManager.manager.currentAudioMixerSnapshot == AudioManager.manager.eventSnap)
                    {
                        idleSnapshot.TransitionTo(0.5f);
                        AudioManager.manager.currentAudioMixerSnapshot = idleSnapshot;
                        AudioManager.manager.auxIn = false;
                    }
                }
            }
        }
        //New code ends here
        if (hits.Length > 0)
        {
            if (!enemyNear)
            {
                auxInSnapshot.TransitionTo(0.5f);
                enemyNear = true;
            }
        }
        else
        {
            if (enemyNear)
            {
                idleSnapshot.TransitionTo(0.5f);
                enemyNear = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            audioS.PlayOneShot(splashSound);
        }
        if (other.CompareTag("EnemyZone"))
        {
            auxInSnapshot.TransitionTo(0.5f);
        }
        if (other.CompareTag("Ambience"))
        {
            ambInSnapshot.TransitionTo(0.5f);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            audioS.PlayOneShot(splashSound);
        }
        if (other.CompareTag("EnemyZone"))
        {
            idleSnapshot.TransitionTo(0.5f);
        }
        if (other.CompareTag("Ambience"))
        {
            ambidleSnapshot.TransitionTo(0.5f);
        }
    }
}
