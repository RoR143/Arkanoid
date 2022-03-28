using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    // Enumerador donde se establecen los tipos de power ups que habrá en el juego.
    public enum PowerUpTypes
    {
        SplitBall,
        ProtectingWall,
        HealthRecovery,
        Laser
    }
    // Tipo de power up.
    public PowerUpTypes type;
    // Duración del power up.
    public float duration;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Comprobamos si el objeto con el que ha chocado el PowerUp es el player.
        if(collision.CompareTag("Player"))
        {
            switch (type)
            {
                case PowerUpTypes.SplitBall:
                    PowerUpManager.instance.SplitBall();
                    break;
                case PowerUpTypes.ProtectingWall:
                    PowerUpManager.instance.StartCoroutine(PowerUpManager.instance.ProtectingWall(duration));
                    break;
                case PowerUpTypes.HealthRecovery:
                    PowerUpManager.instance.HealthRecovery();
                    break;
                case PowerUpTypes.Laser:
                    PowerUpManager.instance.StartCoroutine(PowerUpManager.instance.Laser(duration));
                    break;
            }
            Destroy(gameObject);
        }
    }
}
