using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Phone.Tasks;
using WPCordovaClassLib.Cordova;
using WPCordovaClassLib.Cordova.Commands;
using WPCordovaClassLib.Cordova.JSON;

namespace Cordova.Extension.Commands
{
    class Calendar : BaseCommand
    {
        public void createEvent(string options)
        {
            string[] jsOptions = JsonHelper.Deserialize<string[]>(options);
            string heading = jsOptions[0];
            string location = jsOptions[1];
            string from = jsOptions[3];
            string to = jsOptions[4];
            string perex = jsOptions[2];

            SaveAppointmentTask saveAppointmentTask = new SaveAppointmentTask();

            saveAppointmentTask.Subject = heading;
            saveAppointmentTask.Location = location;
            saveAppointmentTask.StartTime = Calendar.UnixTimeStampToDateTime(from);
            saveAppointmentTask.EndTime = Calendar.UnixTimeStampToDateTime(to).AddDays(1);
            saveAppointmentTask.Details = perex;
            saveAppointmentTask.IsAllDayEvent = true;
            saveAppointmentTask.Reminder = Reminder.None;
            saveAppointmentTask.AppointmentStatus = Microsoft.Phone.UserData.AppointmentStatus.Free;
            
            saveAppointmentTask.Show();
        }

        public static DateTime UnixTimeStampToDateTime(string unixTimeStampInMs)
        {
            //strip miliseconds
            double seconds = Double.Parse(unixTimeStampInMs) / 1000;

            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            dtDateTime = dtDateTime.AddSeconds(seconds).ToLocalTime();
            return dtDateTime;
        }
    }
}
