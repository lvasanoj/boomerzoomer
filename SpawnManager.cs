using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnManager : MonoBehaviour
{
    // Spawns players 
    // Controls timers
    [Header("Timers")]
    [SerializeField] private float _timer;
    [SerializeField] private float _timeLeft = 30f;
    public Text timerText;
    [Space]
    [SerializeField] private int _currentCharacterAge; //0 = old, 1 = middle, 2 = baby
    [SerializeField] private PlayerController _playerController;
    [Header("Player list")]
    [SerializeField] private GameObject[] _activePlayerList;
    public GameObject currentActivePlayer;

    private MenuScript _menuScript;


    void Start()
    {
        _playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        _activePlayerList = GameObject.FindGameObjectsWithTag("Player");
        currentActivePlayer = _activePlayerList[_currentCharacterAge];
        _menuScript = GetComponent<MenuScript>();
    }

    void Update()
    {
        if (_menuScript.startGame == true)
        {
            _timer = Time.deltaTime;
            _timeLeft -= _timer;
            //Debug.Log(_timeLeft);
            timerText.text = _timeLeft.ToString("#.##");

            if (_timeLeft < 0)
            {
                if (_playerController != null)
                    _playerController.PlayerDeath();
            }
        }
    }
        

    public void ChangePlayer(int _currentCharacterAge)
    {
        if(_playerController != null &&currentActivePlayer != null)
        {
            switch (_currentCharacterAge)
            {
                case 0:

                    //Debug.Log("Old");
                    // Switch current player to old
                    _currentCharacterAge++;
                    currentActivePlayer.GetComponent<AudioScript>().enabled = false;
                    currentActivePlayer.GetComponent<PlayerController>().enabled = false;
                    currentActivePlayer = _activePlayerList[_currentCharacterAge];
                    currentActivePlayer.GetComponent<PlayerController>().enabled = true;
                    currentActivePlayer.GetComponent<AudioScript>().enabled = true;
                    _timeLeft = 5;
                    currentActivePlayer.GetComponent<PlayerController>().ChangeJumpHeight(5);
                    break;

                case 1:
                    //Debug.Log("Middle dude");
                    _currentCharacterAge++;
                    currentActivePlayer.GetComponent<PlayerController>().enabled = false;
                    currentActivePlayer.GetComponent<AudioScript>().enabled = false;
                    currentActivePlayer = _activePlayerList[_currentCharacterAge];
                    currentActivePlayer.GetComponent<PlayerController>().enabled = true;
                    currentActivePlayer.GetComponent<AudioScript>().enabled = true;
                    _timeLeft = 10;
                    currentActivePlayer.GetComponent<PlayerController>().ChangeJumpHeight(3);
                    // Switch current player to middle
                    break;

                case 2:
                    //Debug.Log("Baby");
                    _menuScript.gameOverText.gameObject.SetActive(true);
                    timerText.gameObject.SetActive(false);
                    break;
            }
        }
    }

    public int ReturnPlayerArray()
    {
        return _activePlayerList.Length;
    }
}
