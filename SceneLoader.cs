using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public string sceneToLoad;
    public AudioClip victoryMusic;
    private AudioSource _audioSource;

    void Start()
    {
        _audioSource = GameObject.Find("MusicManager").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.gameObject.name == "PlayerBaby"){
            Debug.Log("coroutining.");
            StartCoroutine(SwitchSceneRoutine());
        }
    }

    IEnumerator SwitchSceneRoutine()
    {
        if (_audioSource != null)
            _audioSource.PlayOneShot(victoryMusic);

        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(sceneToLoad);
    }


}
