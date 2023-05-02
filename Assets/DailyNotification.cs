using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Notifications.Android;
using UnityEngine;
using UnityEngine.Android;

public class DailyNotification : MonoBehaviour
{
    int ID = 6969;
    List<string> JunkCorpQuotes = new List<string> { "\"You have new daily quotas to complete!\" -Jeremiah",
                                                     "\"That litter isn't gonne pick up itself! Come back and complete your quota.\" -Jeremiah",
                                                     "\"I am in need of your assistance, my model needs more work to be efficient.\"",
                                                     "\"You aren't slacking off at work are you? If you are we have a fresh batch of tasks ready on your desk.\" -Jeremiah"};
    // Start is called before the first frame update
    void Start() {
        var channel = new AndroidNotificationChannel() {
            Id = "my_channel",
            Name = "Daily Notifications",
            Description = "Notifications when daily tasks reset everyday, interaction reminders, etc.",
            Importance = Importance.High,
        };

        AndroidNotificationCenter.RegisterNotificationChannel(channel);
        Debug.Log(AndroidNotificationCenter.GetNotificationChannel("my_channel").Description);
        if (!Permission.HasUserAuthorizedPermission("android.permission.POST_NOTIFICATIONS")) {
            Permission.RequestUserPermission("android.permission.POST_NOTIFICATIONS");
        }

        var status = AndroidNotificationCenter.CheckScheduledNotificationStatus(ID) == NotificationStatus.Scheduled ? NotificationStatus.Scheduled : NotificationStatus.Unknown;
        if (status != NotificationStatus.Scheduled) {
            AndroidNotificationCenter.SendNotificationWithExplicitID(newNotification(), "my_channel", ID);
        }
        var notification = newNotification();
        notification.Text = PlayerPrefs.GetString("Tag") == "Narr" ? JunkCorpQuotes[UnityEngine.Random.Range(0, JunkCorpQuotes.Count)] : "Daily Tasks have been Reset!";
        AndroidNotificationCenter.UpdateScheduledNotification(ID, notification, "my_channel");
    }

    AndroidNotification newNotification() {
        // Register the notification again
        var notification = new AndroidNotification();
        notification.Title = PlayerPrefs.GetString("Tag") == "Narr" ? "JunkCorp needs you!" : "Gaaubage";
        notification.Text = PlayerPrefs.GetString("Tag") == "Narr" ? JunkCorpQuotes[UnityEngine.Random.Range(0, JunkCorpQuotes.Count)] : "Daily Tasks have been Reset!";

        notification.SmallIcon = PlayerPrefs.GetString("Tag") == "Narr" ? "notification_icon" : "";
        notification.LargeIcon = PlayerPrefs.GetString("Tag") == "Narr" ? "icon_0" : "";

        //DateTime notificationTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 11, 00, 0);
        //Debug.Log(notificationTime);
        //if (TimeZoneInfo.Local.IsDaylightSavingTime(notificationTime)) {
        //    Debug.Log(notificationTime);
        //    notificationTime = notificationTime.AddHours(1);
        //    Debug.Log(notificationTime);
        //}
        notification.FireTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 11, 00, 0);
        //notification.FireTime = DateTime.Now.AddSeconds(30);
        notification.ShowTimestamp = true;

        //Debug.Log(notification.FireTime);
        //Debug.Log("Now " + DateTime.Now);
        //Debug.Log("Now " + DateTimeOffset.Now);
        //Debug.Log("Now-1h " + DateTime.Now.AddHours(-1));
        //Debug.Log("NowtoLocalTime " + DateTime.Now.ToLocalTime());
        //Debug.Log("NowUTC " + DateTime.UtcNow);

        //Debug.Log("TimeZoneTest " + TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.Local));
        notification.RepeatInterval = new TimeSpan(1, 0, 0, 0); // repeat every 24 hours
        notification.ShouldAutoCancel = true;
        return notification;
        //PlayerPrefs.SetInt("ScheduledNotification", AndroidNotificationCenter.SendNotificationWithExplicitID(notification, "default_channel", 6969));

        //if (AndroidNotificationCenter.CheckScheduledNotificationStatus(ID) == NotificationStatus.Scheduled) {
        //    AndroidNotificationCenter.CancelAllScheduledNotifications();
        //    AndroidNotificationCenter.SendNotification(notification, "my_channel");
        //}
    }

}
