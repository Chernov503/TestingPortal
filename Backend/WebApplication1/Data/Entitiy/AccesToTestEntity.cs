namespace WebApplication1.Data.Entitiy
{
    public class AccesToTestEntity
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public UserEntity? User { get; set; }
        public Guid TestId { get; set; }
        public TestEntity? Test { get; set; }


    }
}
