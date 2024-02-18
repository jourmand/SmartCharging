using SmartCharging.Core.Domain.GroupItemAggregate;
using SmartCharging.Infrastructures.Data.Commons;
using static SmartCharging.Core.Domain.GroupItemAggregate.WebApiCommand;

namespace Test.SmartCharging.Infrastructure.Infrastructure
{
    public class DatabaseInitializer
    {
        public static void Initialize(SmartChargingDbContext context)
        {
            if (!context.Groups.Any())
                SeedSmartChargingData(context);
        }

        private static void SeedSmartChargingData(SmartChargingDbContext context)
        {
            var groupItems = new[]
            {
                GroupItem.Create(new CreateGroupCommand
                {
                    Id = Guid.Parse("654b7573-9501-436a-ad36-94c5696ac28f"),
                    CapacityInAmps = 10,
                    Name  = "Group1"
                }),
                GroupItem.Create(new CreateGroupCommand
                {
                    Id = Guid.Parse("971316e1-4966-4426-b1ea-a36c9dde1066"),
                    CapacityInAmps = 12,
                    Name  = "Group2"
                }),
                GroupItem.Create(new CreateGroupCommand
                {
                    Id = Guid.Parse("971316e1-4966-4426-b1ea-a36c9dde1067"),
                    CapacityInAmps = 20,
                    Name  = "Group3"
                }),
                GroupItem.Create(new CreateGroupCommand
                {
                    Id = Guid.Parse("971316e1-4966-4426-b1ea-a36c9dde1068"),
                    CapacityInAmps = 2,
                    Name  = "Group4"
                }),
            };

            context.Groups.AddRange(groupItems);
            context.SaveChanges();
        }
    }
}