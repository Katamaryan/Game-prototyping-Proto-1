using System.Collections;
using UnityEngine;

public class Damage : MonoBehaviour
{
    [SerializeField] private GameObject m_damageTarget;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //This destroys the trigger for the player slash attack to be destroyed shortly after being created
        Destroy(gameObject, 0.2f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //gets the enemy that is being damaged
        m_damageTarget = collision.gameObject.GetComponent<GameObject>();
    }

}
