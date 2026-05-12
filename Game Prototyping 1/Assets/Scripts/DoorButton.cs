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
        if (m_isSwordInteractable && collision.gameObject.CompareTag("Player Attack"))
        {
            if (!m_isButtonActive)
            {
                m_buttonActivator = collision.gameObject;
                m_isButtonActive = true;
                for (int i = 0; i < m_doorArray1.Length; i++)
                {
                    m_doorArray1[i].SetActive(false);
                    m_doorArray2[i].SetActive(true);
                }
            }
        }

        if (!m_isSwordInteractable && !collision.gameObject.CompareTag("Player Attack"))
        {
            if (!m_isButtonActive)
            {
                m_buttonActivator = collision.gameObject;
                m_isButtonActive = true;
                for (int i = 0; i < m_doorArray1.Length; i++)
                {
                    m_doorArray1[i].SetActive(false);
                    m_doorArray2[i].SetActive(true);
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (m_isButtonActive && collision.gameObject == m_buttonActivator)
        {
            m_isButtonActive = false;
            for (int i = 0; i < m_doorArray1.Length; i++)
            {
                m_doorArray1[i].SetActive(true);
                m_doorArray2[i].SetActive(false);
            }
        }
    }

}
