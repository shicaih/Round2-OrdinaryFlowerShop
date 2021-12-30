using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperSetting : MonoBehaviour
{
    public static event GameStatusEvent FlowerLit, FlowerHoneyed, FlowerFireworked;
    public static int numOfPaperSetting = 0;

    public bool newFlowerBunch = false;
    public int numOfPaperSettingDebug;
    public GameObject roseBunch;
    public GameObject lilyBunch;
    public GameObject daisyBunch;
    public GameObject tulipBunch;
    public GameObject candleLightFire;
    public GameObject beeEffects;
    public GameObject fireworkEffects;
    public Transform branch;
    public Dictionary<string, bool> flowerDic = new Dictionary<string, bool>()
    {
        {"isRose", false },
        {"isLily", false },
        {"isDaisy", false },
        {"isTulip", false },
        {"hasFire", false },
        {"hasHoney", false },
        {"hasFirework", false },
    };
    public AudioClip ignite, honeyed, fireworked;

    public Vector3 originalPosition;
    AudioSource audioManager;
    // Start is called before the first frame update
    void Start()
    {
        originalPosition = transform.position;
        audioManager = GetComponent<AudioSource>();
        Debug.Log("Num + 1");
        numOfPaperSetting += 1;
    }
    // Update is called once per frame
    void Update()
    {
        numOfPaperSettingDebug = numOfPaperSetting;
        // Debug.Log(flowerDic.ContainsKey("Main Flower"));

    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Rose" && !newFlowerBunch)
        {
            Debug.Log("摆放Rose花朵了");
            newFlowerBunch = true;
            roseBunch.SetActive(true);
            Destroy(other.gameObject);
            flowerDic["isRose"] = true;

        }
        if (other.gameObject.tag == "Lily" && !newFlowerBunch)
        {
            Debug.Log("摆放Lily花朵了");
            newFlowerBunch = true;
            lilyBunch.SetActive(true);
            Destroy(other.gameObject);
            flowerDic["isLily"] = true;
        }
        if (other.gameObject.tag == "Daisy" && !newFlowerBunch)
        {
            Debug.Log("摆放Daisy花朵了");
            newFlowerBunch = true;
            daisyBunch.SetActive(true);
            Destroy(other.gameObject);
            flowerDic["isDaisy"] = true;
        }
        if (other.gameObject.tag == "Tulip" && !newFlowerBunch)
        {
            Debug.Log("摆放Tulip花朵了");
            newFlowerBunch = true;
            tulipBunch.SetActive(true);
            Destroy(other.gameObject);
            flowerDic["isTulip"] = true;
        }
        if (other.gameObject.tag == "Candle" && newFlowerBunch)
        {
            candleLightFire.SetActive(true);
            flowerDic["hasFire"] = true;
            audioManager.PlayOneShot(ignite);
            if (flowerDic["isRose"])
            {
                FlowerLit.Invoke();
            }
            
        }
        if (other.gameObject.tag == "Honey" && newFlowerBunch)
        {
            beeEffects.SetActive(true);
            flowerDic["hasHoney"] = true;
            audioManager.PlayOneShot(honeyed);
            if (flowerDic["isLily"])
            {
                FlowerHoneyed.Invoke();
            }
            
        }
        if (other.gameObject.tag == "Firework" && newFlowerBunch)
        {
            fireworkEffects.SetActive(true);
            flowerDic["hasFirework"] = true;
            audioManager.PlayOneShot(fireworked);
            if (flowerDic["isTulip"])
            {
                FlowerFireworked.Invoke();
            }
            
        }
    }

    private void OnDestroy()
    {
        Debug.Log("Num - 1");
        numOfPaperSetting -= 1;
    }

}
