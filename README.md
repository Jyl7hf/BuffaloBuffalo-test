# BuffaloBuffalo-test

Author: Jillian Li

Completion date: June 25, 2025

General notes:
- I coded this with Java naming conventions.
- I like to comment out Start() and Update() in MonoBehaviour scripts if I don't use them.
- Currently I have new enemies spawn every 10 seconds.
- Currently I have the enemies move to follow the mouse so new enemies don't spawn inside old enemies.

General assumptions:
- Grunt, Archer, and Assassin enemy classes will be expanded on to have their own unique behaviours in the future, which is why I have separate scripts for them.
- Spawn points can spawn multiple enemies but at most one of each type at a time.
- The spawn rate of an enemy type corresponds to: a spawn point's (that can spawn that enemy type) probability of spawning the enemy during each spawn wave.
- When a spawn rate increases/decreases, it applies to all spawn points for the duration of the game (1 value that does not change). Other attributes (attack power and speed) that increase/decrease by a random value are unique to the enemy instance (different value for each enemy object).
