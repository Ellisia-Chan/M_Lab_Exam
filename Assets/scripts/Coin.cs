using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {
    private const string COIN_COLLECT = "coinCollect";

    [SerializeField] private int coinValue = 1;

    private BoxCollider2D boxCollider2D;
    private Animator animator;

    private void Awake() {
        boxCollider2D = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.GetComponent<PlayerController>() != null) {
            boxCollider2D.enabled = false;
            animator.SetTrigger(COIN_COLLECT);
            EventManager.Instance.coinEvents.CoinCollected(coinValue);
        }
    }

    private void DestroyCoin() {
        Destroy(gameObject);
    }

}
