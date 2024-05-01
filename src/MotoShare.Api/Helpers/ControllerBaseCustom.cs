using MediatR;
using Microsoft.AspNetCore.Mvc;
using MotoShare.Domain;

namespace MotoShare.Api.Helpers;

/// <summary>
/// Classe com métodos auxiliares para controllers
/// </summary>
public class ControllerBaseCustom : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<ControllerBaseCustom> _logger;

    /// <summary>
    /// </summary>
    public ControllerBaseCustom(IMediator mediator, ILogger<ControllerBaseCustom> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Realiza a tentativa de envio do comando ao handler
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    protected async Task<ActionResult> TrySendCommand(object request) 
    {
        try
        {
            var result = (ResultModel?) await _mediator.Send(request);
            if (result == null)
                throw new ArgumentException("Problemas ao receber resultado.");

            return StatusCode(
                result.Success ? StatusCodes.Status200OK : StatusCodes.Status400BadRequest,
                result
            );
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);

            var result = new ResultModel(false, "Um erro inesperado aconteceu. Tente novamente.", ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, result);
        }
    }
}
