
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [Header("移動速度"), Range(0, 300)]
    public float speed = 10.5f;
    [Header("虛擬搖桿")]
    public FixedJoystick joystick;
    [Header("變形元件")]
    public Transform tra;
    [Header("動畫元件")]
    public Animator ani;
    [Header("偵測範圍")]
    public float rangeAttack = 2.5f;
    [Header("音效來源")]
    public AudioSource aud;
    [Header("攻擊音效")]
    public AudioClip soudnAttack;
    [Header("吃能量音效")]
    public AudioClip soundEat;
    [Header("能量數量")]
    public Text textCoin;
    [Header("血量")]
    public float hp = 200;
    [Header("血條系統")]
    public HpManager hpManager;
    [Header("攻擊力"), Range(0, 1000)]
    public float attack = 20;
    [Header("角色是否死亡")]
    public bool isDead = false;
    [Tooltip("這是角色的名稱")]
    public string cName = "女孩";
    private float hpMax;
    private int coin;
    // 方法
    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, 0.2f);
        Gizmos.DrawSphere(transform.position, rangeAttack);
    }
    public void Attack()
    {
        aud.PlayOneShot(soudnAttack, 0.5f);

        RaycastHit2D hit = Physics2D.CircleCast(transform.position, rangeAttack, transform.right, 0, 1 << 8);
        print("碰到的物件:" + hit.collider.name);

        if (hit && hit.collider.tag == "道具") hit.collider.GetComponent<Item>().DropProp();
        if (hit && hit.collider.tag == "敵人") hit.collider.GetComponent<Enemy>().Hit(attack);
    }
    private void Move()
    {
        float h = joystick.Horizontal;
        float v = joystick.Vertical;

        tra.Translate(h * speed * Time.deltaTime, v * speed * Time.deltaTime, 0);
        ani.SetFloat("水平", h);
    }
    //語法
    private void Start()
    {
        hpMax = hp;
    }
    private void Update()
    {
        Move();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "能量")
        {
            coin++;
            aud.PlayOneShot(soundEat);
            Destroy(collision.gameObject);
            textCoin.text = "能量: " + coin;
        }

    }
    /// <summary>
    /// 受傷
    /// </summary>
    /// <param name="damage">接收到的傷害值</param>
    public void Hit(float damage)
    {
        hp -= damage;
        hpManager.UpdateHpBar(hp, hpMax);
        StartCoroutine(hpManager.ShowDamage(damage));

        if (hp <= 0) Dead();
    }
    /// <summary>
    /// 死亡
    /// </summary>
    private void Dead()
    {
        hp = 0;
        isDead = true;
        Invoke("Replay", 2);
    }
}