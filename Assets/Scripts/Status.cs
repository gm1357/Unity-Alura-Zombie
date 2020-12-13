using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour {

    public float vidaInicial = 100;
    [HideInInspector]
    public float vida;
    public float velocidade = 5;

    private void Awake() {
        vida = vidaInicial;
    }
}
