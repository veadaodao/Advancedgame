using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    public GameObject Arrow;
    Vector3 startPosition;
    Quaternion startRotation;
    void Start()
    {
        startPosition = Arrow.transform.position;
        startRotation = Arrow.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "arrow")
        {
            Rigidbody rb = gameObject.GetComponent<Rigidbody>();
            rb.useGravity = false;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            Arrow.transform.position = startPosition;
            Arrow.transform.rotation = startRotation;
        }


    }
}
