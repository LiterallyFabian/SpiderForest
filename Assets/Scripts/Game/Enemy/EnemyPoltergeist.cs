using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPoltergeist : MonoBehaviour 
{
    //poltergeisten kastar f�rem�l ist�llet f�r att skada direkt genom att spelaren g�r in i den
    //f�rsvinner n�r spelaren kommer f�r n�ra
    //statisk enemy
    //kolla s� att spelaren �r inom x avst�nd
    //kolla s� att attacken inte �r p� cooldown
    //attackera

    //hittar spelarens gameobject och sparar den och s�tter den nere i Start, aka n�r spelet har startat
    GameObject gameObjectPlayer;
    [SerializeField] GameObject throwableItem;
    //s�tt allting till ett v�rde h�r s� �r det l�ttare att �ndra i unity senare
    public Vector3 attackForce;

    //kan vara SerializeField men �sch
    public float attackDistance = 8;
    public float attackCooldown = 2;

    public SpriteRenderer spriteRenderer;
    public Sprite idleSprite;
    public Sprite newSprite;
    public Sprite stretchSprite;
    

    float lastAttack = 0;

    private void Update()
    {
        Vector3 playerPosition = gameObjectPlayer.transform.position;
        Vector3 enemyPosition = gameObject.transform.position;
        //skillnaden mellan poltergeist och player
        float distance = Vector3.Distance(enemyPosition, playerPosition);

        //om lastAttack �r p� 0, s� kan poltergeist attackera igen
        if (distance < attackDistance && lastAttack < 0)
        {
            ChangeSprite(newSprite);
            lastAttack = attackCooldown;
            //kod som attackerar
            //googla!!!! rotationer i unity �r dj�vulen
            GameObject item = Instantiate(throwableItem, enemyPosition, Quaternion.identity);
            item.GetComponent<Rigidbody2D>().AddForce(attackForce * 300);
            
        }
        //deltaTime = tiden sen f�rra framen
        lastAttack -= Time.deltaTime;

        if (lastAttack < 1.58)
        {
            ChangeSprite(stretchSprite);
        }

        if (lastAttack < 1.5)
        {
            ChangeSprite(idleSprite);
        }
        
    }


    private void Start()
    {
        gameObjectPlayer = GameObject.Find("Player");
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    private void ChangeSprite(Sprite newSprite)
    {
        spriteRenderer.sprite = newSprite;
    }
    internal void OnTriggerEnter2D(Collider2D collision)
    {
        // if enemy hits a player - do harm
        if (collision.gameObject.CompareTag("Player"))
        {
            // do harm if flashlight is enabled or else check a 25% risk
            if (collision.GetComponent<PlayerMovement>().isFlashlightOn || Random.Range(0, 5) == 2)
            {
                PlayerState state = collision.gameObject.GetComponent<PlayerState>();
                state.Harm(2);
            }
        }
    }

}
