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
