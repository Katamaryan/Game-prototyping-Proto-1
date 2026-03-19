using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerInput m_inputMap;
    private Rigidbody2D m_rigidbody;

    [SerializeField] private float m_speed;
    [SerializeField] private Vector2 m_playerDirection;
    [SerializeField] private float m_jumpForce;
    [SerializeField] private bool m_groundCheck;
    private Vector3 m_lastDirection;

    [SerializeField] private GameObject m_slashPrefab;
    [SerializeField] private Transform m_firePoint;
    [SerializeField] float m_slashRate;
    private float m_slashTimeout = 0;


    private void Awake()
    {
        m_inputMap = GetComponent<PlayerInput>();
        m_rigidbody = GetComponent<Rigidbody2D>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

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
        if (ctx.performed && Time.time > m_slashTimeout)
        {
            if (m_lastDirection == Vector3.zero)
                m_lastDirection = Vector3.right;

            m_slashTimeout = Time.time + m_slashRate;
            GameObject spawnedSlash = Instantiate(m_slashPrefab, m_firePoint.position + m_lastDirection, Quaternion.identity);
            Debug.Log($"Button_Basic");
        }
    }

    public void Handle_Button_DoubleTap(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            Debug.Log($"Button_DoubleTap");
        }
    }

}
