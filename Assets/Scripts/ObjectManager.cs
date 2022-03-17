using System;
using UnityEngine;
public enum Objects
{
    EnemyS, EnemyM, EnemyL, EnemyB, itemCoin, itemPower, itemBoom, BulletPlayerA, BulletPlayerB, BulletEnemyA, BulletEnemyB, BulletFollower, BulletBossA, BulletBossB, Explosion
}
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
    
    void Awake()
    {
        enemyB = new GameObject[10];
        enemyL = new GameObject[10];
        enemyM = new GameObject[10];
        enemyS = new GameObject[20];

        itemCoin = new GameObject[20];
        itemPower = new GameObject[10];
        itemBoom = new GameObject[10];

        bulletPlayerA = new GameObject[100];
        bulletPlayerB = new GameObject[100];
        bulletEnemyA = new GameObject[100];
        bulletEnemyB = new GameObject[100];
        bulletFollower = new GameObject[100];
        bulletBossA = new GameObject[500];
        bulletBossB = new GameObject[500];
        explosion = new GameObject[100];
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
    public GameObject MakeObj(Objects objects)
    {
        if (objects == Objects.EnemyS)
            targetPool = enemyS;
        else if(objects == Objects.EnemyM) 
            targetPool = enemyM;
        else if(objects == Objects.EnemyL) 
            targetPool = enemyL;
        else if(objects == Objects.EnemyB)
            targetPool = enemyB;
        else if(objects == Objects.itemCoin)
            targetPool = itemCoin;
        else if(objects == Objects.itemPower)
            targetPool = itemPower;
        else if(objects == Objects.itemBoom)
            targetPool = itemBoom;
        else if(objects == Objects.BulletPlayerA)
            targetPool = bulletPlayerA;
        else if(objects == Objects.BulletPlayerB)
            targetPool = bulletPlayerB;
        else if(objects == Objects.BulletEnemyA)
            targetPool = bulletEnemyA;
        else if(objects == Objects.BulletEnemyB)
            targetPool = bulletEnemyB;
        else if(objects == Objects.BulletFollower)
            targetPool = bulletFollower;
        else if(objects == Objects.BulletBossA)
            targetPool = bulletBossA;
        else if(objects == Objects.BulletBossB)
            targetPool = bulletBossB;
        else if(objects == Objects.Explosion)
            targetPool = explosion;
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
    public GameObject[] GetPool(Objects objects)
    {
        if (objects == Objects.EnemyS)
            targetPool = enemyS;
        else if(objects == Objects.EnemyM) 
            targetPool = enemyM;
        else if(objects == Objects.EnemyL) 
            targetPool = enemyL;
        else if(objects == Objects.EnemyB)
            targetPool = enemyB;
        else if(objects == Objects.itemCoin)
            targetPool = itemCoin;
        else if(objects == Objects.itemPower)
            targetPool = itemPower;
        else if(objects == Objects.itemBoom)
            targetPool = itemBoom;
        else if(objects == Objects.BulletPlayerA)
            targetPool = bulletPlayerA;
        else if(objects == Objects.BulletPlayerB)
            targetPool = bulletPlayerB;
        else if(objects == Objects.BulletEnemyA)
            targetPool = bulletEnemyA;
        else if(objects == Objects.BulletEnemyB)
            targetPool = bulletEnemyB;
        else if(objects == Objects.BulletFollower)
            targetPool = bulletFollower;
        else if(objects == Objects.BulletBossA)
            targetPool = bulletBossA;
        else if(objects == Objects.BulletBossB)
            targetPool = bulletBossB;

        return targetPool;
    }
}
