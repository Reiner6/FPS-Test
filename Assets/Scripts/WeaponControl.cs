using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponControl : MonoBehaviour
{
    #region Variables
    public Transform gunRoot;
    public Transform bulletSpawn;
    public ItemDetector itemDetector;

    public GameObject myBulletType;

    public List<BulletComponent> bulletPool = new List<BulletComponent>();
    WeaponComponent currentWeapon;
    bool shootCommand;
    bool grabCommand;

    #endregion

    #region MonoBehaviour
    void Update()
    {
        Grab();
        Shoot();
    }
    #endregion

    #region Methods
    /// <summary>
    /// grab a new weapon, drop the previous one
    /// </summary>
    public void Grab()
    {
        if (!grabCommand)
            return;

        if (itemDetector.gunList.Count == 0)
        {
            grabCommand = false;
            return;
        }
        if (currentWeapon != null)
        {
            currentWeapon.transform.parent = null;
            currentWeapon.myRigidbody.isKinematic = false;
            currentWeapon.myRigidbody.useGravity = true;
            currentWeapon.grabbed = false;
            currentWeapon.Reposition();
        }
        currentWeapon = itemDetector.gunList[0];
        bulletSpawn = currentWeapon.spawnBullet;
        currentWeapon.transform.parent = gunRoot;
        currentWeapon.transform.localPosition = Vector3.zero;
        currentWeapon.transform.localEulerAngles = Vector3.zero;
        currentWeapon.myRigidbody.isKinematic = true;
        currentWeapon.myRigidbody.useGravity = false;
        currentWeapon.grabbed = true;
        itemDetector.gunList.Remove(itemDetector.gunList[0]);

        grabCommand = false;

    }
    /// <summary>
    /// shoots a bullet everytime its called
    /// </summary>
    public void Shoot()
    {
        if (!shootCommand)
            return;
        if (!currentWeapon)
        {
            shootCommand = false;
            return;
        }
        for (int i = 0; i < bulletPool.Count; i++)
        {
            if (!bulletPool[i].gameObject.activeInHierarchy)
            {
                bulletPool[i].Fire(bulletSpawn.position, bulletSpawn.transform.forward, currentWeapon.speed.bulletData);
                shootCommand = false;
                return;
            }
        }
        BulletComponent temp = Instantiate(myBulletType).GetComponent<BulletComponent>();
        bulletPool.Add(temp);
        temp.Fire(bulletSpawn.position, bulletSpawn.transform.forward, currentWeapon.speed.bulletData);
        shootCommand = false;
    }
    #endregion

    #region GetSet
    public bool ShootCommand { set { shootCommand = value; } }
    public bool GrabCommand { set { grabCommand = value; } }
    #endregion
}