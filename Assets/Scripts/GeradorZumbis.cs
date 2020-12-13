using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeradorZumbis : MonoBehaviour {

    public GameObject zumbi;
    public float tempoGerarZumbi = 1;
    public LayerMask layerZumbi;

    private GameObject jogador;
    private float contadorTempo = 0;
    private int distanciaGeracao = 3;
    private int distanciaJogador = 20;
    private int quantidadeMaximaZumbisVivos = 2;
    private int quantidadeZumbisVivos = 0;
    private float tempoProximoAumentoDeDificuldade = 30;
    private float contadorDeAumentarDificuldade;

    private void Start() {
        jogador = GameObject.FindWithTag(Tags.Jogador);
        contadorDeAumentarDificuldade = tempoProximoAumentoDeDificuldade;

        for (int i = 0; i < quantidadeMaximaZumbisVivos; i++) {
            StartCoroutine(GerarZumbi());
        }
    }

    private void Update() {

        if (Vector3.Distance(transform.position, jogador.transform.position) > distanciaJogador) {
            contadorTempo += Time.deltaTime;

            if (VerificarSePodeGerarZumbi()) {
                StartCoroutine(GerarZumbi());
                contadorTempo = 0;
            }
        }

        if (Time.timeSinceLevelLoad > contadorDeAumentarDificuldade) {
            quantidadeMaximaZumbisVivos++;
            contadorDeAumentarDificuldade = Time.timeSinceLevelLoad + tempoProximoAumentoDeDificuldade;
        }
    }

    private bool VerificarSePodeGerarZumbi() {
        return contadorTempo >= tempoGerarZumbi && quantidadeZumbisVivos < quantidadeMaximaZumbisVivos;
    }

    private IEnumerator GerarZumbi() {
        Vector3 posicaoGeracao = AleatorizarPosicao();

        while(Physics.OverlapSphere(posicaoGeracao, 2, layerZumbi).Length > 0) {
            posicaoGeracao = AleatorizarPosicao();
            yield return null;
        }

        ControlaZumbi controlaZumbi =
            Instantiate(zumbi, posicaoGeracao, transform.rotation).GetComponent<ControlaZumbi>();

        controlaZumbi.meuGerador = this;
        quantidadeZumbisVivos++;
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, distanciaGeracao);
    }

    Vector3 AleatorizarPosicao() {
        Vector3 posicao = Random.insideUnitSphere * distanciaGeracao;
        posicao += transform.position;
        posicao.y = 0;

        return posicao;
    }

    public void DiminuirQuantidadeDeZumbisVivos() {
        quantidadeZumbisVivos--;
    }
}
