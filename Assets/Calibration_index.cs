using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Calibration_index : MonoBehaviour
{
    // 0.8f is default threshold that gets overwritten only in case the calibration is done
    public static Calibration_index instance;
    public float threshold_index = 0.2f;
    //public FMOD.Studio.EventInstance instance_chord_Am_ARP1;
    // public HandTracking handInstance;
    private static float distanceInRelaxedState;
    private static float distanceInMaxEffortState;
    private bool flagRelaxed = false;
    private bool flagEffort = false;
    private bool rightDistance;
    private float thScalingFactor = 1.05f;
    
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        // if volume > .. IDEA
        // takeRelaxedPositionsDistances();
        takeMaxEffortPositionsDistances();
        takeRelaxedPositionsDistances();
        resetThresholdValue();
        rightDistance = handIsOnRightDistance(HandTracking.instance.handPoints);

        // In case both states are recorded, we calculate the new threshold
        if (rightDistance && flagRelaxed && flagEffort){
        
            threshold_index = Mathf.Abs(distanceInMaxEffortState)*thScalingFactor;
            UnityEngine.Debug.Log("Threshold calibrated!!! " + threshold_index);
            flagRelaxed = false;
            flagEffort = false;
        
        }

        // instance_chord_Am_ARP1.setVolume(0.3f);
    }

    void takeRelaxedPositionsDistances(){
        
        if (rightDistance && (Input.GetKeyDown(KeyCode.Space)) && (!flagRelaxed)) // this part needs adjustment 
        {
            UnityEngine.Debug.Log("You took the measurement in relaxed state!");
            // Measuring euclidean distance in relaxed state because it is used to determine threshold
            distanceInRelaxedState = Vector3.Distance(HandTracking.instance.handPoints[4].transform.localPosition, HandTracking.instance.handPoints[8].transform.localPosition);
            flagRelaxed = true;

        }

    }

    void takeMaxEffortPositionsDistances(){
        
        if (rightDistance && Input.GetKeyDown(KeyCode.Space) && (flagRelaxed)) // this part needs adjustment
        {
            UnityEngine.Debug.Log("You took the measurement in effort state!");
            // Measuring euclidean distance in effort state because it is used for threshold determination
            distanceInMaxEffortState = Vector3.Distance(HandTracking.instance.handPoints[4].transform.localPosition, HandTracking.instance.handPoints[8].transform.localPosition);
            flagEffort = true;

        } 

    }

    void resetThresholdValue()
    {
        // threshold_index is reseted to default value in case calibration doesnt work properly
        if (Input.GetKeyDown(KeyCode.Escape)){

            UnityEngine.Debug.Log("Values reseted!");
            threshold_index = 0.8f;
            flagRelaxed = false;
            flagEffort = false;
        }

    }

    bool handIsOnRightDistance(GameObject[] handPoints) 
    {
        int numberOfPoints = 21;
        float sumOfDepths = 0;
        float meanDepths = 0;
        float lowerBoundMean = 0.25f;
        float upperBoundMean = 1.0f;

        for(int i = 0; i < numberOfPoints; i++)
        {
            sumOfDepths += Mathf.Abs(handPoints[i].transform.localPosition[2]);
        }

        meanDepths = sumOfDepths / numberOfPoints;
        //UnityEngine.Debug.Log("Mean Depths" + meanDepths);

        if ((meanDepths > lowerBoundMean) && (meanDepths < upperBoundMean)) 
        {
            // Print message: TOO FAR!
            meanDepths = 0;
            //UnityEngine.Debug.Log("You are in range!!!!");
            return true;
        } // these values are faulty definitely because they should be in smaller range (experiment with this)
        else
        {
            // Print message: TOO CLOSE!
            //UnityEngine.Debug.Log("You are out of range!!!");
            meanDepths = 0;
            return false;
        }
    }
}
