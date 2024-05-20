namespace BookingSystem.Entities
{
    public record BaseEntity
    {
        public virtual string Id { get; init; } = Guid.NewGuid().ToString();
    }
}