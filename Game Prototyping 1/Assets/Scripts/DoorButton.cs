using UnityEngine;

public class DoorButton : MonoBehaviour
{
    [SerializeField] public GameObject[] m_doorArray1;
    [SerializeField] public GameObject[] m_doorArray2;
    private bool m_isButtonActive;
    private GameObject m_buttonActivator;
    [SerializeField] private bool m_isSwordInteractable;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //this checks if the button can be activated by the player's sword so the button can only be activated by the player's thrown sword
        if (m_isSwordInteractable && collision.gameObject.CompareTag("Player Attack"))
        {
            //checks if the button is not active and if it is then it activates the button's affect and activates door array 1 and deactivates door array 2
            if (!m_isButtonActive)
            {
                m_buttonActivator = collision.gameObject;
                m_isButtonActive = true;
                for (int i = 0; i < m_doorArray1.Length; i++)
                {
                    m_doorArray1[i].SetActive(false);
                }
                for (int i = 0; i < m_doorArray2.Length; i++)
                { 
                    m_doorArray2[i].SetActive(true);
                }
            }
        }

        //this checks if the button can be activated by the player's sword so the button can only be activated by the player's thrown sword
        if (!m_isSwordInteractable && !collision.gameObject.CompareTag("Player Attack"))
        {
            //checks if the button is not active and if it is then it activates the button's affect and activates door array 1 and deactivates door array 2
            if (!m_isButtonActive)
            {
                m_buttonActivator = collision.gameObject;
                m_isButtonActive = true;
                for (int i = 0; i < m_doorArray1.Length; i++)
                {
                    m_doorArray1[i].SetActive(false);
                }
                for (int i = 0; i < m_doorArray2.Length; i++)
                {
                    m_doorArray2[i].SetActive(true);
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //checks if the button is already activated and if the object that is exitting the trigger is the object that activated is the one exitting the trigger
        if (m_isButtonActive && collision.gameObject == m_buttonActivator)
        {
            m_isButtonActive = false;
            for (int i = 0; i < m_doorArray1.Length; i++)
            {
                m_doorArray1[i].SetActive(true);
            }
            for (int i = 0; i < m_doorArray2.Length; i++)
            {
                m_doorArray2[i].SetActive(false);
            }
        }
    }

}
