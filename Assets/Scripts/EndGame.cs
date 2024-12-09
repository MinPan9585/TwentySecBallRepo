using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGame : MonoBehaviour
{
    public float countDownTime = 20f;
    public GameObject gameOverText;
    //public float spawnRadius = 5f; // 生成范围半径
    //public float minSpawnInterval = 1f;
    //public float maxSpawnInterval = 2f;
    public Transform Player;
    //public GameObject Enemy;
    Image timerImage;

    public bool isStarted = false;

    private void Awake()
    {
        timerImage = GameObject.Find("TimeBarImage").GetComponent<Image>();
    }
    void Start()
    {
        StartCoroutine(CountDownTimer());
        //StartCoroutine(SpawnEnemy());
        GameObject.Find("Player").GetComponent<PlayerControl>().enabled = true;
    }

    IEnumerator CountDownTimer()
    {

        while (countDownTime > -1)
            {

                timerImage.fillAmount = countDownTime / 20f;
                //timerText.GetComponent<Text>().text = countDownTime.ToString();
                yield return new WaitForSeconds(1);
                countDownTime -= 1;

        }

            // 倒计时结束后的操作
            gameOverText.GetComponent<Text>().text = "Times Up!";
            gameOverText.SetActive(true);
            Time.timeScale = 0;
            PlayerControl.isDead = true;
    }
    //IEnumerator SpawnEnemy()
    //{
    //    while (true)
    //    {
    //        yield return new WaitForSeconds(Random.Range(minSpawnInterval, maxSpawnInterval));

    //        Vector2 randomPos = Random.insideUnitCircle * spawnRadius;
    //        //spawnPos.y = 0;
    //        Vector3 spawnPos = Player.position + new Vector3(randomPos.x, 0, randomPos.y);
    //        Instantiate(Enemy, spawnPos, Quaternion.identity);
    //    }
    //}
}
