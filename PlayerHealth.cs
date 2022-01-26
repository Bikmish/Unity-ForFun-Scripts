using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    public int Health = 100;
    public TextMeshProUGUI HealthInterface;
    Color InterfaceColor;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HealthInterface.SetText(Health + "");
        if (Health >= 75)
            InterfaceColor = new Color32(7, 255, 0, 255);
        else if (Health >= 35)
            InterfaceColor = new Color32(255, 189, 0, 255);
        else
            InterfaceColor = new Color32(255, 23, 0, 255);

        HealthInterface.color = InterfaceColor;
    }
    public void TakeDamage(int dmg)
    {
        Health -= dmg;
    }
}
