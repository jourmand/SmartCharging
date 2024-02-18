using FluentAssertions;
using SmartCharging.Core.Domain.GroupItemAggregate;
using SmartCharging.Infrastructures.Data.GroupItemAggregate.Repositories;
using Test.SmartCharging.Infrastructure.Infrastructure;
using Xunit;

namespace Test.SmartCharging.Infrastructure;

public class RepositoryTests : DatabaseTestBase
{
    private readonly GroupItemRepository _testGroupItemRepository;

    public RepositoryTests()
    {
        _testGroupItemRepository = new GroupItemRepository(Context);
    }

    [Fact]
    public void Add_New_Group()
    {
        var item = GroupItem.Create(new WebApiCommand.CreateGroupCommand
        {
            CapacityInAmps = 1,
            Name = "GroupNew",
            Id = Guid.NewGuid()
        });
        var result = Context.Groups.Add(item);

        result.Entity.Should().BeOfType<GroupItem>();
        result.Entity.Name.Should().Be(item.Name);
    }

    [Theory]
    [InlineData("654b7573-9501-436a-ad36-94c5696ac28f")]
    [InlineData("971316e1-4966-4426-b1ea-a36c9dde1066")]
    public async void Group_Item_Should_Available(string id)
    {
        var result = await _testGroupItemRepository.GetAsync(Guid.Parse(id));

        result.Should().BeOfType<GroupItem>();
        result.Id.Should().Be(result.Id);
    }
}
