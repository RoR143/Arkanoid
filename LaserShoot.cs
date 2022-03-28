using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserShoot : MonoBehaviour
{
    // Si true, el personaje podrá disparar su láser.
    public bool canShoot;
    // Referencia al prefab del láser.
    public GameObject laserPrefab;
    // Punto en el que se creará el láser.
    public Transform shootingPoint;
    // Velocidad de desplazamiento del láser.
    public float laserSpeed;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Shoot();
    }

    void Shoot()
    {
        if (canShoot && Input.GetKeyDown(KeyCode.Space))
        {
            GameObject newLaser = Instantiate(laserPrefab, shootingPoint.position, Quaternion.identity);

            newLaser.GetComponent<Rigidbody2D>().velocity = Vector2.up * laserSpeed;
        }
    }
}
