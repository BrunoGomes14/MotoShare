using MediatR;
using Microsoft.AspNetCore.Mvc;
using MotoShare.Api.Helpers;
using MotoShare.Domain;
using MotoShare.Domain.Commands.DeliveryMen.Command;
using MotoShare.Domain.Commands.MotorcycleRental.Command;

namespace MotoShare.Api.Controllers;

/// <summary>
///     DeliveryMenController
/// </summary>
/// <remarks>
///     Conjunto de endpoitns destinados ao entregador.
/// </remarks>
[ApiController]
[Route("delivery-men")]
public class DeliveryMenController : ControllerBaseCustom
{
    /// <summary>
    /// </summary>
    public DeliveryMenController(IMediator mediator, ILogger<ControllerBaseCustom> logger) : base(mediator, logger)
    {
    }

    /// <summary>
    /// Cadastra novo entregador.
    /// </summary>
    /// <param name="command">Payload</param>
    /// <returns></returns>
    [HttpPost("save")]
	public async Task<IActionResult> CreateMotorcycle([FromBody] CreateDeliveryManCommand command) =>
        await TrySendCommand(command);

    /// <summary>
    /// Adiciona foto da CNH ao entregador.
    /// </summary>
    /// <param name="command">Payload</param>
    /// <returns></returns>
    [HttpPut("save-license-image")]
	public async Task<IActionResult> SaveLicenseDriveImage([FromForm] SaveDriverLicenseImageCommand command) =>
        await TrySendCommand(command);

    /// <summary>
    /// Obter planos disponíveis.
    /// </summary>
    /// <returns></returns>
    [HttpGet("plans")]
	public async Task<IActionResult> GetPlans() =>
        await TrySendCommand(new GetMotorycycleRentPlansCommand());

    /// <summary>
    /// Realizar o aluguel de uma moto.
    /// </summary>
    /// <param name="command">Payload</param>
    /// <returns></returns>
    [HttpPost("rent-motorcycle")]
	public async Task<IActionResult> RentMotorcycle([FromBody] CreateMotorcycleRentCommand command) =>
        await TrySendCommand(command);

    /// <summary>
    /// Informar data de devolução da moto alugada.
    /// </summary>
    /// <param name="command">Payload</param>
    /// <returns></returns>
    [HttpPost("inform-return-date")]
	public async Task<IActionResult> SaveReturnDate([FromBody] SaveMotorcycleRentEndDateCommand command) =>
        await TrySendCommand(command);
}
