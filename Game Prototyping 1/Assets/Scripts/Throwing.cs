using UnityEngine;
public class Throwing : MonoBehaviour
{
    [SerializeField] private float m_airLifespan;
    GameObject m_player;
    GameObject m_swordHeld;
    PlayerController m_playerController;
    Rigidbody2D m_rigidbody;
    private float m_destroyTimer;
    GameObject m_collision;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
        m_player = GameObject.FindWithTag("Player"); 
        m_playerController = m_player.GetComponent<PlayerController>();
        m_swordHeld = m_player.transform.Find("Sword").gameObject;
        m_destroyTimer = Time.time + m_airLifespan;
    }

    // Update is called once per frame
    void Update()
    {
        //checks the length of time the thrown sword has been in the air and destroys it if it has been longer than the life span of the air and reactivates the sword as held by the player
        if (Time.time > m_destroyTimer && m_collision == null)
        {
            Destroy(gameObject);
            m_swordHeld.SetActive(true);
            m_playerController.m_isSwordHeld = true;
        }
        //stops the sword from sliding when hitting the wall
        if (m_collision != null)
            m_rigidbody.linearVelocity = Vector3.zero;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // checks that the sword has hit an obstacle that isn't the player or an enemy
        if (!collision.gameObject.CompareTag("Player") && !collision.gameObject.CompareTag("Enemy"))
        {
            m_collision = collision.gameObject;
        }
        //destroys the thrown sword when hitting an enemy and reactivates the sword as held by the player
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
            m_swordHeld.SetActive(true);
            m_playerController.m_isSwordHeld = true;
        }
    }

}
