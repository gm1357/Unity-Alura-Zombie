using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimaPersonagem : MonoBehaviour {

    private Animator animator;

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    public void Atacar(bool estado) {
        animator.SetBool("atacando", estado);
    }

    public void Movimentar(float movimento) {
        animator.SetFloat("movendo", movimento);
    }

    public void Morrer() {
        animator.SetTrigger("morrer");
    }
}
