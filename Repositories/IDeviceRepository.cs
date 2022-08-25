using LanguageExt;
using NetworkManagerApi.Models;
using System.Collections.Generic;

namespace NetworkManagerApi.Repositories
{
    public interface IDeviceRepository
    {
        /// <summary>
        /// Gets all the devices.
        /// </summary>
        /// <returns>A list of the devices, if successful, or an error message otherwise</returns>
        Either<string, IEnumerable<Device>> GetAllDevices();

        /// <summary>
        /// Gets all the devices that have their name equal to the provided name.
        /// </summary>
        /// <returns>A list of the devices with the provided name, if successful, or an error message otherwise</returns>
        Either<string, IEnumerable<Device>> GetDevicesByName(string name, bool exactMatch);

        /// <summary>
        /// Gets a list with the ids of all the devices.
        /// </summary>
        /// <returns>A list of ids of the devices, if successful, or an error message otherwise</returns>
        Either<string, IEnumerable<int>> GetDeviceIds();

        /// <summary>
        /// Gets all the devices that have their name equal to the provided name, in a paginated manner.
        /// </summary>
        /// <returns>A (paginated) list of the devices with the provided name, if successful, or an error message otherwise</returns>
        Either<string, IEnumerable<Device>> GetDevicesByNamePaginated(string name, int offset, int count, bool exactMatch);

        /// <summary>
        /// Gets all the devices in the requested page (offset + count).
        /// </summary>
        /// <returns>A list of the devices in the requested page, if successful, or an error message otherwise</returns>
        Either<string, IEnumerable<Device>> GetDevicesPaginated(int offset, int count);

        /// <summary>
        /// Adds a device to the existing devices.
        /// </summary>
        /// <param name="name">The name of the device to add</param>
        /// <returns>The Id of the inserted device, if successful, or an error message otherwise</returns>
        Either<string, int> AddDevice(string name);

        /// <summary>
        /// Updates an existing device.
        /// </summary>
        /// <param name="id">The Id of the device to update</param>
        /// <param name="name">The updated name of the device</param>
        /// <returns>An empty option if successful, or an error message otherwise</returns>
        Option<string> UpdateDevice(int id, string name);

        /// <summary>
        /// Deletes the device with the specified Id.
        /// </summary>
        /// <param name="id">The Id of the device to delete</param>
        /// <returns>An empty option if successful, or an error message otherwise</returns>
        Option<string> DeleteDevice(int id);

        /// <summary>
        /// Finds the device with the provided Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>If successful, the device, or None if it does not exist, or an error message if an error happens.</returns>
        Either<string, Option<Device>> GetDeviceById(int id);

        /// <summary>
        /// Returns the total number of devices.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>If successful, the total number of devices, or an error message if an error happens.</returns>
        Either<string, int> GetDevicesCount();

        /// <summary>
        /// Returns the total number of devices with the provided name.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>If successful, the total number of devices, or an error message if an error happens.</returns>
        Either<string, int> GetDevicesCountForName(string deviceName, bool exactMatch);
    }
}
