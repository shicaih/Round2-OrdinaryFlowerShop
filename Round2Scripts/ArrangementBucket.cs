using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrangementBucket : MonoBehaviour
{
    public static CheckOutEvent paperSettingGenerated;
    public GameObject paperSetting;
    public Transform newPaperPosition;
    public static bool isGenerated = false;

    private void OnTriggerExit(Collider other)
    {
        //Debug.Log(other.gameObject.name);
        if (other.gameObject.tag == "PaperSetting" && !isGenerated)
        {
            PaperSetting paperSettingInstance = Instantiate(paperSetting, newPaperPosition.position, Quaternion.identity).GetComponent<PaperSetting>();
            isGenerated = true;
            paperSettingGenerated.Invoke(paperSettingInstance.flowerDic, paperSettingInstance.GetComponent<Collider>());
        }
    }

    private void Update()
    {
        if (PaperSetting.numOfPaperSetting == 0)
        {
            Instantiate(paperSetting, newPaperPosition.position, Quaternion.identity).GetComponent<PaperSetting>();
        }
    }
}
