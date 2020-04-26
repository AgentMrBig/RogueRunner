using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{

    public static PlayerHealthController instance;
    //public GameObject player;

    public int currentHealth;
    public int maxHealth = 100;

    public float noDmgLength = 1f;
    public float noDmgCount;

    public float deathScreenFadeLength = 1f;
    public float deathScreenFadeCount;

    public GameObject deathSplatter;
    public GameObject deathPuddle;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        UIController.instance.healthSlider.maxValue = maxHealth;
        UIController.instance.healthSlider.value = currentHealth;
        UIController.instance.healthText.text = currentHealth.ToString() + " / " + maxHealth.ToString();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(noDmgCount > 0)
        {
            noDmgCount -= Time.deltaTime;
            if(noDmgCount <= 0)
            {
                PlayerController.instance.bodySR.color = new Color(1, 1f, 1f, 1f);
            }
        }

        if (PlayerController.instance.gameObject.activeInHierarchy == false)
        {
            UIController.instance.deathScreen.SetActive(true);
            deathScreenFadeCount -= Time.deltaTime;
            UIController.instance.deathScreenImg.color = new Color(0, 0, 0, +deathScreenFadeLength);
            UIController.instance.deathScreenTxt.color = new Color(1, 0, 0, +deathScreenFadeLength);
            if (deathScreenFadeCount <= 0)
            {
                UIController.instance.deathScreenImg.color = new Color(0, 0, 0, 1);
                UIController.instance.deathScreenTxt.color = new Color(1, 0, 0, 1);
            }

        }
    }

    public void DamagePlayer()
    {
        if(noDmgCount <= 0)
        {
            currentHealth--;
            noDmgCount = noDmgLength;

            PlayerController.instance.bodySR.color = new Color(1, 0.5f,0.5f, 0.5f);

            if (currentHealth <= 0)
            {
                Instantiate(deathPuddle,PlayerController.instance.gameObject.transform.position, PlayerController.instance.gameObject.transform.rotation);
                Instantiate(deathSplatter, PlayerController.instance.gameObject.transform.position, PlayerController.instance.gameObject.transform.rotation);

                
                PlayerController.instance.gameObject.SetActive(false);
                
            }

            UIController.instance.healthSlider.value = currentHealth;
            UIController.instance.healthText.text = currentHealth.ToString() + " / " + maxHealth.ToString();

        }

    }

    public void MakeInvicible(float length)
    {
        noDmgCount = length;
        PlayerController.instance.bodySR.color = new Color(1, 1f, 1f, 0.5f);
    }
}
