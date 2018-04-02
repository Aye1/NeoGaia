﻿using Assets.Scripts.Helpers;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float speed = 0.25f;
    public float jumpForce = 750.0f;
    //public bool grounded = true;
    public int maxJumpsAvailable = 1;
    public int jumpsAvailable = 1;

    private IInteractable _currentInteractable = null;

    private Rigidbody2D _body;
    private Player _player;

	// Use this for initialization
	void Start () {
        _body = GetComponent<Rigidbody2D>();
        _player = GetComponent<Player>();
        ObjectChecker.CheckNullity(_body, "RigidBody2D not found for Player");
        ObjectChecker.CheckNullity(_player, "Player not found");
    }

    // Update is called once per frame
    void Update () {
        ManageInputs();
        UpdateJumpsAvailable();
	}

    private void UpdateJumpsAvailable()
    {
        maxJumpsAvailable = _player.has10Jumps ? 10 : 1;
    }

    private void ManageInputs()
    {
        ManageMove();
        ManageJump();
        ManageInteraction();
    }

    private void ManageMove()
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

    private void ManageInteraction()
    {
        // Can interact
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Debug.Log("Interact button pushed");
            if (_currentInteractable != null)
            {
                Debug.Log("Interacting");
                _currentInteractable.OnInteract(_player);
            }
        }
    }

    public void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("Collision");
        //grounded = true;
        jumpsAvailable = maxJumpsAvailable;
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.GetComponent<IInteractable>() != null)
        {
            Debug.Log("Can interact");
            _currentInteractable = col.gameObject.GetComponent<IInteractable>();
        }
    }

    public void OnTriggerExit2D(Collider2D col)
    {
        IInteractable interactable = col.gameObject.GetComponent<IInteractable>();
        if (interactable != null && interactable == _currentInteractable)
        {
            Debug.Log("Can't interact anymore");
            _currentInteractable = null;
        }
    }
}
