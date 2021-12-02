using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagneticBulletComponent : MonoBehaviour
{
    #region Variables
    public LayerMask myMagnetLayer;

    public List<Rigidbody> myObjects;

    public float magneticPull = 50;

    Vector3 forward;
    Vector3 dir;
    Vector3 cross;
    Vector3 side;
    #endregion

    #region MonoBehviour
    void Start()
    {
        forward = transform.forward;
        side = Vector3.Cross(dir, forward);
    }

    private void Update()
    {
        for (int i = 0; i < myObjects.Count; i++)
        {
            dir = this.transform.position - myObjects[i].transform.position;

            myObjects[i].transform.LookAt(dir.normalized);
            myObjects[i].AddForce(dir.normalized * magneticPull);
            cross = Vector3.Cross(dir, side);
            myObjects[i].AddForce(cross.normalized * magneticPull);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.attachedRigidbody)
            if ((myMagnetLayer.value & 1 << other.attachedRigidbody.gameObject.layer) > 0)
            {
                if (!myObjects.Contains(other.attachedRigidbody))
                {
                    other.attachedRigidbody.useGravity = false;
                    myObjects.Add(other.attachedRigidbody);
                }
            }
    }
    #endregion

    #region Methods
    public void ClearList()
    {
        for (int i = 0; i < myObjects.Count; i++)
        {
            myObjects[i].useGravity = true;
        }
        myObjects.Clear();
    }
    #endregion
}