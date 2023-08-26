using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorBegin : MonoBehaviour
{
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void OnTriggerEnter2D(Collider2D col)
    {
        Player player = col.GetComponent<Player>();
        if (player)
        {
            animator.enabled = true;
        }

    }
}
