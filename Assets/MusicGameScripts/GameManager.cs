using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // public AudioSource theMusic; // music in the scene

    public bool startPlaying;

    public BeatScroller theBS; // reference to the beat scroller script 

    public static GameManager instance; // way for other scripts to get the current static instance --> we use it in the note onject script

    // public PopUpMessages popUpMessages;
    public int currentScore;
    public int scorePerNote = 0;

    public Text scoreText; // reference to score text element on canvas, it should match the current score
    // public Text multiText; // this would be for the multiplier but I am not sure we want this part
    // public string messageText;

    public Text messageText;
     public float displayTime = 1.5f; // Time in seconds to display the message
    // public Text messageText;
    private float displayTimer;
    private bool showingMessage;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        scoreText.text = "Score: 0"; // make sure at the beginning the score displayed is equal to 0
        // popUpMessages =  FindObjectOfType<PopUpMessages>();
        // messageText = popUpMessages.messageText;
        messageText.text = " ";
        HideMessage();
    }

    // Update is called once per frame
    void Update()
    {
        if(!startPlaying)
        {
            if(Input.anyKeyDown)
            {
                startPlaying = true;
                theBS.hasStarted = true;
                HandTracking.instance.fmodEvents.instance_LetItBe_140.start();
        
            }
        }


         if (showingMessage)
        {
            displayTimer -= Time.deltaTime;
            if (displayTimer <= 0f)
            {
                HideMessage();
            }
        }
    }

    public void NoteHit()
    {
        // Debug.Log("Hit on Time");

        currentScore += scorePerNote;
        scoreText.text = "Score: " + currentScore;
        //DisplayMessage(true);
        messageText.text = "Hit!";
        ShowMessage(messageText.text);
    }

    public void NoteMissed()
    {
       // Debug.Log("Missed Note");
       // DisplayMessage(false);
       messageText.text = "Missed!";
       ShowMessage(messageText.text);
    }
   
      public void ShowMessage(string message)
    {
        messageText.text = message;
        showingMessage = true;
        displayTimer = displayTime;
    }

    private void HideMessage()
    {
        showingMessage = false;
        messageText.text = string.Empty;
    }

    
//    private void DisplayMessage(bool isHit)
//     {
//      if (isHit)
//     {
//         messageText = "Hit";
//         PopUpMessages.instance.ShowMessage(messageText);
//     }
//     else
//     {
//         messageText = "Missed";
//         PopUpMessages.instance.ShowMessage(messageText);
//     }
//     }
}