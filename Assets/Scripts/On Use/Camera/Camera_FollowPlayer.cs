using JetBrains.Annotations;

using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Camera_FollowPlayer : MonoBehaviour
{
    public Transform followedObject;
    public bool ignoreBorders = false;

    public float timeOffset;
    public Vector2 offset;

    public float rightLimit;
    public float leftLimit;
    public float topLimit;
    public float bottomLimit;


    private void FixedUpdate()
    {

        Vector3 startPosition = transform.position;
        Vector3 endPosition = followedObject.position;

        endPosition.x += offset.x;
        endPosition.y += offset.y;
        endPosition.z = transform.position.z;

        transform.position = Vector3.Lerp(startPosition, endPosition, timeOffset * Time.deltaTime);
        if (ignoreBorders)
            return;
        transform.position = new Vector3(

            Mathf.Clamp(transform.position.x, leftLimit, rightLimit),
            Mathf.Clamp(transform.position.y, bottomLimit, topLimit),
            transform.position.z

        );

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawLine(new Vector2(leftLimit, topLimit), new Vector2(rightLimit, topLimit));
        Gizmos.DrawLine(new Vector2(rightLimit, topLimit), new Vector2(rightLimit, bottomLimit));
        Gizmos.DrawLine(new Vector2(rightLimit, bottomLimit), new Vector2(leftLimit, bottomLimit));
        Gizmos.DrawLine(new Vector2(leftLimit, bottomLimit), new Vector2(leftLimit, topLimit));
    }
}
