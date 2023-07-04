using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] GameObject followed;
    [SerializeField] float distance;

    void LateUpdate()
    {
        transform.position = followed.transform.position + new Vector3(0,0,distance);    
    }
}
