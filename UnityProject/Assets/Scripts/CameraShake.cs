﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    // Coroutine that can be called by the enemy, needs to be called in script with StartCoroutine(Shake());
    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 startPos = transform.localPosition;
        
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(x, y, startPos.z);

            elapsed += Time.deltaTime;

            yield return null;
        }
    }
}
