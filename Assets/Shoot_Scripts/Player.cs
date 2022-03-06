using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool isTouchUp;
    public bool isTouchDown;
    public bool isTouchLeft;
    public bool isTouchRight;
    public bool isHit;
    public bool isBoomTime;
    public FixedJoystick joystick;
    public bool isButtonA;
    public bool isButtonB;
    public int life;
    public int score;

    public int power;
    public int boom;
    public int maxboom;
    public int maxpower;
   
    public float maxShotDealay;
    public float curShotDealay;
    public float speed;

    public GameManager gameManager;
    public ObjectManager objectManager;
    public GameObject bulletObA;
    public GameObject bulletObB;
    public GameObject boomEffect;
    public GameObject[] followers;
    SpriteRenderer spriteRenderer;
    Animator animator;

    void Awake() {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        Boom();
        Move();
        Fire();
        Reload();
    }

    void Move()
    {
        //#Keyboard Control Value
        float x = joystick.Horizontal;
        float y = joystick.Vertical;

        if ((isTouchRight && x == 1) || (isTouchLeft && x == -1))
            x = 0;
        if ((isTouchUp && y == 1) || (isTouchDown && y == -1))
            y = 0;
        Vector3 curPos = transform.position;
        Vector3 nextPos = new Vector3(x, y, 0) * speed * Time.deltaTime;
        // 물리적이동이 아닌 transfor이동에는 Time.DeltaTime(완료되는데까지 걸린 시간/s) 

        transform.position = curPos + nextPos;

        // PC Version
        // if(Input.GetButtonDown("Horizontal") || Input.GetButtonUp("Horizontal"))
        //     animator.SetInteger("Input", (int)x);
    }
    public void ButtonADown()
    {
        isButtonA = true;
    }
    public void ButtonAUp()
    {
        isButtonA = false;
    }
    public void ButtonBDown()
    {
        isButtonB = true;
    }
    public void ButtonBUp()
    {
        isButtonB = false;
    }
    void Fire()
    {
        // if(!Input.GetButton("Fire1"))
        //     return;
        if(!isButtonA)
            return;
        if(curShotDealay < maxShotDealay)
            return;
        switch (power)
        {
            case 1:
                //Instantiate : 
                GameObject bullet = objectManager.MakeObj("BulletPlayerA");
                bullet.transform.position = transform.position; 
                
                Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
                rigid.AddForce(Vector2.up * 12, ForceMode2D.Impulse);
                break;
            case 2:
                GameObject bulletR = objectManager.MakeObj("BulletPlayerA");
                bulletR.transform.position = transform.position +  Vector3.right * 0.1f;
                Rigidbody2D rigidR = bulletR.GetComponent<Rigidbody2D>();
                rigidR.AddForce(Vector2.up * 12, ForceMode2D.Impulse);
                GameObject bulletL = objectManager.MakeObj("BulletPlayerA");
                bulletL.transform.position = transform.position + Vector3.left * 0.1f;
                Rigidbody2D rigidL = bulletL.GetComponent<Rigidbody2D>();
                rigidL.AddForce(Vector2.up * 12, ForceMode2D.Impulse);
                break;
            case 3:
                GameObject bulletC = objectManager.MakeObj("BulletPlayerB");
                bulletC.transform.position =transform.position;
                Rigidbody2D rigidC = bulletC.GetComponent<Rigidbody2D>();
                rigidC.AddForce(Vector2.up * 12, ForceMode2D.Impulse);
                break;
            default:
                GameObject bulletRR = objectManager.MakeObj("BulletPlayerA");
                bulletRR.transform.position = transform.position +  Vector3.right * 0.25f;
                Rigidbody2D rigidRR = bulletRR.GetComponent<Rigidbody2D>();
                rigidRR.AddForce(Vector2.up * 12, ForceMode2D.Impulse);
                GameObject bulletCC = objectManager.MakeObj("BulletPlayerB");
                bulletCC.transform.position = transform.position;
                Rigidbody2D rigidCC = bulletCC.GetComponent<Rigidbody2D>();
                rigidCC.AddForce(Vector2.up * 12, ForceMode2D.Impulse);
                GameObject bulletLL = objectManager.MakeObj("BulletPlayerA");
                bulletLL.transform.position = transform.position + Vector3.left * 0.25f;
                Rigidbody2D rigidLL = bulletLL.GetComponent<Rigidbody2D>();
                rigidLL.AddForce(Vector2.up * 12, ForceMode2D.Impulse);
                break;
        }
        curShotDealay = 0;
        return;
    }
    void Reload()
    {
        curShotDealay += Time.deltaTime;
    }

    void Boom()
    {
        // if(!Input.GetButton("Fire2"))
        //     return;

        if(!isButtonB)
            return;

        if(isBoomTime)
            return;

        if(boom == 0)
            return;
            
        boom --;
        isBoomTime = true;
        gameManager.UpdateBoomIcon(boom);

        boomEffect.SetActive(true);
        Invoke("OffBoomEffect", 2f);
        
        GameObject[] enemiesL = objectManager.GetPool("EnemyL");//GameObject.FindGameObjectsWithTag("Enemy");
        GameObject[] enemiesM = objectManager.GetPool("EnemyM");
        GameObject[] enemiesS = objectManager.GetPool("EnemyS");
        GameObject[] enemiesB = objectManager.GetPool("EnemyB");
        // FindGameObjectsWithTag : 태그로 장면의 모든 오브젝트 추출
        //# Remove Enemy
        for(int i = 0; i < enemiesL.Length; i++) {
            if(enemiesL[i].activeSelf) {
                Enemy enmemyLogic = enemiesL[i].GetComponent<Enemy>();
                enmemyLogic.OnHit(100);
            }
        }

        for(int i = 0; i < enemiesM.Length; i++) {
            if(enemiesM[i].activeSelf) {
                Enemy enmemyLogic = enemiesM[i].GetComponent<Enemy>();
                enmemyLogic.OnHit(100);
            }
        }

        for(int i = 0; i < enemiesS.Length; i++) {
            if(enemiesS[i].activeSelf) {
                Enemy enmemyLogic = enemiesS[i].GetComponent<Enemy>();
                enmemyLogic.OnHit(100);
            }
        }
        
        for(int i = 0; i < enemiesB.Length; i++) {
            if(enemiesB[i].activeSelf) {
                Enemy enmemyLogic = enemiesB[i].GetComponent<Enemy>();
                enmemyLogic.OnHit(100);
            }
        }

        //# Remove Enemy Bullet
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
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Border")
        {
            switch(collision.gameObject.name)
            {
                case "UP":
                    isTouchUp = true;
                    break;
                case "DOWN":
                    isTouchDown = true;
                    break;
                case "LEFT":
                    isTouchLeft = true;
                    break;
                case "RIGHT":
                    isTouchRight = true;
                    break;
            }
        }
        else if(collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "EnemyBullet")
        {
            if(isHit)
                return;
            isHit = true;
            life--;
            gameManager.UpdateLifeIcon(life);
            gameManager.CallExplosion(transform.position,"P");
            if(life == 0) {
                gameManager.GameOver();
            }
            else {
                gameManager.RespawnPlayer();
                Ondamaged();
                power = 1;
                followers[0].SetActive(false);
                followers[1].SetActive(false);
            }
            gameObject.SetActive(false);
            collision.gameObject.SetActive(false);
        }
        else if (collision.gameObject.tag == "Item")
        {
            Item item = collision.gameObject.GetComponent<Item>();
            switch (item.type){
                case "Coin":
                    score += 300;
                    break;
                case "Power":
                    if(power == maxpower)
                        score += 200;
                    else {
                        power++;
                        AddFollower();
                    }
                    break;
                case "Boom":
                    if(boom == maxboom)
                        score += 200;
                    else {
                        boom++;
                        gameManager.UpdateBoomIcon(boom);
                    }
                    break;
            }
            collision.gameObject.SetActive(false);
        }
        return;
    }
    void AddFollower()
    {
        if(power == 5)
            followers[0].SetActive(true);
        else if(power == 6)
            followers[1].SetActive(true);
    }
    void Ondamaged()
    {
        gameObject.layer = 7;
        spriteRenderer.color = new Color(1,1,1,0.4f);
        Invoke("Offdamaged", 3);
    }
    void Offdamaged()
    {
        gameObject.layer = 8;
        spriteRenderer.color = new Color(1,1,1,1);
    }
    void OffBoomEffect()
    {
        boomEffect.SetActive(false);
        isBoomTime = false;
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Border")
        {
            switch(collision.gameObject.name)
            {
                case "UP":
                    isTouchUp = false;
                    break;
                case "DOWN":
                    isTouchDown = false;
                    break;
                case "LEFT":
                    isTouchLeft = false;
                    break;
                case "RIGHT":
                    isTouchRight = false;
                    break;
            }
        }
    }
}
