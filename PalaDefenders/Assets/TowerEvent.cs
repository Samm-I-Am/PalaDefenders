using UnityEngine;
using UnityEngine.Events;

public class TowerEvent : MonoBehaviour
{

    [SerializeField] private UnityEvent myTrigger;

    private void OnTriggerEnter(Collider other)
    {
        myTrigger.Invoke();
    }


}
