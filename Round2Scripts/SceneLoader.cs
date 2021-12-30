using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private Animator loaderAni;
    public GameObject button;
    // Start is called before the first frame update
    void Start()
    {
        loaderAni = GameObject.Find("LoaderImage").GetComponent<Animator>();
        StartCoroutine(ShowButton());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartOver()
    {
        StartCoroutine(SceneChange());
    }

    IEnumerator SceneChange()
    {
        yield return new WaitForSeconds(1f);
        loaderAni.SetTrigger("Fade");
        SceneManager.LoadScene(0);
    }
    IEnumerator ShowButton()
    {
        yield return new WaitForSeconds(30f);
        button.SetActive(true);
    }
}
