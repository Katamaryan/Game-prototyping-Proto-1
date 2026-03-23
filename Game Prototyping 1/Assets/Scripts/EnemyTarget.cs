using UnityEditor.Rendering;
using UnityEngine;

public class EnemyTarget : MonoBehaviour
{
    private Rigidbody2D m_rigidbody;

    private Vector2 m_enemyDirection;
    [SerializeField] private float m_enemySpeed;
    [SerializeField] private float m_knockback;
    [SerializeField] private float m_movementStunRate;
    [SerializeField] private float m_movementStun;
    [SerializeField] private float m_health;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
        m_enemyDirection = Vector2.right;
        m_rigidbody.linearVelocityX = m_enemyDirection.x * m_enemySpeed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Time.time >= m_movementStun)
            m_rigidbody.linearVelocityX = m_enemyDirection.x * (m_enemySpeed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Floor") && !collision.gameObject.CompareTag("Player"))
        {
            if (collision.gameObject.CompareTag("Player Attack"))
            {
                m_health -= 1;
                Debug.Log(m_health);
                if (m_health < 0)
                    Destroy(gameObject);
            }

            else
            {
                m_enemyDirection = -m_enemyDirection;
                Debug.Log(m_enemyDirection);
                Debug.Log("from collision");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Floor"))
        {
            if (collision.gameObject.CompareTag("Player Attack"))
            {
                m_movementStun = m_movementStunRate + Time.time;
                Vector3 distanceFromAttack = (transform.position - collision.transform.position);
                distanceFromAttack.y = 0; distanceFromAttack.z = 0;
                distanceFromAttack = distanceFromAttack.normalized;
                m_rigidbody.AddForce( distanceFromAttack * m_knockback, ForceMode2D.Impulse);
                m_health -= 1;
                Debug.Log(m_health);
                if (m_health < 0)
                    Destroy(gameObject);
            }

            else
            {
                m_enemyDirection = -m_enemyDirection;
                Debug.Log(m_enemyDirection);
                Debug.Log("from trigger");
            }
        }
    }
}
