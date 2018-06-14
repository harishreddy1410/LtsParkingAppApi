using AppServices.Interfaces;
using System;
using Xunit;

namespace LtsParkingAppApiUnitTests
{
    public class UnitTest1
    {
        IParkingSlotServices _parkingSlotService;
        public UnitTest1(IParkingSlotServices parkingSlotService)
        {
            _parkingSlotService = parkingSlotService;
        }
        [Fact]
        public void Test1()
        {
            Assert.Equal("1", _parkingSlotService.Get(1).Result.Id.ToString());
        }
    }
}
