﻿using UnityEngine;
using Zenject;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    private string moveState = "walking";
    [SerializeField] private float Speed = 1.0f;
    //Serialized private fields flag a warning as of v2018.3. 
    //This pragma disables the warning in this one case.
    #pragma warning disable 0649

    private float ModifiedSpeed = 1.0f;
    private float DashSpeed = 5.0f;
    private float dashTimer = 0.1f;
    private float timer = 0.0f;
    private Vector3 MovementDirection; 
    private InputManager _inputManager;
    private Player _player;

    [Range(0, 1f)] [SerializeField] private float velocitySmoothing = 0.05f;
    [SerializeField] private float baseSpeed = 20f;

    private Rigidbody2D _rigidbody2D;
    private Vector3 _velocity = Vector3.zero;
    
    
    [Inject]
    private void Init(Player player, InputManager inputManager)
    {
        _inputManager = inputManager;
        _player = player;
    }

    private void Start()
    {
        //where you place the main character art
        var player_art = this.GetComponent<SpriteRenderer> ();
        var basic_char_sprite = Resources.Load<Sprite>("basic_char");
        player_art.sprite = basic_char_sprite;
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (_player.CanControl)
        {
            Move();
        }
    }

    private void Move()
    {
        if (moveState == "walking")
        {
            this.ModifiedSpeed = this.Speed;
            this.MovementDirection = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f);
            this.gameObject.transform.Translate(this.MovementDirection * Time.deltaTime * this.ModifiedSpeed);

            if (Input.GetKeyDown("space")) //dash
            {
                this.ModifiedSpeed = this.DashSpeed;
                moveState = "dashing";
            }
        }
        else if (moveState == "dashing")
        {
            this.ModifiedSpeed = this.DashSpeed;
            this.gameObject.transform.Translate(this.MovementDirection * Time.deltaTime * this.ModifiedSpeed);
            timer += Time.deltaTime;
            if (timer >= dashTimer)
            {
                moveState = "walking";
                timer = 0.0f; 
            }
        }

    }
}