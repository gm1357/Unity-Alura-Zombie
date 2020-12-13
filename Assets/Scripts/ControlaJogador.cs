using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaJogador : MonoBehaviour, IMatavel, ICuravel {

    public ControlaInterface controlaInterface;
    public AudioClip somDano;
    public Status statusJogador;

    private Vector3 direcao;
    private MovimentaJogador movimentaJogador;
    private AnimaPersonagem animaJogador;

    private void Start() {
        movimentaJogador = GetComponent<MovimentaJogador>();
        animaJogador = GetComponent<AnimaPersonagem>();
        statusJogador = GetComponent<Status>();
    }

    private void Update() {
        float eixoX = Input.GetAxis("Horizontal");
        float eixoZ = Input.GetAxis("Vertical");
        direcao = new Vector3(eixoX, 0, eixoZ);

        animaJogador.Movimentar(direcao.magnitude);
    }

    private void FixedUpdate() {
        movimentaJogador.Movimentar(direcao, statusJogador.velocidade);
        movimentaJogador.RotacionarJogador();
    }

    public void TomarDano(int dano) {
        statusJogador.vida -= dano;
        controlaInterface.AtualizaSliderVida();
        ControlaAudio.instancia.PlayOneShot(somDano);

        if (statusJogador.vida <= 0) {
            Morrer();
        }
    }

    public void Morrer() {
        controlaInterface.GameOver();
    }

    public void CurarVida(int quantidadeDeCura) {
        statusJogador.vida += quantidadeDeCura;

        if (statusJogador.vida > statusJogador.vidaInicial) {
            statusJogador.vida = statusJogador.vidaInicial;
        }

        controlaInterface.AtualizaSliderVida();
    }
}
