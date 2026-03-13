using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerInput m_inputMap;
    private Rigidbody2D m_rigidbody;

    [SerializeField] private float m_speed;
    [SerializeField] private Vector2 m_playerDirection;


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
        m_rigidbody.linearVelocity = m_playerDirection * (m_speed * Time.deltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        m_playerDirection.x = m_inputMap.actions.FindAction("Move").ReadValue<float>();


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
            Debug.Log($"Jump");
        }
    }

    public void Handle_Button_Basic(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
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
