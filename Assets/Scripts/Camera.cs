using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] private Transform trackingTarget = default(Transform);

    [SerializeField] private float xOffset = default(float);
    [SerializeField] private float yOffset = 3.79f;
    [SerializeField] protected float followSpeed = default(float);
    [SerializeField] protected bool isXLocked = false;
    [SerializeField] protected bool isYLocked = false;


    private void Awake()
    {
        if(trackingTarget == null)
        {
            throw new System.Exception("Camera does not have tracking target");
        }
    }

    private void Update()
    {
        float xNew = transform.position.x;
       
        float xTarget = trackingTarget.position.x + xOffset;
        float yTarget = trackingTarget.position.y + yOffset;

        if (!isXLocked)
        {
            xNew = Mathf.Lerp(transform.position.x, xTarget, Time.deltaTime * followSpeed);
        }

        float yNew = transform.position.y;

        if (!isYLocked)
        {
            yNew = Mathf.Lerp(transform.position.y, yTarget, Time.deltaTime * followSpeed);
        }


        transform.position = new Vector3(xNew, yNew, transform.position.z);
    }
}
