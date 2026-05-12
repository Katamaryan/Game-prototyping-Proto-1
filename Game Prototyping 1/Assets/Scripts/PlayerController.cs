using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerInput m_inputMap;
    private Rigidbody2D m_rigidbody;
    private GameObject m_swordHeld;
    [SerializeField] private Transform m_spawnPoint;

    [SerializeField] private float m_speed;
    [SerializeField] private Vector2 m_playerDirection;
    [SerializeField] private float m_jumpForce;
    [SerializeField] private bool m_groundCheck;
    private bool m_isTouchingFloor;
    private Vector3 m_lastDirection;

    [SerializeField] private GameObject m_slashPrefab;
    [SerializeField] private GameObject m_swordThrownPrefab;
    [SerializeField] GameObject m_spawnedSwordThrown;
    [SerializeField] private Transform m_firePoint;
    [SerializeField] private float m_slashRate;
    [SerializeField] private float m_throwSpeed;
    private float m_slashTimeout = 0;
    private float m_throwTimeout = 0;
    public bool m_isSwordHeld;


    private void Awake()
    {
        m_inputMap = GetComponent<PlayerInput>();
        m_rigidbody = GetComponent<Rigidbody2D>();
        m_swordHeld = GameObject.FindWithTag("Sword Held").gameObject; /*GetComponentInChildren<Transform>(GameObject.FindWithTag("Sword Held"));*/
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_swordHeld.transform.position = transform.position + Vector3.right;
        m_isSwordHeld = true;
    }

    private void FixedUpdate()
    {
        m_rigidbody.linearVelocityX = m_playerDirection.x * (m_speed * Time.deltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        

        if (m_playerDirection != Vector2.zero)
        {
            m_lastDirection = m_playerDirection;
            m_swordHeld.transform.position = transform.position + m_lastDirection;
        }
    }

    public void Handle_Move(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            m_playerDirection.x = m_inputMap.actions.FindAction("Move").ReadValue<float>();
            Debug.Log($"Move {ctx.ReadValue<float>()}");
        }
        if (ctx.canceled)
        {
            m_playerDirection.x = m_inputMap.actions.FindAction("Move").ReadValue<float>();
            Debug.Log($"Move {ctx.ReadValue<float>()}");
        }
    }
    public void Handle_Jump(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            if ((m_rigidbody.linearVelocityY >= -0.0001f && m_rigidbody.linearVelocityY <= 0.0001f) || (m_isTouchingFloor && m_rigidbody.linearVelocityY >= -0.5f && m_rigidbody.linearVelocityY <= 0.15f))
                m_groundCheck = true;
            else
                m_groundCheck = false;

            if (m_groundCheck)
            {
                m_rigidbody.AddForce(Vector2.up * m_jumpForce, ForceMode2D.Impulse);
                Debug.Log($"Jump");
            }
        }
    }

    public void Handle_Button_Basic(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            if (m_spawnedSwordThrown == null)
                m_isSwordHeld = true; m_swordHeld.SetActive(true);

        }

        if (ctx.performed && Time.time > m_slashTimeout && m_isSwordHeld == true)
        {

            if (m_lastDirection == Vector3.zero)
                m_lastDirection = Vector3.right;

            m_slashTimeout = Time.time + m_slashRate;
            GameObject spawnedSlash = Instantiate(m_slashPrefab, m_firePoint.position + m_lastDirection * 1.2f, Quaternion.identity);
            Debug.Log($"Button_Basic");

        }
    }
  
    public void Handle_Button_DoubleTap(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && m_isSwordHeld == false)
        {
            Destroy(m_spawnedSwordThrown);
            m_isSwordHeld = true;
            m_swordHeld.SetActive(true);
            m_throwTimeout = Time.time + m_slashRate;
            Debug.Log("Destroy thing");
        }

        if (ctx.performed && m_isSwordHeld == true && Time.time > m_throwTimeout)
        {
            
            m_isSwordHeld = false;
            m_swordHeld.SetActive(false);
            if (m_lastDirection == Vector3.zero)
                m_lastDirection = Vector3.right;

            m_spawnedSwordThrown = Instantiate(m_swordThrownPrefab, m_firePoint.position + m_lastDirection * 1.5f, Quaternion.identity);
            Rigidbody2D spawnedSwordrb = m_spawnedSwordThrown.GetComponent<Rigidbody2D>();

            if (spawnedSwordrb != null)
                spawnedSwordrb.AddForce(m_lastDirection * m_throwSpeed, ForceMode2D.Impulse);

            Debug.Log($"Button_DoubleTap");
                
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            transform.position = m_spawnPoint.position;
        }
        if (collision.gameObject.CompareTag("Floor"))
        {
            m_isTouchingFloor = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            m_isTouchingFloor = false;
        }
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Ladder"))
    //    {
    //        m_isTouchingLadder = true;
    //    }
    //}

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Ladder"))
    //    {
    //        m_isTouchingLadder = false;
    //    }
    //}
}
