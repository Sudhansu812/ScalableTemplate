using Asp.Versioning;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ScalableApplication.Application.Interfaces.Services;

namespace ScalableApplication.API.Controllers
{
    [ApiVersion(1)]
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController(IDepartmentService departmentService) : ControllerBase
    {
        private readonly IDepartmentService _departmentService = departmentService;
    }
}
