using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterMovement : MonoBehaviour
{
    [SerializeField]
    private float currentairspeed;
    [SerializeField]
    private float targetairspeed;
    [SerializeField]
    private float maxairspeedbygear = 100f;
    [SerializeField]
    private float currentheightfromterrain = 0f;
    private RaycastHit hit;
    private float currentthrottlevalue;
    [SerializeField]
    private Transform camera, fighter, propeller, gearleft, gearright, flapright, flapleft, aileronleft, aileronright, gearfront, tirefront;
    [SerializeField]
    private Transform lockgatefront1, lockgatefront2, lockgateleft1, lockgateleft2, lockgateleft3, lockgateright1, lockgateright2, lockgateright3;
    private Vector2 default_look_Limits = new Vector2(-70f, 80f);
    private float yawrate;
    private Rigidbody rb;
    private bool geardown;
    private float geardistancefromground = 5.5f;
    private ConstantForce cf;
    private float lift = 0f;
    [SerializeField]
    private float yawright;
    [SerializeField]
    private float yawleft;

    void Start(){
        targetairspeed = 0f;
        currentairspeed = 0f;
        rb = this.GetComponent<Rigidbody>();
        currentheightfromterrain = 0f;
        geardown = true;
        cf = this.GetComponent<ConstantForce>();
    }

    // Update is called once per frame
    void Update()
    {
        // simulating throttle

        if (Input.GetKey(KeyCode.Alpha5)){
            currentthrottlevalue = 5f;
            if (targetairspeed != (0f/100f) * maxairspeedbygear){
                targetairspeed = (0f/100f) * maxairspeedbygear;
            }
        }
        if (Input.GetKey(KeyCode.Alpha6)){
            currentthrottlevalue = 6f;
            if (targetairspeed != (30f/100f) * maxairspeedbygear){
                targetairspeed = (30f/100f) * maxairspeedbygear;
            }
        }
        if (Input.GetKey(KeyCode.Alpha7)){
            currentthrottlevalue = 7f;
            if (targetairspeed != (50f/100f) * maxairspeedbygear){
                targetairspeed = (50f/100f) * maxairspeedbygear;
            }
        }
        if (Input.GetKey(KeyCode.Alpha8)){
            currentthrottlevalue = 8f;
            if (targetairspeed != (70f/100f) * maxairspeedbygear){
                targetairspeed = (70f/100f) * maxairspeedbygear;
            }
        }
        if (Input.GetKey(KeyCode.Alpha9)){
            currentthrottlevalue = 9f;
            if (targetairspeed != (90f/100f) * maxairspeedbygear){
                targetairspeed = (90f/100f) * maxairspeedbygear;
            }
        }
        if (Input.GetKey(KeyCode.Alpha0)){
            currentthrottlevalue = 10f;
            if (targetairspeed != (100f/100f) * maxairspeedbygear){
                targetairspeed = (100f/100f) * maxairspeedbygear;
            }
        }

        if (currentairspeed > targetairspeed){
            currentairspeed -= 5*Time.deltaTime;
        }
        if (currentairspeed < targetairspeed){
            currentairspeed += 15*Time.deltaTime;
        }

        //simulating throttle


        //forward force
        transform.position += transform.forward * Time.deltaTime * currentairspeed;

        //simulate gravity
        // if (transform.forward.y > 0 && currentairspeed > (30/100)*maxairspeedbygear){
        //     currentairspeed -= transform.forward.y * Time.deltaTime * 0.5f;
        // }
        // if (transform.forward.y < 0 && currentairspeed > (30/100)*maxairspeedbygear){
        //     currentairspeed -= transform.forward.y * Time.deltaTime * 3f;
        // }
        //simulate acceleration due to gravity

        // set mininum take off speed
        if (currentairspeed > (50/100)*maxairspeedbygear){
            if(currentheightfromterrain <= 0.5f && Input.GetAxis("Vertical") <= 0f){
                transform.Rotate(Input.GetAxis("Vertical"), 0f, 0f);
            }
            else if(currentheightfromterrain > 0.5f){
                transform.Rotate(Input.GetAxis("Vertical"), 0f, -Input.GetAxis("Horizontal"));
            }

        } else {
            transform.Rotate(0f, 0f, -Input.GetAxis("Horizontal"));
        }
        //setting current distance from hard deck

        Terrain GetClosestCurrentTerrain(Vector3 playerPos)
        {
            //Get all terrain
            Terrain[] terrains = Terrain.activeTerrains;

            //Make sure that terrains length is ok
            if (terrains.Length == 0)
                return null;

            //If just one, return that one terrain
            if (terrains.Length == 1)
                return terrains[0];

            //Get the closest one to the player
            float lowDist = (terrains[0].GetPosition() - playerPos).sqrMagnitude;
            var terrainIndex = 0;

            for (int i = 1; i < terrains.Length; i++)
            {
                Terrain terrain123 = terrains[i];
                Vector3 terrainPos = terrain123.GetPosition();

                //Find the distance and check if it is lower than the last one then store it
                var dist = (terrainPos - playerPos).sqrMagnitude;
                if (dist < lowDist)
                {
                    lowDist = dist;
                    terrainIndex = i;
                }
            }
            return terrains[terrainIndex];
        }
        //Get the current terrain
        Terrain terrain = GetClosestCurrentTerrain(transform.position);
        Vector3 point = new Vector3(0, 0, 0);
        float yHeight = terrain.SampleHeight(point);
        currentheightfromterrain = tirefront.position.y - yHeight - geardistancefromground;

        //terrain collision detection
        if (currentheightfromterrain <= 0f ){
              transform.position = new Vector3(
                transform.position.x,
                yHeight + geardistancefromground,
                transform.position.z
             );

             //transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, 0f);
        }
        //terrain collision detection

        if (currentheightfromterrain <= 1f){
            yawrate = 0.5f * currentairspeed;
        }
        else {
            yawrate = 0.05f * currentairspeed;
        }
        if (Input.GetKey(KeyCode.Delete)){
            transform.Rotate(0f, -yawrate * Time.deltaTime, 0f);
        }
        if (Input.GetKey(KeyCode.PageDown)){
            transform.Rotate(0f, yawrate * Time.deltaTime, 0f);
        }

        //setting minimum speed
        if(currentairspeed < 10f && currentheightfromterrain > 100f){
            currentairspeed = 10f;
        }
        //setting minimum speed

        //programming air break
        if (Input.GetKey(KeyCode.B)){
            currentairspeed -= Time.deltaTime * 30f;
        }

        //gear up/down

        // if (Input.GetKey(KeyCode.G)){
        //     StartCoroutine("CheckGear");
        // }


    }

    // IEnumerator CheckGear(){

    //     if (geardown == true){
    //         geardown = false;
    //         gearfront.rotation = Quaternion.Euler(194.171f, -94.039f, -177.916f);
    //         lockgatefront1.rotation = Quaternion.Euler(180.606f, 5.145996f, -21.48297f);
    //         lockgatefront2.rotation = Quaternion.Euler(194.27f, -90.48599f, -79.92999f);
    //         gearleft.rotation = Quaternion.Euler(-0.541f, 114.714f, 2.681f);
    //         lockgateleft1.rotation = Quaternion.Euler(-3.574f, -184.36f, 183.792f);
    //         lockgateleft2.rotation = Quaternion.Euler(1.584f, -5.861f, 172.03f);
    //         lockgateleft3.rotation = Quaternion.Euler(36.94f, 15.702f, 6.552001f);
    //         gearright.rotation = Quaternion.Euler(0f, 0f, 110.183f);
    //         lockgateright1.rotation = Quaternion.Euler(163.589f, -124.594f, -104.887f);
    //         lockgateright2.rotation = Quaternion.Euler(24.221f, 279.546f, -70.32101f);
    //         lockgateright3.rotation = Quaternion.Euler(0f, 98.32101f, 90.00001f);
    //     }
    //     if(geardown == false){
    //         geardown = true;
    //         gearfront.rotation = Quaternion.Euler(181.173f, -0.1879883f, -194.273f);
    //         lockgatefront1.rotation = Quaternion.Euler(158.683f, 89.031f, -2.796997f);
    //         lockgatefront2.rotation = Quaternion.Euler(194.27f, -90.48599f, -249.844f);
    //         gearleft.rotation = Quaternion.Euler(2.654f, -0.655f, -0.659f);
    //         lockgateleft1.rotation = Quaternion.Euler(5.145f, -326.645f, 179.183f);
    //         lockgateleft2.rotation = Quaternion.Euler(78.08501f, -165.24f, 42.404f);
    //         lockgateleft3.rotation = Quaternion.Euler(-73.58601f, 69.804f, -71.18501f);
    //         gearright.rotation = Quaternion.Euler(0f, 0f, 0f);
    //         lockgateright1.rotation = Quaternion.Euler(154.011f, 5.470993f, -61.51599f);
    //         lockgateright2.rotation = Quaternion.Euler(-32.383f, 103.009f, 68.675f);
    //         lockgateright3.rotation = Quaternion.Euler(0f, 2.064f, 90.00001f);
    //     }

    //     yield return new WaitForSeconds(3f); 

    // }



}
