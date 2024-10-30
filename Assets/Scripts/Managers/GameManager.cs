using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public Texture2D cursorTexture;
    public GameObject block;
    public PoolManager pool;
    public Slider costSlider;
    public Transform bulletPos;
    public GameObject bullet;

    public GameObject player;

    public GameObject rune;

    public Rigidbody2D playerRigid;

    public TextMeshProUGUI ScriptTxt;
    public BulletScript bulletScript;
    public bool Rune2 = false;

    GameObject curBullet;

    public float bulletSpeed = 0.1f;
    public float enemysHp = 3;
    public float cost = 100;
    Vector2 dirVec;
    Vector2 prevPos;
    Vector2 curPos;
    float bulletCharge = 0;
    int bulletLimit = 20;

    bool isShoting = false;
    public bool isImmortal = false;

    void Awake()
    {
        Debug.Log(gameObject.GetInstanceID());

        if (null == instance)
        {
            //이 클래스 인스턴스가 탄생했을 때 전역변수 instance에 게임매니저 인스턴스가 담겨있지 않다면, 자신을 넣어준다.
            instance = this;

            //씬 전환이 되더라도 파괴되지 않게 한다.
            //gameObject만으로도 이 스크립트가 컴포넌트로서 붙어있는 Hierarchy상의 게임오브젝트라는 뜻이지만, 
            //나는 헷갈림 방지를 위해 this를 붙여주기도 한다.
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            //만약 씬 이동이 되었는데 그 씬에도 Hierarchy에 GameMgr이 존재할 수도 있다.
            //그럴 경우엔 이전 씬에서 사용하던 인스턴스를 계속 사용해주는 경우가 많은 것 같다.
            //그래서 이미 전역변수인 instance에 인스턴스가 존재한다면 자신(새로운 씬의 GameMgr)을 삭제해준다.
            Debug.Log(gameObject.GetInstanceID());
            Destroy(this.gameObject);
        }
        playerRigid = player.GetComponent<Rigidbody2D>();

    }

    public static GameManager Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(1) == true)
        {

            string currentSceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(currentSceneName);
        }
        /*
        //game over
        if(cost<=0){
            Debug.Log("Game Over");
                }
        */
        ScriptTxt.text = cost.ToString();
        if (true)
        {
            costSlider.value = cost / 100;
        }
        MouseClick();
        if (Rune2)
        {
            bulletScript.bulletDamage = 1;
        } else {
            bulletScript.bulletDamage = 0;
        }
        if (cost <= 0)
        {
            if (Rune2)
            {
                PlayerPrefs.SetFloat("X", player.transform.position.x);
                PlayerPrefs.SetFloat("Y", player.transform.position.y);
                PlayerPrefs.SetFloat("Z", player.transform.position.z);
                Vector3 pp = new Vector3 (PlayerPrefs.GetFloat("X"),PlayerPrefs.GetFloat("Y"),PlayerPrefs.GetFloat("Z"));
                Instantiate(rune, pp, Quaternion.identity);
                Rune2 = false;
            }
            player.transform.position = GameObject.FindWithTag("ReStart").transform.position;
            cost = 100;
        }
    }


    void MouseClick()
    {
        //RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);
        if (!isShoting )//&& !Input.GetKey(KeyCode.LeftShift)
        {
            if(Input.GetMouseButtonDown(0)==true)//마우스 왼쪽 버튼 클릭시 true
            {
                prevPos = curPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Debug.Log("Mouse Down");
            }
            if(Input.GetMouseButton(0)==true)//마우스 왼쪽 버튼 클릭중일때 true
            {
                Debug.Log("Mouse Clicked");
                bulletCharge+= 0.1f;//차지 게이지
                if(bulletCharge>5&&curBullet == null)
                {
                    curBullet = Instantiate(bullet, bulletPos.position, Quaternion.identity);
                    curBullet.layer = 11;
                }
                if(bulletCharge>5&&curBullet != null)
                {
                    curBullet.transform.position = bulletPos.position;
                    if(bulletCharge<bulletLimit){
                        //최대 차지 이상으로 scale이 커지지 않게
                        curBullet.transform.localScale = new Vector3(0.2f+bulletCharge/25, 0.2f+bulletCharge/25, 1);
                    }
                }
            }
            if(Input.GetMouseButtonUp(0)==true&&bulletCharge>5)
            {
                if(bulletCharge>bulletLimit){
                    //최대 차지 제한
                    bulletCharge = bulletLimit;
                }
                curBullet.layer = 12;
                Debug.Log("Mouse Up");
                StartCoroutine("Shot");
                curBullet = null;
            }
        //instantiate block 잠시 보류
        /*if (Input.GetMouseButton(0) && Input.GetKey(KeyCode.LeftShift))
        {

            if (hit.collider != null)
            {
                Debug.Log(hit.collider.gameObject.name);
            }
            else
            {
                Instantiate(block, mousePosition, Quaternion.identity);
                cost -= 2;
            }*/
        }
    }

    IEnumerator Shot()
    {   
        curPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        int bNum = (int)bulletCharge/5;

        curBullet.GetComponent<BulletScript>().bulletDamage += bNum;
        //dirVec= new Vector2((mousePosition.x - player.transform.position.x),
        // (mousePosition.y - player.transform.position.y));    //플레이어를 기준으로 마우스 위치

        dirVec = new Vector2((curPos.x - prevPos.x),
         (curPos.y - prevPos.y));    //이전 마우스 위치를 기준으로 마우스 위치

        curBullet.gameObject.GetComponent<Rigidbody2D>().velocity
         = dirVec* bulletSpeed; //.normalized를 붙일까 말까..
        player.GetComponent<Rigidbody2D>().velocity += dirVec*-1*bNum*0.1f; //  *-1 *bNum

        isShoting = true;
        cost -= bNum; //총알 발사시 비용 차감
        bulletCharge = 0;
        yield return new WaitForSeconds(0.5f);
        isShoting = false;

    
    }

    public bool PlayerHit(Transform EnemyTrans)
    {
        int nuckBack = 5, yNuckBack = 1;

        if (!isImmortal)
        {
            playerRigid.velocity += new Vector2((player.transform.position.x - EnemyTrans.position.x),
            (player.transform.position.y - EnemyTrans.position.y + yNuckBack)) * nuckBack;
            //플레이어가 적에게 맞았을 때 밀려나는 효과
            StartCoroutine("Immortal");
            return true;
        }
        else
        {
            return false;
        }
    }
    IEnumerator Immortal()
    {
        isImmortal = true;
        yield return new WaitForSeconds(1.5f);
        isImmortal = false;
    }
}
