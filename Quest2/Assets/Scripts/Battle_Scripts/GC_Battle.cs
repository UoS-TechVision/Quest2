/**
* Battle Scene Game Controller
* Controls the flow of battle
*/
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;

public enum BattleState {Start, PlayerTurn, EnemyTurn, WON, LOST}

public class GC_Battle : MonoBehaviour
{

    public BattleState state;

    public GameObject playerObj;
    public GameObject enemyObj;

    public Transform playerSpawnPos;
    public Transform enemySpawnPos;

    Unit playerUnit;
    public GC_BattleHUD playerHUD;
    public GC_BattleHUD enemyHUD;

    Unit enemyUnit;

    public TMP_Text dialogueText;

    [SerializeField] private string overworldScene;    //Name of the Overworld Scene to transition into

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("Loaded with Scaling of " + BattleInfo.difficultyScaling);
        state = BattleState.Start;
        //Transfer transfer = GameObject.FindAnyObjectByType<Transfer>(); 
        playerObj = GameObject.FindGameObjectWithTag("Player");
        enemyObj = GameObject.FindGameObjectWithTag("Enemy");
        StartCoroutine(SetUpBattle());

    }


    IEnumerator SetUpBattle()
    {
        //GameObject playerGO = Instantiate(playerObj, playerSpawnPos);
        playerUnit = playerObj.GetComponent<Unit>();


        //GameObject enemyGO = Instantiate(enemyObj, enemySpawnPos);
        enemyUnit = enemyObj.GetComponent<Unit>();

        playerHUD.SetHUD(playerUnit);
        enemyHUD.SetHUD(enemyUnit);

        yield return new WaitForSeconds(2f);
        state = BattleState.PlayerTurn;

        dialogueText.text = "You have encountered a " + enemyObj.name + " . FIGHT!!";
        yield return new WaitForSeconds(2f);
        PlayerTurn();
    }

    void PlayerTurn()
    {
        // Set player dialogue text

        dialogueText.text = "Choose an action: ";


    }

    IEnumerator PlayerAttack()
    {
        // Damage Enemy

        bool isDead = enemyUnit.TakeDamage(playerUnit.Strength);

        enemyHUD.SetHP(enemyUnit);

        dialogueText.text = "Player attacks...";
        yield return new WaitForSeconds(1f);

        if (isDead)
        {
            state = BattleState.WON;
            EndBattle();
        }
        else
        {
            state = BattleState.EnemyTurn;
            StartCoroutine(EnemyTurn());
        }


        yield return new WaitForSeconds(2f);
    }

    IEnumerator PlayerSkill()
    {
        // Damage Enemy
        dialogueText.text = "Player used " + playerUnit.SkillName + "!!";
        bool isDead = enemyUnit.TakeDamage(playerUnit.SkillDamage);

        playerUnit.DeductMana();

        enemyHUD.SetHP(enemyUnit);
        yield return new WaitForSeconds(1f);

        if (isDead)
        {
            state = BattleState.WON;
            EndBattle();
        }
        else
        {
            state = BattleState.EnemyTurn;
            StartCoroutine(EnemyTurn());
        }


        yield return new WaitForSeconds(2f);
    }


    IEnumerator EnemyTurn()
    {
        dialogueText.text = enemyUnit.name + " attacks!";

        yield return new WaitForSeconds(1f);

        bool isDead = playerUnit.TakeDamage(enemyUnit.Strength);
        playerHUD.SetHP(playerUnit);

        yield return new WaitForSeconds(1f);

        if (isDead)
        {
            state = BattleState.LOST;
            EndBattle();
        }
        else
        {
            state = BattleState.PlayerTurn;
            PlayerTurn();
        }

    }

    void EndBattle()
    {
        if (state == BattleState.WON)
        {
            dialogueText.text = "You have defeated " + enemyUnit.name + "!!";
        }
        else if (state == BattleState.LOST)
        {
            dialogueText.text = "You have lost....";
        }
    }

    public void onAttackButton()
    {
        if (state != BattleState.PlayerTurn)
        {
            return;
        }

        StartCoroutine(PlayerAttack());
    }


    public void onSkillButton()
    {
        if (state != BattleState.PlayerTurn && playerUnit.Mana < playerUnit.SkillCost)
        {
            return;
        }

        StartCoroutine(PlayerSkill());
    }

    void TransitionToOverworld()
    {
        //Ensure we're not trying to transition to a scene that doesn't exist
        if (string.IsNullOrEmpty(overworldScene))
        {
            Debug.LogWarning("Warning: Invalid Overworld Scene Name!");
            return;
        }

        //Transition to the Overworld scene
        SceneManager.LoadScene(overworldScene, LoadSceneMode.Single);
    }
}
