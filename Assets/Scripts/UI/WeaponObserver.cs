using System;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponObserver : MonoBehaviour
{
    [SerializeField] private Image _weaponImage;
    [SerializeField] private TMP_Text _bulletsText;

    private Weapon _currentWeapon;

    private int _spareBullets;
    private int _currentBulletsCount;

    public void SetInitialWeapon(Weapon weapon)
    {
        SetData(weapon);
        weapon.Shooted += UpdateRemainingBullets;
        _currentWeapon = weapon;
    }

    public void ChangeWeapon(Weapon weapon)
    {
        SetData(weapon);
        _currentWeapon.Shooted -= UpdateRemainingBullets;
        weapon.Shooted += UpdateRemainingBullets;
        _currentWeapon = weapon;
    }

    private void SetData(Weapon weapon)
    {
        WeaponBase data = weapon.WeaponData;
        _weaponImage.sprite = data.WeaponSprite;
        _spareBullets = data.SpareBullets;
        _currentBulletsCount = weapon.GetCurrentBulletsCount();
        if (_spareBullets < 0) _bulletsText.text = "";
        else ChangeBulletsText();
    }

    private void UpdateRemainingBullets(int remainingBullets)
    {
        _currentBulletsCount = remainingBullets;
        ChangeBulletsText();
    }

    private void ChangeBulletsText()
    {
        _bulletsText.text = String.Format("{0:00}/{1:00}", _currentBulletsCount, _spareBullets);
    }

    private void OnDisable()
    {
        _currentWeapon.Shooted -= UpdateRemainingBullets;
    }
}