using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

    [SerializeField] private float speed = 16;
    private Vector3 direction;
    private Rigidbody2D rgBody;
    private float v;
    private float h;
    
    
	// Use this for initialization
	void Start () {
        rgBody = this.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        if (h!=0)
        {
            v = 0;
        }
        else
        {
            h = 0;
        }

        Move(h, v);
	}

    private void Move(float h, float v)
    {
        direction.x = h;
        direction.y = v;
        rgBody.velocity = direction * speed;
    }
}
