using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// class to control the time of day and spawn rates
public class DayCycle : MonoBehaviour
{
    public static readonly int MORNING = 1;
    public static readonly int AFTERNOON = 2;
    public static readonly int NIGHT = 3;

    public static int timeOfDay { get; private set; } // current time of day
    // currently I have the floor change colour to reflect the time of day
    // colours for the platform/floor
    private Color morningColour = new Color(0.5f, 0.9f, 1f);
    private Color afternoonColour = new Color(0.9f, 0.7f, 0.4f);
    private Color nightColour = new Color(0.4f, 0.1f, 0.7f);

    [SerializeField] private GameObject[] allEnemies; // array of all enemy types/prefabs
    // dictionary for spawn rates of enemy types
    // key: prefab object
    // value: spawn rate
    public static Dictionary<GameObject, float> spawnRates { get; private set; }

    private SpawnPoint[] spawnPoints; // array of all spawn points
    [SerializeField] private int timeBetweenSpawns = 10; // number of seconds between spawn waves
    [SerializeField] private float spawnTimer = 0.5f; // timer until next spawn wave

    void Awake() {
        // initiate spawnRates dictionary with enemy types and default spawn rates
        spawnRates = new Dictionary<GameObject, float>();
        foreach(GameObject enemyType in allEnemies) {
            spawnRates.Add(enemyType, enemyType.GetComponent<Enemy>().newSpawnRate(0, 0));
        }

        // initiate spawnPoints array with the spawn points in the scene
        spawnPoints = FindObjectsByType<SpawnPoint>(FindObjectsSortMode.None);

        setTimeOfDay();
    }

    // // Start is called once before the first execution of Update after the MonoBehaviour is created
    // void Start()
    // {
        
    // }

    // // Update is called once per frame
    // void Update()
    // {
        
    // }

    void FixedUpdate() {
        // handle timer for spawning enemies
        if(spawnTimer > 0) {
            spawnTimer -= Time.fixedDeltaTime;
            if(spawnTimer <= 0) {
                foreach(SpawnPoint sp in spawnPoints) {
                    sp.spawnEnemies();
                }
                spawnTimer = timeBetweenSpawns;
            }
        }
    }

    // set random time of day
    // ASSUMPTION: in the future, the time of day may change multiple times during runtime
    public void setTimeOfDay() {
        resetSpawnRates();
        timeOfDay = Random.Range(1, 4);
        // if morning
        if(timeOfDay == MORNING) {
            Debug.Log("Morning");
            GetComponent<Renderer>().material.color = morningColour;
            // alter spawn rates that change during morning
            foreach(GameObject enemyType in allEnemies) {
                // increase spawn rate of Archers by a range of 0.2 to 0.4
                if(enemyType.GetComponent<Enemy>() is Archer) {
                    spawnRates[enemyType] = enemyType.GetComponent<Enemy>().newSpawnRate(0.2f, 0.4f);
                }
                // decrease spawn rate of Brown enemies by a range of -0.1 to -0.3
                else if(enemyType.name == "Brown Grunt") {
                    spawnRates[enemyType] = enemyType.GetComponent<Enemy>().newSpawnRate(-0.3f, -0.1f);
                }
            }
        }
        // if afternoon
        else if(timeOfDay == AFTERNOON) {
            Debug.Log("Afternoon");
            GetComponent<Renderer>().material.color = afternoonColour;
            // alter spawn rates that change during afternoon
            foreach(GameObject enemyType in allEnemies) {
                // no Assassins can spawn
                if(enemyType.GetComponent<Enemy>() is Assassin) {
                    spawnRates[enemyType] = 0;
                }
                // all other enemies except Grunts increase/decrease their spawn rate by a range of -0.2 to 0.2
                else if(!(enemyType.GetComponent<Enemy>() is Grunt)) {
                    spawnRates[enemyType] = enemyType.GetComponent<Enemy>().newSpawnRate(-0.2f, 0.2f);
                }
            }
        }
        // if night
        else if(timeOfDay == NIGHT) {
            Debug.Log("Night");
            GetComponent<Renderer>().material.color = nightColour;
            // no spawn rate changes
        }

        // print the spawn rates on debug console
        foreach(KeyValuePair<GameObject, float> kvp in spawnRates) {
            Debug.Log(kvp.Key.name + " " + kvp.Value);
        }
    }

    // reset spawnRates dictionary to default enemy spawn rates
    public void resetSpawnRates() {
        foreach(GameObject enemyType in allEnemies) {
            spawnRates[enemyType] = enemyType.GetComponent<Enemy>().newSpawnRate(0, 0);
        }
    }
}
