using UnityEngine;

public class Destroyer : MonoBehaviour
{
    public bool dontDestory = false;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Connection Point")
            Destroy(other.gameObject);

        /*if (other.gameObject.tag == "Room")
        {
            Destroyer otherDestroyer = other.gameObject.GetComponent<Destroyer>();
            if (otherDestroyer.dontDestory)
            {
                Destroy(this.gameObject.transform.parent.gameObject);
            }
            else
            {
                this.dontDestory = true;
                Destroy(other.gameObject.transform.parent.gameObject);
            }
        }*/
    }    
}
