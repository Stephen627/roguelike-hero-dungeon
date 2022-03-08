using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {
    [SerializeField] private Color baseColor, offsetColor;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private GameObject highlight;

    public void Init(bool isOffset) {
        this.spriteRenderer.color = isOffset ? this.offsetColor : this.baseColor;
    }

    private void OnMouseEnter() {
        this.highlight.SetActive(true);    
    }

    private void OnMouseExit() {
        this.highlight.SetActive(false);
    }
}
