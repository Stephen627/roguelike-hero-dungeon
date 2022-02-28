using UnityEngine;

public class GameManager : MonoBehaviour
{
   private void Start()
   {
       Invoke("StartRoomGeneration", 0);
   }

   private void StartRoomGeneration()
   {
       Dungeon.RoomSpawner roomSpawner = this.gameObject.GetComponent<Dungeon.RoomSpawner>();
       roomSpawner.Spawn();
   }
}
