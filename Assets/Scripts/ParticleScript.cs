using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ParticleScript : MonoBehaviour
{
    [SerializeField] float rayRange = .5f;
    RaycastHit2D upRayToMakeWhite;
    SpriteRenderer sr;
    bool alreadyLighted = false;
    [SerializeField] Color thisObjectColor;
    public GameObject stoneParticle;
    Color lightBlue = new Color(0.1f,0.4f,0.6f);
    void Start()
    {
        this.thisObjectColor = this.GetComponent<SpriteRenderer>().color;
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < 1000; i++)
        {
            WaterLavaSpring.GetCurrentParticles()++;
        }
        Debug.Log("A crescut la o mie:" + WaterLavaSpring.GetCurrentParticles());
        ShouldBeLightBlue();
    }

    private void ShouldBeLightBlue()
    {
        float rayOffsetOnY =.5f;
        Vector2 origin = new Vector2(transform.position.x, transform.position.y + rayOffsetOnY);
        upRayToMakeWhite = Physics2D.Raycast(origin, new Vector2(0f, 1f), rayRange);
        Debug.DrawRay(origin, new Vector2(0f, 1f), Color.red);
        if (upRayToMakeWhite.collider != null)
        {
            if (upRayToMakeWhite.collider.name == "WaterParticle(Clone)" || upRayToMakeWhite.collider.name == "LavaParticle(Clone)")
                ResetColor();
        }
        else
        {
            if(!alreadyLighted)
                 MakeLighter();

        }
            
       
    }
    private void MakeLighter()
    {
        if (this.name == "WaterParticle(Clone)")
            sr.color = new Color32(102,147,221,255); 
        else
            sr.color = new Color32(220,13,28,255);

        alreadyLighted = true;
    }
    private void ResetColor()
    {
        sr.color = thisObjectColor;
        alreadyLighted = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 spawnLocation = (this.transform.position + collision.transform.position) / 2;
        if(this.name == "LavaParticle(Clone)" && collision.collider.name == "WaterParticle(Clone)")
        {
            Instantiate(stoneParticle, spawnLocation, Quaternion.identity);
            WaterLavaSpring.GetCurrentParticles()++;
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
            
            
            
        }
        else if (this.name == "WaterParticle(Clone)" && collision.collider.name == "LavaParticle(Clone)")
        {
            Instantiate(stoneParticle, spawnLocation, Quaternion.identity);
            //DeleteOneObjectInList("WaterParticle(Clone)");
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        OnCollisionEnter2D(collision);
    }
 
}
