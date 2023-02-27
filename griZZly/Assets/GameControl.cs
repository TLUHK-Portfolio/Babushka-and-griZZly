using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public enum  BattleState  { START, PLAYERTURN, ENEMYTURN, WON, LOST}

public class GameControl : MonoBehaviour {

    [SerializeField] public GameObject Live1;
    [SerializeField] public GameObject Live2;
    [SerializeField] public GameObject Live3;
    public BattleState state;
    private Unit enemyUnit;
    private Unit playerUnit;
    public HealthScript healthScript;
    public GameObject playerPrefab;
    public GameObject enemyPrefab;
    public Text whosTurnText;
    // Start is called before the first frame update
    void Start() {
        state = BattleState.START;
        enemyUnit = GameObject.Find("Enemy").GetComponent<Unit>();
        playerUnit = GameObject.Find("Player").GetComponent<Unit>(); 

        Live1.gameObject.SetActive(true);
        Live2.gameObject.SetActive(true);
        Live3.gameObject.SetActive(true);

        StartCoroutine(SetupLevelOne());
    }

    IEnumerator SetupLevelOne()
    {
        enemyUnit.health = enemyUnit.maxHealth;
        playerUnit.health = playerUnit.maxHealth;

        yield return new WaitForSeconds(1);

        PlayerTurn();
    }

    public void PlayerTurn()
    {
        state = BattleState.PLAYERTURN;
        whosTurnText.text = "Player's turn";
    }

    public void EnemyTurn()
    {
        Debug.Log("enemy turn");
        state = BattleState.ENEMYTURN;
        whosTurnText.text = "Enemy's turn";
    }

    // Update is called once per frame
    void Update() {
        switch (ThrowableScript.Lives) {
            case 3:
                Live1.gameObject.SetActive(true);
                Live2.gameObject.SetActive(true);
                Live3.gameObject.SetActive(true);
                break;
            case 2: 
                Live1.gameObject.SetActive(true);
                Live2.gameObject.SetActive(true);
                Live3.gameObject.SetActive(false);
                break;
            case 1:
                Live1.gameObject.SetActive(true);
                Live2.gameObject.SetActive(false);
                Live3.gameObject.SetActive(false);
                break;
            case 0:
                Live1.gameObject.SetActive(false);
                Live2.gameObject.SetActive(false);
                Live3.gameObject.SetActive(false);
                break;
        }
    }
}