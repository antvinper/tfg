using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST, THINKING, MUSTCHANGE }

public class BattleSystem : MonoBehaviour
{
    SaveManager saveManager;

    [Header("Personajes")]
    public Character[] players = new Character[3];
    Character player;

    public Character[] enemiesTier1 = new Character[3];
    public Character[] enemiesTier2 = new Character[3];
    public Character[] enemiesTier3 = new Character[3];
    public Character[] bosses = new Character[3];
    Character enemy;
    public bool isBoss = false;
    int worldLevel;
    int enemyPicker;

    Character playerCharacter;
    Character enemyCharacter;

    [Header("Elementos UI")]
    public BattleState state;

    public Text dialogueText;

    public BattleHUD playerHUD;
    public BattleHUD enemyHUD;

    public bool ganaExp0 = false;
    public bool ganaExp1 = false;
    public bool ganaExp2 = false;

    [Header("UI Subir de nivel")]
    public GameObject interfazSubirNivel;
    public Text playerSubeNiveles;
    public Text levelUp;
    public Text healthUp;
    public Text strenghtUp;
    public Text magicUp;
    public Text armorUp;
    public Text mrUp;

    [Header("UI Cambios")]
    public GameObject hudCambios;
    public ChangeHUD change1;
    public ChangeHUD change2;
    public ChangeHUD change3;
    int indiceCambio;
    int auxCambio;
    public GameObject botonA;
    public GameObject botonB;
    public GameObject debilitadoA;
    public GameObject debilitadoB;

    void Start()
    {
        saveManager = GameObject.Find("MainManager").GetComponent<SaveManager>();
        worldLevel = MainManager.Instance.worldLevel;
        isBoss = MainManager.Instance.isBoss;
        if (isBoss)
        {
            enemy = bosses[worldLevel];
        }
        else
        {
            enemyPicker = Random.Range(0, 2);
            switch (worldLevel)
            {
                case 0:
                    enemy = enemiesTier1[enemyPicker];
                    break;
                case 1:
                    enemy = enemiesTier2[enemyPicker];
                    break;
                case 2:
                    enemy = enemiesTier3[enemyPicker];
                    break;
            }
        }

        int auxPicker = MainManager.Instance.playerPicker;

        while(players[auxPicker].currentHP == 0)
        {
            auxPicker++;
            if (auxPicker > 2) auxPicker = 0;
        }

        player = players[auxPicker];
        indiceCambio = auxPicker;

        if (auxPicker == 0)
        {
            ganaExp0 = true;
        }else if(auxPicker == 1)
        {
            ganaExp1 = true;
        }else if(auxPicker == 2)
        {
            ganaExp2 = true;
        }

        state = BattleState.START;
        StartCoroutine(SetupBattle());
    }

    IEnumerator SetupBattle()
    {
        playerCharacter = player;
        enemyCharacter = enemy;

        playerHUD.SetHUD(playerCharacter);
        enemyHUD.SetHUD(enemyCharacter);

        yield return new WaitForSeconds(2.0f);

        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    void PlayerTurn()
    {
        dialogueText.text = "Elige una acción";
    }

    IEnumerator PlayerAttack()
    {
        //Comprobación del daño a realizar
        bool isDead;

        if(playerCharacter.weapon != null && playerCharacter.weapon.tipo == Tipo.MAGIC)
        {
            isDead = enemyCharacter.TakeDamage(playerCharacter.magicPower, playerCharacter.weapon);
        }
        else if(playerCharacter.weapon != null && playerCharacter.weapon.tipo == Tipo.PHYSICAL)
        {
            isDead = enemyCharacter.TakeDamage(playerCharacter.attackPower, playerCharacter.weapon);
        }
        else
        {
            isDead = enemyCharacter.TakeDamage(playerCharacter.attackPower, null);
        }
        enemyHUD.SetHP(enemyCharacter.currentHP);

        yield return new WaitForSeconds(2.0f);

        if (isDead)
        {
            state = BattleState.WON;
            StartCoroutine(EndBattle());
        }
        else
        {
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
    }

    IEnumerator EnemyTurn()
    {
        // Crear IA del enemigo, que pueda cambiar o atacar
        dialogueText.text = "¡" + enemyCharacter.unitName + " ataca!";

        yield return new WaitForSeconds(1.0f);

        bool isDead;
        
        if (enemyCharacter.weapon != null && enemyCharacter.weapon.tipo == Tipo.MAGIC)
        {
            isDead = playerCharacter.TakeDamage(enemyCharacter.magicPower, enemyCharacter.weapon);
        }
        else if (enemyCharacter.weapon != null && enemyCharacter.weapon.tipo == Tipo.PHYSICAL)
        {
            isDead = playerCharacter.TakeDamage(enemyCharacter.attackPower, enemyCharacter.weapon);
        }
        else
        {
            isDead = playerCharacter.TakeDamage(enemyCharacter.attackPower, null);
        }

        playerHUD.SetHP(playerCharacter.currentHP);

        yield return new WaitForSeconds(1.0f);

        if (isDead)
        {
            CheckTeam();
        }
        else
        {
            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }

    }

    public void CheckTeam()
    {
        bool canContinue = false;
        foreach(Character pje in players)
        {
            if (pje.currentHP != 0) canContinue = true;
        }

        if (canContinue)
        {
            state = BattleState.MUSTCHANGE;
            PlayerTurn();
        }
        else
        {
            state = BattleState.LOST;
            StartCoroutine(EndBattle());
        }
    }

    IEnumerator EndBattle()
    {
        if (state == BattleState.WON)
        {
            dialogueText.text = "¡Has ganado la batalla!";
            yield return new WaitForSeconds(1.5f);
            
            if (ganaExp0)
            {
                StartCoroutine(CalculaExp(0));
            }
            if (ganaExp1)
            {
                StartCoroutine(CalculaExp(1));
            }
            if (ganaExp2)
            {
                StartCoroutine(CalculaExp(2));
            }
        }
        else if(state == BattleState.LOST)
        {
            dialogueText.text = "Has sido derrotado...";
            yield return new WaitForSeconds(3.0f);
            MainManager.Instance.worldLevel = 0;
            players[0].currentHP = players[0].maxHP;
            players[1].currentHP = players[1].maxHP;
            players[2].currentHP = players[2].maxHP;
            saveManager.GuardarPartida();
            SceneManager.LoadScene("Derrota", LoadSceneMode.Single);
        }

        yield return new WaitForSeconds(5.0f);
        interfazSubirNivel.SetActive(false);
        enemyCharacter.currentHP = enemyCharacter.maxHP;
        
        if (MainManager.Instance.worldLevel == 2)
        {
            saveManager.GuardarPartida();
            SceneManager.LoadScene("FinalDelJuego", LoadSceneMode.Single);
        }
        else
        {
            SceneManager.LoadScene("Mapa01", LoadSceneMode.Single);
        }

        if (isBoss && MainManager.Instance.worldLevel < 2) MainManager.Instance.worldLevel += 1;

        // Devuelve a falso este valor, de manera que solamente se activa
        // cuando se accede al combate mediante la interacción con el boss
        MainManager.Instance.isBoss = false;
    }

    public void OnAttackButton()
    {
        if(state != BattleState.PLAYERTURN)
        {
            return;
        }
        else
        {
            dialogueText.text = "¡" + playerCharacter.unitName + " ataca!";
            StartCoroutine(PlayerAttack());
            state = BattleState.THINKING;
        }
    }

    public void OnRunAwayButton()
    {
        if(state == BattleState.PLAYERTURN || state == BattleState.MUSTCHANGE)
        {
            dialogueText.text = "¡" + playerCharacter.unitName + " trata de escapar!";
            StartCoroutine(TryToRunAway());
            state = BattleState.THINKING;
        }
        else
        {
            return;
        }
    }

    public void OnChangeButton()
    {
        if (state == BattleState.PLAYERTURN || state == BattleState.MUSTCHANGE)
        {
            dialogueText.text = "Elige a un personaje para el combate";
            state = BattleState.THINKING;
            OpenChangeHUD();
        }
        else
        {
            return;
        }
    }

    public void OpenChangeHUD()
    {
        hudCambios.SetActive(true);
        if(indiceCambio == 0)
        {
            change1.SetUpHUD(players[0]);

            change2.SetUpHUD(players[1]);
            if (players[1].currentHP == 0)
            {
                botonA.SetActive(false);
                debilitadoA.SetActive(true);
            }
            else
            {
                botonA.SetActive(true);
                debilitadoA.SetActive(false);
            }

            change3.SetUpHUD(players[2]);
            if (players[2].currentHP == 0)
            {
                botonB.SetActive(false);
                debilitadoB.SetActive(true);
            }
            else
            {
                botonB.SetActive(true); 
                debilitadoB.SetActive(false);
            }
        }
        else if (indiceCambio == 1)
        {
            change1.SetUpHUD(players[1]);

            change2.SetUpHUD(players[0]);
            if (players[0].currentHP == 0)
            {
                botonA.SetActive(false);
                debilitadoA.SetActive(true);
            }
            else
            {
                botonA.SetActive(true);
                debilitadoA.SetActive(false);
            }

            change3.SetUpHUD(players[2]);
            if (players[2].currentHP == 0)
            {
                botonB.SetActive(false);
                debilitadoB.SetActive(true);
            }
            else
            {
                botonB.SetActive(true);
                debilitadoB.SetActive(false);
            }
        }
        else if (indiceCambio == 2)
        {
            change1.SetUpHUD(players[2]);

            change2.SetUpHUD(players[0]);
            if (players[0].currentHP == 0)
            {
                botonA.SetActive(false);
                debilitadoA.SetActive(true);
            }
            else
            {
                botonA.SetActive(true);
                debilitadoA.SetActive(false);
            }

            change3.SetUpHUD(players[1]);
            if (players[1].currentHP == 0)
            {
                botonB.SetActive(false);
                debilitadoB.SetActive(true);
            }
            else
            {
                botonB.SetActive(true);
                debilitadoB.SetActive(false);
            }
        }

    }

    public void ChangeButtonA()
    {
        if(indiceCambio == 0)
        {
            auxCambio = 1;
            ganaExp1 = true;
            ganaExp0 = false;
            ganaExp2 = false;
        }
        else if(indiceCambio == 1 || indiceCambio == 2)
        {
            auxCambio = 0;
            ganaExp0 = true;
            ganaExp1 = false;
            ganaExp2 = false;
        }
        playerCharacter = players[auxCambio];
        playerHUD.SetHUD(playerCharacter);
        state = BattleState.ENEMYTURN;
        hudCambios.SetActive(false);
        StartCoroutine(EnemyTurn());
        indiceCambio = auxCambio;
    }

    public void ChangeButtonB()
    {
        if (indiceCambio == 0 || indiceCambio == 1)
        {
            auxCambio = 2;
            ganaExp2 = true;
            ganaExp0 = false;
            ganaExp1 = false;
        }
        else if (indiceCambio == 2)
        {
            auxCambio = 1;
            ganaExp1 = true;
            ganaExp0 = false;
            ganaExp2 = false;
        }
        playerCharacter = players[auxCambio];
        playerHUD.SetHUD(playerCharacter);
        state = BattleState.ENEMYTURN;
        hudCambios.SetActive(false);
        StartCoroutine(EnemyTurn());
        indiceCambio = auxCambio;
    }

    public void CancelChange()
    {
        hudCambios.SetActive(false);
        state = BattleState.PLAYERTURN;
    }

    IEnumerator TryToRunAway()
    {
        yield return new WaitForSeconds(1.0f);
        float canRunAway = Random.Range(0.0f, 10.0f);
        if(canRunAway < 7.5f)
        {
            dialogueText.text = "¡Has logrado escapar!";
            yield return new WaitForSeconds(1.0f);
            SceneManager.LoadScene("Mapa01", LoadSceneMode.Single);
            enemyCharacter.currentHP = enemyCharacter.maxHP;
            MainManager.Instance.isBoss = false;
        }
        else
        {
            dialogueText.text = "¡No has conseguido escapar!";
            yield return new WaitForSeconds(1.0f);
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }  
    }

    IEnumerator CalculaExp(int i)
    {
        int sumaExp = Mathf.CeilToInt(((float)enemy.level / (float)players[i].level) * 50f);
        dialogueText.text = players[i].name + " ha ganado " + sumaExp + " puntos de experiencia.";
        yield return new WaitForSeconds(1.5f);
        players[i].exp += sumaExp;
        if (players[i].exp >= 100)
        {
            int sumaNiveles = 0;
            while (players[i].exp >= 100)
            {
                players[i].exp -= 100;
                sumaNiveles++;
            }
            if (sumaNiveles == 1)
            {
                dialogueText.text = players[i].name + " ha subido " + sumaNiveles + " nivel.";
            }
            else
            {
                dialogueText.text = players[i].name + " ha subido " + sumaNiveles + " niveles.";
            }
            yield return new WaitForSeconds(1.5f);
            StartCoroutine(SubirNiveles(players[i], sumaNiveles, i));
        }
    }

    IEnumerator SubirNiveles(Character character, int numNiveles, int id)
    {
        float randomizer;
        int bucle = 0;
        int sumaVida = 0;
        int sumaFuerza = 0;
        int sumaMagia = 0;
        int sumaArmadura = 0;
        int sumaResistencia = 0;
        if (id == 0)
        {
            while(bucle < numNiveles)
            {
                //Growths de las estadisticas
                //Vida
                randomizer = Random.Range(0f, 10f);
                if (randomizer < 8.0f) sumaVida += 3;

                //Fuerza
                randomizer = Random.Range(0f, 10f);
                if (randomizer < 7.5f) sumaFuerza += 2;

                //Magia
                randomizer = Random.Range(0f, 10f);
                if (randomizer < 2.5f) sumaMagia++;

                //Armadura
                randomizer = Random.Range(0f, 10f);
                if (randomizer < 6.0f) sumaArmadura += 2;

                //Resistencia mágica
                randomizer = Random.Range(0f, 10f);
                if (randomizer < 3.5f) sumaResistencia++;

                bucle++;
            }
        }else if(id == 1)
        {
            while (bucle < numNiveles)
            {
                //Growths de las estadisticas
                //Vida
                randomizer = Random.Range(0f, 10f);
                if (randomizer < 9.0f) sumaVida += 2;

                //Fuerza
                randomizer = Random.Range(0f, 10f);
                if (randomizer < 3.5f) sumaFuerza++;

                //Magia
                randomizer = Random.Range(0f, 10f);
                if (randomizer < 9.5f) sumaMagia += 3;

                //Armadura
                randomizer = Random.Range(0f, 10f);
                if (randomizer < 5.0f) sumaArmadura++;

                //Resistencia mágica
                randomizer = Random.Range(0f, 10f);
                if (randomizer < 7.5f) sumaResistencia += 2;

                bucle++;
            }
        }
        else if(id == 2)
        {
            while (bucle < numNiveles)
            {
                //Growths de las estadisticas
                //Vida
                randomizer = Random.Range(0f, 10f);
                if (randomizer < 8.5f) sumaVida += 4;

                //Fuerza
                randomizer = Random.Range(0f, 10f);
                if (randomizer < 7.5f) sumaFuerza++;

                //Magia
                randomizer = Random.Range(0f, 10f);
                if (randomizer < 1.5f) sumaMagia++;

                //Armadura
                randomizer = Random.Range(0f, 10f);
                if (randomizer < 8.0f) sumaArmadura += 2;

                //Resistencia mágica
                randomizer = Random.Range(0f, 10f);
                if (randomizer < 6.5f) sumaResistencia += 2;

                bucle++;
            }
        }
        
        interfazSubirNivel.SetActive(true);

        if (numNiveles == 1)
        {
            playerSubeNiveles.text = character.name + " ha subido " + numNiveles + " nivel";
        }
        else
        {
            playerSubeNiveles.text = character.name + " ha subido " + numNiveles + " niveles";
        }

        levelUp.text = "Nivel: " + character.level + " + " + numNiveles;
        healthUp.text = "PV: " + character.maxHP + " + " + sumaVida;
        strenghtUp.text = "Fuerza: " + character.attackPower + " + " + sumaFuerza;
        magicUp.text = "Magia: " + character.magicPower + " + " + sumaMagia;
        armorUp.text = "Armadura: " + character.armor + " + " + sumaArmadura;
        mrUp.text = "Resistencia: " + character.maxHP + " + " + sumaResistencia;
        
        character.SubeDeNivel(numNiveles, sumaVida, sumaFuerza, sumaMagia, sumaArmadura, sumaResistencia);
        yield return new WaitForSeconds(3.0f);
    }

}