using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{

    public float drawTime;
    public float currentTime = 10f;
    public int randomNumber;
    public bool? playerWins;
    private UI _ui;
    public bool roundEnd = false;

    public GameObject tryAgainBtn;
    public GameObject nextLevelBtn;

    private AudioSource _audioSource;

    private void Awake()
    {
        Debug.Log("Awake called!");
        switch (SceneManager.GetActiveScene().name)
        {
            case ("Level1"):
                drawTime = 1f;
                break;
            case ("Level2"):
                drawTime = 0.85f;
                break;
            case ("Level3"):
                drawTime = 0.7f;
                break;
            case ("Level4"):
                drawTime = 0.65f;
                break;
            case ("Level5"):
                drawTime = 0.5f;
                break;
            case ("Level6"):
                drawTime = 0.45f;
                break;
            case ("Level7"):
                drawTime = 0.35f;
                break;
            case ("Level8"):
                drawTime = 0.30f;
                break;
            case ("Level9"):
                drawTime = 0.29f;
                break;
            case ("Level10"):
                drawTime = 0.27f;
                break;

        }
    }
    void Start()
    {
        Debug.Log("Start called!");
        randomNumber = Random.Range(1, 7);
        _ui = gameObject.GetComponent<UI>();
        _audioSource = gameObject.GetComponent<AudioSource>();
        nextLevelBtn.GetComponentInChildren<Button>().onClick.AddListener(NextLevel);
        tryAgainBtn.GetComponentInChildren<Button>().onClick.AddListener(RetryLevel);
    }

    void Update()
    {
        Debug.Log("ROUND ENDED => " + roundEnd);
        if (!roundEnd)
        {
            if (playerWins == false)
            {
                Debug.Log("Enemy wins!");

                _ui.ShowResultWinText(false);
                var clip = Resources.Load<AudioClip>("Sounds/lose");
                StartCoroutine(PlayResultSound(clip));
                roundEnd = true;
                tryAgainBtn.SetActive(true);
            }
            else if (playerWins == true)
            {
                Debug.Log("Player wins!");
                _ui.ShowResultWinText(true);
                var clip = Resources.Load<AudioClip>("Sounds/win");
                StartCoroutine(PlayResultSound(clip));
                roundEnd = true;
                nextLevelBtn.SetActive(true);
                // Button[] buttons = GameObject.FindGameObjectWithTag("Canvas").GetComponentsInChildren<Button>();
                // for (int i = 0; i < buttons.Length; i++)
                // {
                //     Debug.Log(buttons[i].name);
                // }
                // SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }



    }

    void StartCountDownTimer()
    {

        Debug.Log("Random number is => " + randomNumber);
        currentTime = currentTime - 1 * Time.deltaTime;
        if (Mathf.Abs(currentTime) >= randomNumber)
        {
            gameObject.GetComponent<UI>().ShowDrawText();
            Enemy enemyGameObject = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Enemy>();
            enemyGameObject.Invoke("SetEnemyDraw", drawTime);
        }
    }

    IEnumerator PlayResultSound(AudioClip clip)
    {
        yield return new WaitForSeconds(0.5f);
        _audioSource.PlayOneShot(clip);
    }

    private void NextLevel()
    {
        Debug.Log("Clicked!");
        string currentSceneName = SceneManager.GetActiveScene().name;
        int sceneNumber = int.Parse(currentSceneName.Replace("Level", ""));
        int newSceneNumber = sceneNumber + 1;
        string newSceneName = "Level" + newSceneNumber.ToString();
        SceneManager.LoadScene(newSceneName);


    }

    private void RetryLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
