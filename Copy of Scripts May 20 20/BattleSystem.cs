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
    public Text skillZeroText;
    public Text skillOneText;
    public Text skillTwoText;
    public Text skillThreeText;
    

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
        skillZeroText.text = playerUnit.currentSkills[0].skillName;
        skillOneText.text = playerUnit.currentSkills[1].skillName;
        skillTwoText.text = playerUnit.currentSkills[2].skillName;
        skillThreeText.text = playerUnit.currentSkills[3].skillName;

        //sets the player and enemy HUDs with their stats
        playerHUD.SetHUD(playerUnit);
        enemyHUD.SetHUD(enemyUnit);

        yield return new WaitForSeconds(2f);

        //changes game state to the player's turn
        if (playerUnit.speed > enemyUnit.speed)
        {
            battleState = BattleState.PlayerTurn;
            PlayerTurn();
        } else
        {
            battleState = BattleState.EnemyTurn;
            StartCoroutine(EnemyTurn());
        }
    }

    //SKILL BUTTON ACTIONS
    IEnumerator Attack(int skillNumber)
    {
        //calculating damage here
        int damage = ((playerUnit.attack) + (playerUnit.currentSkills[skillNumber].skillPower)) - enemyUnit.defense;
        bool isDead = enemyUnit.TakeDamage(damage); //damage enemy - return T/F is dead

        enemyHUD.SetHP(enemyUnit.currentHP); //sets enemy's HP level in HUD
        //display successful hit, and damage
        textBox.text = playerUnit.currentSkills[skillNumber].skillName + " hits for " + damage + " damage!"; 

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
        //displays the name of attack that enemy uses
        textBox.text = enemyUnit.unitName + " uses " + enemyUnit.currentSkills[0].skillName + "!";
        yield return new WaitForSeconds(1f);

        //calculating damage here
        int damage = ((enemyUnit.attack) + (enemyUnit.currentSkills[0].skillPower)) - playerUnit.defense;
        bool isDead = playerUnit.TakeDamage(damage); //damage player - return T/F is dead

        playerHUD.SetHP(playerUnit.currentHP); //sets player's HP level in HUD
        //display successful hit, and damage
        textBox.text = enemyUnit.currentSkills[0].skillName + " hits for " + damage + " damage!"; 

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

    public void OnSkillButtonZero()
    {
        if (battleState != BattleState.PlayerTurn)
            return;

        StartCoroutine(Attack(0));
    }

    public void OnSkillButtonOne()
    {
        if (battleState != BattleState.PlayerTurn)
            return;

        StartCoroutine(Attack(1));
    }

    public void OnSkillButtonTwo()
    {
        if (battleState != BattleState.PlayerTurn)
            return;

        StartCoroutine(Attack(2));
    }

    public void OnSkillButtonThree()
    {
        if (battleState != BattleState.PlayerTurn)
            return;

        StartCoroutine(Attack(3));
    }
}
