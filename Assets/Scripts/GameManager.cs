using UnityEngine;

public class GameManager : MonoBehaviour
{
   private void Start()
   {
       Invoke("StartRoomGeneration", 0);
   }

   private void StartRoomGeneration()
   {
       RoomSpawner roomSpawner = this.gameObject.GetComponent<RoomSpawner>();
       roomSpawner.Spawn();
   }
}
