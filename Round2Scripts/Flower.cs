using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : MonoBehaviour
{
    public GameObject flowernew;
    public GameObject UIchangeColor;
    public Transform flowernewPosition, turningTray, bouquet;
    public AudioClip grabSound;
    public bool isManipulated = false;
    bool firstTimeDuplicate = true;
    bool closeOrOpen = false;
    Collider bCollider;
    AudioSource audioManager;

    // Start is called before the first frame update
    void Start()
    {
        bCollider = GetComponent<Collider>();
        audioManager = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void manipulateStart()
    {
        isManipulated = true;
        audioManager.PlayOneShot(grabSound);
        if (firstTimeDuplicate)
        {
            Debug.Log("new a flower" + flowernew.name);
            Instantiate(flowernew, flowernewPosition.position, Quaternion.identity, turningTray);
            Show(UIchangeColor);
            firstTimeDuplicate = false;
            transform.parent = null;
        }
            
    }

    public void manipulateStop()
    {
        isManipulated = false;
    }

    public void ShowObject()
    {
        Show(UIchangeColor);
    }

    public void UnshowObject()
    {
        Hide(UIchangeColor);
    }

    void Show(GameObject prompt)
    {
        prompt.SetActive(true);
    }

    void Hide(GameObject prompt)
    {
        prompt.SetActive(false);
    }

    void OnTriggerStay(Collider colliderW)
    {
        Debug.Log("we hit sth");
        if (!isManipulated && colliderW.gameObject.tag == "Bouquet")
        {
            Debug.Log("we hit the bouquet");
            transform.parent = bouquet;
            transform.position = bouquet.position;
            bCollider.enabled = false;

        }
    }
}
