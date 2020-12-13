using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlaBala : MonoBehaviour {

    public float velocidade = 30;
    public int danoTiro = 1;

    private GameObject jogador;

    private void Start() {
        jogador = GameObject.FindWithTag(Tags.Jogador);
    }

    private void FixedUpdate() {
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        rigidbody.MovePosition(rigidbody.position + transform.forward * velocidade * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider objetoDeColisao) {
        switch(objetoDeColisao.tag) {
            case Tags.Inimigo:
                objetoDeColisao.GetComponent<ControlaZumbi>().TomarDano(danoTiro);
                break;
            case Tags.Chefe:
                objetoDeColisao.GetComponent<ControlaChefe>().TomarDano(danoTiro);
                break;
        }
        Destroy(gameObject);
    }
}
