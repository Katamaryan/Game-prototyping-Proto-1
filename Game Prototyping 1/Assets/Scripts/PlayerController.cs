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
    [SerializeField] GameObject m_MoveMessage, m_attackMessage;
    private bool m_isFirstMove = true, m_isFirstAttack = true;

    private void Awake()
    {
        m_inputMap = GetComponent<PlayerInput>();
        m_rigidbody = GetComponent<Rigidbody2D>();
        m_swordHeld = GameObject.FindWithTag("Sword Held").gameObject;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //changes the held sword's position to the right upon starting and makes sure it is seen as held by the rest of the code
        m_swordHeld.transform.position = transform.position + Vector3.right;
        m_isSwordHeld = true;
    }

    private void FixedUpdate()
    {
        //moves the player in the direction of input and ensures and makes sure it is tied to the framerate
        m_rigidbody.linearVelocityX = m_playerDirection.x * (m_speed * Time.deltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        //changes the last direction the player was facing and moves the held sword to that side
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
        }
        if (ctx.canceled)
        {
            m_playerDirection.x = m_inputMap.actions.FindAction("Move").ReadValue<float>();
        }
        if (ctx.canceled && m_isFirstMove)
        {
            m_isFirstMove = false;
            m_MoveMessage.SetActive(false);
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
        //spawns a slash prefab object if the input is pressed and the cooldown is over then reactivates the cooldown
        if (ctx.performed && Time.time > m_slashTimeout && m_isSwordHeld == true)
        {

            if (m_lastDirection == Vector3.zero)
                m_lastDirection = Vector3.right;

            m_slashTimeout = Time.time + m_slashRate;
            GameObject spawnedSlash = Instantiate(m_slashPrefab, m_firePoint.position + m_lastDirection * 1.2f, Quaternion.identity);

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
        }

        if (ctx.performed && m_isSwordHeld == true && Time.time > m_throwTimeout)
        {
            //makes it so the sword is no longer held by the player and hides the held version of the sword from view also shoots the thrown version of the sword in the last faced direction
            m_isSwordHeld = false;
            m_swordHeld.SetActive(false);
            if (m_lastDirection == Vector3.zero)
                m_lastDirection = Vector3.right;

            m_spawnedSwordThrown = Instantiate(m_swordThrownPrefab, m_firePoint.position + m_lastDirection * 1.5f, Quaternion.identity);
            Rigidbody2D spawnedSwordrb = m_spawnedSwordThrown.GetComponent<Rigidbody2D>();

            if (spawnedSwordrb != null)
                spawnedSwordrb.AddForce(m_lastDirection * m_throwSpeed, ForceMode2D.Impulse);
        }

        if (ctx.canceled && m_isFirstAttack)
        {
            m_isFirstAttack = false;
            m_attackMessage.SetActive(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //respawns the player when hitting an enemy 
        if (collision.gameObject.CompareTag("Enemy"))
        {
            transform.position = m_spawnPoint.position;
        }
        //checks the player is grounded
        if (collision.gameObject.CompareTag("Floor") || collision.gameObject.CompareTag("Player Attack"))
        {
            m_isTouchingFloor = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor") || collision.gameObject.CompareTag("Player Attack"))
        {
            m_isTouchingFloor = false;
        }
    }

}
