using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketShootStrategy : IShootStrategy
{
    ShootInteractor interactor;
    Transform shootPoint;

    //Constructor 
    public RocketShootStrategy(ShootInteractor interactor)
    {
        Debug.Log("Switched to Rocket Mode");
        this.interactor = interactor;
        shootPoint = interactor.GetShootPoint();

        //Change Gun Color
        interactor.gunRenderer.material.color = interactor.rocketGunColor;
    }
    public void Shoot()
    {
        PooledObject pooledObject = interactor.rocketPool.GetPooledObject();
        pooledObject.gameObject.SetActive(true);

        //Rigidbody bullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
        Rigidbody bullet = pooledObject.GetComponent<Rigidbody>();
        bullet.transform.position = shootPoint.position;
        bullet.transform.rotation = shootPoint.rotation;

        bullet.velocity = shootPoint.forward * interactor.GetShootVelocity();

        interactor.rocketPool.DestroyObjectFromPool(pooledObject, 3.0f);
        //Destroy(bullet.gameObject, 5.0f);
    }

}