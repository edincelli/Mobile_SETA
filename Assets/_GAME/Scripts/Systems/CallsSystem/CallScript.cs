using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Call Script")]
public class CallScript : ScriptableObject
{
    public string ID => name;
    public MessageContactData contact;
    public List<CallScript_Stage> stages = new List<CallScript_Stage>();

}
