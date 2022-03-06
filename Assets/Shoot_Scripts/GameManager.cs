using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
public class GameManager : MonoBehaviour
{
    public int stage;
    public Animator stageAnim;
    public Animator clearAnim;
    public Animator fadeAnim;
    public Transform playerPos;
    public float nextSpawnDelay;
    public float curSpawnDelay;
    public Text scoreText;
    public Image[] lifeImage;
    public Image[] boomImage;
    public string[] enemyObjs;
    public Transform[] spawnPoints;
    public GameObject gameOverSet;
    public GameObject player;
    public ObjectManager objectManager;
    public List<spawn> spawnList; 
    public int spawnIndex;
    public bool spawnEnd;

    void Awake()
    {
        spawnList = new List<spawn>();
        enemyObjs = new string[] {"EnemyS", "EnemyM", "EnemyL","EnemyB"};
        StageStart();
    }
    public void StageStart()
    {
        //#.StageUI
        stageAnim.SetTrigger("On");
        stageAnim.GetComponent<Text>().text = "Stage " + stage + "\nStart";
        //#.Enemy Spawn File Read
        ReadSpawnFile();

        //#.Fade In
        fadeAnim.SetTrigger("In");
    }
    public void StageEnd()
    {
        //#.Clear UI Load
        clearAnim.SetTrigger("On");
        clearAnim.GetComponent<Text>().text = "Stage " + stage + "\nClear!!!";

        //#.Fade Out
        fadeAnim.SetTrigger("Out");

        //#.Plyaer Repos
        playerPos.transform.position = playerPos.position;

        //#.Stage Increament
        stage++;
        if(stage > 3)
            GameOver();
        else
            Invoke("StageStart", 2);
    }

    void ReadSpawnFile()
    {
        //#.변수 초기화 
        spawnList.Clear();
        spawnIndex = 0;
        spawnEnd = false;

        //#.리스폰 파일 읽기
        TextAsset textFile = Resources.Load("stage " + stage) as TextAsset; // Resouces폴더 내 파일 불러오기
        StringReader stringReader = new StringReader(textFile.text);
        
        while(stringReader != null)
        {
            string line = stringReader.ReadLine(); // 텍스트 데이터를 한줄씩 반환. 
            if(line == null)
                break;
            
        //#.리스폰 데이터 생성 
        spawn spawnData = new spawn();
        spawnData.delay = float.Parse(line.Split(',')[0]);
        spawnData.type = line.Split(',')[1];
        spawnData.point = int.Parse(line.Split(',')[2]);
        spawnList.Add(spawnData);
        }
        //#. 텍스트 파일 닫기
        stringReader.Close();
        nextSpawnDelay = spawnList[0].delay;
    }
    void Update()
    {
        curSpawnDelay += Time.deltaTime;

        if(curSpawnDelay > nextSpawnDelay && !spawnEnd)
        {
            SpawnEnemy();
            curSpawnDelay = 0;
        }

        // #UI Score Update
        Player playerLogic = player.GetComponent<Player>();
        scoreText.text = string.Format("{0:n0}", playerLogic.score);
        // string.Format("{0:n0}") 세자리마다 쉼표로 나눠주는 숫자 양식
    }
    void SpawnEnemy()
    {
        int enemyIndex = 0;
        switch(spawnList[spawnIndex].type)
        {
            case "S":
                enemyIndex = 0;
                break;
            case "M":
                enemyIndex = 1;
                break;
            case "L":
                enemyIndex = 2;
                break;
            case "B":
                enemyIndex = 3;
                break;
        }
        int enemyPoint = spawnList[spawnIndex].point;
        GameObject enemy = objectManager.MakeObj(enemyObjs[enemyIndex]);
        enemy.transform.position = spawnPoints[enemyPoint].position;

        Rigidbody2D rigid = enemy.GetComponent<Rigidbody2D>();
        Enemy enemyLogic = enemy.GetComponent<Enemy>();

        enemyLogic.player = player; // 적 생성 직후 플레이어 변수를 넘겨줌 
        enemyLogic.gameManager = this; // 클래스 자신을 일컫는 키워드
        enemyLogic.objectManager = objectManager;

        if(enemyPoint == 9 || enemyPoint == 10) { // Right Spaw
            enemy.transform.Rotate(Vector3.back * 360);
            rigid.velocity = new Vector2(enemyLogic.speed * (-1), -2);
        }
        else if(enemyPoint == 7 || enemyPoint == 8) { // Left Spaw
            enemy.transform.Rotate(Vector3.forward * 360);
            rigid.velocity = new Vector2(enemyLogic.speed, -2);
        }       
        else // Front Spawn
            rigid.velocity = new Vector2(0, enemyLogic.speed)*(-1);

        //#.리스폰 인덱스 증가
        spawnIndex++;
        if(spawnIndex == spawnList.Count)
        {
            spawnEnd = true;
            return;
        }
        nextSpawnDelay = spawnList[spawnIndex].delay;
    }
    public void UpdateLifeIcon(int life)
    {
        // #.UI Boom Init Disable
        for(int index = 0; index < 3; index++)
        {
            lifeImage[index].color = new Color(1,1,1,0);
        }
        // #.UI Boom Active
        for(int index = 0; index < life; index++)
        {
            lifeImage[index].color = new Color(1,1,1,1);
        }
    }
    public void UpdateBoomIcon(int boom)
    {
        // #.UI Life Init Disable
        for(int index = 0; index < 3; index++)
        {
            boomImage[index].color = new Color(1,1,1,0);
        }
        // #.UI Life Active
        for(int index = 0; index < boom; index++)
        {
            boomImage[index].color = new Color(1,1,1,1);
        }
    }
    public void RespawnPlayer()
    {
        Invoke("RespawnPlayerExe", 1f);
    }
    void RespawnPlayerExe()
    {
        player.transform.position = Vector3.down * 4;
        player.SetActive(true);
        Player playerLogic = player.GetComponent<Player>();
        playerLogic.isHit = false;
    }
    public void GameOver()
    {
        gameOverSet.SetActive(true);
    }
    public void GameRetry()
    {
        SceneManager.LoadScene(0);
    }
    public void CallExplosion(Vector3 pos, string type)
    {
        GameObject explosion = objectManager.MakeObj("Explosion");
        Explosion explosionLogic = explosion.GetComponent<Explosion>();

        explosion.transform.position = pos;
        explosionLogic.StartExplosion(type);
    }
}
