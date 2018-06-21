using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaController : MonoBehaviour
{
    //References
    public Text manaText;
    public Image manaBar;

    //Variables
    public float manaRechargeRate;
    private float currentMana;

    //Singleton Instance Variable
    private static ManaController instance;
    public static ManaController Instance
    {
        get
        {
            return instance;
        }
    }

    //On Object Awake
    private void Awake()
    {
        //Check Singleton
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    //On Object Destroy (Safeguard)
    public void OnDestroy()
    {
        instance = null;
    }

    //Start Method
    private void Start()
    {
        currentMana = 1f;
    }

    //Get Current Mana Method
    public int getCurrentMana()
    {
        return Mathf.FloorToInt(currentMana * 100);
    }

    //Spend Mana Method
    public void spendMana(int value)
    {
        currentMana -= (value * 0.01f);
    }

    //Update
    private void Update()
    {
        //Recharge Mana
        if (currentMana < 100)
        {
            currentMana = Mathf.Min(currentMana + manaRechargeRate * Time.deltaTime, 1f);

            //Update Mana Bar
            manaBar.fillAmount = currentMana;

            //Update Mana Text
            manaText.text = (Mathf.FloorToInt(currentMana * 100f)).ToString();
        }
    }
}
