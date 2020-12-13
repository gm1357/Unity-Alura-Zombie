using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class ControlaChefe : MonoBehaviour, IMatavel {

    public AudioClip somMorte;
    public GameObject kitMedico;
    public Slider sliderVida;
    public Image imagemSlider;
    public Color corVidaMaxima;
    public Color corVidaMinima;
    public GameObject particulaSangue;

    private Transform jogador;
    private NavMeshAgent agente;
    private Status statusChefe;
    private AnimaPersonagem animacaoChefe;
    private MovimentaPersonagem movimentoChefe;
    private ControlaInterface scriptControlaInterface;

    private void Start() {
        jogador = GameObject.FindWithTag(Tags.Jogador).transform;
        agente = GetComponent<NavMeshAgent>();
        statusChefe = GetComponent<Status>();
        agente.speed = statusChefe.velocidade;
        animacaoChefe = GetComponent<AnimaPersonagem>();
        movimentoChefe = GetComponent<MovimentaPersonagem>();
        scriptControlaInterface = GameObject.FindWithTag(Tags.Interface).GetComponent<ControlaInterface>();
        sliderVida.maxValue = statusChefe.vidaInicial;
        AtualizarInterface();
    }

    private void Update() {
        agente.SetDestination(jogador.position);
        animacaoChefe.Movimentar(agente.velocity.magnitude);

        if (agente.hasPath == true) {
            bool pertoJogador = agente.remainingDistance <= agente.stoppingDistance;


            if (pertoJogador) {
                animacaoChefe.Atacar(true);
                Vector3 direcao = jogador.position - transform.position;
                movimentoChefe.Rotacionar(direcao);
            } else {
                animacaoChefe.Atacar(false);
            }
        }
    }

    void AtacaJogador() {
        int dano = Random.Range(30, 40);
        jogador.GetComponent<ControlaJogador>().TomarDano(dano);
    }

    public void TomarDano(int dano) {
        statusChefe.vida -= dano;
        AtualizarInterface();
        if (statusChefe.vida <= 0) {
            Morrer();
        }
    }

    public void ParticulaSangue(Vector3 posicao, Quaternion rotacao) {
        Instantiate(particulaSangue, posicao, rotacao);
    }

    public void Morrer() {
        animacaoChefe.Morrer();
        movimentoChefe.Morrer();
        this.enabled = false;
        agente.enabled = false;
        ControlaAudio.instancia.PlayOneShot(somMorte);
        scriptControlaInterface.AtualizarQuantidadeDeZumbisMortos();
        Instantiate(kitMedico, transform.position, Quaternion.identity);
        Destroy(gameObject, 3);
    }

    private void AtualizarInterface() {
        sliderVida.value = statusChefe.vida;
        float porcentagemVida = statusChefe.vida / statusChefe.vidaInicial;
        Color corVida = Color.Lerp(corVidaMinima, corVidaMaxima, porcentagemVida);
        imagemSlider.color = corVida;
    }
}
