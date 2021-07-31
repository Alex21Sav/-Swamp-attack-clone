using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private List<Weapon> _weapons;
    [SerializeField] private Transform _shootPoint;

    private Weapon _currentWeapon;
    private int _currentHealth;
    private int _currentWeaponNumber;
    private Animator _animator;

    public int Money { get; private set; }

    public event UnityAction<int, int> HealthChanget;
    public event UnityAction<int> MoneyChanget;

    private void Start()
    {
        ChangetWeapon(_weapons[_currentWeaponNumber]);
        _currentHealth = _health;
        _animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            _currentWeapon.Shoot(_shootPoint);
            _animator.Play("Shoot");
        }
    }
    public void ApplyDamage(int damage)
    {
        _currentHealth -= damage;
        HealthChanget?.Invoke(_currentHealth, _health);

        if (_currentHealth < 0)
        {
            Destroy(gameObject);
        }
    }

    //private void OnEnemeDied(int Reward)
    //{
    //    Money += Reward;  
    //}

    public void AddMoney(int money)
    {
        Money += money;
        MoneyChanget?.Invoke(Money);
    }

    public void BuyWeapon(Weapon weapon)
    {
        Money -= weapon.Price;
        MoneyChanget?.Invoke(Money);
        _weapons.Add(weapon);
    }

    public void NextWeapon()
    {
        if (_currentWeaponNumber == _weapons.Count - 1)
        {
            _currentWeaponNumber = 0;
        }
        else
            _currentWeaponNumber++;

        ChangetWeapon(_weapons[_currentWeaponNumber]);
    }

    public void PreviousWeapon()
    {
        if (_currentWeaponNumber == 0)
        {
            _currentWeaponNumber = _weapons.Count - 1;
        }
        else
            _currentWeaponNumber--;

        ChangetWeapon(_weapons[_currentWeaponNumber]);
    }

    private void ChangetWeapon(Weapon weapon)
    {
        _currentWeapon = weapon;
    }
}
