using TMPro;
using UnityEngine;

public class ControladorCanvas : MonoBehaviour
{
    public TMP_Text textoGemas;
    public TMP_Text textoFinNivel;

    void Start()
    {
        if (textoFinNivel != null)
            textoFinNivel.gameObject.SetActive(false);
    }

    void Update()
    {

    }
    public void ActualizarGemas(int actuales, int total)
    {
        if (textoGemas != null)
            textoGemas.text = $"GEMAS: {actuales}/{total}";
    }

    public void MostrarFinNivel()
    {
        if (textoFinNivel != null)
        {
            textoFinNivel.text = "¡NIVEL COMPLETADO!";
            textoFinNivel.gameObject.SetActive(true);
        }
    }
}
