using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    [SerializeField]
    
    Animator _avatar;
    [SerializeField]
    Animator view;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _avatar.SetTrigger("Death");
            view.SetTrigger("Death");
        }
    }
}
