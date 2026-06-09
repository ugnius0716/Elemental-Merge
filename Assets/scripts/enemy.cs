using UnityEngine;

public class Enemy : MonoBehaviour
{ 

    public float startSpeed = 5f;
    [HideInInspector]
    public float speed;

    public float health = 100f;
    public int coinsGiven = 10;

    public GameObject deathEffect;

    void Start()
    {
        speed = startSpeed;
    }
    public void TakeDamage(float amount)
    {
        health -= amount;
        if(health <= 0)
        {
            Die();
        }
    }
    public void Slow(float percent)
    {
        speed = startSpeed * (1f - percent);
    }
    void Die() {
        PlayerStats.money += coinsGiven;

        GameObject effect = (GameObject)Instantiate( deathEffect, transform.position, Quaternion.identity);
        Destroy(effect,5f);

        Destroy(gameObject);
    }
    
}
