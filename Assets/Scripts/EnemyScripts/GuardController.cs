﻿using UnityEngine;
using System.Collections;

public class GuardController : MonoBehaviour
{
    //Used to trigger walking animation
    bool _move;
    //Used to trigger firing animation
    bool _seePlayer;
    //Animation of the enemy
    public Animator animate;
    public GameObject player;
    public float movingSpeed;
    public int count = 0;
    float health = 10;
    // Use this for initialization
    void Start()
    {
        _move = false;
        _seePlayer = false;
        animate = this.gameObject.GetComponent<Animator>();
        animate.SetBool("seePlayer", _seePlayer);
        animate.SetBool("move", _move);
        gameObject.tag = "Enemy";
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -0.77f);
    }

    // Update is called once per frame
    void Update()
    {
        if (health > 0)
        {
            if ((player.transform.position.x - gameObject.transform.position.x < 4 && player.transform.position.x > gameObject.transform.position.x && movingSpeed < 0) || (gameObject.transform.position.x - player.transform.position.x < 4 && player.transform.position.x < gameObject.transform.position.x && movingSpeed > 0))
            {
                _move = false;
                _seePlayer = true;
                Attack();
                animate.SetBool("seePlayer", _seePlayer);
                animate.SetBool("move", _move);
            }
            else
            {

                _move = true;
                _seePlayer = false;
                Move();
                animate.SetBool("move", _move);
                animate.SetBool("seePlayer", _seePlayer);
            }
        }


    }
    //Will add the direction into a seperate method later
    void Move()
    {
        count++;
        //handles the direction of the enemy
        if (movingSpeed > 0)
        {
            this.gameObject.transform.eulerAngles = new Vector3(0, 0, 0);
            this.gameObject.transform.Translate(new Vector3(-movingSpeed, 0, 0));
        }
        else
        {
            this.gameObject.transform.eulerAngles = new Vector3(0, 180, 0);
            this.gameObject.transform.Translate(new Vector3(movingSpeed, 0, 0));
        }
        if (count >= 500)
        {
            movingSpeed *= -1;
            count = 0;
        }
    }
    void Attack()
    {
        if (movingSpeed > 0)
        {
            this.gameObject.transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else
        {
            this.gameObject.transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }
    public void TakeHit(float f)
    {
        health -= f;
        if (health <= 0)
        {
            animate.SetBool("dead", true);
        }
    }
}

