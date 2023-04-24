namespace BeautySalon.ContractEntity;

public interface IIdentifiable : IValidable
{
    public long Id { get; set; }
}
