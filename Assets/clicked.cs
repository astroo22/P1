using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class clicked : MonoBehaviour
{
    [SerializeField] UnityEvent anEvent;
    private void OnMouseDown()
    {
        anEvent.Invoke();
    }
    public void EventClick()
    {
        print("wut");
    }
}
