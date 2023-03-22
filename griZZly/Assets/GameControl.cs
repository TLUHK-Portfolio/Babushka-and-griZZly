using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public enum  BattleState  { START, PLAYERTURN, ENEMYTURN, WON, LOST }

public class GameControl : MonoBehaviour {
    public BattleState state;
    public GameObject bombPrefab;
    public GameObject molotovPrefab;
    private Unit enemyUnit;
    private Unit playerUnit;
    public GameObject playerPrefab;
    public GameObject enemyPrefab;
    public List<GameObject> throwablePrefabs = new List<GameObject>();
    public Text whosTurnText;

    void Awake()
    {
        throwablePrefabs.Add(bombPrefab);
        throwablePrefabs.Add(molotovPrefab);
    }

    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.START;
        enemyUnit = GameObject.Find("Enemy").GetComponent<Unit>();
        playerUnit = GameObject.Find("Player").GetComponent<Unit>(); 
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
        state = BattleState.ENEMYTURN;
        Debug.Log("Enemy turn!!");
        whosTurnText.text = "Enemy's turn";
    }

    // Update is called once per frame
    void Update() {
        if (enemyUnit.isDead) {
            state = BattleState.WON;
            whosTurnText.text = "You won!!";
        } else if (playerUnit.isDead) {
            state = BattleState.LOST;
            whosTurnText.text = "You lost.";
        }

        if (state == BattleState.ENEMYTURN)
        {
            enemyUnit.EnemyDoSomething();

            PlayerTurn();
        }
    }
}