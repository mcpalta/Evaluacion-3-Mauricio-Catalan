using UnityEngine;

public class Coin : MonoBehaviour
{
    public float velocidadGiro = 1f;
    public float tiempoVida = 4;
    public float contadorVida = 0;

    public CoinSpawner spawner;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawner = FindAnyObjectByType<CoinSpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(0f, velocidadGiro, 0f);
        contadorVida = contadorVida + Time.deltaTime;

        DestruyeMoneda();
    }

    public void DestruyeMoneda()
    {
        if (contadorVida >= tiempoVida)
        {
            spawner.monedasCreadas.Remove(this.gameObject);
            GameObject.Destroy(this.gameObject);
        }
    }
}
