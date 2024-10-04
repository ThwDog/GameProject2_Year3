using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider) , typeof(EventScript))]
public class CheckCollision : MonoBehaviour
{
    enum type{
        triggerEnter , triggerStay
    }

    EventScript _event;
    [Header("Setting")]
    [SerializeField] type triggerType;

    private void Start() {
        _event = GetComponent<EventScript>();
    }

    private void OnTriggerEnter(Collider other) {
        if(triggerType != type.triggerEnter) return;
            Debug.Log("Enter");
        if(other.gameObject.GetComponent<PlayerController>()) {
            Debug.Log("Enter");
            _event._StartEvent();
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.gameObject.GetComponent<PlayerController>()) {
            _event._ExitEvent();
        }
    }

    private void OnTriggerStay(Collider other) {
        if(triggerType != type.triggerStay) return;
        if(other.gameObject.GetComponent<PlayerController>()) {
            Debug.Log("stay");
            _event._StartEvent();
        }
    }

}
