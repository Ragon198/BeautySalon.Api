using BeautySalon.ContractService;
using BeautySalon.Model;

namespace BeautySalon.Service;

public abstract class BaseService
{
    private readonly INotifier _notifier;

    private List<Notification> _notifications;

    protected BaseService(INotifier notifier)
    {
        _notifier = notifier;
        _notifications = new List<Notification>();
    }
}
