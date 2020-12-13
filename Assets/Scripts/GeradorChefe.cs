using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeradorChefe : MonoBehaviour {
    public float tempoEntreGeracoes = 60;
    public GameObject chefe;
    public Transform[] posicoesPossiveisGeracao;

    private float tempoParaProximaGeracao = 0;
    private ControlaInterface scriptControlaInterface;
    private Transform jogador;

    private void Start() {
        tempoParaProximaGeracao = tempoEntreGeracoes;
        scriptControlaInterface = GameObject.FindWithTag(Tags.Interface).GetComponent<ControlaInterface>();
        jogador = GameObject.FindWithTag(Tags.Jogador).transform;
    }

    private void Update() {
        if (Time.timeSinceLevelLoad > tempoParaProximaGeracao) {
            Vector3 posicaoCriacao = CalcularPosicaoMaisDistanteDoJogador();
            Instantiate(chefe, posicaoCriacao, Quaternion.identity);
            scriptControlaInterface.AparecerTextoChefeCriado();
            tempoParaProximaGeracao = Time.timeSinceLevelLoad + tempoEntreGeracoes;
        }
    }

    Vector3 CalcularPosicaoMaisDistanteDoJogador() {
        Vector3 posicaoMaiorDistancia = Vector3.zero;
        float maiorDistancia = 0;

        foreach (Transform posicao in posicoesPossiveisGeracao) {
            float distanciaEntreJogador = Vector3.Distance(posicao.position, jogador.position);
            if (distanciaEntreJogador > maiorDistancia) {
                maiorDistancia = distanciaEntreJogador;
                posicaoMaiorDistancia = posicao.position;
            }
        }

        return posicaoMaiorDistancia;
    }
}
