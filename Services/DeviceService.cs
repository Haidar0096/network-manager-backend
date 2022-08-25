using LanguageExt;
using NetworkManagerApi.Models;
using NetworkManagerApi.Repositories;
using System.Collections.Generic;

namespace NetworkManagerApi.Services
{
    public class DeviceService
    {
        private readonly IDeviceRepository _devicesRepository;

        public DeviceService(IDeviceRepository repository) => _devicesRepository = repository;

        public Either<string, IEnumerable<Device>> GetAllDevices() => _devicesRepository.GetAllDevices();

        public Either<string, IEnumerable<Device>> GetDevicesByName(string deviceName, bool exactMatch)
        {
            if (deviceName == null)
            {
                return "The device name cannot be empty";
            }
            return _devicesRepository.GetDevicesByName(deviceName, exactMatch);
        }

        public Either<string, IEnumerable<int>> GetDeviceIds()
        {
            return _devicesRepository.GetDeviceIds();
        }

        public Either<string, IEnumerable<Device>> GetDevicesByNamePaginated(string deviceName, int offset, int count, bool exactMatch)
        {
            if (deviceName == null)
            {
                return "The device name cannot be empty";
            }
            if (offset < 0)
            {
                return "The offset cannot be empty or negative";
            }
            if (count == 0)
            {
                return "The count cannot be empty, zero, or less than zero";
            }
            return _devicesRepository.GetDevicesByNamePaginated(deviceName, offset, count, exactMatch);
        }

        public Either<string, IEnumerable<Device>> GetDevicesPaginated(int offset, int count)
        {
            if (offset < 0)
            {
                return "The offset cannot be empty or negative";
            }
            if (count == 0)
            {
                return "The count cannot be empty, zero, or less than zero";
            }

            return _devicesRepository.GetDevicesPaginated(offset, count);
        }

        public Either<string, int> AddDevice(string name)
        {
            if (name == null)
            {
                return "Device name cannot be null";
            }
            return _devicesRepository.AddDevice(name);
        }

        public Option<string> UpdateDevice(int id, string name)
        {
            // check if the updated name is empty
            if (name.Length == 0)
            {
                return "The updated device name cannot be empty";
            }
            // check if the device exists before updating
            var failureMessageOption = _devicesRepository
                .GetDeviceById(id)
                .BiFold<Option<string>>(
                    null,
                    (_, deviceOption) =>
                        deviceOption.BiFold<Option<string>>(
                            null,
                            (__, device) => Option<string>.None,
                            (__, none) => "The device to update does not exist."
                            ),
                    (_, failureMessage) => failureMessage
                );
            return failureMessageOption.BiFold<Option<string>>(
                null,
                (_, failureMessage) => failureMessage,
                (_, none) => _devicesRepository.UpdateDevice(id, name)
            );
        }

        public Option<string> DeleteDevice(int id) => _devicesRepository.DeleteDevice(id);

        public Either<string, Option<Device>> GetDeviceById(int id) => _devicesRepository.GetDeviceById(id);

        public Either<string, int> GetDevicesCount() => _devicesRepository.GetDevicesCount();

        public Either<string, int> GetDevicesCountForName(string deviceName, bool exactMatch) =>
            _devicesRepository.GetDevicesCountForName(deviceName, exactMatch);
    }
}