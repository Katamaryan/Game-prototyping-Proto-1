using TMPro;
using UnityEngine;

public class Goal : MonoBehaviour
{
    [SerializeField] Canvas m_canvas;
    [SerializeField] private TextMeshPro m_TextMeshPro;

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {

        }
    }
}
