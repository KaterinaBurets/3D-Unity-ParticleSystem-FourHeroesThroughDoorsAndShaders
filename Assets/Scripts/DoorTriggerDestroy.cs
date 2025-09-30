using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTriggerDestroy : MonoBehaviour
{
    [SerializeField] private GameObject _hero;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == _hero)
            this.gameObject.SetActive(false);
    }
}