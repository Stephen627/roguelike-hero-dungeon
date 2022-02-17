# The plan

## Generating the dungeon

- Need a max depth to the dungeon
- Add more rooms with more than two exits
- When two rooms colide generate a room with both exits needed rather than blocking the room off
    - This should generate a maze rather than a path
    - The plan
        - Raycast2D from the spawn pointer for a distance of 10
        - Add a collider to the middle of all rooms
        - Intersect the arrays for the rooms that we need 