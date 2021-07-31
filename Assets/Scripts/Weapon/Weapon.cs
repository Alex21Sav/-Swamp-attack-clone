using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] private string _lable;
    [SerializeField] private int _price;
    [SerializeField] private Sprite _icon;
    [SerializeField] private bool _isBuyet = false;

    [SerializeField] protected Bullet Bollet;

    public string Lable => _lable;
    public int Price => _price;
    public Sprite Icon => _icon;
    public bool IsBuyet => _isBuyet; 
    public abstract void Shoot(Transform shootPoint);

    public void Buy()
    {
        _isBuyet = true;
    }
}
