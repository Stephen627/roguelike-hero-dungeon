using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {
    [SerializeField] protected SpriteRenderer spriteRenderer;
    [SerializeField] private GameObject highlight;

    public virtual void Init(int x, int y) {
    }

    private void OnMouseEnter() {
        this.highlight.SetActive(true);    
    }

    private void OnMouseExit() {
        this.highlight.SetActive(false);
    }
}
