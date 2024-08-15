using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class MovimientoEnemigo : MonoBehaviour
{
    public NavMeshAgent enemigoAgent;
    public GameObject jugador;
    public Rigidbody enemigoRb;

    private bool _onNavMeshLink = false;
    private float _jumpDuration = 2f;
    public float distanciaPerseguir;
    public bool estaSiguiendo;


    public UnityEvent OnLand, OnStartJump; // Tutorial MeshLinks - PARA SONIDOS Y ANIMACIONES


    void Start()
    {
        enemigoAgent.autoTraverseOffMeshLink = false;
        enemigoAgent = GetComponent<NavMeshAgent>();
        enemigoRb = GetComponent<Rigidbody>();
        jugador = GameObject.Find("Jugador");
    }

    void Update()
    {
        float distance = Vector3.Distance(jugador.transform.position, this.transform.position);

        if (distance <= enemigoAgent.stoppingDistance + 0.5f)
        {
            enemigoRb.isKinematic = true;
        }
        else
        { 
            enemigoRb.isKinematic= false;
        }

        if (jugador != null && enemigoAgent.enabled && distance >= enemigoAgent.stoppingDistance && distance <= distanciaPerseguir ) // Revisar
        {
            SetDestination(jugador.transform.position);
            estaSiguiendo = true;
        }
        else if (enemigoAgent.enabled)
        {
            estaSiguiendo = false;
            enemigoAgent.destination = enemigoAgent.transform.position;
            //enemigoRb.isKinematic = false;
        }

        if (enemigoAgent.isOnOffMeshLink && _onNavMeshLink == false && jugador.gameObject.GetComponent<MovimientoPlayer_Controller>().estaEnSuelo)
        {
            StartNavMeshLinkMovement();            
        }

        if (_onNavMeshLink)
        {
            FaceTarget(enemigoAgent.currentOffMeshLinkData.endPos);
            enemigoRb.isKinematic = true;
        }
    }

    private void StartNavMeshLinkMovement()
    {
        _onNavMeshLink = true;        
        NavMeshLink link = (NavMeshLink)enemigoAgent.navMeshOwner;
        Spline spline = link.GetComponentInChildren<Spline>();

        PerformJump(link, spline);

    }

    private void PerformJump(NavMeshLink link, Spline spline)
    {
        bool reverseDirection = CheckIfJumpingFromEndToStart(link);
        StartCoroutine(MoveOnOffMeshLink(spline, reverseDirection));

        OnStartJump?.Invoke();
    }

    private bool CheckIfJumpingFromEndToStart(NavMeshLink link)
    {
        Vector3 startPosWorld = link.gameObject.transform.TransformPoint(link.startPoint);
        Vector3 endPosWorld = link.gameObject.transform.TransformPoint(link.endPoint);

        float distancePlayerToStart = Vector3.Distance(enemigoAgent.transform.position, startPosWorld);
        float distancePlayerToEnd = Vector3.Distance(enemigoAgent.transform.position, endPosWorld);

        return distancePlayerToStart > distancePlayerToEnd;
    }
    private IEnumerator MoveOnOffMeshLink(Spline spline, bool reverseDirection)
    {
        float currentTime = 0;
        Vector3 agentStartPosition = enemigoAgent.transform.position;

        while (currentTime < _jumpDuration)
        {
            currentTime += Time.deltaTime;

            float amount = Mathf.Clamp01(currentTime / _jumpDuration);
            amount = reverseDirection ? 1 - amount : amount;

            enemigoAgent.transform.position =
                reverseDirection ?
                spline.CalculatePositionCustomEnd(amount, agentStartPosition)
                : spline.CalculatePositionCustomStart(amount, agentStartPosition);

            yield return new WaitForEndOfFrame();
        }

        enemigoAgent.CompleteOffMeshLink();

        OnLand?.Invoke();
        yield return new WaitForSeconds(0.1f);
        _onNavMeshLink = false;

    }


    void FaceTarget(Vector3 target)
    {
        Vector3 direction = (target - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5);
    }



    public void SetDestination(Vector3 destination)
    {
        if (_onNavMeshLink)
        {
            return;
        }

        enemigoAgent.destination = destination;
    }

}
