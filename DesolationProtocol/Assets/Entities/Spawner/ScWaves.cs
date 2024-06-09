using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class ScWaves : MonoBehaviour
{
    private int ActualWave = 0;


    [SerializeField] private GameObject[] Enemies;
    [SerializeField] private Transform PositionmMin;
    [SerializeField] private Transform PositionMax;


    void Start()
    {
        Invoke("NextWave", 5f);
    }

    void NextWave()
    {
        for (int i = 0; i < (10 + 2 * (ActualWave - 1)); i++)
        {
            int random = Random.Range(0, 100);
            if (random <50)
            {
                SpawnEnemy(0);
            }
            else
            {
                if (random<70)
                {
                    SpawnEnemy(1);
                }
                else
                {
                    if (random < 90)
                    {
                        SpawnEnemy(2);
                    }
                    else
                    {
                        SpawnEnemy(3);
                    }
                }
            }
        }
        ActualWave++;
        Invoke("NextWave", 30f);
    }
    public void SpawnEnemy(int Class)
    {
        Vector3 Place = new Vector3(Random.Range(PositionmMin.position.x, PositionMax.position.x), Random.Range(PositionmMin.position.y, PositionMax.position.y), Random.Range(PositionmMin.position.z, PositionMax.position.z));

        NavMeshHit hit;

        // Intentar encontrar una posición válida dentro del NavMesh
        if (NavMesh.SamplePosition(Place, out hit, 100, NavMesh.AllAreas))
        {
            // Instanciar el prefab en la posición encontrada
            GameObject SpawnedEnemy = Instantiate(Enemies[Class], hit.position, Quaternion.identity);
            //SpawnedEnemy.GetComponent<ScEntity>().level = ActualWave;
        }
    }
}
