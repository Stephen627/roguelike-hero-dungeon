using UnityEngine;

namespace Dungeon
    {
    public class Destroyer : MonoBehaviour
    {
        public bool dontDestory = false;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.tag == "Connection Point")
                Destroy(other.gameObject);
        }    
    }
}
