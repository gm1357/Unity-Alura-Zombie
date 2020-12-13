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
        Quaternion rotacaoOpastaBala = Quaternion.LookRotation(-transform.forward);
        switch(objetoDeColisao.tag) {
            case Tags.Inimigo:
                ControlaZumbi zumbi = objetoDeColisao.GetComponent<ControlaZumbi>();
                zumbi.TomarDano(danoTiro);
                zumbi.ParticulaSangue(transform.position, rotacaoOpastaBala);
                break;
            case Tags.Chefe:
                ControlaChefe chefe = objetoDeColisao.GetComponent<ControlaChefe>();
                chefe.TomarDano(danoTiro);
                chefe.ParticulaSangue(transform.position, rotacaoOpastaBala);
                break;
        }
        Destroy(gameObject);
    }
}
