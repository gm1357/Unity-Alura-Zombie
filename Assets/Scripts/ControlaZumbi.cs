using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaZumbi : MonoBehaviour, IMatavel {

    public GameObject jogador;
    public AudioClip somMorte;
    public GameObject kitMedico;
    [HideInInspector]
    public GeradorZumbis meuGerador;
    public GameObject particulaSangue;
    
    private ControlaJogador controlaJogador;
    private MovimentaPersonagem movimentaZumbi;
    private AnimaPersonagem animaZumbi;
    private Status statusZumbi;
    private Vector3 direcao;
    private Vector3 posicaoAleatoria;
    private float contadorVagar = 0;
    private float tempoParaAleatorizar = 4;
    private float porcentagemGerarKitMedico = 0.1f;
    private ControlaInterface scriptControlaInterface;

    private void Start() {
        jogador = GameObject.FindWithTag(Tags.Jogador);
        controlaJogador = jogador.GetComponent<ControlaJogador>();
        movimentaZumbi = GetComponent<MovimentaPersonagem>();
        animaZumbi = GetComponent<AnimaPersonagem>();
        statusZumbi = GetComponent<Status>();
        direcao = transform.position;
        aleatorizarZumbi();
        scriptControlaInterface = GameObject.FindWithTag(Tags.Interface).GetComponent<ControlaInterface>();
    }

    private void FixedUpdate() {
        float distancia = Vector3.Distance(transform.position, jogador.transform.position);

        movimentaZumbi.Rotacionar(direcao);
        animaZumbi.Movimentar(direcao.magnitude);
        
        if (distancia > 15) {
            Vagar();
        } else if (distancia > 2.5) {
            Perseguir();
        } else {
            Atacar();
        }
    }

    private void Atacar() {
        direcao = jogador.transform.position - transform.position;
        animaZumbi.Atacar(true);
    }

    private void Perseguir() {
        direcao = jogador.transform.position - transform.position;
        movimentaZumbi.Movimentar(direcao, statusZumbi.velocidade);
        animaZumbi.Atacar(false);
    }

    private void Vagar() {

        contadorVagar -= Time.deltaTime;
        if (contadorVagar <= 0) {
            posicaoAleatoria = AleatorizarPosicao();
            contadorVagar += tempoParaAleatorizar + Random.Range(-1f, 1f);
        }

        if (Vector3.Distance(transform.position, posicaoAleatoria) > 0.05) {
            direcao = posicaoAleatoria - transform.position;
            movimentaZumbi.Movimentar(direcao, statusZumbi.velocidade);
        }
    }

    private Vector3 AleatorizarPosicao() {
        Vector3 posicao = Random.insideUnitSphere * 10;
        posicao += transform.position;
        posicao.y = transform.position.y;

        return posicao;
    }

    private void AtacaJogador() {
        controlaJogador.TomarDano(20);
    }

    private void aleatorizarZumbi() {
        int tipoZumbi = Random.Range(1, transform.childCount);
        transform.GetChild(tipoZumbi).gameObject.SetActive(true);
    }

    public void TomarDano(int dano) {
        statusZumbi.vida -= dano;

        if (statusZumbi.vida <= 0) {
            Morrer();
        }
    }

    public void ParticulaSangue(Vector3 posicao, Quaternion rotacao) {
        Instantiate(particulaSangue, posicao, rotacao);
    }

    public void Morrer() {
        Destroy(gameObject, 3);
        animaZumbi.Morrer();
        movimentaZumbi.Morrer();
        this.enabled = false;
        ControlaAudio.instancia.PlayOneShot(somMorte);
        VerificarGeracaoKitMedico(porcentagemGerarKitMedico);
        scriptControlaInterface.AtualizarQuantidadeDeZumbisMortos();
        meuGerador.DiminuirQuantidadeDeZumbisVivos();
    }

    private void VerificarGeracaoKitMedico(float porcentagemGeracao) {
        if (Random.value <= porcentagemGeracao) {
            Instantiate(kitMedico, transform.position, Quaternion.identity);
        }
    }
}
