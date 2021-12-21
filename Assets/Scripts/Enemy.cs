using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public GameObject gm;
    private GameManager _gameManager;
    private float _drawTime;
    public bool hasDrawn = false;
    public bool enemyDrawReady = false;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        _gameManager = gm.GetComponent<GameManager>();
        _drawTime = _gameManager.drawTime;
        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Waiting here...");
        if (enemyDrawReady)
        {
            if (!hasDrawn) {
            Debug.Log("ENEMY DRAW => " + _drawTime);
            anim.SetTrigger("weaponDraw");
            _gameManager.playerWins = false;
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.GetComponent<Animator>().SetTrigger("death");
            hasDrawn = true;
            } else {
                Invoke("ResetTrigger",0.5f);
                
            }

        }
    }

        void SetEnemyDraw() {
            Debug.Log("Here is player wins => " + _gameManager.playerWins);
        if (_gameManager.playerWins == null || _gameManager.playerWins == false) {
            
                enemyDrawReady = true;
        }

    }

    void ResetTrigger(){
    anim.ResetTrigger("weaponDraw");
    }

}
