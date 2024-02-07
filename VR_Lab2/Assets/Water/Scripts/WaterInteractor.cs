using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterInteractor : MonoBehaviour
{

    public float threshold = 0.1f;
    public ParticleSystem ripplesParticle;
    public GameObject splashPrefab;
    public GameObject jumpPrefab;

    bool underwater = false;
    bool splash = false;

    public float acc = 50.0f;
    public float maxSpeed = 5.0f;
    public float jumpSpeed = 10.0f;
    public float boyancy = 10.0f;
    public float drag = 5.0f;

    private Vector3 velocity = Vector3.zero;

    private void Awake()
    {
        ripplesParticle.Stop();
    }

    void Update()
    {
        velocity.y -= 9.8f * Time.deltaTime;
        Vector3 moveDir = Vector3.zero;

        moveDir.Normalize();

        Vector3 flatVelocity = velocity;
        flatVelocity.y = 0f;

        float accCoef = 1.0f - Mathf.Clamp01(flatVelocity.magnitude / maxSpeed) * Vector3.Dot(flatVelocity, moveDir);
        velocity += moveDir * acc * accCoef * Time.deltaTime;
        Debug.Log("Splash is " + splash);
        if (underwater)
        {
            velocity -= velocity * drag * Time.deltaTime;
            velocity.y += Mathf.Clamp01(-transform.position.y) * boyancy * Time.deltaTime;
            if (transform.position.y < -threshold && !splash)
            {
                ripplesParticle.Play();
                Vector3 surfacePos = transform.position;
                surfacePos.y = 0f;
                //Instantiate(splashPrefab, surfacePos, Quaternion.identity);
                splash = true;
            }
            if (splash)
            {
                ripplesParticle.Stop();
            }
            transform.position += velocity * Time.deltaTime;
        }
        else
        {
            if (transform.position.y < -threshold)
            {
                //ripplesParticle.Play();
                Vector3 surfacePos = transform.position;
                surfacePos.y = 0f;
                //Instantiate(splashPrefab, surfacePos, Quaternion.identity);
                splash = false;
            }
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Water")
        {
            boyancy = 5+ 10 * int.Parse(other.name);
            threshold = other.transform.position.y + transform.localScale.y;
            underwater = true;
            GetComponent<Rigidbody>().isKinematic = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Water")
        {
            ripplesParticle.Stop();
            underwater = false;
            GetComponent<Rigidbody>().isKinematic = false;
        }
    }
}
