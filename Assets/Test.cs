using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Test : MonoBehaviour
{
    public TMP_Text textMeshPro;

    private void Update()
    {
        textMeshPro.text = $"TimeZoneInfo.Local: {TimeZoneInfo.Local}" +
            $"\nBaseUtcOffset: {TimeZoneInfo.Local.BaseUtcOffset}" +
            $"\nDaylightName: {TimeZoneInfo.Local.DaylightName}" +
            $"\nDisplayName: {TimeZoneInfo.Local.DisplayName}" +
            $"\nId: {TimeZoneInfo.Local.Id}" +
            $"\nStandardName: {TimeZoneInfo.Local.StandardName}" +
            $"\nSupportsDaylightSavingTime: {TimeZoneInfo.Local.SupportsDaylightSavingTime}" +
            $"\nTimeZoneInfo.Utc: {TimeZoneInfo.Utc}" +
            $"\nBaseUtcOffset: {TimeZoneInfo.Utc.BaseUtcOffset}" +
            $"\nDaylightName: {TimeZoneInfo.Utc.DaylightName}" +
            $"\nDisplayName: {TimeZoneInfo.Utc.DisplayName}" +
            $"\nId: {TimeZoneInfo.Utc.Id}" +
            $"\nStandardName: {TimeZoneInfo.Utc.StandardName}" +
            $"\nSupportsDaylightSavingTime: {TimeZoneInfo.Utc.SupportsDaylightSavingTime}" +
            $"\nDateTime.Now: {DateTime.Now.TimeOfDay}" +
            $"\nDateTime.UtcNow: {DateTime.UtcNow.TimeOfDay}" +
            $"\nDateTimeOffset.Now: {DateTimeOffset.Now.TimeOfDay}" +
            $"\nDateTimeOffset.UtcNow: {DateTimeOffset.UtcNow.TimeOfDay}"; 
    }
}
