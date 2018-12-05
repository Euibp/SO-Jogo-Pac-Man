using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour {

    public float base_speed;
    public float base_stamina;

    public string condition;

    private Rigidbody2D CharacterBody;
    private float timer;

    private float speed;
    private float stamina;
    private int coins;
    public int max_coins;


    public Slider stamina_slider;
    public Text coins_text;

    AudioSource death_sound;

    void Start()
    {
        CharacterBody = GetComponent<Rigidbody2D>();
        death_sound = GetComponent<AudioSource>();

        condition = "Running";

        speed = base_speed;
        stamina = base_stamina;
        timer = 0;

        coins = 0;  
        DrawTextCoins();

        stamina_slider.maxValue = stamina;
    }

    void FixedUpdate () {
        if (condition != "Running") return;

        var mouseposition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Quaternion rot = Quaternion.LookRotation(transform.position-mouseposition,Vector3.forward);

        transform.rotation = rot;
        transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z);
        CharacterBody.angularVelocity = 0;


        if (Input.GetKeyDown(KeyCode.LeftShift)) {
            speed = base_speed * 2.0f;
        };
        timer = timer + Time.deltaTime;
        if (timer > 0.1)
        {
            //if (Input.GetKey(KeyCode.LeftShift) && stamina > 0 ) stamina = stamina - 5;
            if ( speed > base_speed && stamina > 0) stamina = stamina - 5;
            else if (stamina < 100) stamina = stamina + 3;
            timer = 0;
            stamina_slider.value = (float)stamina;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift) || stamina < 0) {
            speed = base_speed;
        };

        CharacterBody.AddForce(gameObject.transform.up * speed);

	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if(condition != "Running")return;

        if (other.gameObject.CompareTag("Coletable"))
        {
            other.gameObject.SetActive(false);
            Destroy(other.gameObject);

            coins++;
            if (coins == max_coins) condition = "Game_Win";
            DrawTextCoins();

        }

        if (other.gameObject.CompareTag("Enemy") && coins<max_coins) {
            death_sound.Play(0);
            condition = "Game_Lose";
            
            //Destroy(gameObject);
        }
    }


    void DrawTextCoins() {
        coins_text.text = "Coins: " + coins.ToString() + "/" + max_coins;
    }
}
