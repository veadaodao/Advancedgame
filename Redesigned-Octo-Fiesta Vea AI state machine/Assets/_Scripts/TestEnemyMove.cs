using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TestEnemyMove : MonoBehaviour
{
    public float lookRadius = 10f;

    private Transform target;
    public Transform home;
    NavMeshAgent agent;

    public int health = 3;
    public AudioManager audioManager;
    RaycastHit hit;

    Vector3 rayDirection;

    public GameObject deathBubbles;

    enum ENEMY_STATE { S_CHASING, S_IDLE };
    ENEMY_STATE state;
    Animator anim;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        agent = GetComponent<NavMeshAgent>();
        state = ENEMY_STATE.S_IDLE;
        anim = GetComponent<Animator>();
    }


    void Update()
    {
        if(health <= 0)
        {
            Debug.Log("I died");
            audioManager.updateAudio("boom");
            GameObject bubbles = Instantiate(deathBubbles, transform.position, Quaternion.identity);
            Destroy(bubbles, 2);
            gameObject.SetActive(false);
        }

        float distance = Vector3.Distance(target.position, transform.position);


        switch (state)
        {
            case ENEMY_STATE.S_IDLE:
                agent.SetDestination(home.position);
                FaceTarget();
                anim.SetTrigger("idle");

                if (distance <= lookRadius)
                {
                    state = ENEMY_STATE.S_CHASING;

                }

                break;

            case ENEMY_STATE.S_CHASING:

                rayDirection = (target.transform.position + new Vector3(0, 1, 0)) - (transform.position + new Vector3(0, 1, 0));
                anim.SetTrigger("chasing");
                bool raycastdown = Physics.Raycast((transform.position + new Vector3(0, 1, 0)), rayDirection, out hit);
                Debug.DrawLine(new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z), 
                    new Vector3(target.transform.position.x, transform.position.y + 1.5f, target.transform.position.z));
                if (raycastdown && hit.transform.name.Equals("OctopusPlayer Variant"))
                {
                    agent.SetDestination(target.position);
                    audioManager.updateAudio("attack");

                    if (distance <= agent.stoppingDistance)
                    {
                        //Attack target -- once script is written, paste here.
                        FaceTarget();
                    }
                }
                if (distance > lookRadius)
                {
                    state = ENEMY_STATE.S_IDLE;
                }
                break;
        }


    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

}
