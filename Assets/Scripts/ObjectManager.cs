using System;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    // 오브젝트 풀링 -> 생성, 삭제하면서 조각난 메모리(가비지컬렉트=GC)가 쌓여 에러발생유도를 방지 
    // 미리 생성해둔 풀에서 활성화 / 비활성화로 사용 
    // 첫 로딩시간 = 장면 배치 + 오브젝트 풀 생성 
    public GameObject enemyBPrefab;
    public GameObject enemyLPrefab;
    public GameObject enemyMPrefab;
    public GameObject enemySPrefab;
    public GameObject itemCoinPrefab;
    public GameObject itemPowerPrefab;
    public GameObject itemBoomPrefab;
    public GameObject bulletPlayerAPrefab;
    public GameObject bulletPlayerBPrefab;
    public GameObject bulletEnemyAPrefab;
    public GameObject bulletEnemyBPrefab;
    public GameObject bulletFollowerPrefab;
    public GameObject bulletBossAprefab;
    public GameObject bulletBossBprefab;
    public GameObject explosionPrefab;

    GameObject[] enemyB;
    GameObject[] enemyL;
    GameObject[] enemyM;
    GameObject[] enemyS;

    GameObject[] itemCoin;
    GameObject[] itemPower;
    GameObject[] itemBoom;

    GameObject[] bulletPlayerA;
    GameObject[] bulletPlayerB;
    GameObject[] bulletEnemyA;
    GameObject[] bulletEnemyB;
    GameObject[] bulletFollower;
    GameObject[] targetPool;
    GameObject[] bulletBossA;
    GameObject[] bulletBossB;
    GameObject[] explosion;
    enum enemies{
        EnemyB, EnemyL, EnemyM, EnemyS, itemCoin,itemPower,itemBoom,BulletPlayerA, BulletPlayerB, BulletEnemyA, BulletEnemyB, BulletFollower, BulletBossA, BulletBossB, Explosion
    }

    void Awake() {
        enemyB = new GameObject[10];
        enemyL = new GameObject[100];
        enemyM = new GameObject[100];
        enemyS = new GameObject[200];

        itemCoin = new GameObject[200];
        itemPower = new GameObject[100];
        itemBoom = new GameObject[100];

        bulletPlayerA = new GameObject[1000];
        bulletPlayerB = new GameObject[1000];
        bulletEnemyA = new GameObject[1000];
        bulletEnemyB = new GameObject[1000];
        bulletFollower = new GameObject[1000];
        bulletBossA = new GameObject[5000];
        bulletBossB = new GameObject[5000];
        explosion = new GameObject[1000];
        Gernerate();
    }
    void Gernerate()
    {
        //#1. Enemy
        for (int index = 0; index < enemyB.Length; index++)
        {
            enemyB[index] = Instantiate(enemyBPrefab);
            enemyB[index].SetActive(false);
        }
        for (int index = 0; index < enemyL.Length; index++)
        {
            enemyL[index] = Instantiate(enemyLPrefab);
            enemyL[index].SetActive(false);
        }
        for (int index = 0; index < enemyM.Length; index++)
        {
            enemyM[index] = Instantiate(enemyMPrefab);
            enemyM[index].SetActive(false);
        }
        for (int index = 0; index < enemyS.Length; index++)
        {
            enemyS[index] = Instantiate(enemySPrefab);
            enemyS[index].SetActive(false);
        }
        //#2. Item
        for (int index = 0; index < itemCoin.Length; index++)
        {
            itemCoin[index] = Instantiate(itemCoinPrefab);
            itemCoin[index].SetActive(false);
        }
        for (int index = 0; index < itemPower.Length; index++)
        {
            itemPower[index] = Instantiate(itemPowerPrefab);
            itemPower[index].SetActive(false);
        }
        for (int index = 0; index < itemBoom.Length; index++)
        {
            itemBoom[index] = Instantiate(itemBoomPrefab);
            itemBoom[index].SetActive(false);
        }
        //#. Bullet
        for (int index = 0; index < bulletPlayerA.Length; index++)
        {
            bulletPlayerA[index] = Instantiate(bulletPlayerAPrefab);
            bulletPlayerA[index].SetActive(false);
        }
        for (int index = 0; index < bulletPlayerB.Length; index++)
        {
            bulletPlayerB[index] = Instantiate(bulletPlayerBPrefab);
            bulletPlayerB[index].SetActive(false);
        }
        for (int index = 0; index < bulletEnemyA.Length; index++)
        {
            bulletEnemyA[index] = Instantiate(bulletEnemyAPrefab);
            bulletEnemyA[index].SetActive(false);
        }
        for (int index = 0; index < bulletEnemyB.Length; index++)
        {
            bulletEnemyB[index] = Instantiate(bulletEnemyBPrefab);
            bulletEnemyB[index].SetActive(false);
        }
        for (int index = 0; index < bulletFollower.Length; index++)
        {
            bulletFollower[index] = Instantiate(bulletFollowerPrefab);
            bulletFollower[index].SetActive(false);
        }
        for (int index = 0; index < bulletBossA.Length; index++)
        {
            bulletBossA[index] = Instantiate(bulletBossAprefab);
            bulletBossA[index].SetActive(false);
        }
        for (int index = 0; index < bulletBossB.Length; index++)
        {
            bulletBossB[index] = Instantiate(bulletBossBprefab);
            bulletBossB[index].SetActive(false);
        }
        for (int index = 0; index < explosion.Length; index++)
        {
            explosion[index] = Instantiate(explosionPrefab);
            explosion[index].SetActive(false);
        }
    }
    public GameObject MakeObj(string type)
    {
        // if(type == "EnemyB") {
        //     targetPool = enemyB;
        //     return targetPool[targetPool.Length];
        // }
        // else if(type == "EnemyL")
        //     targetPool = enemyL;
        // else if(type == "EnemyM")
        //     targetPool = enemyM;
        // else if(type == "EnemyS")
        //     targetPool = enemyS;
        // else if(type == "itemCoin")
        //     targetPool = itemCoin;
        // else if(type == "itemPower")
        //     targetPool = itemPower;
        // else if(type == "itemBoom")
        //     targetPool = itemBoom;
        // else if(type == "BulletPlayerA")
        //     targetPool = bulletPlayerA;
        // else if(type == "BulletPlayerB")
        //     targetPool = bulletPlayerB;
        // else if(type == "BulletEnemyA")
        //     targetPool = bulletEnemyA;
        // else if(type == "BulletEnemyB")
        //     targetPool = bulletEnemyB;
        // else if(type == "BulletFollower")
        //     targetPool = bulletFollower;
        // else if(type == "BulletBossA")
        //     targetPool = bulletBossA;
        // else if(type == "BulletBossB")
        //     targetPool = bulletBossB;
        enemies Enemies;
        if(Enum.TryParse("0",out Enemies))
        {
            Debug.Log(Enemies);
        }
        
        switch (type)
        {
            case "EnemyB":
                targetPool = enemyB;
                break;
            case "EnemyL":
                targetPool = enemyL;
                break;
            case "EnemyM":
                targetPool = enemyM;
                break;
            case "EnemyS":
                targetPool = enemyS;
                break;
            case "itemCoin":
                targetPool = itemCoin;
                break;
            case "itemPower":
                targetPool = itemPower;
                break;
            case "itemBoom":
                targetPool = itemBoom;
                break;
            case "BulletPlayerA":
                targetPool = bulletPlayerA;
                break;
            case "BulletPlayerB":
                targetPool = bulletPlayerB;
                break;
            case "BulletEnemyA":
                targetPool = bulletEnemyA;
                break;
            case "BulletEnemyB":
                targetPool = bulletEnemyB;
                break;
            case "BulletFollower":
                targetPool = bulletFollower;
                break;
            case "BulletBossA":
                targetPool = bulletBossA;
                break;
            case "BulletBossB":
                targetPool = bulletBossB;
                break;
            case "Explosion":
                targetPool = explosion;
                break;
        }
        int index = 0;
        while (index < targetPool.Length) {
            if (!targetPool[index].activeSelf){
                targetPool[index].SetActive(true);
                return targetPool[index];
            }
            index++;
        }
        return null;
    }
    public GameObject[] GetPool(string type)
    {
        // if(type == "EnemyB")
        //     targetPool = enemyB;
        // else if(type == "EnemyL")
        //     targetPool = enemyL;
        // else if(type == "EnemyM")
        //     targetPool = enemyM;
        // else if(type == "EnemyS")
        //     targetPool = enemyS;
        // else if(type == "itemCoin")
        //     targetPool = itemCoin;
        // else if(type == "itemPower")
        //     targetPool = itemPower;
        // else if(type == "itemBoom")
        //     targetPool = itemBoom;
        // else if(type == "BulletPlayerA")
        //     targetPool = bulletPlayerA;
        // else if(type == "BulletPlayerB")
        //     targetPool = bulletPlayerB;
        // else if(type == "BulletEnemyA")
        //     targetPool = bulletEnemyA;
        // else if(type == "BulletEnemyB")
        //     targetPool = bulletEnemyB;
        // else if(type == "BulletFollower")
        //     targetPool = bulletFollower;
        // else if(type == "BulletBossA")
        //     targetPool = bulletBossA;
        // else if(type == "BulletBossB")
        //     targetPool = bulletBossB;
        switch (type)
        {
            case "EnemyB":
                targetPool = enemyB;
                break;
            case "EnemyL":
                targetPool = enemyL;
                break;
            case "EnemyM":
                targetPool = enemyM;
                break;
            case "EnemyS":
                targetPool = enemyS;
                break;
            case "itemCoin":
                targetPool = itemCoin;
                break;
            case "itemPower":
                targetPool = itemPower;
                break;
            case "itemBoom":
                targetPool = itemBoom;
                break;
            case "BulletPlayerA":
                targetPool = bulletPlayerA;
                break;
            case "BulletPlayerB":
                targetPool = bulletPlayerB;
                break;
            case "BulletEnemyA":
                targetPool = bulletEnemyA;
                break;
            case "BulletEnemyB":
                targetPool = bulletEnemyB;
                break;
            case "BulletFollower":
                targetPool = bulletFollower;
                break;
            case "BulletBossA":
                targetPool = bulletBossA;
                break;
            case "BulletBossB":
                targetPool = bulletBossB;
                break;
            case "Explosion":
                targetPool = explosion;
                break;
        }
        return targetPool;
    }
}
