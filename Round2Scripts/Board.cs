using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public static GameStatusEvent BoardFlipped;

    public AudioClip startSound;
    AudioSource audioManager;
    // Start is called before the first frame update
    void Start()
    {
        audioManager = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.rotation.eulerAngles.y > 30f && transform.rotation.eulerAngles.y < 150f)
        {
            audioManager.PlayOneShot(startSound);
            BoardFlipped.Invoke();
        }
    }

    public void NextScene()
    {
        audioManager.PlayOneShot(startSound);
        BoardFlipped.Invoke();
    }
}
