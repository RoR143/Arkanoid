using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    [Header("Control de creación de power ups")]
    // Array que contiene todos los power ups que pueden aparecer en el juego.
    public GameObject[] powerUpPrefabs;
    // Probabilidad de que aparezca un power up cuando destruimos un bloque.
    public int probability;

    [Header("Configuración de Protecting Wall")]
    // Referencia al objeto del ProtectingBall en la jerarquía.
    public GameObject protectingWall;

    // Referencia estática.
    public static PowerUpManager instance;
    private void Awake()
    {
        // Recuperación de la referencia estática.
        if (instance == null)
        {
            instance = this; 
        }
    }

    [Header("Configuración del láser")]
    // Referencia al componente del Player que le permite disparar.
    public LaserShoot lS;

    public void SpawnPowerUp(Vector2 position)
    {
        // Calculamos un número aleatorio entre 1 y 100.
        int chance = Random.Range(1, 101);
        // Comprobamos si el número obtenido es menor que la probabilidad establecida para la creación de
        // power ups. Si es así, lo creamos.
        if (chance <= probability)
        {
            // Instanciamos un powerup aleatorio de nuestro array en la pos del bloque destruido.
            Instantiate(powerUpPrefabs[Random.Range(0, powerUpPrefabs.Length)], position, Quaternion.identity);
        }
    }

    public void SplitBall()
    {
        Debug.Log("Se ha recogido el power up: SplitBall");
        // Recuperamos una referencia de la primera pelota que haya activa en la lista del GameManager.
        Transform b = GameManager.instance.activeBalls[0].transform;

        // Creamos dos bolas adicionales a cada lado de la bola principal.
        GameObject ball1 = Instantiate(b.gameObject, b.position + b.right / 2f, Quaternion.identity);
        GameObject ball2 = Instantiate(b.gameObject, b.position - b.right / 2f, Quaternion.identity);

        // Iniciamos el movimiento de las pelotas que acabamos de crear.
        ball1.GetComponent<BallMovement>().StartMovement();
        ball2.GetComponent<BallMovement>().StartMovement();
    }
    public IEnumerator ProtectingWall(float duration)
    {
        // Activamos el objeto ProtectingWall.
        protectingWall.SetActive(true);
        // Aplicamos la espera del PowerUp.
        yield return new WaitForSeconds(duration);
        // Desactivamos el objeto ProtectingWall.
        protectingWall.SetActive(false);
    }
    public void HealthRecovery()
    {
        GameManager.instance.lifeCounter++;
    }
    
    public IEnumerator Laser(float duration)
    {
        // Permitimos que el jugador dispare.
        lS.canShoot = true;
        // Aplicamos la espera.
        yield return new WaitForSeconds(duration);
        // Impedimos que el jugador dispare.
        lS.canShoot = false;
    }
}
