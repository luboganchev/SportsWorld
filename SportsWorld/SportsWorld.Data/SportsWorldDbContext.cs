namespace SportsWorld.Data
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using SportsWorld.Data.Migrations;
    using SportsWorld.Models;
    using System.Data.Entity;

    public class SportsWorldDbContext : IdentityDbContext<AppUser>, ISportsWorldDbContext
    {
        public SportsWorldDbContext()
            : base("SportsWorld", throwIfV1Schema: false)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<SportsWorldDbContext, Configuration>());
        }

        public virtual IDbSet<CardInfo> CardInfoes { get; set; }

        public virtual IDbSet<City> Cities { get; set; }

        public virtual IDbSet<Comment> Comments { get; set; }

        public virtual IDbSet<Company> Companies { get; set; }

        public virtual IDbSet<Country> Countries { get; set; }

        public virtual IDbSet<FieldRating> FieldRatings { get; set; }

        public virtual IDbSet<Field> Fields { get; set; }

        public virtual IDbSet<GameEvent> GameEvents { get; set; }

        public virtual IDbSet<Image> Images { get; set; }

        public virtual IDbSet<Message> Messages { get; set; }

        public virtual IDbSet<Participant> Participants { get; set; }

        public virtual IDbSet<TeamMember> TeamMembers { get; set; }

        public virtual IDbSet<Team> Teams { get; set; }

        public virtual IDbSet<Transaction> Transactions { get; set; }

        public static SportsWorldDbContext Create()
        {
            return new SportsWorldDbContext();
        }

        public new IDbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }

        public DbContext DbContext
        {
            get
            {
                return this;
            }
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>()
                .HasMany(e => e.Fields)
                .WithRequired(e => e.City)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Company>()
                .Property(e => e.TotalRevenue)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Company>()
                .HasMany(e => e.Fields)
                .WithRequired(e => e.Company)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Country>()
                .HasMany(e => e.Cities)
                .WithRequired(e => e.Country)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Field>()
                .Property(e => e.PricePerHour)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Field>()
                .HasMany(e => e.Comments)
                .WithRequired(e => e.Field)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Field>()
                .HasMany(e => e.FieldRatings)
                .WithRequired(e => e.Field)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Field>()
                .HasMany(e => e.GameEvents)
                .WithRequired(e => e.Field)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<GameEvent>()
                .HasMany(e => e.Participants)
                .WithRequired(e => e.GameEvent)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<GameEvent>()
                .HasMany(e => e.Transactions)
                .WithRequired(e => e.GameEvent)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<GameEvent>()
                .HasMany(e => e.Teams)
                .WithMany(e => e.GameEvents)
                .Map(m => m.ToTable("EventTeams").MapLeftKey("GameEventID").MapRightKey("TeamID"));

            modelBuilder.Entity<Image>()
                .HasMany(e => e.Teams)
                .WithOptional(e => e.Image)
                .HasForeignKey(e => e.LogoID);

            modelBuilder.Entity<Team>()
                .HasMany(e => e.TeamMembers)
                .WithRequired(e => e.Team)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<IdentityUserLogin>().HasKey<string>(l => l.UserId);
            modelBuilder.Entity<IdentityRole>().HasKey<string>(r => r.Id);
            modelBuilder.Entity<IdentityUserRole>().HasKey(r => new { r.RoleId, r.UserId });
        }
    }
}