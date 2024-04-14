// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using System;

// public class Calibration : MonoBehaviour
// {
//     public UDPReceive udpReceive;
//     public float[] distancesInRelaxedState = new float[4];
//     public float[] distancesInMaxEffortState = new float[4];
//     public float[] differenceBetweenDistances = new float[4];
//     public bool flagButton1 = false;
//     public bool flagButton2 = false;
//     public float[] threshold_easy_arr = new float[4];
//     public float[] threshold_medium_arr = new float[4];
//     public float[] threshold_hard_arr = new float[4];
//     public float hard_ratio = 0.25f;
//     public float medium_ratio = 0.5f;
//     public float easy_ratio = 0.75f;


//     void Start(){

//     }

//     void Update(){

//         // constantly updating the hand points
//         // string data = udpReceive.data;
//         // data = data.Remove(0, 1); // removing first element '['
//         // data = data.Remove(data.Length - 1, 1); // removing last element ']'
//         // string[] points = data.Split(','); // we are splitting all the data to remove commas

//         // // assume for now that I will be taking the raw coordinates
//         // for (int i = 0; i < 21; i++ )
//         // {
//         //     float x = 6 - float.Parse(points[i*3]) / 100; // accessing every x value because the data is arranged as x1,y1,z1,x2,y2,z2,...
//         //     float y = float.Parse(points[i*3 + 1]) / 100; // dividing by 100 because the values of positions in unity are low
//         //     float z = float.Parse(points[i*3 + 2]) / 100;

//         //     handPoints[i].transform.localPosition = new Vector3(x, y, z); // Property "Transform" on our points -> by accessing Transform we can change the position of points
//         // }

//         takeRelaxedPositionsDistances();
//         takeMaxEffortPositionsDistances();
//         calculateDifferencesOfDistances();

//     }

//     // void detectHandMovement(){
        
//     //     float[] eu = new float[4];
//     //     eu[0] = Vector3.Distance(handPoints[4].transform.localPosition, handPoints[8].transform.localPosition);
//     //     eu[1] = Vector3.Distance(handPoints[4].transform.localPosition, handPoints[12].transform.localPosition);
//     //     eu[2] = Vector3.Distance(handPoints[4].transform.localPosition, handPoints[16].transform.localPosition);
//     //     eu[3] = Vector3.Distance(handPoints[4].transform.localPosition, handPoints[20].transform.localPosition);

//     //     float thres = 0.8f;

//     // }

//     void takeRelaxedPositionsDistances(){
        
//         int number_of_distances = 4;
//         if (Input.GetKeyDown(KeyCode.Space)) // this part needs adjustment 
//         {
//             // taking distance between thumb and the other 4 fingers in relaxed state
//             for(int i = 0; i < number_of_distances; i++)
//             {
//                 distancesInRelaxedState[i] = Vector3.Distance(handPoints[4].transform.localPosition, handPoints[4*(i+2)].transform.localPosition);
//             }
//             flagButton1 = true;

//         }

//     }

//     void takeMaxEffortPositionsDistances(){
        
//         int number_of_distances = 4;
//         if (Input.GetKeyDown(KeyCode.Escape)) // this part needs adjustment
//         {
//             // taking distance between thumb and the other 4 fingers in max effort state
//             for(int i = 0; i < number_of_distances; i++)
//             {
//                 distancesInMaxEffortState[i] = Vector3.Distance(handPoints[4].transform.localPosition, handPoints[4*(i+1)].transform.localPosition);
//             }
//             flagButton2 = true;
//         } 

//     } 

//     void calculateDifferencesOfDistances(){

//         if (flagButton1 && flagButton2){
            
//             for(int i = 0; i < 4; i++)
//             {
//                 differenceBetweenDistances[i] = Mathf.Abs(distancesInRelaxedState[i] - distancesInMaxEffortState[i]);
//             }
//             flagButton1 = false;
//             flagButton2 = false;
//         }

//     }

//     void determineThreshold(){
        
//         for(int i = 0; i < 4; i++)
//         {
//             threshold_easy[i] = differenceBetweenDistances[i] - easy_ratio*differenceBetweenDistances[i];
//             threshold_medium[i] = differenceBetweenDistances[i] - medium_ratio*differenceBetweenDistances[i];
//             threshold_hard[i] = differenceBetweenDistances[i] - hard_ratio*differenceBetweenDistances[i];
//         }
        
//     }

// }