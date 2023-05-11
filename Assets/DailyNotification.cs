using System;
using System.Collections.Generic;
using Unity.Notifications.Android;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Android;

public class DailyNotification : MonoBehaviour
{
    private readonly string[] JunkCorpQuotes = new string[]{ "\"You have new daily quotas to complete!\" -Jeremiah",
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

        var version = PlayerPrefs.GetString(nameof(Application.version), string.Empty);
        if (!version.Equals(Application.version))
        {
            Debug.Log("New version; purge notifcations");
            PlayerPrefs.SetString(nameof(Application.version), Application.version);
            AndroidNotificationCenter.CancelAllNotifications();
            foreach (var nc in AndroidNotificationCenter.GetNotificationChannels())
            {
                AndroidNotificationCenter.DeleteNotificationChannel(nc.Id);
            }
        }

        var notificationChannel = AndroidNotificationCenter.GetNotificationChannel(NCId);
        if (!NCId.Equals(notificationChannel.Id))
        {
            Debug.Log("RegisterNotificationChannel");
            AndroidNotificationCenter.RegisterNotificationChannel(new AndroidNotificationChannel()
            {
                Id = NCId,
                Name = "Daily Notifications",
                Description = "Notifications when daily tasks reset everyday, interaction reminders, etc.",
                Importance = Importance.High,
                CanBypassDnd = true,
                CanShowBadge = true,
                EnableLights = true,
                EnableVibration = true,
                LockScreenVisibility = LockScreenVisibility.Public,
                VibrationPattern = new long[] { 0, 10, 10, 20, 20, 30, 30 }

            });
            notificationChannel = AndroidNotificationCenter.GetNotificationChannel(NCId);
        }
        Debug.Log(notificationChannel.Id + " | " + notificationChannel.Name);

        SendOrUpdateNotofication();

        AndroidNotificationCenter.OnNotificationReceived += (data) =>
        {
            Debug.Log("OnNotificationReceived");
            SendOrUpdateNotofication();
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
        var fireTime = DateTime.Today.AddHours(11);
        if (DateTime.Now > fireTime)
            fireTime = fireTime.AddDays(1);

        Debug.Log("GetAndroidNotification @ " + fireTime);
        var isNarr = PlayerPrefs.GetString("Tag") == "Narr";
        return new AndroidNotification()
        {
            Title = isNarr ? "JunkCorp needs you!" : "Gaaubage",
            Text = isNarr ? JunkCorpQuotes[UnityEngine.Random.Range(0, JunkCorpQuotes.Length)] : "Daily Tasks have been Reset!",
            SmallIcon = isNarr ? "junk_small" : "gaaubage_small",
            LargeIcon = isNarr ? "junk_large" : "gaaubage_large",
            FireTime = fireTime,
            ShouldAutoCancel = true,
            RepeatInterval = TimeSpan.FromDays(1),
            ShowTimestamp = true,
        };
    }
}