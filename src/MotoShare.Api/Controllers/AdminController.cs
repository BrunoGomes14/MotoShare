using MediatR;
using Microsoft.AspNetCore.Mvc;
using MotoShare.Api.Helpers;
using MotoShare.Domain.Commands.Motorcycle.Command;

namespace MotoShare.Api.Controlers;

/// <summary>
///     AdminController
/// </summary>
/// <remarks>
///     Conjunto de endpoints destinados ao administrador.
/// </remarks>
[ApiController]
[Route("admin")]
public class AdminController : ControllerBaseCustom
{
    /// <summary>
    /// </summary>
    public AdminController(IMediator mediator, ILogger<ControllerBaseCustom> logger) : base(mediator, logger)
    {
    }

    /// <summary>
    /// Adicionar nova moto.
    /// </summary>
    /// <param name="command">Payload</param>
    /// <returns></returns>
    [HttpPost("save")]
	public async Task<IActionResult> CreateMotorcycle([FromBody] CreateMotorcycleCommand command) =>
        await TrySendCommand(command);

    /// <summary>
    /// Atualizar placa de uma moto.
    /// </summary>
    /// <param name="command">Payload</param>
    /// <returns></returns>
    [HttpPut("update-plate")]
	public async Task<IActionResult> UpdateMotorcycleLicensePlate([FromBody] UpdateMotorcycleLicensePlateCommand command) =>
        await TrySendCommand(command);

    /// <summary>
    /// Consultar motos.
    /// </summary>
    /// <returns></returns>
    [HttpGet("list-motorcycles")]
	public async Task<IActionResult> GetMotorcycles([FromQuery] string? licensePlate) =>
        await TrySendCommand(new GetMotorcyclesCommand(licensePlate));

}
