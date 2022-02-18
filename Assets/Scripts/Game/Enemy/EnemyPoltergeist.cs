using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPoltergeist : EnemyMovingPoints //samma funktioner som EnemyMovingPoints (som �rver fr�n Enemy), �ndrar spriten
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

    public float attackDistance = 8;
    public float attackCooldown = 2;
    float lastAttack = 0;

    private void Update()
    {
        base.Update();
        Vector3 playerPosition = gameObjectPlayer.transform.position;
        Vector3 enemyPosition = gameObject.transform.position;
        //skillnaden mellan poltergeist och player
        float distance = Vector3.Distance(enemyPosition, playerPosition);

        if (distance < attackDistance && lastAttack < 0)
        {
            //om lastAttack �r p� 0, s� kan poltergeist attackera igen
            lastAttack = attackCooldown;
            //kod som attackerar
            //googla!!!! rotationer i unity �r dj�vulen
            GameObject item = Instantiate(throwableItem, enemyPosition, Quaternion.identity);
            item.GetComponent<Rigidbody2D>().AddForce(attackForce * 300);
        }
        //deltaTime = tiden sen f�rra framen
        lastAttack -= Time.deltaTime;
    }


    private void Start()
    {
        base.Start();
        gameObjectPlayer = GameObject.Find("Player");
    }


}
