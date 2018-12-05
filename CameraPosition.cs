using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPosition : MonoBehaviour {

	private Transform player;       
	private Vector3 offset;


    void Awake()
    {
        // Set up the references.
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Use this for initialization
    void Start () {
		offset = transform.position - player.position;
	}

	// LateUpdate is called after Update each frame
	void LateUpdate () { 
		transform.position = player.position + offset;
	}
}