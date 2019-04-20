using System;
using Patterns;
using UnityEngine;

public class Logger : PersistentSingleton<Logger>
{
    //----------------------------------------------------------------------------------------------------------

    #region Log

    public void Log<T>(object log, string colorName = "black", Type param = null)
    {
        if (AreLogsEnabled)
        {
            var context = GetTypeName(typeof(T));
            log = string.Format("[" + context + OpenColor + log + CloseColor + GetTypeName(param), colorName);

            if(Application.isEditor)
                Debug.Log(log);
        }
    }

    #endregion

    //----------------------------------------------------------------------------------------------------------

    #region

    private static string GetTypeName(Type type)
    {
        if (type == null)
            return string.Empty;

        var split = type.ToString().Split(Period);
        var last = split.Length - 1;
        return last > 0 ? split[last] : string.Empty;
    }

    #endregion

    //----------------------------------------------------------------------------------------------------------

    #region Fields and Properties

    private const char Period = '.';
    private const string OpenColor = "]: <color={0}><b>";
    private const string CloseColor = "</b></color>";
    [SerializeField] private bool AreLogsEnabled = true;

    #endregion
}