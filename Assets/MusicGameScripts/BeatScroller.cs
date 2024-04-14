using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatScroller : MonoBehaviour
{
    public static BeatScroller instance;
    public float beatTempo; // how fast arrows go down
    public float velocity;
    public bool hasStarted; // pressing a button to make things scrolling down the screen

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        velocity = beatTempo / 60f; // (beat tempo is in bpm --> dividing by 60, this is how fast per second
        // 120 bpm means 2 beats per second, and in Unity means that the arrow will go over 2 squares on the grid per second
    }

    // Update is called once per frame
    void Update()
    {
        if(!hasStarted)
        {
            /* if(Input.anyKeyDown) // commented out because we're doing that now in the Game Manager script
            {
                hasStarted = true;
            } */ 
        } else // if "hasStarted" is true, then start moving things down based on beatTempo 

        {
            transform.position -= new Vector3(0f, velocity * Time.deltaTime, 0f); // arrow moving on y axis
        }
    }
}
