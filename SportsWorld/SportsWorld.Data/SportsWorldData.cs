namespace SportsWorld.Data
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using SportsWorld.Data.Repositories;
    using SportsWorld.Models;
    using System;
    using System.Collections.Generic;

    public class SportsWorldData : ISportsWorldData
    {
        private ISportsWorldDbContext context;
        private IDictionary<Type, object> repositories;

        public SportsWorldData(ISportsWorldDbContext context)
        {
            this.context = context;
            this.repositories = new Dictionary<Type, object>();
        }

        public ISportsWorldDbContext Context 
        {
            get
            {
                return this.context;
            }
        }

        public IRepository<Comment> Comments
        {
            get
            {
                return this.GetRepository<Comment>();
            }
        }

        public IRepository<Country> Countries
        {
            get
            {
                return this.GetRepository<Country>();
            }
        }

        public IRepository<AppUser> Users
        {
            get
            {
                return this.GetRepository<AppUser>();
            }
        }

        public IRepository<Image> Images
        {
            get
            {
                return this.GetRepository<Image>();
            }
        }

        public IRepository<CardInfo> CardInfoes
        {
            get
            {
                return this.GetRepository<CardInfo>();
            }
        }

        public IRepository<City> Cities
        {
            get
            {
                return this.GetRepository<City>();
            }
        }

        public IRepository<Company> Companies
        {
            get
            {
                return this.GetRepository<Company>();
            }
        }

        public IRepository<FieldRating> FieldRatings
        {
            get
            {
                return this.GetRepository<FieldRating>();
            }
        }

        public IRepository<Field> Fields
        {
            get
            {
                return this.GetRepository<Field>();
            }
        }

        public IRepository<GameEvent> GameEvents
        {
            get
            {
                return this.GetRepository<GameEvent>();
            }
        }

        public IRepository<Message> Messages
        {
            get
            {
                return this.GetRepository<Message>();
            }
        }

        public IRepository<Participant> Participants
        {
            get
            {
                return this.GetRepository<Participant>();
            }
        }

        public IRepository<TeamMember> TeamMembers
        {
            get
            {
                return this.GetRepository<TeamMember>();
            }
        }

        public IRepository<Team> Teams
        {
            get
            {
                return this.GetRepository<Team>();
            }
        }

        public IRepository<Transaction> Transactions
        {
            get
            {
                return this.GetRepository<Transaction>();
            }
        }

        public int SaveChanges()
        {
            return this.context.SaveChanges();
        }

        private IRepository<T> GetRepository<T>() where T : class
        {
            var typeOfRepository = typeof(T);
            if (!this.repositories.ContainsKey(typeOfRepository))
            {
                var newRepository = Activator.CreateInstance(typeof(EFRepository<T>), context);
                this.repositories.Add(typeOfRepository, newRepository);
            }

            return (IRepository<T>)this.repositories[typeOfRepository];
        }
    }
}