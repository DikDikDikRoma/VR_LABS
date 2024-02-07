using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveByTouch : MonoBehaviour
{
    public Transform touchFrom;
    public GameObject Enable;

    private void OnTriggerEnter(Collider other)
    {
        if (other == touchFrom.GetComponent<MeshCollider>())
        {
            Enable.SetActive(true);
            touchFrom.position = transform.position;
            touchFrom.rotation = transform.rotation;
        }
    }
}
