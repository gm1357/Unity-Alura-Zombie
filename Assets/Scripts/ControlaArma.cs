using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaArma : MonoBehaviour {

    public GameObject bala;
    public GameObject canoArma;
    public AudioClip somTiro;

    private void Update() {
        if (Input.GetButtonDown("Fire1")) {
            Instantiate(bala, canoArma.transform.position, canoArma.transform.rotation);
            ControlaAudio.instancia.PlayOneShot(somTiro);
        }
    }
}
