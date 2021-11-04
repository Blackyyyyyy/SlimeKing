using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuseDisplayPosition : MonoBehaviour
{
    void Update()
    {
        transform.position = transform.parent.position + Vector3.up * 0.3f;
        transform.rotation = Quaternion.identity;
    }
}
