using System.Collections;
using System.Collections.Generic;
using Unity.Notifications.Android;
using UnityEngine;
using UnityEngine.Android;

public class NotificationTest : MonoBehaviour
{
    public void Start() {
        var channel = new AndroidNotificationChannel() {
            Id = "channel_id",
            Name = "Default Channel",
            Importance = Importance.High,
            Description = "Generic notifications",
        };
        AndroidNotificationCenter.RegisterNotificationChannel(channel);


        if (!Permission.HasUserAuthorizedPermission("android.permission.POST_NOTIFICATIONS")) {
            Permission.RequestUserPermission("android.permission.POST_NOTIFICATIONS");
        }
        var notification = new AndroidNotification();
        notification.Title = "Your Title";
        notification.Text = "Your Text";
        notification.SmallIcon = "";
        notification.LargeIcon = "icon0";
        notification.ShowTimestamp = true;

        notification.FireTime = System.DateTime.Now.AddSeconds(30);

        var id = AndroidNotificationCenter.SendNotification(notification, "channel_id");

        if (AndroidNotificationCenter.CheckScheduledNotificationStatus(id)== NotificationStatus.Scheduled) {
            AndroidNotificationCenter.CancelAllScheduledNotifications();
            AndroidNotificationCenter.SendNotification(notification, "channel_id");
        }
    }

}
