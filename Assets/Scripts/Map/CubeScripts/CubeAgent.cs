using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CubeAgent : MonoBehaviour
{
    // References
    public GameObject CubeDestination;
    public SphereCollider sphereCollider;
    NavMeshAgent TheAgent;
    public GameObject PlayerObject;
    public PlayerMovement PlayerMovement;
    public AudioSource ChaseSound;

    // Values
    public float FieldOfViewAngle = 320f;
    public Vector3 LastSighting;
    public float RoamSpeed = 7.0f;
    public float ChaseSpeed = 12.0f;
    public bool SpotedPlayer = false;
    public bool HeardPlayer = false;
    public float CheckTimer = 20f;

    void Start()
    {
        TheAgent = GetComponent<NavMeshAgent>();
        GoToRandomPosition();
        Invoke("CheckStatus", CheckTimer);
    }

    void CheckStatus ()
    {
        if (!SpotedPlayer && !HeardPlayer)
        {
            GoToRandomPosition();
        }
        else
        {
            if (LastSighting != null)
            {
                TheAgent.SetDestination(LastSighting);
            }    
            else
            {
                GoToRandomPosition();
            }
        }
        Invoke("CheckStatus", CheckTimer);
    }

    // Order Cube to wander
    public void GoToRandomPosition()
    {
        TheAgent.speed = RoamSpeed;
        TheAgent.SetDestination(CubeDestination.transform.position);
    }

    private void OnTriggerStay(Collider other)
    {
        //TheAgent.SetDestination(PlayerObject.transform.position);        
        if (other.gameObject == PlayerObject)
        {

            Vector3 direction = other.transform.position - transform.position;
            float angle = Vector3.Angle(direction, transform.forward);

            // Check if Cube can see player
            if (angle < FieldOfViewAngle * 0.5f)
            {
                RaycastHit hit;

                if (Physics.Raycast(transform.position + transform.up, direction.normalized, out hit, sphereCollider.radius))
                {
                    if (hit.collider.gameObject == PlayerObject)
                    {
                        LastSighting = PlayerObject.transform.position;
                        TheAgent.SetDestination(LastSighting);
                        TheAgent.speed = ChaseSpeed;
                        Debug.Log("Player In Sight");
                        SpotedPlayer = true;
                        if (!ChaseSound.isPlaying)
                        {
                            ChaseSound.Play();
                        }
                    }
                }
            }

            // Check if Cube can hear the player
            if (PlayerMovement.isSprinting || PlayerMovement.isTired)
            {
                if ((CalculatePathLength(PlayerObject.transform.position) <= sphereCollider.radius))
                {
                    LastSighting = PlayerObject.transform.position;
                    TheAgent.SetDestination(LastSighting);
                    Debug.Log("Player Heard");
                    HeardPlayer = true;
                }
            }

        }
    }

    // If player goes out of range, cube goes back to wander
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == PlayerObject)
        {
            SpotedPlayer = false;
            HeardPlayer = false;
            Debug.Log("Player Outside Radius");
            GoToRandomPosition();
        }
    }

    // Caculat sound distance
    float CalculatePathLength(Vector3 targetPosition)
    {
        NavMeshPath path = new NavMeshPath();

        if (TheAgent.enabled)
        {
            TheAgent.CalculatePath(targetPosition, path);
        }

        Vector3[] allWayPoints = new Vector3[path.corners.Length+2];

        allWayPoints[0] = transform.position;
        allWayPoints[allWayPoints.Length - 1] = targetPosition;

        for (int i = 0; i < path.corners.Length; i++)
        {
            allWayPoints[i + 1] = path.corners[i];
        }

        float pathLength = 0f;
        for (int i = 0; i < allWayPoints.Length - 1; i++)
        {
            pathLength += Vector3.Distance(allWayPoints[i], allWayPoints[i + 1]);
        }
        return pathLength;
    }
}
