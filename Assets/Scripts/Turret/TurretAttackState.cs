using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAttackState : TurretState
{
    Health playerHealth;
    float damagePerSecond = 20f;


    public TurretAttackState(TurretController turret): base(turret)
    {
        playerHealth = turret.player.GetComponent<Health>();
    }


    public override void OnStateEnter()
    {
        //Debug.Log("turret entered Attack");
    }

    public override void OnStateUpdate()
    {
        if (Physics.Raycast(turret.laserStartPoint.position, new Vector3(0, 0, -1), out RaycastHit hitinfo, turret.laserLength))
        {
            
            turret.endPoint = hitinfo.point;
            turret.lineRenderer.SetPosition(1, turret.endPoint);
            

            if (hitinfo.transform.CompareTag("Player"))
            {
                //Debug.Log("damage player");
                Attack();

            }
            else
            {
                turret.ChangeState(new TurretIdleState(turret));
            }
        }

        else
        {
            turret.endPoint = turret.laserStartPoint.position + new Vector3(0, 0, -turret.laserLength);
            turret.lineRenderer.SetPosition(1, turret.endPoint);

            turret.ChangeState(new TurretIdleState(turret));
        }
    }

    private void Attack()
    {
        if (playerHealth != null)
        {
            playerHealth.DeductHealth(damagePerSecond * Time.deltaTime);
        }
    }

    public override void OnStateExit()
    {
        //Debug.Log("turret is no more attacking");
    }
}
