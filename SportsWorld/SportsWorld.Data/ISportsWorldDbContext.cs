namespace SportsWorld.Data
{
    using SportsWorld.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;

    public interface ISportsWorldDbContext : IDisposable
    {
        IDbSet<CardInfo> CardInfoes { get; set; }

        IDbSet<City> Cities { get; set; }

        IDbSet<Comment> Comments { get; set; }

        IDbSet<Company> Companies { get; set; }

        IDbSet<Country> Countries { get; set; }

        IDbSet<FieldRating> FieldRatings { get; set; }

        IDbSet<Field> Fields { get; set; }

        IDbSet<GameEvent> GameEvents { get; set; }

        IDbSet<Image> Images { get; set; }

        IDbSet<Message> Messages { get; set; }

        IDbSet<Participant> Participants { get; set; }

        IDbSet<TeamMember> TeamMembers { get; set; }

        IDbSet<Team> Teams { get; set; }

        IDbSet<Transaction> Transactions { get; set; }

        IDbSet<AppUser> Users { get; set; }

        int SaveChanges();

        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        IDbSet<T> Set<T>() where T : class;
    }
}