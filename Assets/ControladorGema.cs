using UnityEngine;

public class ControladorGema : MonoBehaviour
{
    public int valor = 1;
    public ParticleSystem efectoRecoger;
    public AudioClip sonidoRecoger;
    private bool recogida = false;

    void Start()
    {
        Collider col = GetComponent<Collider>();
        if (col != null) col.isTrigger = true;
        gameObject.tag = "Gema";
    }
    void Update()
    {

    }
    void OnTriggerEnter(Collider other)
    {
        if (recogida) return;
        if (other.CompareTag("Player"))
        {
            recogida = true;
            RecogerGema(other.gameObject);
        }
    }

    void RecogerGema(GameObject player)
    {
        if (efectoRecoger != null)
            Instantiate(efectoRecoger, transform.position, Quaternion.identity);

        if (sonidoRecoger != null)
            AudioSource.PlayClipAtPoint(sonidoRecoger, transform.position);

        ControladorPlayer playerCtrl = player.GetComponent<ControladorPlayer>();
        if (playerCtrl != null)
            playerCtrl.contadorMonedas += valor;

        GameManager.Instance.SumarGema(valor);
        Destroy(gameObject);
    }
}
