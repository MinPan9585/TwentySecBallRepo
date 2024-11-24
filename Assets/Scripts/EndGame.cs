using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGame : MonoBehaviour
{
    public float countDownTime = 20f;
    public GameObject gameOverText;
    public float spawnRadius = 5f; // 生成范围半径
    public float minSpawnInterval = 1f;
    public float maxSpawnInterval = 2f;
    public Transform Player;
    public GameObject Enemy;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CountDownTimer());
        StartCoroutine(SpawnEnemy());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator CountDownTimer()
    {
        while (countDownTime > 0)
        {
            yield return new WaitForSeconds(1);
            countDownTime -= 1;
        }

        // 倒计时结束后的操作
        gameOverText.GetComponent<Text>().text = "You Win!";
        gameOverText.SetActive(true);
    }
    IEnumerator SpawnEnemy()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minSpawnInterval, maxSpawnInterval));

            Vector3 spawnPos = Random.insideUnitCircle * spawnRadius;
            spawnPos.y = 0; // 假设敌人是2D游戏物体，z坐标为0
            spawnPos += Player.position;
            Instantiate(Enemy, spawnPos, Quaternion.identity);
        }
    }
}
