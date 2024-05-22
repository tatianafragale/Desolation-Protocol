using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieEating : MonoBehaviour
{
    public float initialDelay = 9.0f;
    public float delayBetweenLoops = 5.0f;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = false;
        StartCoroutine(DelayedLoop());
    }

    IEnumerator DelayedLoop()
    {
        yield return new WaitForSeconds(initialDelay);

        while (true)
        {
            audioSource.Play();
            yield return new WaitForSeconds(audioSource.clip.length + delayBetweenLoops);
        }
    }
}
