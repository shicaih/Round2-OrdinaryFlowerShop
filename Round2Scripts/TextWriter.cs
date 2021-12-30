using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextWriter : MonoBehaviour
{
    public static GameStatusEvent SingleWriterFinishedEvent;
    private static TextWriter instance;
    private List<TextWriterSingle> textWriterSingleList;

    private void Awake()
    {
        instance = this;
        textWriterSingleList = new List<TextWriterSingle>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }


    private void Update()
    {
        for (int i = 0; i < textWriterSingleList.Count; i++)
        {
            bool destroyInstance = textWriterSingleList[i].Update();
            if (destroyInstance)
            {
                textWriterSingleList.RemoveAt(i);
                SingleWriterFinishedEvent.Invoke();
                i--;
            }
        }

    }

    public static void AddWriter_Static(string textToShow, TMPro.TextMeshProUGUI uiText, float timeEachChar, bool invisibleChars)
    {
        instance.AddWriter(textToShow, uiText, timeEachChar, invisibleChars);
    }

    public void AddWriter(string textToShow, TMPro.TextMeshProUGUI uiText, float timeEachChar, bool invisibleChars)
    {
        textWriterSingleList.Add(new TextWriterSingle(textToShow, uiText, timeEachChar, invisibleChars));
    }


    public class TextWriterSingle
    {
        private string textToShow;
        private TMPro.TextMeshProUGUI uiText;
        private float timer;
        private int charIndex;
        private float timeEachChar;
        private bool invisibleChars;

        public TextWriterSingle(string textToShow, TMPro.TextMeshProUGUI uiText, float timeEachChar, bool invisibleChars)
        {
            this.textToShow = textToShow;
            this.uiText = uiText;
            this.timeEachChar = timeEachChar;
            this.invisibleChars = invisibleChars;
        }

        public bool Update()
        {

            timer -= Time.deltaTime;
            while (timer <= 0f)
            {
                // show next character
                timer += timeEachChar;
                charIndex++;
                string text = textToShow.Substring(0, charIndex);
                if (invisibleChars)
                {
                    text += "<color=#00000000>" + textToShow.Substring(charIndex) + "</color>";
                }
                uiText.text = text;

                if (charIndex == textToShow.Length)
                {
                    return true;
                }
            }
            return false;

        }
    }

    // Update is called once per frame

}
