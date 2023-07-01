namespace ETicaretAPI.Domain.Entities.Common
{
    public class BaseEntity
    {
        public Guid Id { get; set; }
        public DateTime CratedDate { get; set; }
        virtual public DateTime UpdatedDate { get; set; }


    }
}
