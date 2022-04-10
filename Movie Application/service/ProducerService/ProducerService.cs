namespace Movie_Application.service.ProducerService
{
    public class ProducerService :EntityBaseRepository<Producer> ,IProducerService
    {
        public ProducerService(ApplicationDbContext context) : base(context) { }

    }

   
}
