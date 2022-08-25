using NetworkManagerApi.Models;
using NetworkManagerApi.Services;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Results;

namespace NetworkManagerApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class PortController : ApiController
    {
        private readonly PortService _portsService;

        const string _baseUrl = "api/ports";

        const int _delayTimeInMilliseconds = 0;

        public PortController(PortService portsService) => _portsService = portsService;

        [HttpGet]
        [Route(_baseUrl + "/paginated")]
        public JsonResult<object> GetPortsPaginated([FromUri] int offset, int count)
        {
            Task.Delay(_delayTimeInMilliseconds).Wait();
            var result = _portsService
                .GetPortsPaginated(offset, count)
                .BiFold<object>(
                    null,
                    (_, ports) => ResponseBase<object>.CreateSuccess(ports),
                    (_, failureMessage) => ResponseBase<object>.CreateFailure<object>(failureMessage)
                 );
            return Json(result);
        }

        [HttpGet]
        [Route(_baseUrl + "/by-port-number-paginated")]
        public JsonResult<object> GetPortsByPortNumberPaginated([FromUri] int portNumber, [FromUri] int offset, [FromUri] int count, bool exactMatch)
        {
            Task.Delay(_delayTimeInMilliseconds).Wait();
            var result = _portsService
                .GetPortsByPortNumberPaginated(portNumber, offset, count, exactMatch)
                .BiFold<object>(
                    null,
                    (_, ports) => ResponseBase<object>.CreateSuccess(ports),
                    (_, failureMessage) => ResponseBase<object>.CreateFailure<object>(failureMessage)
                 );
            return Json(result);
        }

        [HttpGet]
        [Route(_baseUrl + "/count")]
        public JsonResult<object> GetPortsCount()
        {
            Task.Delay(_delayTimeInMilliseconds).Wait();
            var result = _portsService
                .GetPortsCount()
                .BiFold<object>(
                    default,
                    (_, count) => ResponseBase<object>.CreateSuccess<object>(count),
                    (_, failureMessage) => ResponseBase<object>.CreateFailure<object>(failureMessage)
                );
            return Json(result);
        }

        [HttpGet]
        [Route(_baseUrl + "/count-for-port-number")]
        public JsonResult<object> GetPortsCountForPortNumber([FromUri] int portNumber, bool exactMatch)
        {
            Task.Delay(_delayTimeInMilliseconds).Wait();
            var result = _portsService
                .GetPortsCountForPortNumber(portNumber, exactMatch)
                .BiFold<object>(
                    default,
                    (_, count) => ResponseBase<object>.CreateSuccess<object>(count),
                    (_, failureMessage) => ResponseBase<object>.CreateFailure<object>(failureMessage)
                );
            return Json(result);
        }

        [HttpPost]
        [Route(_baseUrl + "/add")]
        public JsonResult<object> AddDevice([FromBody] AddPortRequest request)
        {
            Task.Delay(_delayTimeInMilliseconds).Wait();
            var result = _portsService
                .AddPort(request.Number, request.DeviceId)
                .BiFold<object>(
                    default,
                    (_, portId) => ResponseBase<object>.CreateSuccess(portId),
                    (_, failureMessage) => ResponseBase<object>.CreateFailure<object>(failureMessage)
                );
            return Json(result);
        }
    }

    public class AddPortRequest
    {
        public readonly int Number;

        public readonly int DeviceId;

        public AddPortRequest(int number, int deviceId)
        {
            Number = number;
            DeviceId = deviceId;
        }
    }
}
