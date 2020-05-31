using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeechBox : MonoBehaviour
{
    public Text textBox;
    public GameObject speechBubble;

    List<string> speechText = new List<string>
    {
        "This is a test of the three different strings",
        "Here is the second string I am testing.",
        "And finally, the third string."
    };

    void Start()
    {
        HideSpeechBubble();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
            StartCoroutine(SpeechBubbleStart());
    }

    IEnumerator SpeechBubbleStart()
    {
        foreach (string sentence in speechText)
        {
            yield return new WaitForSeconds(1f);
            ShowSpeechBubble();
            textBox.text = sentence;
            yield return new WaitForSeconds(5f);
            HideSpeechBubble();
        }
    }

    void ShowSpeechBubble()
    {
        speechBubble.SetActive(true);
    }

    void HideSpeechBubble()
    {
        speechBubble.SetActive(false);
    }
}
