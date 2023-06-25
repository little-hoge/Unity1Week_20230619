using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{

    private SpriteRenderer spriteRenderer;
    private new Rigidbody2D rigidbody2D;
    private float speed;
    void Init()
    {
        speed = Random.Range(4.5f, 6.0f);
        rigidbody2D.velocity = new Vector2(0.0f, -speed);
    }

    private void Awake()
    {
        TryGetComponent(out spriteRenderer);
        TryGetComponent(out rigidbody2D);
        Init();
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
