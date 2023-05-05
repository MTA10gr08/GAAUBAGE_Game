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
        if (!Permission.HasUserAuthorizedPermission("android.permission.POST_NOTIFICATIONS"))
        {
            Permission.RequestUserPermission("android.permission.POST_NOTIFICATIONS");
        }

        if (AndroidNotificationCenter.GetNotificationChannel(NCId).Id.NullIfEmpty() == null)
        {
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
                SendOrUpdateNotofication();
            }
        };
    }

    private void SendOrUpdateNotofication()
    {
        NId = PlayerPrefs.GetInt(nameof(NId), -1);
        var status = AndroidNotificationCenter.CheckScheduledNotificationStatus(NId);
        if (NId == -1 || status == NotificationStatus.Unknown)
        {
            PlayerPrefs.SetInt(nameof(NId), AndroidNotificationCenter.SendNotification(GetAndroidNotification(), NCId));
        }
        else if(status == NotificationStatus.Scheduled)
        {
            AndroidNotificationCenter.UpdateScheduledNotification(NId, GetAndroidNotification(), NCId);
        }
        else if (status == NotificationStatus.Delivered || status == NotificationStatus.Unavailable)
        {
            AndroidNotificationCenter.CancelNotification(NId);
            PlayerPrefs.SetInt(nameof(NId), AndroidNotificationCenter.SendNotification(GetAndroidNotification(), NCId));
        }
    }

    private AndroidNotification GetAndroidNotification()
    {
        var isNarr = PlayerPrefs.GetString("Tag") == "Narr";
        return new AndroidNotification() {
            Title = isNarr ? "JunkCorp needs you!" : "Gaaubage",
            Text = isNarr ? JunkCorpQuotes[UnityEngine.Random.Range(0, JunkCorpQuotes.Count)] : "Daily Tasks have been Reset!",
            SmallIcon = isNarr ? "notification_icon" : "",
            LargeIcon = "default",
            FireTime = DateTime.Now.Date.AddHours(11),//.AddMinutes(33), //fucked
            ShouldAutoCancel = true,
            //FireTime = DateTime.Now.AddMinutes(1), //fucked
            RepeatInterval = TimeSpan.FromDays(1),
            ShowTimestamp = true,
        };
    }
}