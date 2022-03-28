using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Velocidad a la que moveremos al jugador.
    public float speed;

    // Referencia.
    public Rigidbody2D rB2D;
    private void Start()
    {
        // Recuperación de referencias.
        rB2D = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        Movement();
    }
    void Movement()
    {
        // Recuperación de los valores de los axis.
        float h = Input.GetAxisRaw("Horizontal");
        //Generación del vector de movimiento.
        Vector2 movement = new Vector2(h, 0f);
        // Aplicación del movimiento.
        // rB2D.MovePosition( (Vector2) transform.position + movement * speed * Time.deltaTime);
        rB2D.MovePosition( rB2D.position + movement * speed * Time.deltaTime);

    }
}
