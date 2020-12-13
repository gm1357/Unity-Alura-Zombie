using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentaJogador : MovimentaPersonagem {

    public LayerMask mascaraChao;

    public void RotacionarJogador() {
        Ray raio = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit impacto;

        if (Physics.Raycast(raio, out impacto, 100, mascaraChao)) {
            Vector3 posicaoMiraJogador = impacto.point - transform.position;
            posicaoMiraJogador.y = transform.position.y;

            Rotacionar(posicaoMiraJogador);
        }
    }
}
