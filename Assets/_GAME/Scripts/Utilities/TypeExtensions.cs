using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class TypeExtensions
{
    public static bool ContainsLayer(this LayerMask layerMask, int layer)
    {
        return ((1 << layer) & layerMask.value) != 0;
    }

    public static Vector2 NormalizedScreenPosition(this Vector3 vector)
    {
        return new Vector2(vector.x, vector.y);
    }

    /// <summary>
    /// Optimized way to change object activity
    /// </summary>
    /// <param name="gameObject">Game Object to change</param>
    /// <param name="value">new activity value</param>
    /// <returns>Returns true if activity has changed</returns>
    public static bool SetActiveOptimized(this GameObject gameObject, bool value)
    {
        if (gameObject.activeSelf != value)
        {
            gameObject.SetActive(value);
            return true;
        }

        return false;
    }

    public static bool IsNullOrEmpty<T>(this List<T> list)
    {
        if (list == null)
            return true;

        if (list.Count == 0)
            return true;

        return false;
    }

    public static T GetRandom<T>(this List<T> list)
    {
        if (list.IsNullOrEmpty())
            return default(T);

        return list[Random.Range(0, list.Count)];
    }

    public static void RemoveNulls<T>(this List<T> list) where T : Object
    {
        for (int i = list.Count - 1; i >= 0; i--)
        {
            if (list[i] == null)
            {
                list.RemoveAt(i);
            }
        }
    }

    public static List<T> GetUniqueRandomElements<T>(this List<T> list, int numberOfElements)
    {
        if (list.Count < numberOfElements)
        {
            numberOfElements = list.Count;
        }

        var randomElements = list.OrderBy(x => System.Guid.NewGuid()).Take(numberOfElements).ToList();
        return randomElements;
    }

    public static void RandomizeOrder<T>(this List<T> list)
    {
        int n = list.Count;
        for (int i = n - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            T value = list[i];
            list[i] = list[j];
            list[j] = value;
        }
    }

    public static void ClampInt(ref this int target, int min, int max)
    {
        if (target < min)
            target = min;
        else if (target > max)
            target = max;
    }

    public static void ClampInt(ref this int target, int newValue, int min, int max)
    {
        if (newValue < min)
            target = min;
        else if (newValue > max)
            target = max;
        else
            target = newValue;
    }

    public static void OverlapInt(ref this int target, int min, int max)
    {
        if (target < min)
            target = max;
        else if (target > max)
            target = min;
    }

    public static float PercentInRange(this float target, float min, float max)
    {
        target -= min;
        max -= min;

        if (max == 0)
            return 1;

        return Mathf.Clamp01(target / max);
    } 

    public static void ClampFloat(ref this float target, float min, float max)
    {
        if (target < min)
            target = min;
        else if (target > max)
            target = max;
    }

    public static void ClampFloat(ref this float target, float newValue, float min, float max)
    {
        if (newValue < min)
            target = min;
        else if (newValue > max)
            target = max;
        else
            target = newValue;
    }

    public static void OverlapFloat(ref this float target, float min, float max)
    {
        if (target < min)
            target = max;
        else if (target > max)
            target = min;
    }

    public static float ClampRotationAngle(this float angle)
    {
        while (angle < 0)
            angle += 360;

        while (angle >= 360)
            angle -= 360;

        return angle;
    }

    public static int Percentage01ToInt(this float percent)
    {
        return Mathf.RoundToInt(percent * 100);
    }

    public static bool IsValueInRange(this float value, float min, float max)
    {
        return (min <= value && value <= max) ? true : false;
    }

    public static bool IsValueInRange(this int value, float min, float max)
    {
        return ((float)value).IsValueInRange(min, max);
    }

    public static void DontDestroyOnLoadImproved(this GameObject gameObject)
    {
        gameObject.transform.SetParent(null);
        Object.DontDestroyOnLoad(gameObject);
    }

    public static T Clone<T>(this T source)
    {
        if (source == null)
            return default;

        string serialized = Newtonsoft.Json.JsonConvert.SerializeObject(source);
        return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(serialized);
    }

    public static T Clone<T>(this T source, System.Type inheritedType)
    {
        if (source == null)
            return default;

        if (!typeof(T).IsAssignableFrom(inheritedType))
        {
            Debug.LogError($"{inheritedType} does not inherit from {typeof(T)}");
            return default;
        }

        string serialized = Newtonsoft.Json.JsonConvert.SerializeObject(source);
        return (T)Newtonsoft.Json.JsonConvert.DeserializeObject(serialized, inheritedType);
    }

    public static string BoolToYesNo(this bool value)
    {
        return value ? "yes" : "no";
    }

    public static string BoolToOnOff(this bool value)
    {
        return value ? "on" : "off";
    }

    public static string MinutesFloatToTimeString(this float minutesFloat)
    {
        int minutes = (int)minutesFloat;
        int seconds = (int)((minutesFloat*60) % 60);

        string minutesString = minutes.ToString();
        string secondsString = seconds.ToString();

        if(minutes < 10)
            minutesString = "0" + minutesString;

        if(seconds <10)
            secondsString = "0" + secondsString;

        return $"{minutesString}:{secondsString}";
    }

    public static bool IsNullOrEmpty(this string value)
    {
        return string.IsNullOrEmpty(value);
    }

    public static bool InRandomRange01(this float value)
    {
        return Random.Range(0f, 1f) < value;
    }

    public static Color ToGrayscale(this Color color)
    {
        float grayscale = color.grayscale;
        return new Color(grayscale, grayscale, grayscale, color.a);
    }

    public static void Destroy(this Object objectToDestroy)
    {
        MonoBehaviour.Destroy(objectToDestroy);
    }

    public static void Destroy(this Object objectToDestroy, float timeToDestroy)
    {
        MonoBehaviour.Destroy(objectToDestroy, timeToDestroy);
    }
}
