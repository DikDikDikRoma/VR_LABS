using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arhimed : MonoBehaviour
{
    Collider col;
    void Update()
    {
        if (col != null)
            GetComponent<Rigidbody>().AddForce(Vector3.up * 13f);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Water")
        {
            col = other;
            GetComponent<Rigidbody>().drag = 3;
            GetComponent<Rigidbody>().angularDrag = 3;
        }
    }
    private void OnTriggerExit(Collider other)
    {

        if (other.tag == "Water")
        {
            col = null;
            GetComponent<Rigidbody>().drag = 0.5f;
            GetComponent<Rigidbody>().angularDrag = 0.05f;
        }
    }
}
