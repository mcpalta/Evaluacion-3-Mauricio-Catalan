using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorBola : MonoBehaviour
{
    public Rigidbody rigid_body;
    [Range(-1f,1f)]
    public float coeficienteFuerza = 1f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //this.transform.Translate(0f, 0f, 1f);
        rigid_body.AddForce(0f, 0f, 1f * coeficienteFuerza);
    }
}
