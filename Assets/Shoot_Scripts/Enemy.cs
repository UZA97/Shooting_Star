using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public string enemyName;
    public float speed;
    public int health;
    public int enemyScore;
    public Sprite[] sprites;
    public GameObject bulletObA;
    public GameObject bulletObB;
    public GameObject itemCoin;
    public GameObject itemBoom;
    public GameObject itemPower;
    public float maxShotDealay;
    public float curShotDealay;
    public GameObject player;
    public ObjectManager objectManager;
    public GameManager gameManager;
    public int patternIndex;
    public int curPatternCount;
    public int[] maxPatternCount;

    SpriteRenderer spriteRenderer;
    Animator anim;
    
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if(enemyName == "B") {
            anim = GetComponent<Animator>();
        }
    }
    void Update()
    {
        if(enemyName == "B") {
            return;
        }
        Fire();
        Reload();
    }
    void OnEnable()
    {
        switch (enemyName)
        {
            case "B":
                health = 1500;
                Invoke("Stop", 2);
                Stop();
                break;
            case "L":
                health = 25;
                break;
            case "M":
                health = 5;
                break;
            case "S":
                health = 4;
                break;
        }
    }
    void Stop()
    {
        if(!gameObject.activeSelf)
            return;
        Rigidbody2D rigid = GetComponent<Rigidbody2D>();
        rigid.velocity = Vector2.zero;
        Invoke("Think", 2);
    }
    void Think()
    {
        if(!gameObject.activeSelf)
            return;
        patternIndex = patternIndex == 3 ? 0 : patternIndex + 1;
        curPatternCount = 0;

        switch(patternIndex)
        {
            case 0:
                FireFoward();
                break;
            case 1:
                FireShot();
                break;
            case 2:
                FireAct();
                break;
            case 3:
                FireAroud();
                break;
        }
    }
    void FireFoward() // 앞으로 6발 
    {
        if(!gameObject.activeSelf)
            return;
        GameObject bulletR = objectManager.MakeObj("BulletBossA");
        bulletR.transform.position = transform.position + Vector3.right * 0.3f;
        GameObject bulletRR = objectManager.MakeObj("BulletBossA");
        bulletRR.transform.position = transform.position + Vector3.right * 0.45f;
        
        GameObject bulletRRR = objectManager.MakeObj("BulletBossA");
        bulletRRR.transform.position = transform.position + Vector3.right * 0.6f; 
        
        GameObject bulletL = objectManager.MakeObj("BulletBossA");
        bulletL.transform.position = transform.position + Vector3.left * 0.3f;
        
        GameObject bulletLL = objectManager.MakeObj("BulletBossA");
        bulletLL.transform.position = transform.position + Vector3.left * 0.45f;
        
        GameObject bulletLLL = objectManager.MakeObj("BulletBossA");
        bulletLLL.transform.position = transform.position + Vector3.left * 0.6f;
        
        Rigidbody2D rigidR = bulletR.GetComponent<Rigidbody2D>();
        Rigidbody2D rigidL = bulletL.GetComponent<Rigidbody2D>();
        Rigidbody2D rigidRR = bulletRR.GetComponent<Rigidbody2D>();
        Rigidbody2D rigidLL= bulletLL.GetComponent<Rigidbody2D>();
        Rigidbody2D rigidRRR = bulletRRR.GetComponent<Rigidbody2D>();
        Rigidbody2D rigidLLL = bulletLLL.GetComponent<Rigidbody2D>();

        rigidR.AddForce(Vector2.down * 10, ForceMode2D.Impulse);
        rigidL.AddForce(Vector2.down * 10, ForceMode2D.Impulse);
        rigidRR.AddForce(Vector2.down * 10, ForceMode2D.Impulse);
        rigidLL.AddForce(Vector2.down * 10, ForceMode2D.Impulse);
        rigidRRR.AddForce(Vector2.down * 10, ForceMode2D.Impulse);
        rigidLLL.AddForce(Vector2.down * 10, ForceMode2D.Impulse);

        curPatternCount++;

        if(curPatternCount < maxPatternCount[patternIndex])
            Invoke("FireFoward", 1.5f);
        else
            Invoke("Think", 2);
    }
    void FireShot() // 플레이어 방향으로 샷건 
    {
        if(!gameObject.activeSelf)
            return;
        for(int i = 0; i < 5; i++)
        {
        GameObject bullet = objectManager.MakeObj("BulletEnemyB");
        bullet.transform.position = transform.position;
        Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
        Vector2 dirVec = player.transform.position - transform.position;
        Vector2 ranVec = new Vector2(Random.Range(-1f,1f),Random.Range(0f,3f));
        dirVec += ranVec;
        rigid.AddForce(dirVec.normalized * 5, ForceMode2D.Impulse);
        }
        curPatternCount++;
        if(curPatternCount < maxPatternCount[patternIndex])
            Invoke("FireShot", 2);
        else
            Invoke("Think", 2);
    }
    void FireAct() // 부채모양으로 발사 
    {
        if(!gameObject.activeSelf)
            return;
        GameObject bullet = objectManager.MakeObj("BulletEnemyA");
        bullet.transform.position = transform.position;
        bullet.transform.rotation = Quaternion.identity;

        Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
        Vector2 dirVec = new Vector2(Mathf.Cos(Mathf.PI * 8 * curPatternCount/maxPatternCount[patternIndex]),-1);
        rigid.AddForce(dirVec.normalized * 5, ForceMode2D.Impulse);
        
        curPatternCount++;
        if(curPatternCount < maxPatternCount[patternIndex])
            Invoke("FireAct", 0.1f);
        else
            Invoke("Think", 2);
    }
    void FireAroud() // 원형태로 전체 공격 
    {
        if(!gameObject.activeSelf)
            return;
        int roundNumA = 50;
        int roundNumB = 55;
        int roundNum = curPatternCount%2 == 0 ? roundNumA : roundNumB;
        for(int i = 0; i < roundNum; i++) {
            GameObject bullet = objectManager.MakeObj("BulletBossB");
            bullet.transform.position = transform.position;
            bullet.transform.rotation = Quaternion.identity;

            Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
            Vector2 dirVec = new Vector2(Mathf.Cos(Mathf.PI * 2 * i/roundNum),Mathf.Sin(Mathf.PI * 2 * i/roundNum));
            rigid.AddForce(dirVec.normalized * 3, ForceMode2D.Impulse);
        
            Vector3 rotVec = Vector3.forward * 360 * i /roundNum + Vector3.forward * 90;
            bullet.transform.Rotate(rotVec);
        }

        curPatternCount++;
        if(curPatternCount < maxPatternCount[patternIndex])
            Invoke("FireAroud", 0.7f);
        else
            Invoke("Think", 2);
    }
    void Fire()
    {
        if(!gameObject.activeSelf)
            return;
        if(curShotDealay < maxShotDealay)
            return;

        if(enemyName == "S")
        {
            GameObject bullet = objectManager.MakeObj("BulletEnemyA");
            bullet.transform.position = transform.position;
            Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
            Vector3 dirVec = player.transform.position - transform.position;
            rigid.AddForce(dirVec.normalized * 5, ForceMode2D.Impulse);
        }
        else if(enemyName == "L")
        {
            GameObject bulletR = objectManager.MakeObj("BulletEnemyB");
            bulletR.transform.position = transform.position + Vector3.right * 0.3f;
            
            GameObject bulletL = objectManager.MakeObj("BulletEnemyB");
            bulletL.transform.position = transform.position + Vector3.left * 0.3f;

            Rigidbody2D rigidR = bulletR.GetComponent<Rigidbody2D>();
            Rigidbody2D rigidL = bulletL.GetComponent<Rigidbody2D>();
            
            Vector3 dirVecR = player.transform.position - (transform.position + Vector3.right * 0.3f);
            Vector3 dirVecL = player.transform.position - (transform.position + Vector3.left * 0.3f);
            
            rigidR.AddForce(dirVecR.normalized * 5, ForceMode2D.Impulse);
            rigidL.AddForce(dirVecL.normalized * 5, ForceMode2D.Impulse);
        }
        curShotDealay = 0;
    }
    void Reload()
    {
        curShotDealay += Time.deltaTime;
    }
    public void OnHit(int dmg) 
    {
        if(health <= 0)
            return;
        health -= dmg;
        if(enemyName == "B") {
            anim.SetTrigger("OnHit");
        }
        else {
            spriteRenderer.sprite = sprites[1];
            Invoke("ReturnSprite", 0.1f);
        }
        if (health <= 0){
            Player playerLogic = player.GetComponent<Player>();
            playerLogic.score += enemyScore;
            
            // Random Itemp Drop
            int ran = enemyName == "B" ? 0 : Random.Range(0,100);
            if(ran < 50) {
                Debug.Log("Not Item");
            }
            else if(ran < 70) { // Coin
                GameObject itemCoin = objectManager.MakeObj("itemCoin");
                itemCoin.transform.position = transform.position;
            }
            else if(ran < 85) { // Power
                GameObject itemPower = objectManager.MakeObj("itemPower");
                itemPower.transform.position = transform.position;
            }
            else if(ran < 100) { // Boom
                GameObject itemBoom = objectManager.MakeObj("itemBoom");
                itemBoom.transform.position = transform.position;
            }
            gameObject.SetActive(false);
            transform.rotation = Quaternion.identity;
            gameManager.CallExplosion(transform.position, enemyName);
            
            //#.Boss Kill
            if(enemyName == "B") {
                gameObject.SetActive(false);
                transform.rotation = Quaternion.identity;
                gameManager.CallExplosion(transform.position, enemyName);
                
                GameObject[] bulletsA = objectManager.GetPool("BulletEnemyA");
                GameObject[] bulletsB = objectManager.GetPool("BulletEnemyB");
                GameObject[] bulletsBosA = objectManager.GetPool("BulletBossA");
                GameObject[] bulletsBosB = objectManager.GetPool("BulletBossB");
                for(int i = 0; i < bulletsA.Length; i++) {
                    if(bulletsA[i].activeSelf) {
                        bulletsA[i].SetActive(false);
                    }
                }
                for(int i = 0; i < bulletsB.Length; i++) {
                    if(bulletsB[i].activeSelf) {
                        bulletsB[i].SetActive(false);
                    }
                }
                for(int i = 0; i < bulletsBosA.Length; i++) {
                    if(bulletsBosA[i].activeSelf) {
                        bulletsBosA[i].SetActive(false);
                    }
                }
                for(int i = 0; i < bulletsBosB.Length; i++) {
                    if(bulletsBosB[i].activeSelf) {
                        bulletsBosB[i].SetActive(false);
                    }
                }
                gameManager.StageEnd();
            }
        }
    }
    void ReturnSprite()
    {
        spriteRenderer.sprite = sprites[0];
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "BorderBullet" && enemyName != "B") {
            gameObject.SetActive(false);
            transform.rotation = Quaternion.identity;
        }
        
        else if (collision.gameObject.tag == "PlayerBullet") {
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            OnHit(bullet.dmg);
            collision.gameObject.SetActive(false);
        }
        
    }
}