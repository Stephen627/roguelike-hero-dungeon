using UnityEngine;

public class Door : MonoBehaviour
{
    public Sprite openDoorSprite;
    public Sprite closeDoorSprite;

    public bool isDoorOpen = true;
    public bool showingOpenDoor;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        this.spriteRenderer = this.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (!this.showingOpenDoor && isDoorOpen)
        {
            this.spriteRenderer.sprite = this.openDoorSprite;
            this.showingOpenDoor = true;
        }
        if (this.showingOpenDoor && !isDoorOpen)
        {
            this.spriteRenderer.sprite = this.closeDoorSprite;
            this.showingOpenDoor = false;
        }
    }
}
