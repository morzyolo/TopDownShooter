using System;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponObserver : MonoBehaviour
{
    [SerializeField] private Image _weaponImage;
    [SerializeField] private TMP_Text _bulletsText;

    public void SetData(WeaponBase data, int currentBullets, int spareBullets)
    {
        _weaponImage.sprite = data.WeaponSprite;
        if (currentBullets < 0) _bulletsText.text = "";
        else ChangeBulletsText(currentBullets, spareBullets);
    }

    public void ChangeBulletsText(int currentBullets, int spareBullets)
    {
        _bulletsText.text = String.Format("{0:00}/{1:00}", currentBullets, spareBullets);
    }
}