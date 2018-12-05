using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Warp_Move : MonoBehaviour {
    public GameObject compare_point;

   

    // Use this for initialization
    void Start () {
        
        }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D Player)
    {

        if (Player.gameObject.tag == "Player" || Player.gameObject.tag == "Enemy")
        {
            if (Player.gameObject.transform.position.x - compare_point.transform.position.x > 0)
            {
                Player.gameObject.transform.position = new Vector3(-79, Player.gameObject.transform.position.y, 0);
            }
            else
            {
                Player.gameObject.transform.position = new Vector3(-31, Player.gameObject.transform.position.y, 0);
            }
        }
    }

}
