namespace SportsWorld.Data
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using SportsWorld.Data.Repositories;
    using SportsWorld.Models;

    public interface ISportsWorldData
    {
        IRepository<Comment> Comments { get; }

        IRepository<Country> Countries { get; }

        IRepository<Image> Images { get; }

        IRepository<AppUser> Users { get; }

        IRepository<CardInfo> CardInfoes { get; }

        IRepository<City> Cities { get; }

        IRepository<Company> Companies { get; }

        IRepository<FieldRating> FieldRatings { get; }

        IRepository<Field> Fields { get; }

        IRepository<GameEvent> GameEvents { get; }

        IRepository<Message> Messages { get; }

        IRepository<Participant> Participants { get; }

        IRepository<TeamMember> TeamMembers { get; }

        IRepository<Team> Teams { get; }

        IRepository<Transaction> Transactions { get; }


        int SaveChanges();
    }
}