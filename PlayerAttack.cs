using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

    public float Charged_Value;
    private float Charge;
	// Use this for initialization
	void Start () {
        Charge = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Fire1"))
        {
            
        }
	}
}
