using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    Transform target;
    NavMeshAgent agent;
    CharacterCombat enemyCombat;
    public float lookRadius = 10f;

    public GameObject[] waypoints;
    public int waypointNumber = 0;
    
    public bool randomizeOrder = false;
    public bool followWaypoints = true;


    // Start is called before the first frame update
    void Start()
    {
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        enemyCombat = GetComponent<CharacterCombat>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);
        float distanceToWaypoint = Vector3.Distance(gameObject.transform.position, waypoints[waypointNumber].transform.position);


        if (distance <= lookRadius)
        {
            agent.SetDestination(target.position);

            if (distance <= agent.stoppingDistance)
            {
                CharacterStats targetStats = target.GetComponent<CharacterStats>();
                if(targetStats != null)
                {
                    enemyCombat.Attack(targetStats);
                }
                
                FaceTarget(target.position);
                //attack
            }
        }
        else if (followWaypoints)
        {
            if (distanceToWaypoint > agent.stoppingDistance)
            {
                FaceTarget(waypoints[waypointNumber].transform.position);
                agent.SetDestination(waypoints[waypointNumber].transform.position);
            }
            else
            {
                if (!randomizeOrder)
                {
                    if (waypointNumber + 1 == waypoints.Length)
                    {
                        waypointNumber = 0;
                    }
                    else
                    {
                        waypointNumber++;
                    }
                }
                else
                {
                    waypointNumber = Random.Range(0, waypoints.Length);
                }
            }
        }
    }

    void FaceTarget(Vector3 targetOfRotation)
    {
        Vector3 direction = (targetOfRotation - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
