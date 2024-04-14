using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MidiJack;

// LUT for chords
// chords = {
//         'A': [57, 61, 64],      # A major chord
//         'Am': [57, 60, 64],     # A minor chord
//         'Am7': [57, 60, 64, 67], # A minor 7th chord
//         'B': [59, 63, 66],      # B major chord
//         'Bm': [59, 62, 66],     # B minor chord
//         'C': [60, 64, 67],      # C major chord

//         'Cm': [60, 63, 67],     # C minor chord
//         'C/B': [59, 60, 64],    # C/B chord
//         'D': [62, 66, 69],      # D major chord
//         'Dm': [62, 65, 69],     # D minor chord
//         'E': [52, 56, 59],      # E major chord
//         'Em': [64, 67, 71],     # E minor chord
//         'F': [57, 60, 65],      # F major chord
//         'Fm': [65, 68, 72],     # F minor chord
//         'G': [67, 71, 74],      # G major chord
//         'Gm': [67, 70, 74]      # G minor chord
//     }

public class playChords : MonoBehaviour
{
    // Define the MIDI channel to listen to
    public int channel = 1;

    // Define some chords
    public int[] chord_Am = {57, 60, 64};    // A minor chord
    public int[] chord_C = {60, 64, 67};     // C major chord
    public int[] chord_F = {57, 60, 65};     // F major chord
    public int[] chord_E = {52, 56, 59};     // E major chord

    private bool isChordPlaying = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            playChord(chord_Am);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            playChord(chord_C);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            playChord(chord_F);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            playChord(chord_E);
        }
        // now note off once button is not pressed
        else if (Input.GetKeyUp(KeyCode.Alpha1) || Input.GetKeyUp(KeyCode.Alpha2) || Input.GetKeyUp(KeyCode.Alpha3) || Input.GetKeyUp(KeyCode.Alpha4))
        {
            stopChord();
        }
   
    }

    private void playChord(int[] notes)
    {
        if(!isChordPlaying)
        {
            foreach (int note in notes)
            {
              MidiMaster.noteOnDelegate += (MidiChannel channel, int noteNumber, float velocity) =>
                {
                    if (channel == (MidiChannel)(this.channel - 1) && IsNoteInChord(noteNumber, notes))
                    {
                        // Play the note
                        Debug.Log("Playing note: " + noteNumber);
                    }
                };
            }
            isChordPlaying = true; 
        }   
    }

    private void stopChord()
    {
        MidiMaster.noteOnDelegate -= NoteOnHandler;
        isChordPlaying = false;
    }

    private bool IsNoteInChord(int note, int[] chord)
    {
        return System.Array.IndexOf(chord, note) != -1;
    }

    private void NoteOnHandler(MidiChannel channel, int noteNumber, float velocity)
    {
        if (channel == (MidiChannel)(this.channel - 1))
        {
            // Stop playing the note
            Debug.Log("Stopping note: " + noteNumber);
        }
    }
}
