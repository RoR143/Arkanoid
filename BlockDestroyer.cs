using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockDestroyer : MonoBehaviour
{
    // Puntuación que proporciona este bloque.
    public int score;
    // Vidas de este bloque.
    public int life;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Comprobamos si el objeto que ha chocado es la pelota. Si es así,
        if (collision.collider.CompareTag("Ball"))
        {
            // Comprobamos si el bloque no tiene vidas.
            if (life == 0)
            {
                // añadimos los puntos al jugador y restamos bloques pendientes.
                GameManager.instance.AddScore(score);
                GameManager.instance.pendingBlocks--;
                // Llamamos al método NextLevel para ver si hay que crear un nivel nuevo.
                GameManager.instance.NextLevel();

                // Lanzamos el método que crea un power up pasando la posición de este bloque como parámetro.
                PowerUpManager.instance.SpawnPowerUp(transform.position);

                // destruimos el objeto que tiene este script.
                Destroy(gameObject);
            }
            else
            {
                life--;
            }
        }
    }
}
