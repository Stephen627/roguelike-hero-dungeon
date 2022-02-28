using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dungeon
{
    public class Door : MonoBehaviour
    {
        public bool isOpen = true;
        public bool showingOpenDoor = false;
        public Sprite openDoorSprite;

        private SpriteRenderer spriteRenderer;
        private BoxCollider2D bc2d;

        private void Start()
        {
            this.spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
            this.bc2d = this.gameObject.GetComponent<BoxCollider2D>();
        }

        private void Update()
        {
            if (this.isOpen && !this.showingOpenDoor)
            {
                this.spriteRenderer.sprite = null;
                this.bc2d.enabled = false;
                this.showingOpenDoor = true;
            }

            if (!this.isOpen && this.showingOpenDoor)
            {
                this.spriteRenderer.sprite = this.openDoorSprite;
                this.bc2d.enabled = true;
                this.showingOpenDoor = false;
            }

        }
    }
}
