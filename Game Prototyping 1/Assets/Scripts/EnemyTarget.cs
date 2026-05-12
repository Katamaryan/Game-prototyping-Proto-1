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
        //Checks that the enemy isnt colliding with the floor or player so that they don't make the enemy turn directions
        if (!collision.gameObject.CompareTag("Floor") && !collision.gameObject.CompareTag("Player"))
        {
            //checks the enemy is colliding with a player attack and then damages it
            if (collision.gameObject.CompareTag("Player Attack"))
            {
                m_health -= 1;
                if (m_health < 0)
                    Destroy(gameObject);
            }
            //swaps direction when the enemy collides with a wall or obstacle
            else
            {
                m_enemyDirection = -m_enemyDirection;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //checks the enemy is colliding with the player slash attack trigger and deals damage also pushs the enemy back 
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
                if (m_health < 0)
                    Destroy(gameObject);
            }

            else
            {
                m_enemyDirection = -m_enemyDirection;
            }
        }
    }
}
