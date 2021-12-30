using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashBin : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        string tag = other.gameObject.tag;
        if (tag != "Candle" && tag != "Honey" && tag != "Firework" && tag != "Env")
        {
            Destroy(other.gameObject);
            //PaperSetting.numOfPaperSetting -= 1;
            ArrangementBucket.isGenerated = false;
        }
    }
}
