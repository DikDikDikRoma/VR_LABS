using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trifgger_on_fire : MonoBehaviour
{
    ParticleSystem fire;
    // Start is called before the first frame update
    void Start()
    {
        fire = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Fire")
        {
            fire.Play();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Fire")
        {
            fire.Stop();
        }
    }
}
