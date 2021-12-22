using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public float drawTime;
    public float currentTime = 10f;
    public int randomNumber;
    public bool? playerWins;
    private UI _ui;
    public bool roundEnd = false;

    private AudioSource _audioSource;

    private void Awake() {
        switch (SceneManager.GetActiveScene().name)
        {
            case ("Level1"):
                drawTime = 2f;
            break;
        }
    }
    void Start()
    {
       randomNumber = Random.Range(1,10);
       _ui = gameObject.GetComponent<UI>();
       _audioSource = gameObject.GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!roundEnd) {
            if (playerWins == false) {
                Debug.Log("Enemy wins!");
                // SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                _ui.ShowResultWinText(false);
                var clip =  Resources.Load<AudioClip>("Sounds/lose");
                StartCoroutine(PlayResultSound(clip));
                roundEnd = true;
            } else if (playerWins == true) {
                Debug.Log("Player wins!");
                _ui.ShowResultWinText(true);
                var clip =  Resources.Load<AudioClip>("Sounds/win");
                StartCoroutine(PlayResultSound(clip));
                roundEnd = true;
                
                // SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }

    
       
    }

    void StartCountDownTimer() {
        
        Debug.Log("Random number is => " + randomNumber);
        currentTime = currentTime - 1 * Time.deltaTime;
        if (Mathf.Abs(currentTime) >= randomNumber) {
            gameObject.GetComponent<UI>().ShowDrawText();
            Enemy enemyGameObject = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Enemy>();
            enemyGameObject.Invoke("SetEnemyDraw", drawTime);
        }
    }

    IEnumerator PlayResultSound(AudioClip clip) {
        yield return new WaitForSeconds(0.5f);
        _audioSource.PlayOneShot(clip);
    }
    
}
