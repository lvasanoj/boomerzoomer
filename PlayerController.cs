using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Player controller
    [Header("Player variables")]
    [SerializeField] private int _movementSpeed = 3;
    [SerializeField] private int _jumpHeight = 4;
    [SerializeField] private bool _canJump = true;
    [SerializeField] private Vector2 _spawnPos;
    Vector2 axisMovement;
    Vector2 direction;
    private Rigidbody2D _rb2d;

    [SerializeField] private bool facingRight = true;    // For flipping character
    private Animator anim;

    [SerializeField] private SpawnManager _spawnManager;
    private int _loops;
    [SerializeField] private MenuScript _menuScript;
    [SerializeField] private AudioScript _audioPlay;
    [SerializeField] private int _arrayLength;

    void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _spawnPos = new Vector2(-4.46f, -0.66f);
        transform.position = _spawnPos;
        anim = GetComponent<Animator>();
        _spawnManager = GameObject.Find("Canvas").GetComponent<SpawnManager>();
        _menuScript = GameObject.Find("Canvas").GetComponent<MenuScript>();
        _audioPlay = GameObject.Find("PlayerOld").GetComponent<AudioScript>();
        ChangeAudioPlay();
        _arrayLength = _spawnManager.ReturnPlayerArray();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
        if(_canJump == true && Input.GetKeyDown("space"))
        {
            PlayerJump();
        }

        if (transform.position.y <= -5)
        {
            PlayerDeath();
        }


    }

    void PlayerMovement()
    {
        axisMovement.x = Input.GetAxisRaw("Horizontal");

        if ((Input.GetKeyDown("a") || Input.GetKeyDown("left")) && facingRight == true)
        {
            anim.SetBool("isWalking", true);
            FlipCharacter();
            facingRight = false;
        }
        
        if ((Input.GetKeyDown("d") || Input.GetKeyDown("right")) && facingRight == false)
        {
            anim.SetBool("isWalking", true);
            FlipCharacter();
            facingRight = true;
        }
        else if ((Input.GetKeyDown("d") || Input.GetKeyDown("right")) && facingRight == true)
        {
            anim.SetBool("isWalking", true);
        }

        else if (!Input.anyKey)
        {
            anim.SetBool("isWalking", false);
           
        }

        direction = new Vector2(axisMovement.x, 0);
        transform.Translate(direction * _movementSpeed * Time.deltaTime);

    }
    
    void PlayerJump()
    {
        _rb2d.velocity = new Vector3(0, _jumpHeight, 0);
        _canJump = false;
        _audioPlay.PlayJumpSound();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("resetJump") || other.gameObject.CompareTag("Player"))
        {
            _canJump = true;
        }
    }

    public void PlayerDeath()
    {
        Debug.Log("Die");
        if (_spawnManager != null)
        {
            Rigidbody2D rigidbody = _spawnManager.currentActivePlayer.GetComponent<Rigidbody2D>();
            if (rigidbody != null)
                rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;

            anim.SetBool("isWalking", false);
            _audioPlay.PlayDeathSound();
            _menuScript.PauseGame();

            _spawnManager.ChangePlayer(_loops);
            if (_loops < 3)
            {
                _loops++;
                Debug.Log(_loops);
            }
        }
    }

    public void ChangeJumpHeight(int height)
    {
        _jumpHeight = height;
        Debug.Log("Jump changed");
    }

    void FlipCharacter()
    {

        Vector3 flipPlayer = transform.localScale;
        flipPlayer.x *= -1;
        transform.localScale = flipPlayer;
    }

    void ChangeAudioPlay()
    {
        if (gameObject.name == "PlayerMiddle")
        {
            _audioPlay = GameObject.Find("PlayerMiddle").GetComponent<AudioScript>();
        }
        if (gameObject.name == "PlayerBaby")
        {
            _audioPlay = GameObject.Find("PlayerBaby").GetComponent<AudioScript>();
        }

    }
}
