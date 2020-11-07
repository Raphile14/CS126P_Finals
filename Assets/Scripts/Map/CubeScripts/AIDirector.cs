using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class AIDirector : MonoBehaviour
{
    public int xPos;
    public int zPos;

    public float StressLevelStart = 200f;
    public float StressLevel;
    public float StressDecay = 1f;
    public float StressThreshold = 80f;
    public float StressMaximum = 400f;
    public float StressDistance = 50f;

    public int xMaxBoundary = 123;
    public int zMaxBoundary = 123;

    public CubeAgent CubeAgent;
    public GameObject CubeObject;
    public GameObject PlayerObject;
    public PlayerMovement PlayerMovement;

    private Vector3 spawn;

    private void Start()
    {
        GenerateLocation();
        StressLevel = StressLevelStart;
        spawn = CubeObject.transform.position;
        // Debug.Log("Position: " + spawn);
    }

    private void Update()
    {
        if (RoomTemplates.isMapFinished)
        {
            //// Reduce Stress if Cube is far from player
            float distance = Vector3.Distance(CubeObject.gameObject.transform.position, PlayerObject.transform.position);
            //Bring cube back to spawn if hit player
            if (distance < 4)
            {
                Debug.Log("hit player");
                Debug.Log("Respawned to: " + CubeObject.transform.position);
                CubeObject.transform.position = new Vector3(spawn.x, spawn.y, spawn.z);
                PlayerMovement.ScreamPlayer();
                GenerateFarPlayerLocation();
                CubeAgent.GoToRandomPosition();
            }

            if (distance > StressDistance)
            {
                StressLevel -= StressDecay * Time.deltaTime;
            }

            // Increase Stress if Cube is near the player
            else if (distance < StressDistance)
            {
                StressLevel += StressDecay * Time.deltaTime;
            }

            // If Stress level reaches 0
            // Point cube to player
            if (StressLevel < 0)
            {
                StressLevel = StressLevelStart;
                GenerateNearPlayerLocation();
                CubeAgent.GoToRandomPosition();
                // Debug.Log("Pointing to player");
            }

            // If Stress level reaches maximum
            // Point cube away from player
            else if (StressLevel > StressMaximum)
            {
                StressLevel = StressLevelStart;
                GenerateFarPlayerLocation();
                CubeAgent.GoToRandomPosition(); 
                // Debug.Log("Pointing away from player");
            }            
        }        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.name == "CubeModel")
        {
            GenerateLocation();
            CubeAgent.GoToRandomPosition();
        }
    }

    private void GenerateLocation()
    {
        xPos = Random.Range(-xMaxBoundary, xMaxBoundary);
        zPos = Random.Range(-zMaxBoundary, zMaxBoundary);
        this.gameObject.transform.position = new Vector3(xPos, 1.5f, zPos);        
    }

    private void GenerateNearPlayerLocation()
    {
        this.gameObject.transform.position = new Vector3(PlayerObject.transform.position.x, 1.5f, PlayerObject.transform.position.z);
    }

    private void GenerateFarPlayerLocation()
    {
        xPos = Random.Range(-xMaxBoundary, xMaxBoundary);
        zPos = Random.Range(-zMaxBoundary, zMaxBoundary);
        
        if ((Mathf.Abs(xPos - PlayerObject.transform.position.x) <= StressDistance) || (Mathf.Abs(zPos - PlayerObject.transform.position.z) <= StressDistance))
        {
            this.gameObject.transform.position = new Vector3(xPos, 1.5f, zPos);
        }
        else
        {
            GenerateFarPlayerLocation();
        }
    }
}
