using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerController : MonoBehaviour
{
    float x;
    float z;
    public int life = 3;
    Animator animator;
    Rigidbody rb;
    public AudioClip sound1;
    AudioSource audioSource;
    public AudioSource audioSource1;
    public Text lifeText;
    public Text clearText;
    public Text goalText;
    public RawImage gameOverText;
    public RawImage ReturnTitleText;
    [SerializeField]
    Button nextStageButton;
    public GameObject door;
    public GameObject[]enemy;
    public Collider weaponCollider;
    bool isGoal;
    bool isGameOver;
    public Text timeText;
    float time;
    public int speed = 2;
    public Vector3 acceleration;
    void Start()
    {
        isGoal = false;
        isGameOver = false;
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        lifeText.text = "Life:" + life.ToString();
        timeText.text = "時間:" + time.ToString("F1") + "秒";
        clearText.gameObject.SetActive(false);
        gameOverText.gameObject.SetActive(false);
        ReturnTitleText.gameObject.SetActive(false);
        goalText.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        x = Input.GetAxisRaw("Horizontal");
        z = Input.GetAxisRaw("Vertical");

    }
    private void FixedUpdate()
    {
        Vector3 direction = transform.position + new Vector3(x, 0, z).normalized * speed;
        transform.LookAt(direction);
        rb.velocity = new Vector3(x, 0, z).normalized * speed;
        rb.AddForce(acceleration, ForceMode.Acceleration);
        animator.SetFloat("Speed", rb.velocity.magnitude);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            
            animator.SetTrigger("Attack");
        }
        if (!isGoal)
        {
            time += Time.deltaTime;
        }
        timeText.text = "記録:" + time.ToString("F1") + "秒";
        if(!isGameOver)
        {
            lifeText.text = "Life:" + life.ToString();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "Goal")
        {
            clearText.gameObject.SetActive(true);
            nextStageButton.gameObject.SetActive(true);
            ReturnTitleText.gameObject.SetActive(true);
            audioSource1.gameObject.SetActive(false);
            isGoal = true;
            rb.isKinematic = true;
        }
        if(collision.gameObject.tag == "Goal2")
        {
            clearText.gameObject.SetActive(true);
            ReturnTitleText.gameObject.SetActive(true);
            audioSource1.gameObject.SetActive(false);
            audioSource.PlayOneShot(sound1);
            isGoal = true;
            rb.isKinematic = true;
        }
        if(collision.gameObject.tag =="Item")
        {
            audioSource.Play();
            Destroy(collision.gameObject);
            Destroy(door);

        }
        if (collision.gameObject.tag == "Item1")
        {
            audioSource.Play();
            Destroy(collision.gameObject);
            Destroy(enemy[0]);
        }
        if (collision.gameObject.tag == "Item2")
        {
            audioSource.Play();
            Destroy(collision.gameObject);
            Destroy(enemy[1]);
            Destroy(door);
        }
        if (collision.gameObject.tag == "LittleEnemy")
        {
            life--;
            animator.SetTrigger("Hurt");
            Debug.Log("当たった");
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        Damage damage = other.GetComponent<Damage>();
        if (damage != null)
        {

            Debug.Log("敵からダメージを受けた");
            animator.SetTrigger("Hurt");
            life--;
        }
        if (life <1 && damage == null)
        {
            Debug.Log("ゲームオーバー");
            isGameOver = true;
            Destroy(gameObject);
            gameOverText.gameObject.SetActive(true);
            ReturnTitleText.gameObject.SetActive(true);

        }
        if (other.gameObject.tag == ("Death"))
        {
            gameOverText.gameObject.SetActive(true);
            goalText.gameObject.SetActive(false);
            isGoal = true;
            isGameOver = true;
            rb.isKinematic = true;
            animator.SetTrigger("Hurt");
            ReturnTitleText.gameObject.SetActive(true);
        }

    }
    public void HideColliderWeapon()
    {
        weaponCollider.enabled = false;
    }
    public void ShowColliderWeapon()
    {
        weaponCollider.enabled = true;
    }
}



