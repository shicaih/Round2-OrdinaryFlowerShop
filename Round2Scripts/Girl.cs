using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Girl : MonoBehaviour
{
    public static event GameStatusEvent AnimationFinished;
    public static event GameStatusEvent CustomerPay;
    public static event CheckOutEvent FlowerRecieved;

    AudioSource audioManager;
    public AudioClip startLine, remind, satisfaction, daisyRej, otherRej, request;
    public UnityEngine.UI.Image emojiImage;
    public Sprite[] emojis;
    public GameObject instructionObj;
    public TMPro.TextMeshProUGUI instructionText;
    public Transform flowerHoldingPosition;
    public GameObject imageObj;

    GameObject newPaperSetting;
    Animator ani;
    float remindTimer = 0f;
    bool tulipButNoFirework = false;
    void Awake()
    {
        PaperSetting.FlowerLit += ReactToFire;
        PaperSetting.FlowerHoneyed += ReactToHoney;
        PaperSetting.FlowerFireworked += ReactToFirework;
        ArrangementBucket.paperSettingGenerated += OnPaperSettingGenerated;
        FlowerRecieved += ReactToFlower;
    }
    public void Start()
    {
        audioManager = GetComponent<AudioSource>();
        audioManager.PlayOneShot(startLine);
        ani = GetComponent<Animator>();
        ani.Play("CustomerCome");
        StartCoroutine(WaitForAnimation());
        
    }

    private void Update()
    {
        if (tulipButNoFirework)
        {
            remindTimer += Time.deltaTime;
            if (remindTimer > 15f)
            {
                audioManager.PlayOneShot(remind);
                remindTimer = 0f;
            }
        }
    }

    void OnPaperSettingGenerated(Dictionary<string, bool> flowerDic, Collider other)
    {
        newPaperSetting = other.gameObject;
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("I got the flower");
        if (other.gameObject.tag == "PaperSetting")
        {
            
            FlowerRecieved.Invoke(other.gameObject.GetComponent<PaperSetting>().flowerDic, other);
        }


    }
    void ReactToFlower(Dictionary<string, bool> flowerDic, Collider other)
    {
        if (flowerDic["isTulip"])
        {
            if (flowerDic["hasFirework"])
            {
                other.gameObject.GetComponent<Collider>().enabled = false;
                instructionText.text = "I love it!";
                emojiImage.sprite = emojis[1];
                remindTimer = -100f;
                audioManager.Stop();
                audioManager.PlayOneShot(satisfaction);
                GameObject customerPaperSetting =
                    Instantiate(other.gameObject, flowerHoldingPosition.position, Quaternion.identity);
                customerPaperSetting.GetComponent<PaperSetting>().flowerDic =
                    other.gameObject.GetComponent<PaperSetting>().flowerDic;
                customerPaperSetting.transform.parent = transform;
                StartCoroutine(WaitForFeedback());
                tulipButNoFirework = false;
            }
            else
            {
                
                GameObject returningPaperSetting = Instantiate
                    (other.gameObject, other.gameObject.GetComponent<PaperSetting>().originalPosition, Quaternion.identity);
                returningPaperSetting.GetComponent<PaperSetting>().flowerDic =
                    other.gameObject.GetComponent<PaperSetting>().flowerDic;
                returningPaperSetting.GetComponent<PaperSetting>().newFlowerBunch = true;
                Destroy(newPaperSetting);

                audioManager.Stop();
                audioManager.PlayOneShot(request);
                tulipButNoFirework = true;
                imageObj.SetActive(true);
                instructionText.text = "I wish to see some firework";
                emojiImage.sprite = emojis[0];

            }
        }
        else
        {
            audioManager.Stop();
            audioManager.PlayOneShot(otherRej);
            instructionText.text = "My friend likes purple";
            emojiImage.sprite = emojis[0];

        }
        Destroy(other.gameObject);
        ArrangementBucket.isGenerated = false;
    }

    IEnumerator WaitForAnimation()
    {
        yield return new WaitForSeconds(1);
        AnimationFinished.Invoke();
    }

    IEnumerator WaitForFeedback()
    {
        yield return new WaitForSeconds(7f);
        ani.Play("CustomerLeave");
        yield return new WaitForSeconds(1f);
        GameManager.servicing = false;
        PaperSetting.FlowerLit -= ReactToFire;
        PaperSetting.FlowerHoneyed -= ReactToHoney;
        PaperSetting.FlowerFireworked -= ReactToFirework;
        ArrangementBucket.paperSettingGenerated -= OnPaperSettingGenerated;
        FlowerRecieved -= ReactToFlower;
        Destroy(gameObject);
    }

    void ReactToFire()
    {

    }

    void ReactToHoney()
    {
        
    }

    void ReactToFirework()
    {
        instructionText.text = "I love this, can I have it?";
        imageObj.SetActive(true);
        emojiImage.sprite = emojis[1];
    }
}
