using BeautySalon.ContractService;
using BeautySalon.Model;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BeautySalon.Api.Controllers;

[ApiController]
[Route("/api/[controller]")]
public abstract class MainController : ControllerBase
{
    private readonly INotifier _notifier;
    private IEnumerable<string> _errorMessages;

    protected MainController(INotifier notifier)
    {
        _notifier = notifier;
        _errorMessages = new List<string>();
    }

    protected bool OperationIsValid()
    {
        return !_notifier.HaveNotification();
    }

    protected ActionResult CustomResponse(ModelStateDictionary modelState)
    {
        if (!modelState.IsValid) NotifyErroInvalidModel(modelState);
        return CustomResponse(modelState, StatusCodes.Status400BadRequest);
    }

    protected ActionResult CustomResponse(object? result = null, int statusCode = 200)
    {
        if (!OperationIsValid())
            _errorMessages = _notifier.GetAllNotifications().Select(x => x.Message);

        switch (statusCode)
        {
            case 200: return Ok(Result.Ok(result));
            case 201: return Created("", Result.Ok(result));
            case 204: return NoContent();
            case 400: return BadRequest(Result.Fail(_errorMessages));
            case 404: return NotFound(Result.Fail(_errorMessages));
            default: throw new Exception($"O status {statusCode} não foi implementado corretamente.");
        }
    }

    protected void NotifyErroInvalidModel(ModelStateDictionary modelState)
    {
        IEnumerable<ModelError> erros = modelState.Values.SelectMany(x => x.Errors);
        foreach (var erro in erros)
        {
            string erroMessage = erro.Exception == null ? erro.ErrorMessage : erro.Exception.Message;
            NotifyErro(erroMessage);
        }
    }

    protected void NotifyErro(string message)
    {
        List<Notification> notifications = new();
        notifications.Add(new Notification(message));
        _notifier.Handle(notifications);
    }
}
