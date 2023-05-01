using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Notifications.Android;
using UnityEngine;
using UnityEngine.Android;

public class DailyNotification : MonoBehaviour
{

    // Start is called before the first frame update
    void Start() {
        if (!Permission.HasUserAuthorizedPermission("android.permission.POST_NOTIFICATIONS")) {
            Permission.RequestUserPermission("android.permission.POST_NOTIFICATIONS");
        }

        var channel = new AndroidNotificationChannel() {
            Id = "my_channel",
            Name = "My Channel",
            Description = "My Channel Description",
            Importance = Importance.Default,
            CanBypassDnd = false,
            CanShowBadge = true,
            EnableLights = true,
            EnableVibration = true,
            LockScreenVisibility = LockScreenVisibility.Public
        };

        AndroidNotificationCenter.RegisterNotificationChannel(channel);
        string lastScheduledDate = PlayerPrefs.GetString("LastScheduledDate");
        string currentDate = DateTime.Now.ToString("yyyy-MM-dd");
        if (lastScheduledDate != currentDate) {
            var notification = new AndroidNotification() {
                Title = PlayerPrefs.GetString("Tag") == "Narr" ? "JunkCorp needs you!": "Daily Tasks have been Reset!",
                Text = PlayerPrefs.GetString("Tag") == "Narr" ? "JunkCorp needs you!" : "Daily Tasks have been Reset!",
                SmallIcon = "notification_icon",
                LargeIcon = "notification_icon",
                FireTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 12, 0, 0),
                RepeatInterval = new TimeSpan(1, 0, 0, 0) // repeat every 24 hours
            };
            AndroidNotificationCenter.SendNotification(notification, "my_channel");
            PlayerPrefs.SetString("LastScheduledDate", currentDate);
        }

    }

}
