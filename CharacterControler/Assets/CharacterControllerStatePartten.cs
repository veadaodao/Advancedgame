using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerStatePartten : MonoBehaviour
{
    enum PLAYER_STATE {S_WALK,S_IDLE,S_RUN,S_JUMP};
    PLAYER_STATE state;
    Animator anim;
    public Rigidbody PlayerRB;
    // Start is called before the first frame update
    void Start()
    {
        LinkedList list = new LinkedList();
        list.Add("the");
        list.Add("the");
        list.Add("the");
        list.Add("the");
        list.Add("the");
        list.Add("the");


        state = PLAYER_STATE.S_IDLE;
        anim = GetComponent<Animator>();
        PlayerRB = GetComponent<Rigidbody>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (state == PLAYER_STATE.S_WALK)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                state = PLAYER_STATE.S_JUMP;
                anim.SetTrigger("jump");
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case PLAYER_STATE.S_IDLE:
                if(Input.GetKeyDown(KeyCode.W))
                {
                    state = PLAYER_STATE.S_WALK;
                    anim.SetTrigger("walk");
                }
                break;

            case PLAYER_STATE.S_WALK:
                if (Input.GetKeyUp(KeyCode.W))
                {
                    state = PLAYER_STATE.S_IDLE;
                    anim.SetTrigger("stop");
                }
                if (Input.GetKeyDown(KeyCode.LeftShift))
                {
                    state = PLAYER_STATE.S_RUN;
                    anim.SetTrigger("run");
                }
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    state = PLAYER_STATE.S_JUMP;
                    anim.SetTrigger("jump");
                    PlayerRB.AddForce(0, 300f, 0);
                }
                break;

            case PLAYER_STATE.S_RUN:
                if (Input.GetKeyUp(KeyCode.LeftShift))
                {
                    state = PLAYER_STATE.S_WALK;
                    anim.SetTrigger("walk");
                }
                if (Input.GetKeyUp(KeyCode.W))
                {
                    state = PLAYER_STATE.S_IDLE;
                    anim.SetTrigger("stop");
                }
                break;

            case PLAYER_STATE.S_JUMP:
                if (Input.GetKeyUp(KeyCode.Space))
                {
                    state = PLAYER_STATE.S_IDLE;
                    anim.SetTrigger("stop");
                    if (Input.GetKeyDown(KeyCode.W))
                    {
                        state = PLAYER_STATE.S_WALK;
                        anim.SetTrigger("walk");
                    }
                }

                break;

        }
    }
}
