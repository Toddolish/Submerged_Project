using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject Bullet;
    public Transform spawnPoint;
    public float rotateSpeed;
    
    void Update()
    {
        Shoot();

        Vector3 RayPos = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 dir = Input.mousePosition - RayPos;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotateSpeed * Time.deltaTime);
    }
    void Shoot()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            Instantiate(Bullet, spawnPoint.position, spawnPoint.rotation);
        }
    }
}
