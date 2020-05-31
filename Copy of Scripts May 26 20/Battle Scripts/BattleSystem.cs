using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//enum - one state active at any time
public enum BattleState { Start, PlayerTurn, EnemyTurn, Won, Lost }

public class BattleSystem : MonoBehaviour
{
    public BattleState battleState;

    public int randomSpawnNumber;
    public int randomEnemyLevel;
    public int damage;
    public bool isDead;

    //locations where the player and enemy sprite will be during the battle
    public Transform playerBattleStation;
    public Transform enemyBattleStation;

    public Text textBox;
    public Text skillZeroText;
    public Text skillOneText;
    public Text skillTwoText;
    public Text skillThreeText;
    

    PlayerUnit playerUnit;
    EnemyUnit1 enemyUnit;
    EnemyUnit1 enemyUnit2;
    EnemyUnit1 enemyUnit3;
    EnemyUnit1 enemyUnit4;
    EnemyUnit1 enemyUnit5;
    EnemyUnit1 enemyUnit6;

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
        //Finds the Party Manager, and sets the first child to Active
        GameObject playerGameObject = GameObject.Find("Party Manager");
        playerGameObject.transform.GetChild(0).gameObject.SetActive(true);
        playerUnit = playerGameObject.transform.GetChild(0).gameObject.GetComponent<PlayerUnit>(); //gets 'Unit" from first child

        //creates the enemy in their location of the battle screen
        SpawnEnemy();
        SetSkillButtons();

        //battle start message in the HUD
        textBox.text = "Battle versus " + enemyUnit.currentUnit.unitName + " begins!";

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

    void SpawnEnemy()
    {
        randomSpawnNumber = Random.Range(0, 4); //spawn random enemy
        GameObject enemyGameObject = GameObject.Find("Enemy Party Manager");
        enemyGameObject.transform.GetChild(0).gameObject.SetActive(true);
        enemyUnit = enemyGameObject.transform.GetChild(0).gameObject.GetComponent<EnemyUnit1>();

        randomEnemyLevel = Random.Range(1, 10); //random level assignment - could base this off area or character level?
        enemyUnit.currentLevel = randomEnemyLevel;
        enemyUnit.OnBattleStart(randomSpawnNumber);

    }

    //PLAYER ATTACKING ------------
    IEnumerator Attack(int skillNumber)
    {
        var skillUsed = playerUnit.currentSkills[skillNumber];
        float elementModifier = DamageCalculation.Instance.CalculateElemental(playerUnit.currentUnit.elementType,
            skillUsed.skillElement, enemyUnit.currentUnit.elementType);
        if (elementModifier == 0.5 || elementModifier == 0.75) //not very effective
            textBox.text = skillUsed.skillElement + " is less effective against " + enemyUnit.currentUnit.elementType + "!";
        if (elementModifier == 2 || elementModifier == 3) //super effective
            textBox.text = skillUsed.skillElement + " is extra effective against " + enemyUnit.currentUnit.elementType + "!";
        yield return new WaitForSeconds(2f);

        //Attack or Special attack type calculation
        if (skillUsed.skillAttackType == SkillsSO.AttackPowerType.Attack)
            damage = DamageCalculation.Instance.CalculateDamage(playerUnit.attack, skillUsed.skillPower,
                playerUnit.currentLevel, enemyUnit.defense, elementModifier);
        else
            damage = DamageCalculation.Instance.CalculateDamage(playerUnit.specialAttack, skillUsed.skillPower,
                playerUnit.currentLevel, enemyUnit.specialDefense, elementModifier);

        enemyUnit.currentHP -= damage; //enemy takes damage

        if (enemyUnit.currentHP <= 0)
        {
            enemyUnit.currentHP = 0;
            isDead = true;
        }

        enemyHUD.SetHP(enemyUnit.currentHP); //sets enemy's HP level in HUD
        //display successful hit, and damage
        textBox.text = playerUnit.currentSkills[skillNumber].skillName + " hits for " + damage + " damage!";
        
        yield return new WaitForSeconds(2f);

        //IF enemy is killed after this attack
        if (isDead)
        {
            textBox.text = "You took down " + enemyUnit.currentUnit.name + "!";
            yield return new WaitForSeconds(2f);
            textBox.text = "You gained " + enemyUnit.xpOnKill + " experience points!";
            yield return new WaitForSeconds(2f);
            playerUnit.currentXP += enemyUnit.xpOnKill; //give player unit XP
            if (playerUnit.currentXP >= playerUnit.nextLevelXPNeeded)
            {
                playerUnit.LevelUp();
                textBox.text = playerUnit.name + " gained a level!";
                yield return new WaitForSeconds(2f);
            }
            

            //IF no other enemies
            battleState = BattleState.Won;
            EndBattle();
        }
        else
        {
            battleState = BattleState.EnemyTurn;
            StartCoroutine(EnemyTurn());
        }
    }

    //ENEMY ATTACKING -----------------
    IEnumerator EnemyTurn()
    {
        var skillUsed = enemyUnit.currentSkills[0];
        textBox.text = enemyUnit.currentUnit.unitName + " uses " + skillUsed.skillName + "!"; //displays the name of attack that enemy uses
        yield return new WaitForSeconds(1f);

        float elementModifier = DamageCalculation.Instance.CalculateElemental(enemyUnit.currentUnit.elementType,
            skillUsed.skillElement, playerUnit.currentUnit.elementType);
        if (elementModifier == 0.5 || elementModifier == 0.75) //not very effective
            textBox.text = skillUsed.skillElement + " is less effective against " + playerUnit.currentUnit.elementType + "!";
        if (elementModifier == 2 || elementModifier == 3) //super effective
            textBox.text = skillUsed.skillElement + " is extra effective against " + playerUnit.currentUnit.elementType + "!";
        yield return new WaitForSeconds(2f);

        //Attack or Special attack type calculation
        if (skillUsed.skillAttackType == SkillsSO.AttackPowerType.Attack)
            damage = DamageCalculation.Instance.CalculateDamage(enemyUnit.attack, skillUsed.skillPower,
                enemyUnit.currentLevel, playerUnit.defense, elementModifier);
        else
            damage = DamageCalculation.Instance.CalculateDamage(enemyUnit.specialAttack, skillUsed.skillPower,
                enemyUnit.currentLevel, playerUnit.specialDefense, elementModifier);

        playerUnit.currentHP -= damage; //player takes damage
        if (playerUnit.currentHP <= 0)
        {
            playerUnit.currentHP = 0;
            isDead = true;
        }

        playerHUD.SetHP(playerUnit.currentHP); //sets player's HP level in HUD
        textBox.text = enemyUnit.currentSkills[0].skillName + " hits for " + damage + " damage!"; //display successful hit, and damage

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

    void SetSkillButtons()
    {
        skillZeroText.text = playerUnit.currentSkills[0].skillName;
        if (playerUnit.currentSkills[1] != null)
        {
            skillOneText.text = playerUnit.currentSkills[1].skillName;
        }
        if (playerUnit.currentSkills[2] != null)
        {
            skillOneText.text = playerUnit.currentSkills[2].skillName;
        }
        if (playerUnit.currentSkills[3] != null)
        {
            skillOneText.text = playerUnit.currentSkills[3].skillName;
        }
    }
    #region Skill Buttons
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
#endregion
}
