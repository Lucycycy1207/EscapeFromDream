using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class TurretIdleState : TurretState
{
    public TurretIdleState(TurretController turret): base(turret)
    {
    }


    public override void OnStateEnter()
    {
        //Debug.Log("turret is Idle");
        if (turret.lineRenderer != null)
        {
            // Set LineRenderer color to red
            turret.lineRenderer.startColor = Color.red;
            turret.lineRenderer.endColor = Color.red;

            turret.lineRenderer.SetPosition(0, turret.laserStartPoint.position);
            turret.endPoint = turret.laserStartPoint.position + new Vector3(0, 0, -turret.laserLength);
            turret.lineRenderer.SetPosition(1, turret.endPoint);
        }
    }
    public override void OnStateUpdate()
    {
        if (Physics.Raycast(turret.laserStartPoint.position, new Vector3(0, 0, -1), out RaycastHit hitinfo, turret.laserLength))
        {
            turret.endPoint = hitinfo.point;
            turret.lineRenderer.SetPosition(1, turret.endPoint);
            

            if (hitinfo.transform.CompareTag("Player"))
            {
                turret.ChangeState(new TurretAttackState(turret));
            }
        }

        else
        {
            turret.endPoint = turret.laserStartPoint.position + new Vector3(0, 0, -turret.laserLength);
            turret.lineRenderer.SetPosition(1, turret.endPoint);
        }
    }
    public override void OnStateExit()
    {
        //Debug.Log("turret is no more Idle");
    }
}
