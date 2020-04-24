using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using static Xamarin.Essentials.Permissions;

namespace WeatherApp.Helpers
{
    public class DeviceLocation
    {
        /// <summary>Gets the location asynchronously using geo coordinates from the device.</summary>
        /// <returns></returns>
        public async static Task<Location> GetLocationAsync()
        {
            var status = await CheckAndRequestPermissionAsync(new Permissions.LocationWhenInUse());
            if (status != PermissionStatus.Granted)
            {
                // Return null and Notify user permission was denied
                return null;
            }

            var location = await Geolocation.GetLocationAsync();
            return location;
        }

        private static async Task<PermissionStatus> CheckAndRequestPermissionAsync(BasePermission permission)
        {
            var status = await permission.CheckStatusAsync();
            if (status != PermissionStatus.Granted)
            {
                status = await permission.RequestAsync();
            }
            return status;
        }
    }
}