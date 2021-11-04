using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class TNT : MonoBehaviour
{
    public static float fuseTime
    {
        get { return fuseTimeStorage; }
        set { fuseTimeStorage = Mathf.Round(Mathf.Clamp(value, minFuseTime, maxFuseTime) * 100f) / 100f; }
    }
    private static float fuseTimeStorage = 0.5f;

    public readonly static float minFuseTime = 0.5f;
    public readonly static float maxFuseTime = 4;


    public Transform explosionPrefab;
    public SpriteRenderer strengthIndicator; //Explosion Strength Indicator
    public TextMesh fuseDisplay;

    private float localFuseTime;
    private float startTime;

    private float timeLeft;

    private float explosionRadius = 2;

    private float playerPositionOffset = 0.3f;
    
    private float maxExplosionForce = 12f;

    public bool exploding = false;

    void Start()
    {
        startTime = Time.time;
        localFuseTime = fuseTime;
        strengthIndicator.transform.localScale *= explosionRadius;
    }
    
    void Update()
    {
        timeLeft = startTime + localFuseTime - Time.time;
        fuseDisplay.text = Mathf.Clamp(Mathf.Round(timeLeft * 100) * 0.01f, 0.01f, localFuseTime).ToString();
        if(timeLeft <= 0) explode();
        
        strengthIndicator.color = new Color(1, 1, 1, Mathf.Sin(Time.time * 6) * 0.05f + 0.14f);
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

            float forceMultiplier = Mathf.Ceil((1 - (distanceToPlayer / explosionRadius)) * 4) / 4f;
            
            playerRigid.AddForce((playerPosition - (Vector2)transform.position).normalized * (forceMultiplier * maxExplosionForce), ForceMode2D.Impulse);
        }

        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void explodeSurroundingTNT()
    {
        Collider2D[] collisions = Physics2D.OverlapCircleAll(transform.position, explosionRadius);

        foreach(Collider2D c in collisions) if(c.tag == "TNT") Destroy(c.gameObject);
    }
}
