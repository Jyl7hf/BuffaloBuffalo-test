using UnityEngine;

// class for the (4) spawn points
public class SpawnPoint : MonoBehaviour
{
    // these arrays should have the same length
    [SerializeField] private Transform[] spawnPositions; // all positions where an enemy can be spawned
    [SerializeField] private GameObject[] enemyPrefabs; // all enemies that can be spawned at this spawn point

    // // Start is called once before the first execution of Update after the MonoBehaviour is created
    // void Start()
    // {
        
    // }

    // // Update is called once per frame
    // void Update()
    // {
        
    // }

    // method to spawn enemies
    public void spawnEnemies() {
        int enemiesSpawned = 0;
        foreach(GameObject enemyType in enemyPrefabs) {
            // RNG to decide if the enemy is spawned or not
            if(Random.Range(0, 1f) < DayCycle.spawnRates[enemyType]) {
                // spawn the enemy in the next available spawn position
                Enemy currentEnemy = (Instantiate(enemyType, spawnPositions[enemiesSpawned].position, Quaternion.identity))
                                    .GetComponent<Enemy>();
                // during afternoon, Grunts increase their attackPower by 1
                if(DayCycle.timeOfDay == DayCycle.AFTERNOON && currentEnemy is Grunt) {
                    currentEnemy.alterAttackPower(1, 1);
                }
                // during night, Assassins increase their speed by a range of 0 to 2
                else if(DayCycle.timeOfDay == DayCycle.NIGHT && currentEnemy is Assassin) {
                    currentEnemy.alterSpeed(0, 2);
                }
                enemiesSpawned++;
            }
        }
    }
}
