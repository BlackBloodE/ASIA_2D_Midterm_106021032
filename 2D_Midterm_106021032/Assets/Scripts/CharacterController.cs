using UnityEngine;
using UnityEngine.UI;


public class CharacterController : MonoBehaviour
{
    ///
    ///<summary></summary>>
    ///
    #region 欄位
    public float hp;
    public float hpMax;
    public float speed;
    public float jump;
    public bool dead = false;
    public int coins;
    public AudioClip jumpFX;
    public AudioClip slideFX;
    public AudioClip hitFX;
    public AudioClip coinFX;
    public AudioSource audioS;

    public Animator ani;
    public CapsuleCollider2D CC;
    public Rigidbody2D rig;

    public bool isGround;

    public Text CoinText;
    public Image imgHP;
    public GameObject END;
    public Text title;
    public Text coinText;
    #endregion

    #region 方法


    /// <summary>
    /// 角色跳躍功能:跳躍動畫、音效、往上跳
    /// </summary>
    
    private void Move()
    {
        if (rig.velocity.magnitude < 5)
        {
            rig.AddForce(new Vector2(speed, 0));
        }
        
    }
    
    private void Jump()
    {
        bool key = Input.GetKeyDown(KeyCode.Space);

        ani.SetBool("Jump", !isGround);

        if (isGround)
        {
            if (key)
            {
                audioS.PlayOneShot(jumpFX);
                rig.AddForce(new Vector2(0, jump));
                isGround = false;
            }
        }
    }

    /// <summary>
    /// 角色滑行功能:滑行動畫、音效、縮小碰撞範圍
    /// </summary>
    private void Slide()
    {
        bool key = Input.GetKey(KeyCode.LeftControl);

        ani.SetBool("Slide", key);

        if (key)
        {
            //AudioSource.PlayClipAtPoint(slideFX, transform.position);
            CC.offset = new Vector2(0.2888068f, -1.288947f);
            CC.size = new Vector2(1.186361f, 1.9621f);
        }
        else
        {
            CC.offset = new Vector2(0.2888068f, -0.2353241f);
            CC.size = new Vector2(1.186361f, 4.069351f);
        }
        
    }

    /// <summary>
    /// 碰到障礙物、扣血
    /// </summary>
    private void Hit()
    {
        audioS.PlayOneShot(hitFX);
        hp--;
        imgHP.fillAmount = hp / hpMax;

        if (hp <= 0)
        {
            Dead();
        }
    }

    /// <summary>
    /// 吃金幣:金幣數量++、音效、UI更新
    /// </summary>
    private void EatCoin(Collider2D collision)
    {
        audioS.PlayOneShot(coinFX);
        coins++;
        CoinText.text = "Coins : " + coins;
        Destroy(collision.gameObject);
    }

    /// <summary>
    /// 死亡:死亡動畫、結束畫面
    /// </summary>
    private void Dead()
    {
        if (dead) return;

        dead = true;
        ani.SetTrigger("Dead");
        speed = 0;
        END.SetActive(true);
        coinText.text = "Coins : " + coins;
        rig.velocity = Vector3.zero;
    }

    private void Pass()
    {
        END.SetActive(true);
        title.text = "恭喜過關";
        coinText.text = "Coins : " + coins;
        speed = 0;
        rig.velocity = Vector3.zero;
    }
    #endregion

    #region 事件
    private void Start()
    {
        hpMax = hp;
    }
    private void Update()
    {
        Slide();

        if (transform.position.y <= -6)
        {
            Dead();
        }
        
    }
    private void FixedUpdate()
    {
        Jump();
        Move();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "floor")
        {
            isGround = true;
        }
        if (collision.gameObject.tag == "air floor")
        {
            isGround = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Coin")
        {
            EatCoin(collision);
        }
        if (collision.gameObject.tag == "障礙物")
        {
            Hit();
        }
        if (collision.gameObject.name == "Portal")
        {
            Pass();
        }
    }

    #endregion
}
