using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FieldOfView : MonoBehaviour
{
    [SerializeField] public float radius;
    [SerializeField] public float angle;

    [SerializeField] public GameObject playerRef;

    [SerializeField] public LayerMask targetMask;
    [SerializeField] public LayerMask obstructionMask;

    [SerializeField] public NavMeshAgent agent;

    private bool canSeePlayer;
    private float distance;

    

    private void Start()
    {
        playerRef = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        FieldOfViewCheck();
        MoveToPlayer();
    }

    
    private void FieldOfViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);
        float angleLower = transform.forward.y - (angle / 2);
        float angleHigher = transform.forward.y + (angle / 2);

        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < angleHigher
                    && Vector3.Angle(transform.forward, directionToTarget) > angleLower)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                    canSeePlayer = true;
                else
                    canSeePlayer = false;
            }
            else
                canSeePlayer = false;

        }
        else if (canSeePlayer)
            canSeePlayer = false;
    }

    private void MoveToPlayer()
    {
        distance = Vector3.Distance(playerRef.transform.position, this.transform.position);

        if (canSeePlayer && distance > 4)
        {
            agent.isStopped = false;
            agent.SetDestination(playerRef.transform.position);
        }
        else
            agent.isStopped = true;
    }

    public bool GetSeePlayer()
    {
        return canSeePlayer;
    }

    public void SetSeePlayer(bool changeSight)
    {
        canSeePlayer = changeSight;
    }

}
