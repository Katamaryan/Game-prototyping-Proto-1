using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private GameObject m_slashPrefab;
    [SerializeField] private Transform m_firePoint;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Handle_Buton_basic(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            Debug.Log($"Button_Basic");
        }

    }

    public void Handle_Button_Double_Tap(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            Debug.Log($"Button_Double_Tap");
        }
    }
}
