using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Progreso del nivel")]
    public int totalGemas;
    public int gemasRecolectadas;

    [Header("Referencias")]
    public ControladorCanvas canvasUI;

    [Header("Configuración")]
    public string siguienteEscena = "";
    public float delayReinicio = 3f;

    private bool nivelTerminado = false;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        totalGemas = GameObject.FindGameObjectsWithTag("Gema").Length;
        if (canvasUI == null)
            canvasUI = FindAnyObjectByType<ControladorCanvas>();
        if (canvasUI != null)
            canvasUI.ActualizarGemas(gemasRecolectadas, totalGemas);
    }
    void Update()
    {

    }
    public void SumarGema(int cantidad)
    {
        gemasRecolectadas += cantidad;
        if (canvasUI != null)
            canvasUI.ActualizarGemas(gemasRecolectadas, totalGemas);
    }

    public void TerminarNivel()
    {
        if (nivelTerminado) return;
        nivelTerminado = true;
        if (canvasUI != null)
            canvasUI.MostrarFinNivel();
        Invoke(nameof(CargarSiguiente), delayReinicio);
    }

    void CargarSiguiente()
    {
        if (!string.IsNullOrEmpty(siguienteEscena))
            SceneManager.LoadScene(siguienteEscena);
        else
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
