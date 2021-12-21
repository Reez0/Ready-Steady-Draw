using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UI : MonoBehaviour
{
    public string[] sentences = {"I'm gonna pump ya' full of lead.",
        "If somebody outdraws you boy, walk away. There's plenty o' time to look tough when your outta' sight",
        "Tellin' a man to git lost and makin' him do it are two entirely different propositions.",
        "Don't worry - I've been in tighter spots than this.",
        "Being silent, may be your best answer.",
        "Dying ain't much of a living, boy.",
        "Go ahead, make my day.",
        "Ever notice how you come across somebody once in a while you shouldn't have f***ed with? That's me.",
        "With all due respect, sir, you're beginning to bore the hell out of me.",
        "'Bout time this town had a new sheriff.",
        "I'll blow a hole in your face then go inside and sleep like a baby."};
    public bool showText = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private string GetRandomText() {


        var randomNum = Random.Range(0,sentences.Length);
        return sentences[randomNum];
    }

    public void ChangeText() {
        GameObject[] speechText = GameObject.FindGameObjectsWithTag("Speech");

        speechText[0].GetComponent<Text>().text = GetRandomText();
        speechText[1].GetComponent<Text>().text = GetRandomText();
        if (speechText[0].GetComponent<Text>().text == speechText[1].GetComponent<Text>().text) {
            speechText[0].GetComponent<Text>().text = GetRandomText();
        }
    }

    public void ResetText() {
        GameObject[] speechText = GameObject.FindGameObjectsWithTag("Speech");

        speechText[0].GetComponent<Text>().enabled = false;
        speechText[1].GetComponent<Text>().enabled = false;
    }

    public void ShowDrawText() {
        GameObject drawText = GameObject.FindGameObjectsWithTag("DrawText")[0];
        drawText.GetComponent<Text>().text = "DRAW";
    }

    public void ShowJammedText() {
        GameObject jammedText = GameObject.FindGameObjectWithTag("JammedText");
        jammedText.GetComponent<Text>().text = "Your weapon has jammed!";
    }

    public void ShowResultWinText(bool result) {
        GameObject resultText = GameObject.FindGameObjectWithTag("ResultText");
        if (result) {
            var resultText2 = resultText.GetComponent<Text>();
             resultText2.text = "You outdrew your opponent!";
             resultText2.color = Color.blue;
        } else {
            var resultText2 = resultText.GetComponent<Text>();
             resultText2.text = "Too slow on the draw";
             resultText2.color = Color.red;
        }
        
    }
}
