using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy_NewMobility : MonoBehaviour {

    public string enemy_name;
    public float base_speed;
    Transform player;


    private Rigidbody2D CharacterBody;
    private float aggro_time = 0.0f;
    private float move_timer = 0.0f;
    private float speed;


    private bool enemy_skill;
    private bool enemy_alert;
    private AudioSource[] sounds;

    void Awake()
    {
        // Set up the references.
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Use this for initialization
    void Start() {

        CharacterBody = GetComponent<Rigidbody2D>();
        speed = base_speed;
        enemy_skill = false;

        if (enemy_name == "Owl")
        {
            sounds = GetComponents<AudioSource>();
            enemy_alert = false;
        }
    }

    // Update is called once per frame
    void FixedUpdate() {
        float player_distance = Mathf.Sqrt(Mathf.Pow(player.transform.position.y - transform.position.y, 2) + Mathf.Pow(player.transform.position.x - transform.position.x,2));
        //Debug.Log(player_distance);

        switch (enemy_name) {
            case "Snake":
                if(player_distance < 5.0f) aggro_time = 5.0f;
                SnakeBehavior();
                break;
            case "Bull":
                if (player_distance < 6.0f && !enemy_skill) aggro_time = 5.0f;
                BullBehavior(4.0f);
                break;
            case "Owl":
                if (player_distance < 15.0f && enemy_alert) aggro_time = 10.0f;
                OwlBehavior();
                break;
            default:
                break; }
    }


//###########################################################################
    void SnakeBehavior()
    {
        if (aggro_time > 0.0f)
        {
            aggro_time = aggro_time - Time.deltaTime;
            MoveHuntBehavior();
        }
        else
        {
            move_timer = move_timer + Time.deltaTime;
            if (move_timer >= 1.5f)
            {
                transform.eulerAngles = new Vector3(0, 0, Random.Range(0, 360));
                move_timer = 0.0f;
            }
            CharacterBody.AddForce(gameObject.transform.up * speed);
        }
    }

    //###########################################################################
    void BullBehavior(float ram_time)
    {
        if (aggro_time > ram_time)
        {
            aggro_time = aggro_time - Time.deltaTime;
            FacePlayer();
            if (enemy_skill == false) enemy_skill = true;
            if (speed == base_speed) speed = base_speed*4.0f;
        }
        else if (aggro_time > 0.0f)
        {
            aggro_time = aggro_time - Time.deltaTime;
            CharacterBody.AddForce(gameObject.transform.up * speed);
        }
        else {
            if (speed > base_speed) speed = base_speed;
            if (enemy_skill == true) enemy_skill = false;

            move_timer = move_timer + Time.deltaTime;
            if (move_timer >= 1.8f)
            {
                transform.eulerAngles = new Vector3(0, 0, Random.Range(0, 360));
                move_timer = 0.0f;
            }
            CharacterBody.AddForce(gameObject.transform.up * speed);     
        }
    }

    //###########################################################################
    void OwlBehavior()
    {
        if (aggro_time > 0.0f)
        {
            if (enemy_alert == true) enemy_alert = false;
            aggro_time = aggro_time - Time.deltaTime;
            MoveHuntBehavior();
        }
        else
        {
            if (speed > base_speed) speed = base_speed;

            move_timer = move_timer + Time.deltaTime;
            if (move_timer >= 1.0f)
            {
                transform.eulerAngles = new Vector3(0, 0, Random.Range(0, 360));
                move_timer = 0.0f;
            }
            CharacterBody.AddForce(gameObject.transform.up * speed);

            if (aggro_time < -10.0f && enemy_alert == false)
            {
                sounds[0].Play(0);
                enemy_alert = true;
                speed = base_speed * 3.0f;
            }
            else
            {
                aggro_time = aggro_time - Time.deltaTime;
                if (enemy_alert == true) enemy_alert = false;
            }
        }
    }

    //###########################################################################
    void MoveHuntBehavior()
    {
        FacePlayer();

        CharacterBody.AddForce(gameObject.transform.up * speed);
    }

    void FacePlayer() {
        float ztrans = Mathf.Atan2((player.transform.position.y - transform.position.y), (player.transform.position.x - transform.position.x)) * Mathf.Rad2Deg - 90;
        transform.eulerAngles = new Vector3(0, 0, ztrans);
    }
}
