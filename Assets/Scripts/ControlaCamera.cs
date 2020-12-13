using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaCamera : MonoBehaviour {

    public GameObject jogador;

    private Vector3 distanciaCompensar;

    private void Start() {
        distanciaCompensar = transform.position - jogador.transform.position;
    }
    
    private void Update() {
        transform.position = jogador.transform.position + distanciaCompensar;
    }
}
