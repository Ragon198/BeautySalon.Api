using BeautySalon.Model;

namespace BeautySalon.ContractService;

public interface INotifier
{
    bool HaveNotification();
    List<Notification> GetAllNotifications();
    void Handle(List<Notification> notifications);
    void CleanNotifier();
}
