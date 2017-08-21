using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMove : MonoBehaviour {

    [SerializeField] private string horizontalAxisName = "Horizontal"; //Default values.
    [SerializeField] private string verticalAxisName = "Vertical";     //Default values.
    [SerializeField] private float speed = 16;
    [SerializeField] private GameObject wallPrefab;

    private Collider2D wall;
    private Vector2 lastWallEnd;
    private Vector3 direction;
    private Rigidbody2D rgBody;
    private GameObject g;
    private GameObject dynamicObject;
    private float v;
    private float h;
    private float dist;
    private bool isMoving;
    
    //----- API methods----
	// Use this for initialization
	void Start () {
        rgBody = this.GetComponent<Rigidbody2D>();
        isMoving = false;
        dynamicObject = GameObject.FindGameObjectWithTag("DynamicObject");
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        h = Input.GetAxisRaw(horizontalAxisName);
        v = Input.GetAxisRaw(verticalAxisName);
        
        //Validates 1 direction-only
        if (h!=0)
            v = 0;
        else
            h = 0;
        
        //If it's not moving don't spawn walls
        if (h != 0 || v != 0)
        {
            SpawnWall();
            Move(h, v);
            
        }
        //Fit the spawned and connect the colliders to be trail-like.
        FitColliderBetween(wall, lastWallEnd, transform.position);
    }

    //-----Custom methods-------
    //Simple movement and once it's used, that means the player is moving.
    private void Move(float h, float v)
    {
        //it started to move.
        isMoving = true;
        direction.x = h;
        direction.y = v;
        rgBody.velocity = direction * speed;
    }

    //Spawns the wall, since only it's working with the player's movement, it's in this class.
    public void SpawnWall()
    {
        if ((direction.x != h || direction.y != v)) //Only when directions is changed
        {
            lastWallEnd = transform.position;
            g = Instantiate(wallPrefab, transform.position, Quaternion.identity);
            wall = g.GetComponent<Collider2D>();
            g.transform.SetParent(dynamicObject.transform);
        }

    }
    
    //Converts the colliders into Tron-traile like, fitting them.
    public void FitColliderBetween(Collider2D co, Vector2 a, Vector2 b)
    {
        if (isMoving)
        {
            co.transform.position = a + (b - a) * 0.5f;
            dist = Vector2.Distance(a, b);

            if (a.x != b.x)
            {
                wall.transform.localScale = new Vector2(dist + 1, 1);
            }
            else
            {
                wall.transform.localScale = new Vector2(1, dist + 1);
            }
        }
    }
}
