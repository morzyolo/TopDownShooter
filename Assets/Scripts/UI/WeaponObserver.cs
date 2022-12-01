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

    private int _magazineSize;
    private int _remainingBullets;

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
        _magazineSize = data.MagazineSize;
        _remainingBullets = weapon.GetRemainingBullets();
        if (_magazineSize < 0)
            _bulletsText.text = "";
        else
            ChangeBulletsText();
    }

    private void UpdateRemainingBullets(int remainingBullets)
    {
        _remainingBullets = remainingBullets;
        ChangeBulletsText();
    }

    private void ChangeBulletsText()
    {
        _bulletsText.text = String.Format("{0:00}/{1:00}", _remainingBullets, _magazineSize);
    }

    private void OnDisable()
    {
        _currentWeapon.Shooted -= UpdateRemainingBullets;
    }
}