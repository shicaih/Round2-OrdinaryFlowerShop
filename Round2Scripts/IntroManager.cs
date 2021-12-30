using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IntroManager: MonoBehaviour
{
    public GameObject board, introCanvas;
    public Transform boardPoint;
    public static GameStatusEvent IntroAnimStart, IntroAnimEnd;
    public AudioClip intro;
    public TMPro.TextMeshProUGUI introTextObj;
    public Animator[] animArray = new Animator[introText.Length];
    public Animator boardAni, loaderAni;

    AudioSource audioManager;
    private string textToShow;
    private int textIndex = 0;
    private float writeSpeed = 0.1f;
    private static string[] introText = new string[]
    {
        "Somewhere in the universe, there exists an ordinary flowershop",
        "which sells the most ordinary flowers.",
        "This quite little shop is run by a",
        "resourceful florist you.        "
    };

    void Awake()
    {
        textToShow = introText[textIndex];
        audioManager = GetComponent<AudioSource>();
        TextWriter.SingleWriterFinishedEvent += NextLine;
        IntroAnimStart += WriteText;
        IntroAnimEnd += OnIntroAnimEnd;
        Board.BoardFlipped += OnBoardFlipped;
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartIntro());
    }

    IEnumerator StartIntro()
    {
        yield return new WaitForSeconds(1.5f);
        IntroAnimStart.Invoke();
        audioManager.PlayOneShot(intro);
    }

    void WriteText()
    {
        TextWriter.AddWriter_Static(textToShow, introTextObj, writeSpeed, true);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void NextLine()
    {
        if (textIndex < introText.Length - 1)
        {
            animArray[textIndex].SetTrigger("Fade");
            textIndex++;
            textToShow = introText[textIndex];
            TextWriter.AddWriter_Static(textToShow, introTextObj, writeSpeed, true);

        }
        else
        {
            IntroAnimEnd.Invoke();
        }

    }

    void OnIntroAnimEnd()
    {
        GameObject boardInstance = Instantiate(board, boardPoint.position, Quaternion.Euler(0f, -90f, 0f));
        introCanvas.SetActive(false);
    }

    void OnBoardFlipped()
    {
        loaderAni.SetTrigger("Fade");
        StartCoroutine(EndScene());
    }
    
    IEnumerator EndScene()
    {
        
        yield return new WaitForSeconds(1);
        TextWriter.SingleWriterFinishedEvent -= NextLine;
        IntroAnimStart -= WriteText;
        IntroAnimEnd -= OnIntroAnimEnd;
        Board.BoardFlipped -= OnBoardFlipped;
        SceneManager.LoadScene("Shicai V1.0");
    }
}
