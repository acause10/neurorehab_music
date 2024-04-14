using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Calibration_middle : MonoBehaviour
{
    public static Calibration_middle instance;
    public float threshold_middle = 0.8f;
    //public HandTracking handInstance;
    private float distanceInRelaxedState;
    private float distanceInMaxEffortState;
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
        //handInstance.handPoints[4];
        //handInstance.handPoints[8];
        // if volume > .. IDEA
        takeMaxEffortPositionsDistances();
        takeRelaxedPositionsDistances();
        resetThresholdValue();
        rightDistance = handIsOnRightDistance(HandTracking.instance.handPoints);

        if (rightDistance && flagRelaxed && flagEffort){
            
            threshold_middle = Mathf.Abs(distanceInMaxEffortState)*thScalingFactor;
            UnityEngine.Debug.Log("Threshold calibrated!!! " + threshold_middle);
            flagRelaxed = false;
            flagEffort = false;
        
        }
    }
    void takeRelaxedPositionsDistances(){
        
        if (rightDistance && (Input.GetKeyDown(KeyCode.Space)) && (!flagRelaxed)) // this part needs adjustment 
        {
            UnityEngine.Debug.Log("You took the measurement in relaxed state!");
            // taking distance between thumb and the other 4 fingers in relaxed state
            distanceInRelaxedState = Vector3.Distance(HandTracking.instance.handPoints[4].transform.localPosition, HandTracking.instance.handPoints[12].transform.localPosition);
            flagRelaxed = true;

        }

    }

    void takeMaxEffortPositionsDistances(){
        
        if (rightDistance && Input.GetKeyDown(KeyCode.Space) && (flagRelaxed)) // this part needs adjustment
        {
            UnityEngine.Debug.Log("You took the measurement in effort state!");
            // taking distance between thumb and the other 4 fingers in max effort state
            distanceInMaxEffortState = Vector3.Distance(HandTracking.instance.handPoints[4].transform.localPosition, HandTracking.instance.handPoints[12].transform.localPosition);
            flagEffort = true;
        } 

    } 

    void resetThresholdValue()
    {
        // threshold_index is reseted to default value in case calibration doesnt work properly
        if (Input.GetKeyDown(KeyCode.Escape)){
            
            UnityEngine.Debug.Log("Values reseted!");
            threshold_middle = 0.8f;
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
