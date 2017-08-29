using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    
    // Movement keys (customizable in Inspector)
    [SerializeField] private KeyCode upKey;
    [SerializeField] private KeyCode downKey;
    [SerializeField] private KeyCode rightKey;
    [SerializeField] private KeyCode leftKey;
    [SerializeField] private float speed = 16;
    [SerializeField] private GameObject wallPrefab;

    private Collider2D wall;
    private Vector2 lastWallEnd;
    private Vector3 direction;
    private Rigidbody2D rgBody;
    private GameObject g;
    private GameObject dynamicObject;
    private float dist;
    private bool isMoving;
    
    //----- API methods------
    // Use this for initialization
    void Start()
    {
        rgBody = this.GetComponent<Rigidbody2D>();
        dynamicObject = GameObject.FindGameObjectWithTag("DynamicObject");
        isMoving = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameControl.instance.GameControlState != GameControl.GameState.playing)
        {
            rgBody.velocity = Vector2.zero;
           
        }
        else
        {
            // Check for key presses
            if (Input.GetKeyDown(upKey))
            {
                rgBody.velocity = Vector2.up * speed;
                SpawnWall();
            }
            else if (Input.GetKeyDown(downKey))
            {
                rgBody.velocity = -Vector2.up * speed;
                SpawnWall();
            }
            else if (Input.GetKeyDown(rightKey))
            {
                rgBody.velocity = Vector2.right * speed;
                SpawnWall();
            }
            else if (Input.GetKeyDown(leftKey))
            {
                rgBody.velocity = -Vector2.right * speed;
                SpawnWall();
            }
            FitColliderBetween(wall, lastWallEnd, transform.position);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col !=wall)
        {
            Debug.Log("Player lost: " + name);
            Destroy(gameObject);
            GameControl.instance.Restart();
        }
    }
    //-----Custom methods-------
    //Spawns the wall, since only it's working with the player's movement, it's in this class.
    private void SpawnWall()
    {
        isMoving = true;
        lastWallEnd = transform.position;
        g = Instantiate(wallPrefab, transform.position, Quaternion.identity);
        wall = g.GetComponent<Collider2D>();
        g.transform.SetParent(dynamicObject.transform);
    }

    //Converts the colliders into Tron-traile like, fitting them.
    void FitColliderBetween(Collider2D co, Vector2 a, Vector2 b)
    {
        if (isMoving)
        {
            co.transform.position = a + (b - a) * 0.5f;
            dist = Vector2.Distance(a, b);

            if (a.x != b.x)
            {
                co.transform.localScale = new Vector2(dist + 1, 1);
            }
            else
            {
                co.transform.localScale = new Vector2(1, dist + 1);
            }
        }
        
    }
}
