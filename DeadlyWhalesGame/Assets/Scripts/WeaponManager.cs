using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public GameObject weapon; 
    private bool isWeaponActive = false; 

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            isWeaponActive = !isWeaponActive; 
            weapon.SetActive(isWeaponActive);
        }
    }
}
