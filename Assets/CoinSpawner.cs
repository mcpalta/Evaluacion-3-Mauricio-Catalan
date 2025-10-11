using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    public GameObject baseCoin;
    public Vector3 posicionSpawn;

    public List<GameObject> monedasCreadas;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        monedasCreadas = new List<GameObject>();

        CreaMoneda("01");
        CreaMoneda("02");
        CreaMoneda("03");

        //        GameObject otroObjeto = new GameObject();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CreaMoneda(string nombrePropio)
    {
        posicionSpawn = this.transform.position + new Vector3(0f, 1f, 0f);

        string nombreMoneda = baseCoin.name;

        GameObject objetoCreado = GameObject.Instantiate(baseCoin);

        objetoCreado.name = nombreMoneda + "_" + nombrePropio;
        objetoCreado.transform.position = posicionSpawn;

        monedasCreadas.Add(objetoCreado);
    }
}
