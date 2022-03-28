using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuCommands : MonoBehaviour
{
    public Text winLoss;
    public Text scoreDisp;
    // Start is called before the first frame update
    void Start()
    {
        // Se comprueba si el jugador ha ganado o no con las vidas
        if (GameManager.instance.lifeCounter <= 0)
        {
            winLoss.text = "You lost!";
        }
        else
        {
            winLoss.text = "You won!";
        }
        // Se actualiza la puntuación
        scoreDisp.text = "Score: " + GameManager.instance.currentScore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
