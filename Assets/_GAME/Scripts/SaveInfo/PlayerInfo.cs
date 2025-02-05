using Sirenix.Serialization;
using System;
using System.Collections.Generic;

[Serializable]
public class PlayerInfo
{
    public string playerID = "game";
    public string gameVersion;
    public int saveFileVersion = 0;

    public float playTime = 0; //in minutes
    public List<MessageInfoStack> messageStacks = new List<MessageInfoStack>();
}
