using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletComponent : MonoBehaviour
{
    #region Variables
    public Rigidbody myRigidbody;

    public MagneticBulletComponent magneticComponent;

    Bullet myData;

    int bounce;
    #endregion

    #region MonoBehaviour
    private void Awake()
    {
        myRigidbody = GetComponent<Rigidbody>();
    }
    #endregion

    #region Methods
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.attachedRigidbody)
            return;
        if (myData.bulletType == BulletType.bounce && bounce <= myData.numberOfBounces)
        {
            StartCoroutine(Cooldown());
            bounce++;
            myRigidbody.velocity = (myRigidbody.velocity.normalized + collision.GetContact(0).normal) * myData.speed;

            return;
        }
        else if (myData.bulletType == BulletType.magnetic)
        {
            magneticComponent.ClearList();
        }
        this.gameObject.SetActive(false);
    }

    public void Fire(Vector3 position, Vector3 direction, Bullet bulletData)
    {

        myData = bulletData;
        SetType();
        this.gameObject.SetActive(true);
        this.transform.position = position;
        myRigidbody.velocity = (direction.normalized * myData.speed);
    }

    void SetType()
    {

        switch (myData.bulletType)
        {
            case BulletType.parabolic:
                myRigidbody.useGravity = true;
                magneticComponent.gameObject.SetActive(false);
                break;
            case BulletType.magnetic:
                myRigidbody.useGravity = false;
                magneticComponent.gameObject.SetActive(true);
                break;
            case BulletType.bounce:
                myRigidbody.useGravity = false;
                bounce = 0;
                magneticComponent.gameObject.SetActive(false);
                break;
            default:
                break;
        }
    }


    IEnumerator Cooldown()
    {
        this.GetComponent<Collider>().enabled = false;
        yield return new WaitForSeconds(0.1f);

        yield return new WaitForEndOfFrame();

        this.GetComponent<Collider>().enabled = true;
    }
    #endregion
}