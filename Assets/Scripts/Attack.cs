using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public float damageAmount;
    public PlayerController playerController;

    private void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject hit = collision.gameObject;
        if (hit  != null )
        {
            if (hit.tag == "Enemy")
            {
                hit.GetComponent<EnemyController>().TakeDamage(damageAmount);
                playerController.EnemyHit();
            }
            
            Destroy(gameObject);
        }
    }
}
