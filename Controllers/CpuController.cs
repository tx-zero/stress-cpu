using Microsoft.AspNetCore.Mvc;

namespace stress_cpu.Controllers;

[ApiController]
public class CpuController : ControllerBase
{
    public CpuController()
    {
    }

    [HttpGet("")]
    public ActionResult ShowPage()
    {
        var html = "<html><body>"
        + "<b>" + Environment.MachineName + "</b><br>"
        + "CPU: " + StressService.Percent + "<br><br>"
        + "<a href=\"/cpu/0\">0%</a>&nbsp;&nbsp;"
        + "<a href=\"/cpu/25\">25%</a>&nbsp;&nbsp;"
        + "<a href=\"/cpu/50\">50%</a>&nbsp;&nbsp;"
        + "<a href=\"/cpu/75\">75%</a>&nbsp;&nbsp;"
        + "<a href=\"/cpu/95\">95%</a>&nbsp;&nbsp;"
        + "<a href=\"/cpu/100\">100%</a>&nbsp;&nbsp;"
        + "</body></html>";
        return Content(html, "text/html");
    }

    [HttpGet("cpu/{percent}")]
    public ActionResult SetCpu([FromRoute] int percent)
    {
        StressService.Percent = percent;
        var html = "<html><body>"
        + "<a href=\"/\">Back</a>"
        + "</body></html>";
        return Content(html, "text/html");
    }
}
