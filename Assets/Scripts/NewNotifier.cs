using System.Collections;
using System.Collections.Generic;
using Unity.Notifications.Android;
using UnityEngine;

public class NewNotifier : MonoBehaviour
{
    private const string ChannelId = "MyChannel";
    //private const string NotificationId = "DailyNotification";

    private void Start() {
        // Create a notification channel

        AndroidNotificationChannel notificationChannel = new AndroidNotificationChannel() {
            Id = ChannelId,
            Name = "Interaction Reminders",
            Description = "Daily reminders when daily tasks reset.",
            Importance = Importance.High
        };
        AndroidNotificationCenter.RegisterNotificationChannel(notificationChannel);

        // Schedule the notification if it is not already scheduled
        if (AndroidNotificationCenter.CheckScheduledNotificationStatus(PlayerPrefs.GetInt("notificationID")) != NotificationStatus.Scheduled) {
            ScheduleNotification();
        }
    }

    private void ScheduleNotification() {

        var isNarr = PlayerPrefs.GetString("Tag") == "Narr";
        // Create the notification
        AndroidNotification notification = new AndroidNotification() {
            Title = isNarr ? "JunkCorp needs you!" : "Gaaubage",
            Text = isNarr ? "\"There are new tasks for you to complete!\" -Jeremiah" : "Daily Tasks have been Reset!",
            SmallIcon = isNarr ? "junk_small" : "gaaubage_small",
            LargeIcon = isNarr ? "junk_large" : "gaaubage_large",
            FireTime = GetNextNotificationTime(),
            ShouldAutoCancel = true,
        };

        // Schedule the notification
        PlayerPrefs.SetInt("notificationID", AndroidNotificationCenter.SendNotification(notification, ChannelId));
    }

    private System.DateTime GetNextNotificationTime() {
        // Get the current date and time
        System.DateTime now = System.DateTime.Now;

        // Set the notification time to 12:00 PM
        System.DateTime notificationTime = System.DateTime.Today.AddHours(11);
        //System.DateTime notificationTime = System.DateTime.Today.AddHours(14).AddMinutes(13);

        // If the notification time has already passed, schedule it for the next day
        if (now > notificationTime) {
            notificationTime = notificationTime.AddDays(1);
        }

        return notificationTime;
    }
}

