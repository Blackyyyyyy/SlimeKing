using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class TNT : MonoBehaviour
{
    public Transform explosionPrefab;

    private float fuseTime;
    private float startTime;

    private float explosionRadius = 2;

    private float playerPositionOffset = 0.3f;

    private float minExplosionForce = 1f;
    private float maxExplosionForce = 11f;

    public bool exploding = false;

    void Start()
    {
        startTime = Time.time;
        fuseTime = Settings.fuseTime;
    }
    
    void Update()
    {
        if(startTime + fuseTime <= Time.time) explode();
    }

    public void explode()
    {
        exploding = true;
        Vector2 playerPosition = (Vector2)GameMemory.getPlayer().position + playerPositionOffset * Vector2.up;
        float distanceToPlayer = Vector2.Distance(transform.position, playerPosition);

        explodeSurroundingTNT();

        if (distanceToPlayer < explosionRadius)
        {
            Rigidbody2D playerRigid = GameMemory.getPlayer().GetComponent<Rigidbody2D>();

            float forceMultiplier = 1 - (distanceToPlayer / explosionRadius);

            playerRigid.AddForce((playerPosition - (Vector2)transform.position).normalized * (forceMultiplier * maxExplosionForce + minExplosionForce), ForceMode2D.Impulse);
        }

        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void explodeSurroundingTNT()
    {
        Collider2D[] collisions = Physics2D.OverlapCircleAll(transform.position, explosionRadius);

        foreach(Collider2D c in collisions)
        {
            TNT tnt = c.GetComponent<TNT>();
            if(tnt != null)
            {
                Destroy(c.gameObject);
            }
        }
    }
}
