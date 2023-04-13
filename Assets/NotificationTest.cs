using System.Collections;
using System.Collections.Generic;
using Unity.Notifications.Android;
using UnityEngine;
using UnityEngine.Android;

public class NotificationTest : MonoBehaviour
{
    public void SendNotification() {
        var channel = new AndroidNotificationChannel() {
            Id = "channel_id",
            Name = "Default Channel",
            Importance = Importance.Default,
            Description = "Generic notifications",
        };
        AndroidNotificationCenter.RegisterNotificationChannel(channel);


        if (!Permission.HasUserAuthorizedPermission("android.permission.POST_NOTIFICATIONS")) {
            Permission.RequestUserPermission("android.permission.POST_NOTIFICATIONS");
        }
        var notification = new AndroidNotification();
        notification.Title = "Your Title";
        notification.Text = "Your Text";
        notification.FireTime = System.DateTime.Now.AddMinutes(1);

        AndroidNotificationCenter.SendNotification(notification, "channel_id");
    }

}
