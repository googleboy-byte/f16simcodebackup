using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missioncam : MonoBehaviour
{
    [SerializeField]
    GameObject player;
    public float missioncamh;

    void Update()
    {
        float newXPosition = player.transform.position.x;
        float newYPosition = player.transform.position.y;
        float newZPosition = player.transform.position.z;
    
        transform.position = new Vector3(newXPosition, missioncamh + newYPosition, newZPosition);
        
    }
}
