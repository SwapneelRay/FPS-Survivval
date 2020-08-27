using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemeyManager : MonoBehaviour
{

    public static EnemeyManager instance;

    [SerializeField]
    private GameObject Cannibal_prefabs, Boar_prefabs;

    [SerializeField]
    private float Cannibal_enemy_count, Boar_enemy_count;

    private float initial_cannibal_count, initial_boar_count;
    public float wait_before_enemy_respawn_time= 10f;

    public Transform[] Cannibal_spawn_points, boar_spawn_points;

    void Awake()
    {
        makeInstance();

    }
    private void Start()
    {
        initial_boar_count = Boar_enemy_count;
        initial_cannibal_count = Cannibal_enemy_count;
        Spawnenemies();
        StartCoroutine("CheckToSpawnEnemies");
    }
    

    void Spawnenemies()
    {

    }


    void SpawnCannibals()
    {
        int index = 0;
        for(int i = 0; i<Cannibal_enemy_count; i++ )
        {   if(index>=Cannibal_spawn_points.Length)
            { index = 0; }
            Instantiate(Cannibal_prefabs, Cannibal_spawn_points[index].position, Quaternion.identity);
            index++;
        }

        Cannibal_enemy_count = 0;
    }

    void Spawnboars()
    {
        int index = 0;
        for (int i = 0; i < Boar_enemy_count; i++)
        {
            if (index >= boar_spawn_points.Length)
            { index = 0; }
            Instantiate(Boar_prefabs, boar_spawn_points[index].position, Quaternion.identity);
            index++;
        }

        Boar_enemy_count = 0;
    }
    void makeInstance()
    {
        if (instance == null)
            instance = this;
    }

    IEnumerator CheckToSpawnEnemies()
    {
        yield return new WaitForSeconds(wait_before_enemy_respawn_time);
        SpawnCannibals();
        Spawnboars();
        StartCoroutine("CheckToSpawnEnemies");


    }

    public void EnemyDied(bool Cannibal)
    {
        if (Cannibal)
        {
            Cannibal_enemy_count++;

            if (Cannibal_enemy_count > initial_cannibal_count)
            {
                Cannibal_enemy_count = initial_cannibal_count;
            }
        }
        else
        {

            Boar_enemy_count++;
            if(Boar_enemy_count>initial_boar_count)
            {
                Boar_enemy_count = initial_boar_count;
            }



        }
    }

    public void StopSpawning()
    {
        StopCoroutine("CheckToSpawnEnemies");
    }
}
