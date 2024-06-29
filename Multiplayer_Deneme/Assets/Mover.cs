using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    float time=0;
    [SerializeField] float speed;
    public Transform start, end;
    void Update()
    {
        transform.localPosition = Vector3.Lerp(start.position,end.position,(Mathf.Sin(time*360*speed)+1)/2);
    }
}
