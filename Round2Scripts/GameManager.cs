using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public delegate void CustomerReactionEvent();
public delegate void GameStatusEvent();
public delegate void CheckOutEvent(Dictionary<string, bool> flowerDic, Collider other);




public class GameManager : MonoBehaviour
{
    public Dictionary<string, int> funeralMoodMap = new Dictionary<string, int>();

    public static CustomerReactionEvent CustomerReactionEvent;
    public static GameStatusEvent GameStatusEvent;
    public static event GameStatusEvent IntroStart, IntroEnd;
    public static event GameStatusEvent NewCustomerArrive;
    public static event GameStatusEvent StoreClosed;
    public Animator loaderAni;
 
    public AudioClip intro, flowershop;
    public Transform spawnPoint, boardPoint;
    public GameObject fireGuy, bear, girl, board;
    public Transform turningTray;

    public enum GameStatus { firstCus, secondCus, thirdCus, cusGone, sceneEnd };
    public GameStatus gameStatus = GameStatus.firstCus;

    // public float moneyMade = 0.00f;
    public static bool servicing = false;

    GameObject customerNow;
    AudioSource audioManager;


    private void Awake()
    {
        GameObject.Find("HumanScale").SetActive(false);
        audioManager = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Customer.AnimationFinished += OnNewCustomerArrive;
        Bear.AnimationFinished += OnNewCustomerArrive;
        Girl.AnimationFinished += OnNewCustomerArrive;
        Board.BoardFlipped += OnBoardFlipped;
        audioManager.clip = flowershop;
        audioManager.Play();
        audioManager.loop = true;

    }


    // Update is called once per frame
    void Update()
    {
        if (!servicing)
        {
            NextCustomer();
            gameStatus++;
        }

        if (gameStatus == GameStatus.sceneEnd)
        {
            GameObject boardInstance = Instantiate(board, boardPoint.position, Quaternion.Euler(0f, -90f, 0f));
            gameStatus++;
        }

    }

    void OnNewCustomerArrive()
    {

        
    }

    public void NextCustomer()
    {
        if (gameStatus == GameStatus.firstCus)
        {
            Debug.Log(gameStatus);
            customerNow = Instantiate(fireGuy, spawnPoint.position, Quaternion.identity);
            servicing = true;
        }
        else if (gameStatus == GameStatus.secondCus)
        {
            Debug.Log(gameStatus);
            customerNow = Instantiate(bear, spawnPoint.position, Quaternion.identity);
            servicing = true;
        }
        else if (gameStatus == GameStatus.thirdCus)
        {
            customerNow = Instantiate(girl, spawnPoint.position, Quaternion.identity);
            servicing = true;
        }

    }

    void OnBoardFlipped()
    {
        loaderAni.SetTrigger("Fade");
        StartCoroutine(EndScene());
    }
    IEnumerator EndScene()
    {
        
        yield return new WaitForSeconds(1);
        Customer.AnimationFinished -= OnNewCustomerArrive;
        Bear.AnimationFinished -= OnNewCustomerArrive;
        Girl.AnimationFinished -= OnNewCustomerArrive;
        Board.BoardFlipped -= OnBoardFlipped;
        SceneManager.LoadScene("EndingScene");
    }
}
