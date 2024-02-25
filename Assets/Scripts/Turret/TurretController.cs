using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    [HideInInspector]
    public LineRenderer lineRenderer;
    [SerializeField] public float laserLength;
    [SerializeField] public Transform laserStartPoint;

    public Transform player;

    private TurretState currentState;

    [HideInInspector]
    public Vector3 endPoint;


    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }
    private void Start()
    {
        currentState = new TurretIdleState(this);
        currentState.OnStateEnter();
    }

    public void ChangeState(TurretState state)
    {
        currentState.OnStateExit();
        currentState = state;
        currentState.OnStateEnter();
    }

    // Update is called once per frame
    private void Update()
    {
        currentState.OnStateUpdate();

    }
}
