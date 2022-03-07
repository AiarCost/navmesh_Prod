using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent), typeof(AgentLinkMover))]
public class EnemyMovement : MonoBehaviour
{
    public GameObject target;
    public float UpdateSpeed = .1f;

    private NavMeshAgent Agent;

    [SerializeField]
    private Animator Animator;

    private AgentLinkMover LinkMover;
    private const string IsWalking = "IsWalking";
    private const string Jump = "Jump";
    private const string Landed = "Landed";

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
    private void Start()
    {
        StartCoroutine(FollowTarget());
    }

    private void Update()
    {
        Animator.SetBool(IsWalking, Agent.velocity.magnitude > 0.01f);
    }
    private IEnumerator FollowTarget()
    {
        WaitForSeconds Wait = new WaitForSeconds(UpdateSpeed);

        while (this.enabled)
        {
            Agent.SetDestination(target.transform.position);
            yield return Wait;
        }
    }
}
