using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Spawn_Retangle
{
    public float point_x1, point_y1, point_x2, point_y2;

    public Spawn_Retangle(float point_x1, float point_y1, float point_x2, float point_y2)
    {
        this.point_x1 = point_x1;
        this.point_y1 = point_y1;
        this.point_x2 = point_x2;
        this.point_y2 = point_y2;

    }
}

public class GameHandle : MonoBehaviour {
    //Enemies
    public Transform En_Snake;
    public Transform En_Owl;
    public Transform En_Bull;

    //Coletables
    public Transform Coletable;

    //Canvas
    public Image lose_img;
    public Image win_img;

    private Transform player;
    private string game_condition;

    private List<Transform> objects_list;
    private float timer;
    private AudioSource[] sounds;

    // Use this for initialization
    void Awake()
    {
        sounds = GetComponents<AudioSource>();

        player = GameObject.FindGameObjectWithTag("Player").transform;
        lose_img.enabled = false;
        win_img.enabled = false;
        timer = 0;

    }

    void Start () {
        int max_coins = player.gameObject.GetComponent<PlayerScript>().max_coins;

        objects_list = new List<Transform>();

        List<Spawn_Retangle> points_colectables = new List<Spawn_Retangle>();
        GenerateSpawingPoints(ref points_colectables);

        Debug.Log(max_coins);

        Create_Object(Coletable, max_coins, points_colectables, ref objects_list);

        Create_Object(En_Snake, 5, points_colectables, ref objects_list);
        Create_Object(En_Bull, 3, points_colectables, ref objects_list);
        Create_Object(En_Owl, 1, points_colectables, ref objects_list);

        //Debug.Log(objects_list.Count);
        //Destroy(objects_list[objects_list.Count-1].gameObject);
        //Debug.Log(objects_list[objects_list.Count-1]);


    }

    // Update is called once per frame
    void Update()
    {
        game_condition = player.gameObject.GetComponent<PlayerScript>().condition;
        if (game_condition == "Game_Lose" && lose_img.enabled == false)
        {
            lose_img.enabled = true;
            sounds[0].Stop();
            sounds[2].Play();
        }

        if (game_condition == "Game_Win" && win_img.enabled == false) {
            win_img.enabled = true;
            sounds[0].Stop();
            sounds[1].Play(0);
        }

        if (game_condition != "Running") timer = timer + Time.deltaTime;
        if (timer > 9) SceneManager.LoadScene("Menu");
    }

    void Create_Object(Transform prefab, int number_of_instances, List<Spawn_Retangle> points_list, ref List<Transform> objects_list)
    {
        Transform instance;

        for (int count = 0; count < number_of_instances; count++)
        {
            int index = Random.Range(0, points_list.Count);
            Spawn_Retangle spawn = points_list[index];
            Vector3 position = new Vector3(Random.Range(spawn.point_x1 - 46.8f, spawn.point_x2 - 46.8f), Random.Range(spawn.point_y1 + 19.2f, spawn.point_y2 + 19.2f), 0);
            instance = Instantiate(prefab, position, Quaternion.identity);
            objects_list.Add(instance);
        }
    }

    void GenerateSpawingPoints(ref List<Spawn_Retangle> points_colectables)
    {
        //Left Side
        points_colectables.Add(new Spawn_Retangle(-31.7f, 0.3f, -13.7f, 0.3f));
        points_colectables.Add(new Spawn_Retangle(-31.7f, -1.7f, -30.7f, -19.7f));
        points_colectables.Add(new Spawn_Retangle(-23.7f, -2.7f, -19.7f, -4.7f));
        points_colectables.Add(new Spawn_Retangle(-28.7f, -14.7f, -17.7f, -14.7f));
        points_colectables.Add(new Spawn_Retangle(-18.7f, -8.7f, -17.7f, -12.7f));

        points_colectables.Add(new Spawn_Retangle(-31.7f, -24.7f, -14.7f, -25.7f));
        points_colectables.Add(new Spawn_Retangle(-25.7f, -27.7f, -25.7f, -31.7f));
        points_colectables.Add(new Spawn_Retangle(-31.7f, -30.7f, -30.7f, -36.7f));
        points_colectables.Add(new Spawn_Retangle(-25.7f, -36.7f, -25.7f, -40.7f));

        points_colectables.Add(new Spawn_Retangle(-21.7f, -35.7f, -21.7f, -40.7f));
        points_colectables.Add(new Spawn_Retangle(-19.7f, -38.7f, -13.7f, -38.7f));

        //Right Side
        points_colectables.Add(new Spawn_Retangle(-2.7f, 0.3f, 15.3f, 0.3f));
        points_colectables.Add(new Spawn_Retangle(14.3f, -1.7f, 15.3f, -19.7f));
        points_colectables.Add(new Spawn_Retangle(4.3f, -1.7f, 7.3f, -4.7f));
        points_colectables.Add(new Spawn_Retangle(1.3f, -14.7f, 12.3f, -14.7f));
        points_colectables.Add(new Spawn_Retangle(1.3f, -8.7f, 2.3f, -12.7f));

        points_colectables.Add(new Spawn_Retangle(-1.7f, -24.7f, 15.3f, -25.7f));
        points_colectables.Add(new Spawn_Retangle(9.3f, -27.7f, 9.3f, -31.7f));
        points_colectables.Add(new Spawn_Retangle(14.3f, -30.7f, 15.3f, -36.7f));
        points_colectables.Add(new Spawn_Retangle(9.3f, -36.7f, 10.3f, -40.7f));

        points_colectables.Add(new Spawn_Retangle(5.3f, -35.7f, 5.3f, -40.7f));
        points_colectables.Add(new Spawn_Retangle(-2.7f, -38.7f, 3.3f, -38.7f));

        //Mid
        points_colectables.Add(new Spawn_Retangle(-9.7f, 5.3f, -6.7f, 5.3f));
        points_colectables.Add(new Spawn_Retangle(-9.7f, -0.7f, -6.7f, -1.7f));
        points_colectables.Add(new Spawn_Retangle(-28.7f, -6.7f, 13.3f, -6.7f));
        points_colectables.Add(new Spawn_Retangle(-13.7f, -11.7f, -2.7f, -11.7f));
        points_colectables.Add(new Spawn_Retangle(-8.7f, -21.7f, -7.7f, -31.7f));

        points_colectables.Add(new Spawn_Retangle(-28.7f, -19.7f, 12.3f, -19.7f));
        points_colectables.Add(new Spawn_Retangle(-21.7f, -33.7f, 5.3f, -33.7f));
        points_colectables.Add(new Spawn_Retangle(-13.7f, -40.7f, -2.7f, -40.7f));

    }
}
