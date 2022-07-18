using System.Globalization;
using EntityFramework.Exceptions.SqlServer;
using FlyTonight.Domain.Models;
using FlyTonight.Domain.Models.Events;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FlyTonight.DAL.Data
{
    public class FlyTonightDbContext : IdentityDbContext<User>
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<SeatReservation> Reservations { get; set; }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<Plane> Planes { get; set; }
        public DbSet<Tax> Taxes { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<Airport> Airports { get; set; }
        public DbSet<Partner> Partners { get; set; }
        public DbSet<EnvEventBase> EnvEvents { get; set; }

        public FlyTonightDbContext(DbContextOptions<FlyTonightDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseExceptionProcessor();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            Tax tax0 = new() { Id = Guid.NewGuid(), Name = "DepartureTax", Value = 5000 };
            Tax tax1 = new() { Id = Guid.NewGuid(), Name = "FlatTax", Value = 2000 };
            modelBuilder.Entity<Tax>().HasData(tax0, tax1);

            Discount disc0 = new() { Id = Guid.NewGuid(), Name = "Weekend", Value = 0.1, BlobUrl = "emptyurl" };
            modelBuilder.Entity<Discount>().HasData(disc0);

            Partner part0 = new() { Id = Guid.NewGuid(), Name = "Aeroplan", BlobUrl = "http://127.0.0.1:10000/devstoreaccount1/partners/aeroplan.jpg" };
            Partner part1 = new() { Id = Guid.NewGuid(), Name = "Mileage Plan", BlobUrl = "http://127.0.0.1:10000/devstoreaccount1/partners/mileageplan.jpg" };
            Partner part2 = new() { Id = Guid.NewGuid(), Name = "Executive Club", BlobUrl = "http://127.0.0.1:10000/devstoreaccount1/partners/executiveclub.jpg" };
            Partner part3 = new() { Id = Guid.NewGuid(), Name = "Asia Miles", BlobUrl = "http://127.0.0.1:10000/devstoreaccount1/partners/asiamiles.jpg" };
            Partner part4 = new() { Id = Guid.NewGuid(), Name = "Oneworld", BlobUrl = "http://127.0.0.1:10000/devstoreaccount1/partners/oneworld.png" };
            Partner part5 = new() { Id = Guid.NewGuid(), Name = "Star Alliance", BlobUrl = "http://127.0.0.1:10000/devstoreaccount1/partners/staralliance.png" };
            modelBuilder.Entity<Partner>().HasData(part0, part1, part2, part3, part4, part5);

            Airport airport0 = new() { Id = Guid.NewGuid(), Name = "BUD", CityName = "Budapest", BlobUrl = "http://127.0.0.1:10000/devstoreaccount1/airports/bud_logo.png", Latitude = 47.43134299898153, Longitude = 19.266202715990577 };
            Airport airport1 = new() { Id = Guid.NewGuid(), Name = "AMS", CityName = "Amsterdam", BlobUrl = "http://127.0.0.1:10000/devstoreaccount1/airports/ams_logo.jpg", Latitude = 52.30710006669635, Longitude = 4.76777235439142 };
            Airport airport2 = new() { Id = Guid.NewGuid(), Name = "LUT", CityName = "London", BlobUrl = "http://127.0.0.1:10000/devstoreaccount1/airports/lut_logo.png", Latitude = 51.87934128392485, Longitude = -0.3762627204054328 };
            modelBuilder.Entity<Airport>().HasData(airport0, airport1, airport2);

            Plane plane0 = new() { Id = Guid.NewGuid(), Registration = "HA-ABC", Type = "A221", FuselageLength = 39, Wingspan = 35, SeatColCount = 4, SeatRowCount = 27, FlightDistance = 6390, CruiseHeight = 12000, CruiseSpeed = 0.82 };
            Plane plane1 = new() { Id = Guid.NewGuid(), Registration = "HA-DEF", Type = "A221", FuselageLength = 39, Wingspan = 35, SeatColCount = 4, SeatRowCount = 27, FlightDistance = 6390, CruiseHeight = 12000, CruiseSpeed = 0.82 };
            Plane plane2 = new() { Id = Guid.NewGuid(), Registration = "HA-VEL", Type = "A321", FuselageLength = 46, Wingspan = 36, SeatColCount = 6, SeatRowCount = 31, FlightDistance = 5930, CruiseHeight = 12100, CruiseSpeed = 0.78 };
            Plane plane3 = new() { Id = Guid.NewGuid(), Registration = "HA-RTZ", Type = "A321", FuselageLength = 46, Wingspan = 36, SeatColCount = 6, SeatRowCount = 31, FlightDistance = 5930, CruiseHeight = 12100, CruiseSpeed = 0.78 };
            Plane plane4 = new() { Id = Guid.NewGuid(), Registration = "HA-QWE", Type = "B738", FuselageLength = 40, Wingspan = 34, SeatColCount = 6, SeatRowCount = 29, FlightDistance = 5575, CruiseHeight = 12500, CruiseSpeed = 0.785 };
            Plane plane5 = new() { Id = Guid.NewGuid(), Registration = "HA-KJL", Type = "B738", FuselageLength = 40, Wingspan = 34, SeatColCount = 6, SeatRowCount = 29, FlightDistance = 5575, CruiseHeight = 12500, CruiseSpeed = 0.785 };
            Plane plane6 = new() { Id = Guid.NewGuid(), Registration = "HA-CCV", Type = "B738", FuselageLength = 40, Wingspan = 34, SeatColCount = 6, SeatRowCount = 29, FlightDistance = 5575, CruiseHeight = 12500, CruiseSpeed = 0.785 };
            modelBuilder.Entity<Plane>().HasData(plane0, plane1, plane2, plane3, plane4, plane5, plane6);

            Flight flight0 = new() { Id = Guid.NewGuid(), AirplaneId = plane0.Id, FromId = airport0.Id, ToId = airport1.Id, TimeOfDeparture = DateTime.Parse("2022-08-12T06:30:00.00") };

            Flight flight1 = new() { Id = Guid.NewGuid(), AirplaneId = plane1.Id, FromId = airport0.Id, ToId = airport1.Id, TimeOfDeparture = DateTime.Parse("2022-08-12T08:00:00.00") };
            Flight flight2 = new() { Id = Guid.NewGuid(), AirplaneId = plane2.Id, FromId = airport0.Id, ToId = airport1.Id, TimeOfDeparture = DateTime.Parse("2022-08-12T10:30:00.00") };
            Flight flight3 = new() { Id = Guid.NewGuid(), AirplaneId = plane4.Id, FromId = airport0.Id, ToId = airport1.Id, TimeOfDeparture = DateTime.Parse("2022-08-12T14:00:00.00") };
            Flight flight4 = new() { Id = Guid.NewGuid(), AirplaneId = plane5.Id, FromId = airport0.Id, ToId = airport1.Id, TimeOfDeparture = DateTime.Parse("2022-08-12T16:30:00.00") };
            Flight flight5 = new() { Id = Guid.NewGuid(), AirplaneId = plane0.Id, FromId = airport0.Id, ToId = airport1.Id, TimeOfDeparture = DateTime.Parse("2022-08-12T20:00:00.00") };

            Flight flight6 = new() { Id = Guid.NewGuid(), AirplaneId = plane0.Id, FromId = airport0.Id, ToId = airport1.Id, TimeOfDeparture = DateTime.Parse("2022-08-13T06:30:00.00") };
            Flight flight7 = new() { Id = Guid.NewGuid(), AirplaneId = plane1.Id, FromId = airport0.Id, ToId = airport1.Id, TimeOfDeparture = DateTime.Parse("2022-08-13T12:00:00.00") };
            Flight flight8 = new() { Id = Guid.NewGuid(), AirplaneId = plane5.Id, FromId = airport0.Id, ToId = airport1.Id, TimeOfDeparture = DateTime.Parse("2022-08-13T18:30:00.00") };

            Flight flight9 = new() { Id = Guid.NewGuid(), AirplaneId = plane6.Id, FromId = airport0.Id, ToId = airport2.Id, TimeOfDeparture = DateTime.Parse("2022-08-12T18:30:00.00") };

            Flight flight10 = new() { Id = Guid.NewGuid(), AirplaneId = plane4.Id, FromId = airport1.Id, ToId = airport0.Id, TimeOfDeparture = DateTime.Parse("2022-08-14T09:00:00.00") };
            Flight flight11 = new() { Id = Guid.NewGuid(), AirplaneId = plane5.Id, FromId = airport1.Id, ToId = airport0.Id, TimeOfDeparture = DateTime.Parse("2022-08-14T13:30:00.00") };
            Flight flight12 = new() { Id = Guid.NewGuid(), AirplaneId = plane0.Id, FromId = airport1.Id, ToId = airport0.Id, TimeOfDeparture = DateTime.Parse("2022-08-14T19:00:00.00") };

            Flight flight13 = new() { Id = Guid.NewGuid(), AirplaneId = plane3.Id, FromId = airport1.Id, ToId = airport0.Id, TimeOfDeparture = DateTime.Parse("2022-08-15T12:15:00.00") };
            modelBuilder.Entity<Flight>().HasData(flight0, flight1, flight2, flight3, flight4, flight5, flight6, flight7, flight8, flight9, flight10, flight11, flight12, flight13);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Orders)
                .WithOne(o => o.User)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Order>()
                .HasMany(o => o.Tickets)
                .WithOne(t => t.Order)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Reservation)
                .WithOne(s => s.Ticket)
                .HasForeignKey<SeatReservation>(s => s.TicketId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<SeatReservation>()
                .HasOne(s => s.Flight)
                .WithMany(f => f.Reservations)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SeatReservation>()
                .HasIndex(x => new { x.RowNum, x.ColNum, x.FlightId })
                .IsUnique();

            modelBuilder.Entity<Flight>()
                .HasOne(f => f.From)
                .WithMany(a => a.OutboundFlights)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Flight>()
                .HasOne(f => f.To)
                .WithMany(a => a.InboundFlights)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Flight>()
                .HasMany(f => f.Taxes)
                .WithMany(t => t.Flights)
                .UsingEntity(j => j.ToTable("FlightTax").HasData(new[] {
                    new { FlightsId = flight0.Id, TaxesId = tax0.Id },
                    new { FlightsId = flight1.Id, TaxesId = tax0.Id },
                    new { FlightsId = flight2.Id, TaxesId = tax0.Id },
                    new { FlightsId = flight3.Id, TaxesId = tax0.Id },
                    new { FlightsId = flight4.Id, TaxesId = tax0.Id },
                    new { FlightsId = flight5.Id, TaxesId = tax0.Id },
                    new { FlightsId = flight6.Id, TaxesId = tax0.Id },
                    new { FlightsId = flight7.Id, TaxesId = tax0.Id },
                    new { FlightsId = flight8.Id, TaxesId = tax0.Id },
                    new { FlightsId = flight9.Id, TaxesId = tax0.Id },
                    new { FlightsId = flight10.Id, TaxesId = tax0.Id },
                    new { FlightsId = flight11.Id, TaxesId = tax0.Id },
                    new { FlightsId = flight12.Id, TaxesId = tax0.Id },
                    new { FlightsId = flight13.Id, TaxesId = tax0.Id },
                }));

            modelBuilder.Entity<Flight>()
                .HasMany(f => f.Discounts)
                .WithMany(d => d.Flights)
                .UsingEntity(j => j.ToTable("FlightDiscount").HasData(new[]
                {
                    new { FlightsId = flight6.Id, DiscountsId = disc0.Id },
                    new { FlightsId = flight7.Id, DiscountsId = disc0.Id },
                    new { FlightsId = flight8.Id, DiscountsId = disc0.Id },

                    new { FlightsId = flight10.Id, DiscountsId = disc0.Id },
                    new { FlightsId = flight11.Id, DiscountsId = disc0.Id },
                    new { FlightsId = flight12.Id, DiscountsId = disc0.Id },
                }));

            modelBuilder.Entity<Flight>()
                .HasOne(f => f.EnvEvent)
                .WithOne(d => d.Flight)
                .HasForeignKey<EnvEventBase>(d => d.FlightId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<DeletedFlightEvent>()
                .HasBaseType<EnvEventBase>();

            modelBuilder.Entity<DrunkEvent>()
                .HasBaseType<EnvEventBase>();

            modelBuilder.Entity<ProtestingEvent>()
                .HasBaseType<EnvEventBase>();

            modelBuilder.Entity<RainEvent>()
                .HasBaseType<EnvEventBase>();

            modelBuilder.Entity<WrongAirportEvent>()
                .HasBaseType<EnvEventBase>();

            modelBuilder.Entity<Plane>()
                .HasMany(p => p.Flights)
                .WithOne(f => f.Airplane)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
