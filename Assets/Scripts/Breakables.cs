using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakables : MonoBehaviour
{
    public GameObject[] brokenPieces;
    public int maxPieces = 5;

    public bool shouldDropItem;
    public GameObject[] itemsToDrop;
    public float itemDropPercent;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player" || collision.gameObject.tag =="PlayerBullet")
        {
            if(PlayerController.instance.dashCounter > 0)
            {
                Destroy(gameObject);

                AudioManager.instance.PlaySFX(0);

                // Show broken pieces
                int piecesToDrop = Random.Range(3, maxPieces);

                
                for(int i = 0; i < piecesToDrop; i++)
                {
                    int randomPiece = Random.Range(0, brokenPieces.Length);
                    Instantiate(brokenPieces[randomPiece], transform.position, transform.rotation);
                }

                // drop items
                if (shouldDropItem)
                {
                    float dropChance = Random.Range(0, 100f);

                    if(dropChance < itemDropPercent)
                    {
                        int randomItem = Random.Range(0, itemsToDrop.Length);

                        Instantiate(itemsToDrop[randomItem], transform.position, transform.rotation);
                    }
                }
                
            }
            
        }
    }
}
