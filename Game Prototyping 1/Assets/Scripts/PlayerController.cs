using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerInput m_inputMap;
    private Rigidbody2D m_rigidbody;
    private GameObject m_swordHeld;

    [SerializeField] private float m_speed;
    [SerializeField] private Vector2 m_playerDirection;
    [SerializeField] private float m_jumpForce;
    [SerializeField] private bool m_groundCheck;
    private Vector3 m_lastDirection;

    [SerializeField] private GameObject m_slashPrefab;
    [SerializeField] private GameObject m_swordThrownPrefab;
    [SerializeField] private Transform m_firePoint;
    [SerializeField] private float m_slashRate;
    [SerializeField] private float m_throwSpeed;
    private float m_slashTimeout = 0;
    [SerializeField] private bool m_isSwordHeld;


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
        if (m_rigidbody.linearVelocityY == 0)
            m_groundCheck = true;
        else
            m_groundCheck = false;
    }

    // Update is called once per frame
    void Update()
    {
        m_playerDirection.x = m_inputMap.actions.FindAction("Move").ReadValue<float>();

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
            
            Debug.Log($"Move {ctx.ReadValue<float>()}");
        }
        if (ctx.canceled)
        {
            Debug.Log($"Move {ctx.ReadValue<float>()}");
        }
    }
    public void Handle_Jump(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            if (m_groundCheck)
            {
                m_rigidbody.AddForce(Vector2.up * m_jumpForce, ForceMode2D.Impulse);
                Debug.Log($"Jump");
            }
        }
    }

    public void Handle_Button_Basic(InputAction.CallbackContext ctx)
    {
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
        if (ctx.performed && m_isSwordHeld == true)
        {
            m_isSwordHeld = false;
            m_swordHeld.SetActive(false);
            if (m_lastDirection == Vector3.zero)
                m_lastDirection = Vector3.right;

            GameObject spawnedSwordThrown = Instantiate(m_swordThrownPrefab, m_firePoint.position + m_lastDirection * 2f, Quaternion.identity);
            Rigidbody2D spawnedSwordrb = spawnedSwordThrown.GetComponent<Rigidbody2D>();

            if (spawnedSwordrb != null)
                spawnedSwordrb.AddForce(m_lastDirection * m_throwSpeed, ForceMode2D.Impulse);

            Debug.Log($"Button_DoubleTap");
        }
    }

}
