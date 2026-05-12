using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.InputSystem;

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
        if (Time.time > m_destroyTimer && m_collision == null)
        {
            Destroy(gameObject);
            m_swordHeld.SetActive(true);
            m_playerController.m_isSwordHeld = true;
        }
        if (m_collision != null)
            m_rigidbody.linearVelocity = Vector3.zero;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player") && !collision.gameObject.CompareTag("Enemy"))
        { 
            m_collision = collision.gameObject;
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
            m_swordHeld.SetActive(true);
            m_playerController.m_isSwordHeld = true;
        }
    }

}
