using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentaPersonagem : MonoBehaviour {

    private Rigidbody myRigidbody;

    private void Awake() {
        myRigidbody = GetComponent<Rigidbody>();
    }

    public void Movimentar(Vector3 direcao, float velocidade) {
        myRigidbody.MovePosition(myRigidbody.position + (direcao.normalized * velocidade * Time.deltaTime));
    }

    public void Rotacionar(Vector3 direcao) {
        myRigidbody.MoveRotation(Quaternion.LookRotation(direcao));
    }

    public void Morrer() {
        StartCoroutine(SinkOnGround());
    }

    private IEnumerator SinkOnGround() {
        GetComponent<Collider>().enabled = false;
        yield return new WaitForSeconds(2);
        myRigidbody.constraints = RigidbodyConstraints.None;
        myRigidbody.velocity = Vector3.zero;
        myRigidbody.isKinematic = false;
    }
}
