using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXObjects : MonoBehaviour
{
    Vector3 originalPosition;
    Vector3 originalRotation;
    private void Awake()
    {
        originalPosition = transform.position;
        originalRotation = transform.rotation.eulerAngles;
    }

    public void ResetPosition()
    {
        transform.position = originalPosition;
        transform.rotation = Quaternion.Euler(originalRotation);
    }
}
