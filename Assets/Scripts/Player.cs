using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;
    [Min(0f)]
    public float movingPower = 1000f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        rb.AddForce(new Vector2(100, 10).normalized * GetDirection() * movingPower);
    
      
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.name == "Floor")
        {
            Destroy(gameObject);
        }
    }

    float GetDirection()
    {

        float movingDirection = Input.GetAxisRaw("Horizontal") * Time.deltaTime;//.normalized for pc
        return movingDirection;
    }
}
