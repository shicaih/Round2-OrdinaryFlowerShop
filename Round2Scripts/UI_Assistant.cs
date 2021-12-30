using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_Assistant : MonoBehaviour
{
    public static GameStatusEvent IntroAnimEnd;
    
    public TMPro.TextMeshProUGUI introTextObj;
    public Animator[] animArray = new Animator[introText.Length];

    private string textToShow;
    private int textIndex = 0;
    private float writeSpeed = 0.1f;
    private static string[] introText = new string[]
    {
        "Somewhere in the ordinary universe, there exists an ordinary flowershop, which sells the most ordinary flowers one can ordinarily imagine.",
        "On this ordinary day, this ordinary flower shop ordinarily opens, awaiting ordinary customers, with the ordinary flowers",
        "and an ordinary owner -- you.               "
    };
    
    void Awake()
    {
        textToShow = introText[textIndex];
        GameManager.IntroStart += WriteText;
        TextWriter.SingleWriterFinishedEvent += NextLine;
    }
    // Start is called before the first frame update
    void Start()
    {

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
}
