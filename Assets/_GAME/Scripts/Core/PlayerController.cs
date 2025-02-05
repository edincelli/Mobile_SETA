using NUnit.Framework;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : GameSystemComponent
{
    public static PlayerController Instance;

    [ShowInInspector]
    public static PlayerInfo PlayerInfo { get; private set; }
    
    private void Awake()
    {
        Instance = this;
        PlayerInfo = new PlayerInfo()
        {
            playerID = "devTest"
        };
    }

    private void Start()
    {

    }

    private void Update()
    {

    }
}
