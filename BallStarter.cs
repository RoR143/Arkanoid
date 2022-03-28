using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallStarter : MonoBehaviour
{
    // Referencia al componente BallMovement de la pelota.
    BallMovement bM;

    private void Start()
    {
        bM = GetComponent<BallMovement>();
    }
    private void Update()
    {
        Starter();
    }
    
    void Starter()
    {
        // Si el jugador pusla la tecla espacio y la pelota no se está moviendo actualmente,
        if (Input.GetKeyDown(KeyCode.Space) && !bM.isMoving)
        {
            // la pelota comenzará a moverse.
            bM.StartMovement();
        }
    }

    private void OnEnable()
    {
        GameManager.instance.activeBalls.Add(gameObject);
    }

    private void OnDisable()
    {
        GameManager.instance.activeBalls.Remove(gameObject);
    }
}
