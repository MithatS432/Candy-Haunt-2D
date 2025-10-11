using UnityEngine;

public class Enemy : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {

    }
    public void GetDamage(float damage)
    {
        Debug.Log("Enemy took " + damage + " damage!");
    }
}
