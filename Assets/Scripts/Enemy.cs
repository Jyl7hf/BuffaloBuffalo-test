using UnityEngine;

// abstract Enemy superclass for enemies
public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected float attackPower;
    [SerializeField] protected int health;
    [SerializeField] protected float speed;
    [SerializeField] protected float spawnRate;

    // // Start is called once before the first execution of Update after the MonoBehaviour is created
    // void Start()
    // {
        
    // }

    // // Update is called once per frame
    // void Update()
    // {
        
    // }

    void FixedUpdate() {
        move();
    }

    // enemies currently move toward the mouse position
    // ASSUMPTION: this will be replaced/rewritten when a player character is implemented
    private void move() {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(
            new Vector3(Input.mousePosition.x, Input.mousePosition.y, 29.5f));
        if(Vector3.Distance(transform.position, mousePos) > 3) {
            transform.position = Vector3.MoveTowards(transform.position, mousePos, speed * Time.fixedDeltaTime);
        }
    }

    // alters the attackPower by a random value between arguments
    // ASSUMPTION: there may be cases in the future where attackPower can be altered by a range
    public void alterAttackPower(float minInc, float maxInc) {
        float inc = Random.Range(minInc, maxInc);
        attackPower += inc;
        if(attackPower < 0) {
            attackPower = 0;
        }
    }

    // alters the speed by a random value between arguments
    public void alterSpeed(float minInc, float maxInc) {
        float inc = Random.Range(minInc, maxInc);
        speed += inc;
        if(speed < 0) {
            speed = 0;
        }
    }

    // returns a new spawn rate value (does not affect the spawnRate field)
    public float newSpawnRate(float minInc, float maxInc) {
        float inc = Random.Range(minInc, maxInc);
        float newSpawnRate = spawnRate + inc;
        if(newSpawnRate < 0) {
            newSpawnRate = 0;
        }
        else if(newSpawnRate > 1) {
            newSpawnRate = 1;
        }
        return newSpawnRate;
    }
}
