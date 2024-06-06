using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScWaves : MonoBehaviour
{
    private int ActualWave = 0;


    public GameObject[] Enemigos;

    void Start()
    {
        //Invoke("NextWave", 5f);
    }

    void NextWave()
    {
        for (int i = 0; i < (10 + 2 * (ActualWave - 1)); i++)
        {
            SpawnEnemy();
        }
        Invoke("NextWave", 30f);
    }
    public void SpawnEnemy()
    {
        GameObject LocalEnemy = Instantiate(Enemigos[0], transform.position, Quaternion.identity);
        LocalEnemy.GetComponent<ScEntity>().level = ActualWave;
    }
}
