using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikewall : MonoBehaviour
{
    [SerializeField] private float timeClose;
    [SerializeField] private float timeWait = 2f;
    [SerializeField] private float timeOpen;

    private Quaternion initialRotation;
    private bool isRotating = false;

    private void Start()
    {
        initialRotation = transform.rotation;
    }
    public void RotateWall()
    {
        if (!isRotating)
        {
            isRotating = true;
            Quaternion finalRotation = initialRotation * Quaternion.Euler(0, 90, 0);
            StartCoroutine(RotationCoroutine(finalRotation));
        }
    }

    private IEnumerator RotationCoroutine(Quaternion finalRotation)
    {
        float duration = 0.5f;
        float timer = 0f;
        while (timer < duration)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, finalRotation, timer / duration);
            timer += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(2f);

        duration = 0.5f;
        timer = 0f;
        while (timer < duration)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, initialRotation, timer / duration);
            timer += Time.deltaTime;
            yield return null;
        }

        isRotating = false;
    }
}
