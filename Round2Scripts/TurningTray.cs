using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurningTray : MonoBehaviour
{
    float turningTime = 1f;

    bool isRotatingForward = false;
    bool isRotatingBack = false;
    bool isStill = true;
    float previousRot, angleMove;
    float timeE;

    private void Awake()
    {
        previousRot = transform.localRotation.eulerAngles.y;
    }

    public void RotateForward()
    {
        if (isStill)
        {
            previousRot = transform.localRotation.eulerAngles.y;
            isRotatingForward = true;
            timeE = 0;
            StartCoroutine(ResetTray());
        }

    }

    public void RotateBack()
    {
        if (isStill)
        {
            previousRot = transform.localRotation.eulerAngles.y;
            isRotatingBack = true;
            timeE = 0;
            StartCoroutine(ResetTray());
        }
    }

    void Update()
    {
        if (isRotatingForward)
        {
            angleMove = Mathf.Lerp(previousRot, previousRot + 360 / 7, timeE);   
            transform.localRotation = Quaternion.Euler(0, angleMove, 0);
            timeE += Time.deltaTime;
        }
        else if (isRotatingBack)
        {
            angleMove = Mathf.Lerp(previousRot, previousRot - 360 / 7, timeE);
            transform.localRotation = Quaternion.Euler(0, angleMove, 0);
            timeE += Time.deltaTime;
        }
    }
    
    IEnumerator ResetTray()
    {
        yield return new WaitForSeconds(1);
        isRotatingBack = false;
        isRotatingForward = false;
        timeE = 0;
        isStill = true;
    }
}
