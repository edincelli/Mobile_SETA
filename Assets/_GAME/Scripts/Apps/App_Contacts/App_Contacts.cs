using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;
using UnityEngine.UI;

public class App_Contacts : AppBase
{
    [Space]
    [SerializeField] private App_Contacts_View_Main contactsMainView;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
    }
}
