using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicObjectsController : MonoBehaviour
{
    Rigidbody[] myRigidbodies;
    Vector3[] positions;
    Quaternion[] rotations;
    // Start is called before the first frame update
    void Awake()
    {
        myRigidbodies = GetComponentsInChildren<Rigidbody>();
        positions = new Vector3[myRigidbodies.Length];
        rotations = new Quaternion[myRigidbodies.Length];
        for (int i = 0; i < myRigidbodies.Length; i++)
        {
            positions[i] = myRigidbodies[i].transform.localPosition;
            rotations[i] = myRigidbodies[i].transform.localRotation;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            for (int i = 0; i < myRigidbodies.Length; i++)
            {
                myRigidbodies[i].velocity = Vector3.zero;
                myRigidbodies[i].angularVelocity = Vector3.zero;
                myRigidbodies[i].transform.localPosition = positions[i];
                myRigidbodies[i].transform.localRotation = rotations[i];
            }
        }
    }
}