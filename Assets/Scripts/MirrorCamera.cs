using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorCamera : MonoBehaviour
{
    [SerializeField] Transform player;
    private float maxAngle = -90f;
    private float minAngle = -260f;
    private float originRotationOnY;
    private Vector3 newRotation;
    // Start is called before the first frame update
    void Start()
    {
        originRotationOnY = -180f;
        //Debug.Log($"origin: {originRotationOnY}");
    }

    // Update is called once per frame
    void Update()
    {
        newRotation = transform.rotation.eulerAngles;
        //Debug.Log($"player: {originRotationOnY} + {player.transform.rotation.eulerAngles.y}");
        newRotation.y = Mathf.Clamp(originRotationOnY + player.transform.rotation.eulerAngles.y, minAngle, maxAngle);

        this.transform.rotation = Quaternion.Euler(newRotation);
    }
    private void OnCollisionEnter(Collision collision)
    {
        IDestroyable destroyable = collision.gameObject.GetComponent<IDestroyable>();
        if (destroyable != null)
        {
            Debug.Log(destroyable.ToString() + " by " + this.gameObject.ToString());
            destroyable.OnCollided();
            Destroy(gameObject);
        }

    }

    
}
