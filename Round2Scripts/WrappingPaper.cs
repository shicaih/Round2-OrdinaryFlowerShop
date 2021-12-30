using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrappingPaper : MonoBehaviour
{
    public GameObject newPaperSetting;
    //public GameObject UIchangeColor;
    public Transform paperNewPosition, bouquet;

    public void NewPrefab()
    {

        //Debug.Log("new a flower" + flowernew.name);
        Instantiate(newPaperSetting, paperNewPosition.position, Quaternion.identity, bouquet);
        //Show(UIchangeColor);

    }

    void Show(GameObject prompt)
    {
        prompt.SetActive(true);
    }
    void Hide(GameObject prompt)
    {
        prompt.SetActive(false);
    }
}