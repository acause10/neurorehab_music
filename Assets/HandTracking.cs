using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using FMODUnity;

public enum HandMovements
{
    rest = 0,
    thumb2Tip_1 = 1,
    thumb2Tip_2 = 2,
    thumb2Tip_3 = 3,
    thumb2Tip_4 = 4
}

public class HandTracking : MonoBehaviour
{
    public UDPReceive udpReceive;
    public static HandTracking instance; 
    public FMODEvents fmodEvents;
    public GameObject[] handPoints;
    public int position = 0;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        fmodEvents = FindObjectOfType<FMODEvents>();
    }

    // Update is called once per frame
    void Update()
    {
        string data = udpReceive.data;
        data = data.Remove(0, 1); // removing first element '['
        data = data.Remove(data.Length - 1, 1); // removing last element ']'
        string[] points = data.Split(','); // we are splitting all the data to remove commas

        for (int i = 0; i < 21; i++ )
        {
            float x = 6 - float.Parse(points[i*3]) / 100; // accessing every x value because the data is arranged as x1,y1,z1,x2,y2,z2,...
            float y = float.Parse(points[i*3 + 1]) / 100; // dividing by 100 because the values of positions in unity are low
            float z = float.Parse(points[i*3 + 2]) / 100;
 
            // float x = float.Parse(points[i*3]) / 100; // accessing every x value because the data is arranged as x1,y1,z1,x2,y2,z2,...
            // float y = float.Parse(points[i*3 + 1]) / 100; // dividing by 100 because the values of positions in unity are low
            // float z = float.Parse(points[i*3 + 2]) / 100;

            handPoints[i].transform.localPosition = new Vector3(x, y, z); // Property "Transform" on our points -> by accessing Transform we can change the position of points
        }
        detectHandMovement(handPoints);
    }

    void detectHandMovement(GameObject[] handPoints)
    {
        float[] eu = new float[4];
        eu[0] = Vector3.Distance(handPoints[4].transform.localPosition, handPoints[8].transform.localPosition);
        eu[1] = Vector3.Distance(handPoints[4].transform.localPosition, handPoints[12].transform.localPosition);
        eu[2] = Vector3.Distance(handPoints[4].transform.localPosition, handPoints[16].transform.localPosition);
        eu[3] = Vector3.Distance(handPoints[4].transform.localPosition, handPoints[20].transform.localPosition);

        //float thres = 0.8f;

        if (eu[0] < Calibration_index.instance.threshold_index)
        {
            if(position != 1)
            {
                stopChords();
                fmodEvents.instance_chord_C_ARP_140.start();
                // fmodEvents.instance_chord_C_ARP_140.setVolume(0.3f);
            }  
            position = 1; // index 
        }
        else if (eu[1] < Calibration_middle.instance.threshold_middle)
        {
            if(position != 2)
            {
                stopChords();
                fmodEvents.instance_chord_G_ARP_140.start();
            }
            position = 2; // middle
        }
        else if (eu[2] < Calibration_ring.instance.threshold_ring)
        {
            if(position != 3)
            {
                stopChords();
                fmodEvents.instance_chord_Am_ARP_140.start();
            }
            position = 3; // ring
        }
        else if (eu[3] < Calibration_pinky.instance.threshold_pinky)
        {
            if(position != 4)
            {
                stopChords();
                fmodEvents.instance_chord_F_ARP_140.start();
            }
            position = 4; // pinky
        }
        else
        {
            if(position != 0)
            {
                stopChords();
            }
            position = 0;
        }
     }

     void stopChords()
     {
        // fmodEvents.instance_chord_Am_ARP_67.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        // fmodEvents.instance_chord_Am_ARP_67.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        // fmodEvents.instance_chord_C_ARP_67.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        // fmodEvents.instance_chord_F_ARP_67.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        // fmodEvents.instance_chord_G_ARP_67.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        // fmodEvents.instance_chord_Am_ARP_135.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        // fmodEvents.instance_chord_C_ARP_135.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        // fmodEvents.instance_chord_F_ARP_135.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        // fmodEvents.instance_chord_G_ARP_135.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        // fmodEvents.instance_chord_Am_ARP_270.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        // fmodEvents.instance_chord_C_ARP_270.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        // fmodEvents.instance_chord_F_ARP_270.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        // fmodEvents.instance_chord_G_ARP_270.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        fmodEvents.instance_chord_G_ARP_140.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        fmodEvents.instance_chord_C_ARP_140.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        fmodEvents.instance_chord_F_ARP_140.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        fmodEvents.instance_chord_Am_ARP_140.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
     }

    public bool returnState(HandMovements movementName)
     {
        int enumValue = (int)movementName;
        if(position == enumValue)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    }
