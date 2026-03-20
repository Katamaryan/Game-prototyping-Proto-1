using System.Collections;
using UnityEngine;

public class Damage : MonoBehaviour
{
    [SerializeField] private GameObject m_damageTarget;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, 0.2f);
    }

    private void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collision2D collision)
    {
        m_damageTarget = collision.gameObject.GetComponent<GameObject>();
    }

}
