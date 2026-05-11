using UnityEngine;

public class DoorButton : MonoBehaviour
{
    [SerializeField] public GameObject[] m_doorArray1;
    [SerializeField] public GameObject[] m_doorArray2;
    private bool m_isButtonActive;
    private GameObject m_buttonActivator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
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

    private void OnCollisionExit2D(Collision2D collision)
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
