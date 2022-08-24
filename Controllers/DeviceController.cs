using NetworkManagerApi.Models;
using NetworkManagerApi.Services;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Results;

namespace NetworkManagerApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class DeviceController : ApiController
    {
        private readonly DeviceService _devicesService;

        const string _baseUrl = "api/devices";

        public DeviceController(DeviceService devicesService) => _devicesService = devicesService;

        [HttpGet]
        [Route(_baseUrl + "/all")]
        public JsonResult<object> GetAllDevices()
        {
            Task.Delay(500).Wait();
            var result = _devicesService
                .GetAllDevices()
                .BiFold<object>(
                    null,
                    (_, devices) => ResponseBase<object>.CreateSuccess(devices),
                    (_, failureMessage) => ResponseBase<object>.CreateFailure<object>(failureMessage)
                 );
            return Json(result);
        }

        [HttpGet]
        [Route(_baseUrl + "/by-name")]
        public JsonResult<object> GetDevicesByName([FromUri] string deviceName, [FromUri] bool exactMatch)
        {
            Task.Delay(500).Wait();
            var result = _devicesService
                .GetDevicesByName(deviceName, exactMatch)
                .BiFold<object>(
                    null,
                    (_, devices) => ResponseBase<object>.CreateSuccess(devices),
                    (_, failureMessage) => ResponseBase<object>.CreateFailure<object>(failureMessage)
                 );
            return Json(result);
        }

        [HttpGet]
        [Route(_baseUrl + "/by-name-paginated")]
        public JsonResult<object> GetDevicesByNamePaginated([FromUri] string deviceName, [FromUri] int offset, [FromUri] int count, [FromUri] bool exactMatch)
        {
            Task.Delay(500).Wait();
            var result = _devicesService
                .GetDevicesByNamePaginated(deviceName, offset, count, exactMatch)
                .BiFold<object>(
                    null,
                    (_, devices) => ResponseBase<object>.CreateSuccess(devices),
                    (_, failureMessage) => ResponseBase<object>.CreateFailure<object>(failureMessage)
                 );
            return Json(result);
        }

        [HttpGet]
        [Route(_baseUrl + "/paginated")]
        public JsonResult<object> GetDevicesPaginated([FromUri] int? offset, int? count)
        {
            Task.Delay(500).Wait();
            var result = _devicesService
                .GetDevicesPaginated(offset, count)
                .BiFold<object>(
                    null,
                    (_, devices) => ResponseBase<object>.CreateSuccess(devices),
                    (_, failureMessage) => ResponseBase<object>.CreateFailure<object>(failureMessage)
                 );
            return Json(result);
        }

        [HttpPost]
        [Route(_baseUrl + "/add")]
        public JsonResult<object> AddDevice([FromBody] AddDeviceRequest request)
        {
            Task.Delay(500).Wait();
            var result = _devicesService
                .AddDevice(request.Name)
                .BiFold<object>(
                    default,
                    (_, deviceId) => ResponseBase<object>.CreateSuccess(deviceId),
                    (_, failureMessage) => ResponseBase<object>.CreateFailure<object>(failureMessage)
                );
            return Json(result);
        }

        [HttpPost]
        [Route(_baseUrl + "/update")]
        public JsonResult<object> UpdateDevice([FromBody] UpdateDeviceRequest request)
        {
            Task.Delay(500).Wait();
            var result = _devicesService
                .UpdateDevice(request.Id, request.Name)
                .BiFold<object>(
                    default,
                    (_, failureMessage) => ResponseBase<object>.CreateFailure<object>(failureMessage),
                    (_, none) => ResponseBase<object>.CreateSuccess<object>(null)
              );
            return Json(result);
        }

        [HttpPost]
        [Route(_baseUrl + "/delete")]
        public JsonResult<object> DeleteDevice([FromBody] DeleteDeviceRequest request)
        {
            Task.Delay(500).Wait();
            var result = _devicesService
                .DeleteDevice(request.Id)
                .BiFold<object>(
                    default,
                    (_, failureMessage) => ResponseBase<object>.CreateFailure<object>(failureMessage),
                    (_, none) => ResponseBase<object>.CreateSuccess<object>(null)
                );
            return Json(result);
        }

        [HttpGet]
        [Route(_baseUrl + "/device/{id}")]
        public JsonResult<object> GetDeviceById([FromUri] int Id)
        {
            Task.Delay(500).Wait();
            var result = _devicesService
                .GetDeviceById(Id)
                .BiFold<object>(
                    default,
                    (_, deviceOption) =>
                     deviceOption.BiFold<object>(
                                    default,
                                    (__, device) => ResponseBase<object>.CreateSuccess<object>(device),
                                    (__, none) => ResponseBase<object>.CreateSuccess<object>(null)
                                  ),
                    (_, failureMessage) => ResponseBase<object>.CreateFailure<object>(failureMessage)
                 );
            return Json(result);
        }

        [HttpGet]
        [Route(_baseUrl + "/count")]
        public JsonResult<object> GetDevicesCount()
        {
            Task.Delay(500).Wait();
            var result = _devicesService
                .GetDevicesCount()
                .BiFold<object>(
                    default,
                    (_, count) => ResponseBase<object>.CreateSuccess<object>(count),
                    (_, failureMessage) => ResponseBase<object>.CreateFailure<object>(failureMessage)
                );
            return Json(result);
        }

        [HttpGet]
        [Route(_baseUrl + "/count-by-name")]
        public JsonResult<object> GetDevicesCountByName([FromUri] string deviceName, [FromUri] bool exactMatch)
        {
            Task.Delay(500).Wait();
            var result = _devicesService
                .GetDevicesCountByName(deviceName, exactMatch)
                .BiFold<object>(
                    default,
                    (_, count) => ResponseBase<object>.CreateSuccess<object>(count),
                    (_, failureMessage) => ResponseBase<object>.CreateFailure<object>(failureMessage)
                );
            return Json(result);
        }
    }

    public class AddDeviceRequest
    {
        public readonly string Name;

        public AddDeviceRequest(string name) => Name = name;
    }

    public class UpdateDeviceRequest
    {
        public readonly int Id;
        public readonly string Name;

        public UpdateDeviceRequest(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }

    public class DeleteDeviceRequest
    {
        public readonly int Id;

        public DeleteDeviceRequest(int id) => Id = id;
    }
}
