using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD;
using FMODUnity;
using FMOD.Studio;
using UnityEngine.UI;

public class FMODEvents : MonoBehaviour
{
    public EventInstance instance_chord_C_ARP_140;
    public EventInstance instance_chord_Am_ARP_140;
    public EventInstance instance_chord_F_ARP_140;
    public EventInstance instance_chord_G_ARP_140;
    public EventInstance instance_LetItBe_140;

    float desiredTempo = 0.0f; 

    public void Start()
    {
        instance_chord_Am_ARP_140 = FMODUnity.RuntimeManager.CreateInstance("event:/Let It Be/Chord_Am_ARP_140");
        instance_chord_C_ARP_140 = FMODUnity.RuntimeManager.CreateInstance("event:/Let It Be/Chord_C_ARP_140");
        instance_chord_F_ARP_140 = FMODUnity.RuntimeManager.CreateInstance("event:/Let It Be/Chord_F_ARP_140");
        instance_chord_G_ARP_140 = FMODUnity.RuntimeManager.CreateInstance("event:/Let It Be/Chord_G_ARP_140");
        instance_LetItBe_140 = FMODUnity.RuntimeManager.CreateInstance("event:/Let It Be/LetItBe_140");
        
        // creating an array with all the events for the song let it be:
        EventInstance[] LetItBe_array = new EventInstance[]
        {   
            instance_chord_Am_ARP_140,
            instance_chord_C_ARP_140,
            instance_chord_G_ARP_140,
            instance_chord_F_ARP_140,
            instance_LetItBe_140
        };

        if (BeatScroller.instance.beatTempo == 70){
            desiredTempo = 1.0f; // Adjust this value as per your requirement
        }
        else if (BeatScroller.instance.beatTempo == 56){
            desiredTempo = 0.85f; // Adjust this value as per your requirement
        }
        else if (BeatScroller.instance.beatTempo == 52.5){
            desiredTempo = 0.75f; // Adjust this value as per your requirement
        }
        else if (BeatScroller.instance.beatTempo == 100){ // this value will be for the assessment 
            desiredTempo = 1.43f;
            instance_chord_Am_ARP_140.setVolume(0.0f);
            instance_chord_C_ARP_140.setVolume(0.0f);
            instance_chord_F_ARP_140.setVolume(0.0f);
            instance_chord_G_ARP_140.setVolume(0.0f);
        }

        UnityEngine.Debug.Log("Desired Tempo" + desiredTempo);

        foreach (var eventInstance in LetItBe_array)
        {   
            eventInstance.setPitch(desiredTempo);
        }   
    }
}

// here I am thinking about controlling the desidered tempo from the varaiable "BeatTempo" in beatscroller:
// that variable is controlled manually from the scene settings and we can create different levels 
// by having the same scene triplicated, and in each scene we manually set the beatTempo to 3 different 
// values. Here we create if conditions, namely if beatTempo = ... then desidered tempo = ...
// this way we controll both the velocity of the hands coming down and the tempo of the audios.