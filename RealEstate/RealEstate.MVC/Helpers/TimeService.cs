using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstate.MVC.Helpers
{
    public static class TimeService
    {
        public static string FormatDuration(DateTime time)
        {
            if (time > DateTime.Now)
            {
                return null;
            }

            var span = DateTime.Now.Subtract(time);
            var days = Math.Floor(span.TotalDays);
            if (days > 0.0)
            {
                return $"{days} day{(days != 1.0 ? "s" : "")} ago";
            }

            var hours = Math.Floor(span.TotalHours);
            if (hours > 0.0)
            {
                return $"{hours} hour{(hours != 1.0 ? "s" : "")} ago";
            }

            var minutes = Math.Floor(span.TotalMinutes);
            if (minutes > 0.0)
            {
                return $"{minutes} minute{(minutes != 1.0 ? "s" : "")} ago";
            }

            var seconds = Math.Floor(span.TotalSeconds);
            if (seconds > 0.0)
            {
                return $"{seconds} second{(seconds != 1.0 ? "s" : "")} ago";
            }

            return "now";
        }



    }
}
