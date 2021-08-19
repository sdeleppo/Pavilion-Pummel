using UnityEngine;
using System.Collections;

public class WeaponSwitch : MonoBehaviour
{

    //Handling
    public GameObject[] weapons;
    public GameObject currentWeapon;

    //Internal
    int weaponNum = 0;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(1 + "") || Input.GetKeyDown(KeyCode.Joystick1Button2)) { Equip(0); }
        if (Input.GetKeyDown(2 + "") || Input.GetKeyDown(KeyCode.Joystick1Button3)) { Equip(1); }
        if (Input.GetKeyDown(3 + "") || Input.GetKeyDown(KeyCode.Joystick1Button1)) { Equip(2); }
        if (Input.GetAxisRaw("Mouse ScrollWheel") > 0 && weaponNum != 2)
        {
            weaponNum += 1;
            print(weaponNum);
            CheckNum();
        }
        if (Input.GetAxisRaw("Mouse ScrollWheel") < 0 && weaponNum != 0)
        {
            weaponNum -= 1;
            print(weaponNum);
            CheckNum();
        }


    }
    void CheckNum()
    {
        if (weaponNum == 0)
            Equip(0);
        if (weaponNum == 1)
            Equip(1);
        if (weaponNum == 2)
            Equip(2);
    }
    void Equip(int weapon)
    {
        currentWeapon.SetActive(false);

        for (int i = 0; i < weapons.Length; i++)
        {
            if (weapons[i].name == weapons[weapon].name)
            {
                weapons[weapon].SetActive(true);
                currentWeapon = weapons[i];
            }
            else
            {
                weapons[i].SetActive(false);
            }
        }

    }


}
