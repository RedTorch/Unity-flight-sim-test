using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEventsScript_TrainingRoom : MonoBehaviour
{
    public MainUIManager mum;
    private int index = 0;
    private class EventClass
    {
        public float triggerTime {get;set;}
        public bool MyBooleanValue {get;set;}
    }
    // Start is called before the first frame update
    void Start()
    {
       StartCoroutine(LevelStartDialogue());
    }

    // Update is called once per frame
    void Update()
    {
        //
    }

    IEnumerator LevelStartDialogue() {
        yield return new WaitForSeconds(2);
        mum.setSpeakerAndMessage("AAWACS","Welcome to the training room!");
        yield return new WaitForSeconds(4);
        mum.setSpeakerAndMessage("AWACS","This message will autodelete in 3 seconds.");
        yield return new WaitForSeconds(3);
        mum.setSpeakerAndMessage();
        yield return new WaitForSeconds(3);
    }
}
