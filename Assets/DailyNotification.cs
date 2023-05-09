using System;
using System.Collections.Generic;
using Unity.Notifications.Android;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Android;

public class DailyNotification : MonoBehaviour
{
    private List<string> JunkCorpQuotes = new List<string> { "\"You have new daily quotas to complete!\" -Jeremiah",
                                                     "\"That litter isn't gonne pick up itself! Come back and complete your quota.\" -Jeremiah",
                                                     "\"I am in need of your assistance, my model needs more work to be efficient.\"",
                                                     "\"You aren't slacking off at work are you? If you are we have a fresh batch of tasks ready on your desk.\" -Jeremiah"};
    private string NCId = "daily_task_channel";
    private int NId;
    void Start()
    {
        var permissionStatus = new PermissionRequest().Status;
        Debug.Log(permissionStatus);
        if (permissionStatus != PermissionStatus.Allowed)
            return;

        if (string.IsNullOrEmpty(AndroidNotificationCenter.GetNotificationChannel(NCId).Id))
        {
            Debug.Log("RegisterNotificationChannel");
            AndroidNotificationCenter.RegisterNotificationChannel(new AndroidNotificationChannel()
            {
                Id = NCId,
                Name = "Daily Notifications",
                Description = "Notifications when daily tasks reset everyday, interaction reminders, etc.",
                Importance = Importance.High,
            });
        };

        SendOrUpdateNotofication();

        AndroidNotificationCenter.OnNotificationReceived += (data) =>
        {
            if (data.Id == NId)
            {
                Debug.Log("OnNotificationReceived");
                SendOrUpdateNotofication();
            }
        };
    }

    private void SendOrUpdateNotofication()
    {
        NId = PlayerPrefs.GetInt(nameof(NId), -1);
        var status = AndroidNotificationCenter.CheckScheduledNotificationStatus(NId);
        Debug.Log($"{nameof(NId)}: {NId} | {status}");
        if (NId == -1 || status == NotificationStatus.Unknown)
        {
            Debug.Log("SendNotification");
            PlayerPrefs.SetInt(nameof(NId), AndroidNotificationCenter.SendNotification(GetAndroidNotification(), NCId));
        }
        else if (status == NotificationStatus.Scheduled)
        {
            Debug.Log("UpdateScheduledNotification");
            AndroidNotificationCenter.UpdateScheduledNotification(NId, GetAndroidNotification(), NCId);
        }
        else if (status == NotificationStatus.Delivered || status == NotificationStatus.Unavailable)
        {
            Debug.Log("CancelNotification");
            AndroidNotificationCenter.CancelNotification(NId);
            PlayerPrefs.SetInt(nameof(NId), AndroidNotificationCenter.SendNotification(GetAndroidNotification(), NCId));
        }
    }

    private AndroidNotification GetAndroidNotification()
    {
        var fireTime = DateTimeOffset.Now.AddMinutes(1).LocalDateTime;
        Debug.Log("GetAndroidNotification @ " + fireTime);
        var isNarr = PlayerPrefs.GetString("Tag") == "Narr";
        return new AndroidNotification()
        {
            Title = isNarr ? "JunkCorp needs you!" : "Gaaubage",
            Text = isNarr ? JunkCorpQuotes[UnityEngine.Random.Range(0, JunkCorpQuotes.Count)] : "Daily Tasks have been Reset!",
            SmallIcon = isNarr ? "notification_icon" : "",
            LargeIcon = "default",
            FireTime = fireTime,
            ShouldAutoCancel = true,
            RepeatInterval = TimeSpan.FromDays(1),
            ShowTimestamp = true,
        };
    }
}