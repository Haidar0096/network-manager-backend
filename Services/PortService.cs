using LanguageExt;
using NetworkManagerApi.Models;
using NetworkManagerApi.Repositories;
using System.Collections.Generic;

namespace NetworkManagerApi.Services
{
    public class PortService
    {

        private readonly IPortRepository _portsRepository;

        public PortService(IPortRepository repository) => _portsRepository = repository;

        public Either<string, IEnumerable<Port>> GetPortsPaginated(int offset, int count)
        {
            if (offset < 0)
            {
                return "The offset cannot be empty or negative";
            }
            if (count == 0)
            {
                return "The count cannot be empty, zero, or less than zero";
            }

            return _portsRepository.GetPortsPaginated(offset, count);
        }

        public Either<string, IEnumerable<Port>> GetPortsByPortNumberPaginated(int portNumber, int offset, int count, bool exactMatch)
        {
            if (offset < 0)
            {
                return "The offset cannot be negative";
            }
            if (count == 0)
            {
                return "The count cannot be zero, or less than zero";
            }
            return _portsRepository.GetPortsByPortNumberPaginated(portNumber, offset, count, exactMatch);
        }
        public Either<string, int> GetPortsCount() => _portsRepository.GetPortsCount();

        public Either<string, int> GetPortsCountForPortNumber(int portNumber, bool exactMatch) =>
            _portsRepository.GetPortsCountForPortNumber(portNumber, exactMatch);

        public Either<string, int> AddPort(int portNumber, int deviceId)
        {
            return _portsRepository.AddPort(portNumber, deviceId);
        }
    }
}