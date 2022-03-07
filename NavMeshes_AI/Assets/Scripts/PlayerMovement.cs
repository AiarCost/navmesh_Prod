using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent), typeof(AgentLinkMover))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private Camera camera;
    private NavMeshAgent Agent;

    [SerializeField]
    private Animator Animator;
    private AgentLinkMover LinkMover;

    private const string IsWalking = "IsWalking";
    private const string Jump = "Jump";
    private const string Landed = "Landed";

    private RaycastHit[] Hits = new RaycastHit[1];
    private void Awake()
    {
        Agent = GetComponent<NavMeshAgent>();
        LinkMover = GetComponent<AgentLinkMover>();

        LinkMover.OnLinkStart += HandleLinkStart;
        LinkMover.OnLinkEnd += HandleLinkEnd;
    }

    private void HandleLinkStart()
    {
        Animator.SetTrigger(Jump);
    }

    private void HandleLinkEnd()
    {
        Animator.SetTrigger(Landed);
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);

            if(Physics.RaycastNonAlloc(ray, Hits) > 0)
            {
                Agent.SetDestination(Hits[0].point);
            }
        }

        Animator.SetBool(IsWalking, Agent.velocity.magnitude > .01f);
    }
}
