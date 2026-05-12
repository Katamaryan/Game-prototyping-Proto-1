using TMPro;
using UnityEngine;

public class Goal : MonoBehaviour
{
    [SerializeField] private GameObject m_TextMeshPro;

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            m_TextMeshPro.SetActive(true);
        }
    }
}
