using System;
using System.IO;
using UnityEngine;

public class UtilityMethods
{
    public static void OpenLink(string url, bool useSteamOverlay = true)
    {
        //if (SteamManager.Initialized && useSteamOverlay)
        //    Steamworks.SteamFriends.ActivateGameOverlayToWebPage(url);
        //else
            Application.OpenURL(url);
    }

    public static void OpenPath(string path)
    {
        if (Directory.Exists(path))
            System.Diagnostics.Process.Start(path, "explorer.exe");
        else
            Debug.LogError("Invalid path:" + path);
    }

    public static string GetTimestamp(DateTime dateTime)
    {
        return dateTime.ToString("yyyy-MM-dd_HH-mm-ss_ffff");
    }

    public static string GetTimeDifference(DateTime startDate, DateTime endDate)
    {
        TimeSpan difference = endDate - startDate;

        //if (difference.TotalHours > 6)
            return $"<b>{difference.Days:D2}</b> days<br><b>{difference.Hours:D2}</b> hours <b>{difference.Minutes:D2}</b> minutes <b>{difference.Seconds:D2}</b> seconds";

        //return "<b>Just a few hours</b>";
    }

    public static string GetCurrentTimestamp()
    {
        return GetTimestamp(DateTime.Now);
    }
}
