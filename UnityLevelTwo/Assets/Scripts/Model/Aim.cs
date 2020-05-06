﻿using System;
using UnityEngine;


public sealed class Aim : MonoBehaviour, ICollision, ISelectObj
{
    #region Fields

    public event Action OnPointChange = delegate { };

    [SerializeField] private float _hp = 100;
    private bool _isDead;
    private float _timeToDestroy = 10.0f;

    #endregion


    #region Methods

    // дописать поглощение урона
    public void CollisionEnter(InfoCollision info)
    {
        if (_isDead)
        {
            return;
        }
        if (_hp > 0)
        {
            _hp -= info.Damage;
        }
        if (_hp <= 0)
        {
            if (!TryGetComponent<Rigidbody>(out _))
            {
                gameObject.AddComponent<Rigidbody>();
            }
            Destroy(gameObject, _timeToDestroy);
            OnPointChange.Invoke();
            _isDead = true;
        }
    }

    public string GetMessage()
    {
        return $"{gameObject.name} - {_hp}";
    }

    #endregion
}