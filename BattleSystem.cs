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
    bool damagePhaseDone;

    //locations where the player and enemy sprite will be during the battle
    public Transform playerBattleStation;
    public Transform enemyBattleStation;

    public Text textBox;
    public Text skillZeroText;
    public Text skillOneText;
    public Text skillTwoText;
    public Text skillThreeText;

    GameObject playerGameObject;
    GameObject enemyGameObject;

    Unit playerUnit;
    Unit enemyUnit;

    Unit[] enemyUnits = new Unit[6];
    EnemyUnit1 enemyUnit3;
    EnemyUnit1 enemyUnit4;
    EnemyUnit1 enemyUnit5;
    EnemyUnit1 enemyUnit6;

    public BattleHUD playerHUD;
    public BattleHUD enemyHUD;

    // Start is called before the first frame update
    void Start()
    {
        playerGameObject = GameObject.Find("Party Manager");
        enemyGameObject = GameObject.Find("Enemy Party Manager");
        battleState = BattleState.Start;
        StartCoroutine(SetUpBattle());
    }

    IEnumerator SetUpBattle()
    {
        //Finds the Party Manager, and sets the first child to Active
        playerGameObject.transform.GetChild(0).gameObject.SetActive(true);
        playerUnit = playerGameObject.transform.GetChild(0).gameObject.GetComponent<PlayerUnit>(); //gets 'Unit" from first child

        //creates the enemy in their location of the battle screen
        SpawnEnemy(0);
        SpawnEnemy(1);
        SetSkillButtons();
        enemyGameObject.transform.GetChild(0).gameObject.SetActive(true);
        //battle start message in the HUD
        textBox.text = "Battle versus " + enemyUnits.currentUnit.unitName + " begins!";

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
        enemyUnit = enemyGameObject.transform.GetChild(0).gameObject.GetComponent<EnemyUnit1>();

        randomEnemyLevel = Random.Range(1, 10); //random level assignment - could base this off area or character level?
        enemyUnit.currentLevel = randomEnemyLevel;
        enemyUnit.GetComponentInChildren<EnemyUnit1>().OnBattleStart(randomSpawnNumber);

    }

    void SpawnEnemy(int enemyNumber)
    {
        randomSpawnNumber = Random.Range(0, 4); //spawn random enemy
        enemyUnits[enemyNumber] = enemyGameObject.transform.GetChild(enemyNumber).gameObject.GetComponent<EnemyUnit1>();

        randomEnemyLevel = Random.Range(1, 10); //random level assignment - could base this off area or character level?
        enemyUnits[enemyNumber].currentLevel = randomEnemyLevel;
        enemyUnits[enemyNumber].GetComponentInChildren<EnemyUnit1>().OnBattleStart(randomSpawnNumber);

    }

    //PLAYER ATTACKING ------------
    IEnumerator Attack(int skillNumber)
    {
        StartCoroutine(DamagePhase(playerUnit, enemyUnit, skillNumber));
        yield return new WaitForSeconds(3f);

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
                playerUnit.GetComponentInChildren<PlayerUnit>().LevelUp();
                textBox.text = playerUnit.name + " gained a level!";
                yield return new WaitForSeconds(2f);
            }

            if (enemyUnits[1].currentHP > 0)
            {
                enemyGameObject.transform.GetChild(1).gameObject.SetActive(true);
            }
            else
            {
                battleState = BattleState.Won;
                EndBattle();
            }
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
        StartCoroutine(DamagePhase(enemyUnit, playerUnit, 0));
        yield return new WaitForSeconds(3f);

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

    IEnumerator DamagePhase(Unit attacking, Unit defending, int skillNumber)
    {
        var skillUsed = attacking.currentSkills[skillNumber];
        float elementModifier = DamageCalculation.Instance.CalculateElemental(attacking.currentUnit.elementType,
            skillUsed.skillElement, defending.currentUnit.elementType);
        textBox.text = attacking.currentUnit.unitName + " uses " + skillUsed.skillName + "!"; //displays the name of attack 
        yield return new WaitForSeconds(1f);

        if (elementModifier == 0.5 || elementModifier == 0.75) //not very effective
            textBox.text = skillUsed.skillElement + " is less effective against " + defending.currentUnit.elementType + "!";
        if (elementModifier == 2 || elementModifier == 3) //super effective
            textBox.text = skillUsed.skillElement + " is extra effective against " + defending.currentUnit.elementType + "!";
        yield return new WaitForSeconds(1f); // if neither, causes name of attack to hold for 2s instead of 1s

        //Attack or Special attack type calculation
        if (skillUsed.skillAttackType == SkillsSO.AttackPowerType.Attack)
            damage = DamageCalculation.Instance.CalculateDamage(attacking.attack, skillUsed.skillPower,
                attacking.currentLevel, defending.defense, elementModifier);
        else
            damage = DamageCalculation.Instance.CalculateDamage(attacking.specialAttack, skillUsed.skillPower,
                attacking.currentLevel, defending.specialDefense, elementModifier);

        defending.currentHP -= damage; //defending unit takes damage

        if (defending.currentHP <= 0)
        {
            defending.currentHP = 0;
            isDead = true;
        }

        enemyHUD.SetHP(enemyUnit.currentHP); //sets enemy's HP level in HUD
        playerHUD.SetHP(playerUnit.currentHP); //sets player's HP
        //display successful hit, and damage
        textBox.text = attacking.currentSkills[skillNumber].skillName + " hits for " + damage + " damage!";
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
