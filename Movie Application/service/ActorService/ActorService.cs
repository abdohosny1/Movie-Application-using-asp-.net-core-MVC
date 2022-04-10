namespace Movie_Application.service.ActorService
{
    public class ActorService : EntityBaseRepository<Actor>, IActorService
    {

        public ActorService(ApplicationDbContext context):base(context) { }
       
      
    }
}
