using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private Rigidbody2D rb;
    private Animator anim;
    public GameObject startLocation;
    public bool textShown = false;
    public GameObject gm;
    private GameManager _gameManager;
    private float _drawTime;
    public bool hasDrawn = false;
    private UI _ui;
    
    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        anim.SetBool("levelStart", true);
        _gameManager = gm.GetComponent<GameManager>();
        _drawTime = _gameManager.drawTime;
        _ui = gameObject.GetComponent<UI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            DrawWeapon();
        }
        if (transform.position.x >= -6f)
        {
            anim.SetBool("levelStart", false);
            if (!textShown)
            {
                _ui.ChangeText();
                textShown = true;
            }
            else
            {
                Invoke("ResetText", 5f);
                _gameManager.Invoke("StartCountDownTimer", 5f);

            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, startLocation.transform.position, 3f * Time.deltaTime);
        }
    }

    void ResetText()
    {
        _ui.ResetText();
    }

    void DrawWeapon()
    {
        if (!hasDrawn)
        {
            hasDrawn = true;
            // Player drew before the time
            if (Mathf.Abs(_gameManager.currentTime) <= _gameManager.randomNumber)
            {
                _ui.ShowJammedText();
            }
            else
            {
                 // Player drew after the time
                if (Mathf.Abs(_gameManager.currentTime) > _gameManager.randomNumber)
                {
                    Debug.Log("You drew on => " + _gameManager.currentTime);
                    anim.SetTrigger("weaponDraw");
                    _gameManager.playerWins = true;
                    GameObject enemy = GameObject.FindGameObjectWithTag("Enemy");
                    enemy.GetComponent<Animator>().SetTrigger("death");
                    // anim.SetBool("weaponDrawn", false);
                    // Shooting animation for enemy, death animation for player
                }
            }
        }
        else
        {
            // Debug.Log("Your weapon has jammed!");
            // Shooting animation for enemy
        }
    }
}
