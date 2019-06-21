using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTimer : MonoBehaviour
{
    [SerializeField] private float timer = 0f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, timer);
    }

    
}
