using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//enum - one state active at any time
public enum BattleState { Start, PlayerTurn, EnemyTurn, Won, Lost }

public class BattleSystem : MonoBehaviour
{
    public BattleState battleState;

    //the player and enemy game objects to instantiate into the battle
    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    //locations where the player and enemy sprite will be during the battle
    public Transform playerBattleStation;
    public Transform enemyBattleStation;

    public Text textBox;

    Unit playerUnit;
    Unit enemyUnit;

    public BattleHUD playerHUD;
    public BattleHUD enemyHUD;

    // Start is called before the first frame update
    void Start()
    {
        battleState = BattleState.Start;
        StartCoroutine(SetUpBattle());
    }

    IEnumerator SetUpBattle()
    {
        //Creates the player in their location of the battle screen
        GameObject playerGameObject = Instantiate(playerPrefab, playerBattleStation);
        playerUnit = playerGameObject.GetComponent<Unit>();

        //creates the enemy in their location of the battle screen
        GameObject enemyGameObject = Instantiate(enemyPrefab, enemyBattleStation);
        enemyUnit = enemyGameObject.GetComponent<Unit>();

        //battle start message in the HUD
        textBox.text = "Battle versus " + enemyUnit.unitName + " begins!";

        //sets the player and enemy HUDs with their stats
        playerHUD.SetHUD(playerUnit);
        enemyHUD.SetHUD(enemyUnit);

        yield return new WaitForSeconds(2f);

        //changes game state to the player's turn
        battleState = BattleState.PlayerTurn;
        PlayerTurn();
    }

    IEnumerator PlayerAttack()
    {
        //calling the TakeDamage method in unit.cs - returns a bool true of false if enemy dead
        bool isDead = enemyUnit.TakeDamage(playerUnit.attack);

        //calls the SetHP method in BattleHUD to update enemy HP
        enemyHUD.SetHP(enemyUnit.currentHP);

        //display successful hit, and damage
        textBox.text = "Attack hits for " + playerUnit.attack + " damage!";
        yield return new WaitForSeconds(2f);

        if (isDead)
        {
            battleState = BattleState.Won;
            EndBattle();
        }
        else
        {
            battleState = BattleState.EnemyTurn;
            StartCoroutine(EnemyTurn());
        }
    }

    IEnumerator SkillTwo()
    {
        //calling the TakeDamage method in unit.cs - returns a bool true of false if enemy dead
        bool isDead = enemyUnit.TakeDamage(playerUnit.attack);

        //calls the SetHP method in BattleHUD to update enemy HP
        enemyHUD.SetHP(enemyUnit.currentHP);

        //display successful hit, and damage
        textBox.text = "Attack hits for " + playerUnit.attack + " damage!";
        yield return new WaitForSeconds(2f);

        if (isDead)
        {
            battleState = BattleState.Won;
            EndBattle();
        }
        else
        {
            battleState = BattleState.EnemyTurn;
            StartCoroutine(EnemyTurn());
        }
    }


    IEnumerator EnemyTurn()
    {
        textBox.text = enemyUnit.unitName + " attacks!";
        yield return new WaitForSeconds(1f);

        bool isDead = playerUnit.TakeDamage(enemyUnit.attack); //damage player - return T/F is dead
        playerHUD.SetHP(playerUnit.currentHP); //sets player's HP level in HUD

        yield return new WaitForSeconds(1f);

        if (isDead)
        {
            battleState = BattleState.Lost;
            EndBattle();
        }
        else
        {
            battleState = BattleState.PlayerTurn;
            PlayerTurn();
        }

    }

    //ending the battle
    void EndBattle()
    {
        if (battleState == BattleState.Won)
        {
            textBox.text = "You win!";
        }
        else if (battleState == BattleState.Lost)
        {

            textBox.text = "You were defeated.";
        }
    }

    void PlayerTurn()
    {
        textBox.text = "Choose an action";
    }

    public void OnAttackButton()
    {
        if (battleState != BattleState.PlayerTurn)
            return;

        StartCoroutine(PlayerAttack());
    }

    public void OnSkillButton()
    {
        if (battleState != BattleState.PlayerTurn)
            return;

        StartCoroutine(SkillTwo());
    }
}
