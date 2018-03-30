using Assets.Scripts.Helpers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float speed = 0.25f;
    public float jumpForce = 750.0f;
    //public bool grounded = true;
    public int maxJumpsAvailable = 2;
    public int jumpsAvailable = 2;

    private Rigidbody2D _body;

	// Use this for initialization
	void Start () {
        _body = GetComponent<Rigidbody2D>();
        ObjectChecker.CheckNullity(_body, "RigidBody2D not found for Player");
    }

    // Update is called once per frame
    void Update () {
        ManageInputs();
	}

    private void ManageInputs()
    {
        // Manage Left/Right
        float dirX = 0.0f;
        if (Input.GetKey(KeyCode.RightArrow))
        {
            dirX = 1.0f;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            dirX = -1.0f;
        }

        Vector3 pos = transform.position;
        float moveX = dirX * speed;
        transform.position = new Vector3(pos.x + moveX, pos.y, pos.z);

        ManageJump();
    }

    private void ManageJump()
    {
        // Manage jump
        if (Input.GetKeyDown(KeyCode.Space) && jumpsAvailable > 0)
        {
            _body.AddForce(new Vector2(0.0f, jumpForce));
            //grounded = false;
            jumpsAvailable--;
        }
    }

    public void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("Collision");
        //grounded = true;
        jumpsAvailable = maxJumpsAvailable;
    }
}
