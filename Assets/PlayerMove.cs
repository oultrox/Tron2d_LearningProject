using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

    [SerializeField] private float speed = 16;
    [SerializeField] private GameObject wallPrefab;
    private Collider2D wall;
    private Vector2 lastWallEnd;
    private Vector3 direction;
    private Rigidbody2D rgBody;
    private float v;
    private float h;
    private GameObject g;
    
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
        SpawnWall();
        Move(h, v);
        FitColliderBetween(wall, lastWallEnd, transform.position);
    }

    private void Move(float h, float v)
    {
        direction.x = h;
        direction.y = v;
        rgBody.velocity = direction * speed;
    }

    private void SpawnWall()
    {
        
        if ((direction.x != h || direction.y != v) && (h!=0 || v!=0))
        {
            lastWallEnd = transform.position;
            g = Instantiate(wallPrefab, transform.position, Quaternion.identity);
            wall = g.GetComponent<Collider2D>();
        }
        
    }

    void FitColliderBetween(Collider2D co, Vector2 a, Vector2 b)
    {
        co.transform.position = a + (b - a) * 0.5f;
        float dist = Vector2.Distance(a, b);
        if (a.x != b.x)
        {
            co.transform.localScale = new Vector2(dist + 1 , 1);
        }else
        {
            co.transform.localScale = new Vector2(1 , dist + 1);
        }
    }
}
