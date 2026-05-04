using TMPro;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    [SerializeField] private int waveLenght;
    [SerializeField] private int waveLimit;
    [SerializeField] private float spawnRadius;
    [SerializeField] private TMP_Text winText;
    private int minEnemyNumber;
    private int enemyCounter;
    private int waveCounter = 0;
    private GameObject spawnCenter;

    private void Start()
    {
        spawnCenter = GameObject.FindGameObjectWithTag("Player");
        enemyCounter = 0;
        minEnemyNumber = 0;
    }

    private void Update()
    {
        if (waveCounter <= waveLimit)
        {
            if (enemyCounter <= minEnemyNumber)
            {
                SpawnWave();
                waveCounter++;

                if(waveCounter >= 1)
                {
                    waveLenght += 2;
                }
            }
        }
        else if(waveCounter > waveLimit && enemyCounter == 0)
        {
            winText.text = "You Survived";
        }

    }
    public void SpawnWave()
    {
        if(spawnCenter != null)
        {
            for (int i = 0; i < waveLenght; i++)
            {
                CounterAdder();
                Vector2 randomCircle = Random.insideUnitCircle.normalized * spawnRadius;
                Vector3 spawnPos = spawnCenter.transform.position + new Vector3(randomCircle.x, 0, randomCircle.y);

                Instantiate(enemy, spawnPos, Quaternion.identity);
            }
        }
    }

    public void CounterAdder()
    {
        enemyCounter++;
    }

    public void CounterSubtractor()
    {
        enemyCounter--;
    }
}
