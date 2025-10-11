using UnityEngine;

public class ControladorMeta : MonoBehaviour
{
    public bool llegoPlayer = false;

    void Start()
    {
        Collider col = GetComponent<Collider>();
        if (col != null) col.isTrigger = true;
    }
    void Update()
    {

    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !llegoPlayer)
        {
            llegoPlayer = true;
            Debug.Log("El jugador llegó a la meta.");
            ControladorPlayer cp = other.GetComponent<ControladorPlayer>();
            int total = (cp != null) ? cp.contadorMonedas : 0;
            Debug.Log("Total de gemas recolectadas: " + total);
            if (GameManager.Instance != null)
                GameManager.Instance.TerminarNivel();
        }
    }
}
