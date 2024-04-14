using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteObject : MonoBehaviour
{
    public bool canBePressed;
    public HandMovements movementNumber;

    // Start is called before the first frame update
    void Start()
    {}

    // Update is called once per frame
    void Update()
    {
        if (HandTracking.instance.returnState(movementNumber))
        {
            if (canBePressed) // this means the object is in the correct area and the key is pressed, we destroy the object
        
            {
                gameObject.SetActive(false); 

                GameManager.instance.NoteHit(); // we successfully hit the note

            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Activator") // when we are in the area of the button, we can press the correspondent key
        {
            canBePressed = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Activator") // when the arrow is not longer over the button area, the key cannot longer be pressed
        {
            canBePressed = false;
            GameManager.instance.NoteMissed();
        }
    }
}
