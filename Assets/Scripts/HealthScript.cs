using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HealthScript : MonoBehaviour
{
    public float Health = 100f;
    public EnemyAnimator enemyAnim;
    public NavMeshAgent navAgent;
    public EnemyController enemy_Controller;
    private EnemyAudioScript enemy_audio;
    private PlayerStats player_stats;

    public bool is_player, is_boar, is_Cannibal;

    public bool is_dead;
    // Start is called before the first frame update
    void Awake()
    {
        if(is_boar||is_Cannibal)
        {
            enemyAnim = GetComponent<EnemyAnimator>();
            enemy_Controller = GetComponent<EnemyController>();
            navAgent = GetComponent<NavMeshAgent>();

            enemy_audio = GetComponentInChildren<EnemyAudioScript>();
        }
        if(is_player)
        {
            player_stats = GetComponent<PlayerStats>();
        }
    }

    public void ApplyDamage(float damage)
    {
        if(is_dead)
        {
            return;
        }
        Health -= damage;

        if(is_player)
        {
            player_stats.Display_HealthStats(Health);
        }
        if (is_boar || is_Cannibal)
        {
            if (enemy_Controller.Enemy_State == EnemyState.PATROL)
            {
                enemy_Controller.chase_Distance = 50f;
            }

        if(Health<=0)
            {
                is_dead = true;
                PlayerDied();
            }
        }
    }

    public void PlayerDied()
    {
        if(is_Cannibal)
        {
            GetComponent<Animator>().enabled = false;
            GetComponent<BoxCollider>().isTrigger = false;
            GetComponent<Rigidbody>().AddTorque(-transform.forward* 50f);
            enemy_Controller.enabled = false;
            navAgent.enabled = false;
            enemyAnim.enabled = false;
            StartCoroutine(DeadSound());
        }

        if(is_boar)
        {
            enemyAnim.Dead();
            navAgent.velocity = Vector3.zero;
            enemy_Controller.enabled = false;
            navAgent.enabled = false;

            StartCoroutine(DeadSound());

            EnemeyManager.instance.EnemyDied(true);
        }

        if(is_player)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag(Tags.ENEMY_TAG);


            for(int i=0; i<=enemies.Length; i++)
            {
                enemies[i].GetComponent<EnemyController>().enabled = false;
            }

            EnemeyManager.instance.StopSpawning();
            GetComponent<PlayerMovement>().enabled = false;
            GetComponent<Playerattack>().enabled = false;
            GetComponent<WeaponManager>().GetCurrentSelectedWeapon().gameObject.SetActive(false);

        }
        if(tag==Tags.PLAYER_TAG)
        {
            Invoke("RestartGame", 3f);
        }
        else
        {
            Invoke("TurnOffGameObject", 3f);
        }

        void RestartGame()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Gameplay");
        }

        void TurnOffGameObject()
        {
            gameObject.SetActive(false);
        }

        IEnumerator DeadSound()
        {
            yield return new WaitForSeconds(0.3f);

            enemy_audio.Play_DeadSound();
        }
    }
}
