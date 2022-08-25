using LanguageExt;
using NetworkManagerApi.Models;
using System.Collections.Generic;

namespace NetworkManagerApi.Repositories
{
    public interface IPortRepository
    {
        Either<string, IEnumerable<Port>> GetPortsPaginated(int offset, int count);

        Either<string, IEnumerable<Port>> GetPortsByPortNumberPaginated(int portNumber, int offset, int count, bool exactMatch);

        Either<string, int> GetPortsCount();

        Either<string, int> GetPortsCountForPortNumber(int portNumber, bool exactMatch);

        Either<string, int> AddPort(int portNumber, int deviceId);
    }
}
