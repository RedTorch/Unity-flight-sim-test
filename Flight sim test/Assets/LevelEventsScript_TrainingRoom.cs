using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEventsScript_TrainingRoom : MonoBehaviour
{
    public MainUIManager mum;
    private int index = 0;

    private string[,] introText = {
        {"1","",""},
        {"5","CONTROL","Welcome to the training room!"},
        {"1","",""},
        {"4","CONTROL","Here, you can test your weapons and maneuvers"},
        {"4","CONTROL","on a variety of dummy target cubes!"},
        {"1","",""},
    };

    private string[,] controlsIntroduction = {
        {"1","",""},
        {"5","CONTROL","Let's go over the basic controls:"},
        {"1","",""},
        {"5","CONTROL","Pull UP or DOWN on your mouse to control pitch."},
        {"1","",""},
        {"5","CONTROL","Move the mouse LEFT or RIGHT to roll."},
        {"1","",""},
        {"5","CONTROL","Lastly, use WASD to accelerate and turn."},
        {"1","",""},
        {"1","CONTROL","Sounds good? Let's see you give it a try!"},
        {"1","",""},
    };
    private class EventClass
    {
        public float triggerTime {get;set;}
        public bool MyBooleanValue {get;set;}
    }
    // Start is called before the first frame update
    void Start()
    {
       StartCoroutine(LevelStartDialogue(introText));
    }

    // Update is called once per frame
    void Update()
    {
        //
    }

    IEnumerator LevelStartDialogue(string[,] text) {

        for (int i = 0; i < text.GetLength(0); i++) 
        { 
            if(text.GetLength(1) >= 4) {
                mum.setSpeakerAndMessage(text[i,1], text[i,2], text[i,3]);
            }
            else {
                mum.setSpeakerAndMessage(text[i,1], text[i,2]);
            }
            yield return new WaitForSeconds(int.Parse(text[i,0]));
        } 
        // yield return new WaitForSeconds(2);
        // mum.setSpeakerAndMessage("AWACS","Welcome to the training room!");
        // yield return new WaitForSeconds(4);
        // mum.setSpeakerAndMessage("AWACS","This message will autodelete in 3 seconds.");
        // yield return new WaitForSeconds(3);
        // mum.setSpeakerAndMessage();
        // yield return new WaitForSeconds(3);
    }
}
