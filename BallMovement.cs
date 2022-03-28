using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    // Si true, la pelota se encuentra en movimiento.
    public bool isMoving;

    // Velocidad de movimiento de la pelota.
    public float speed;

    // Objetivo de seguimiento de la pelota para cuando esta no se esté moviendo.
    public Transform stoppedTarget;

    // Variación que aplicaremos en el posicionamiento de la pelota en su seguimiento.
    public Vector2 offsets;


    // Referencias.
    Rigidbody2D rB2D;
    private void Awake()
    {
        // Recuperación de la referencia al componente Rigidbody2D.
        rB2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Follow();
        ConstantSpeed();
    }
    void Follow()
    {
        // La pelota seguirá a la barra si no se está moviendo.
        if (!isMoving)
        {
            // Comprobamos si el vector de movimiento de la pelota es distinto a un vector con valores 0,0.
            if (rB2D.velocity != Vector2.zero)
            {
                // Si es así, detenemos la pelota.
                rB2D.velocity = Vector2.zero;
            }
            // La posición de la pelota será la del target + los offsets.
            transform.position = (Vector2)stoppedTarget.position + offsets;
        }

    }
    void ConstantSpeed()
    {
        // Comprobamos si la pelota se está moviendo.
        if (isMoving)
        {
            rB2D.velocity = speed * rB2D.velocity.normalized * Time.deltaTime;
        }
    }


    public void StartMovement()
    {
        // Indicamos que la pelota se está moviendo.
        isMoving = true;
        // Aplicamos un empuje sobre la pelota.
        rB2D.AddForce(Vector2.up * speed);
    }

    void PlayerHit()
    {
        // Obtenemos la posición del jugador.
        Vector2 playerPos = stoppedTarget.position;
        // Obtenemos la posición de la pelota.
        Vector2 ballPos = transform.position;

        // Direction = targetPosition - originPosition.
        // Calculamos la dirección desde el player hasta la pelota.
        Vector2 direction = ballPos - playerPos;
        // Normalizamos el vector.
        direction.Normalize();
        // Reajustamos la velocidad de la pelota con la dirección obtenida.
        rB2D.velocity = direction;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Comprobamos si el objeto con el que ha chocado la bola es el Player.
        if (collision.collider.CompareTag("Player"))
        {
            PlayerHit();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Si la pelota choca con el muro inferior,
        if (collision.CompareTag("BottomWall"))
        {
            // Destruimos la pelota.
            Destroy(gameObject);
            // Indicamos al GameManager que se ha perdido una pelota.
            GameManager.instance.BallLost();
            
        }
    }
}
