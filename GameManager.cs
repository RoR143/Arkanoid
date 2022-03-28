using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Puntuación actual del jugador.
    public int currentScore = 0;
    // Gestión de niveles.
    // Nivel en el que se encuentra el jugador actualmente.
    public int currentLevel;
    // Array que contiene todos los prefabs de los niveles que se van a jugar.
    public GameObject[] levels;
    // Bloques que quedan por destruir en el nivel actual.
    public int pendingBlocks;

    // Gestión de las bolas del juego. ...
    // Lista que contiene las bolas que hay activas en el juego.
    public List<GameObject> activeBalls = new List<GameObject>();
    //
    public GameObject ballPrefab;
    // Referencia al jugador.
    public Transform player;
    // Interfaz
    public Text scoreDisp;

    [Header("Sistema de guardado de records")]
    // Referencia al campo de texto donde mostramos la puntuación máxima.
    public Text highscoreText;
    // Variable en la que almacenamos el valor de la máxima puntuación actual.
    public int currentHighscore = 0;
    // Si true, se ha conseguido un nuevo récord y se guardará la partida al terminar.
    public bool newRecord;
    // Nombre de la key que se creará en el registro cuando guardemos partida.
    string highscoreKey = "highscore";
    // SINGLETON
    public static GameManager instance;
    // Vidas que tiene el jugador.
    public int lifeCounter = 3;
    // Display de vidas
    public Text lifeDisp;
    [Header("Sistema de pausado")]
    // Referencia al menú de pausa de la jerarquía.
    public GameObject pauseMenu;
    // Si true, el juego está pausado.
    public bool isPaused = false;
    //
    public Button continueButton;
    //
    public Button clearDataButton;

    private void Awake()
    {
        if (instance == null)
        {
            // instance = GetComponent<GameManager>();
            instance = this; // Lo de arriba simplificado.
        }
    }

    public void AddScore(int score)
    {
        // Añadimos la puntuación obtenida por parámetro a la actual.
        currentScore += score;
        // ACtualizamos texto en interfaz.
        scoreDisp.text = "Score: " + currentScore.ToString();

        // Comprobamos si la puntuación que tiene el jugador supera a la máxima actual.
        if (currentScore > currentHighscore)
        {
            // Indicamos que en esa partida se ha superado el récord.
            newRecord = true;
            // Actualizamos el valor de la puntuación máxima en la interfaz.
            highscoreText.text = currentScore.ToString();
        }
    }

    public void NextLevel()
    {
        // Comprobamos si se han destruido todos los bloques.
        if (pendingBlocks == 0)
        {
            // Comprobamos si aún quedan niveles por crear.
            if (currentLevel < levels.Length)
            {
                // Instanciamos el nivel que corresponde.
                GameObject tempLevel = Instantiate(levels[currentLevel++]);
                // Actualizamos la cantidad de bloques restantes.
                pendingBlocks = tempLevel.transform.Find("Blocks").childCount;
            }
            else
            {
                Debug.Log("Se han acabado todos los niveles.");
                SceneManager.LoadScene("Vicdef");
            }
        }
    }

    private void Start()
    {
        Load();
        Restart();
        NextLevel();
        // Bloquea vidas entre 0 y 5
        lifeCounter = Mathf.Clamp(lifeCounter, 0, 5);
    }

    void Restart()
    {
        // Comprobamos si el jugador se ha quedado sin vidas.
        if (lifeCounter <= 0)
        {
            Save();
            // Enseña pantalla final.
            SceneManager.LoadScene("Vicdef");

        }
        else
        {
            // Destruimos todas las bolas que haya activas en este momento.
            foreach (GameObject b in activeBalls)
            {
                Destroy(b);
            }

            // Limpiamos la lista.
            activeBalls.Clear();

            // Creación de la bola que se usa para comenzar a jugar.
            GameObject newBall = Instantiate(ballPrefab, player.position, Quaternion.identity);
            // Asignamos desde código la referencia al stoppedTarget de la pelota.
            newBall.GetComponent<BallMovement>().stoppedTarget = player;

        }
    }
    void Save()
    {
        // Comprobamos si se ha superado el récord en esta partida.
        if (newRecord)
        {
            Debug.Log("El jugador ha superado el récord. Se guardará la información");
            // Actualizamos el valor de la puntuación máxima y guardamos la info usando PlayerPrefs.
            currentHighscore = currentScore;
            PlayerPrefs.SetInt(highscoreKey, currentHighscore);
        }
    }
    void Load()
    {
        // Cargamos la información de PlayerPrefs.
        currentHighscore = PlayerPrefs.GetInt(highscoreKey);
        // Actualizamos la interfaz.
        highscoreText.text = currentHighscore.ToString();
    }
    void LifeLost()
    {
        // Quitamos una vida.
        lifeCounter--;
        // Reiniciamos.
        Restart();
    }

    public void BallLost()
    {
        // Comprobamos si la cantidad de pelotas activas es 0 o inferior a 0. Si es así el jugador ha perdido todas las pelotas.
        if (activeBalls.Count <= 0)
        {
            LifeLost();
        }
    }
    public void TogglePause()
    {
            // Alternamos la boolena que indica que el juego se encuentra en pausa.
            isPaused = !isPaused;
            Time.timeScale = isPaused ? 0f : 1f; // Ternaria para cambiar la velocidad del juego
            pauseMenu.SetActive(isPaused); // Activa o desactiva el menú de pausa según la booleana
    }
    public void ClearData()
    {
        // Borramos toda la info guardada con PlayerPrefs.
        PlayerPrefs.DeleteAll();
        // Actualizamos la máxima puntuación en la interfaz.
        highscoreText.text = "0";
        // Actualizamos nuestra variable donde almacenamos la puntuación máxima.
        currentHighscore = 0;
    }
    private void Update()
    {
        // Enseña vidas en HUD
        lifeDisp.text = "Lives:" + lifeCounter.ToString();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }
}
