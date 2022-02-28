using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Dungeon
{
    public class RoomSpawner : MonoBehaviour
    {
        public GameObject entryRoom;
        public GameObject[] topRooms;
        public GameObject[] bottomRooms;
        public GameObject[] leftRooms;
        public GameObject[] rightRooms;
        public GameObject[] allRooms;
        public int maxDepth = 5;
        public LayerMask roomLayer;
        private List<GameObject> rooms;

        public void Spawn()
        {
            this.rooms = new List<GameObject>();
            GameObject instance = Instantiate(this.entryRoom, new Vector3(0, 0, 0), Quaternion.identity);

            this.rooms.Add(instance);
            this.SpawnConnectingRooms(instance);
        }

        private void SpawnConnectingRooms(GameObject room, int depth = 1)
        {
            ConnectionPoint[] connectionPoints = room.GetComponentsInChildren<ConnectionPoint>();

            for (int i = 0; i < connectionPoints.Length; i++)
            {
                ConnectionPoint connectionPoint = connectionPoints[i];

                if (!connectionPoint.enabled)
                    continue;

                Transform transform = connectionPoint.gameObject.transform;
                GameObject spawnRoom = null;
                bool atMaxDepth = depth > this.maxDepth;
                
                if (atMaxDepth)
                    spawnRoom = this.FindTerminatingRoom(connectionPoint);
                else
                    spawnRoom = this.FindAppropriateRoom(connectionPoint);

                if (spawnRoom == null)
                    continue;
                
                GameObject instance = Instantiate(
                    spawnRoom,
                    new Vector3(transform.position.x, transform.position.y, 0),
                    Quaternion.identity
                );
                this.rooms.Add(instance);
                connectionPoint.enabled = false;

                if (!atMaxDepth)
                    this.SpawnConnectingRooms(instance, ++depth);
            }
        }

        private GameObject FindTerminatingRoom(ConnectionPoint connectionPoint)
        {
            ConnectionPoint.DirectionResult openings = connectionPoint.GetRequiredOpenings();
            List<string> roomNameArray = new List<string>();
            string[] roomNameOrder = { "L", "R", "T", "B" };
            for (int i = 0; i < openings.requiredDirections.Count; i++)
            {
                switch (openings.requiredDirections[i])
                {
                    case ConnectionPoint.Direction.Top:
                        roomNameArray.Add("T");
                        break;
                    case ConnectionPoint.Direction.Bottom:
                        roomNameArray.Add("B");
                        break;
                    case ConnectionPoint.Direction.Left:
                        roomNameArray.Add("L");
                        break;
                    case ConnectionPoint.Direction.Right:
                        roomNameArray.Add("R");
                        break;
                }
            }
            roomNameArray.Sort(delegate (string a, string b) {
                return Array.IndexOf(roomNameOrder, a) > Array.IndexOf(roomNameOrder, b) ? 1 : -1;
            });
            string roomName = String.Join("", roomNameArray);

            return this.allRooms.FirstOrDefault(room => room.name == roomName);
        }

        private GameObject FindAppropriateRoom(ConnectionPoint connectionPoint)
        {
            GameObject[] rooms = null;
            ConnectionPoint.DirectionResult openings = connectionPoint.GetRequiredOpenings();
            List<ConnectionPoint.Direction> requiredOpeningDirections = openings.requiredDirections;
            List<ConnectionPoint.Direction> closedDirections = openings.closedDirections;

            if (requiredOpeningDirections.Count == 0)
                return null;
            
            for (int i = 0; i < requiredOpeningDirections.Count; i++)
            {
                ConnectionPoint.Direction requiredDirection = requiredOpeningDirections[i];
                GameObject[] tmp = this.FindRoomsForDirection(requiredDirection);

                if (rooms == null)
                    rooms = tmp;
                else
                    rooms = rooms.Intersect(tmp).ToArray();
            }

            for (int i = 0; i < closedDirections.Count; i++)
            {
                ConnectionPoint.Direction closedDirection = closedDirections[i];
                GameObject[] tmp = this.FindRoomsForDirection(closedDirection);

                rooms = rooms.Except(tmp).ToArray();
            }

            int rand = Random.Range(0, rooms.Length);
            return rooms[rand];
        }

        private GameObject[] FindRoomsForDirection(ConnectionPoint.Direction direction)
        {
            switch (direction)
            {
                case ConnectionPoint.Direction.Top:
                    return this.topRooms;
                case ConnectionPoint.Direction.Bottom:
                    return this.bottomRooms;
                case ConnectionPoint.Direction.Left:
                    return this.leftRooms;
                case ConnectionPoint.Direction.Right:
                    return this.rightRooms;
                default:
                    return this.topRooms; // This is impossible to reach
            }
        }
    }
}
