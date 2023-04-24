using BeautySalon.ContractEntity;

namespace BeautySalon.Model;

public abstract class Entity : IIdentifiable
{
    protected Entity()
    {
    }

    public long Id { get; set; }
}
