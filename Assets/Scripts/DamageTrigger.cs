using UnityEngine;

public class DamageTrigger : MonoBehaviour
{
    [SerializeField] PlayerHealth _playerHealth;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ObjectTrigger")) 
        {
            _playerHealth.TakeDamage();
        }
    }
}
