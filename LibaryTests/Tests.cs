using Libary;

namespace LibaryTests;

public class Tests
{
    [TestFixture]
    public class CalculationsTests
    {
        [Test]
        public void AvailablePeriods_ExampleScenario_ReturnsExpectedSlots()
        {
           
            TimeSpan[] startTimes = new[]
            {
                TimeSpan.Parse("10:00"),
                TimeSpan.Parse("11:00"),
                TimeSpan.Parse("15:00"),
                TimeSpan.Parse("15:30"),
                TimeSpan.Parse("16:50")
            };
            int[] durations = {60, 30, 10, 10, 40};
            TimeSpan beginWorkingTime = TimeSpan.Parse("08:00");
            TimeSpan endWorkingTime = TimeSpan.Parse("18:00");
            int consultationTime = 30;
            var expected = new[]
            {
                "08:00-08:30","08:30-09:00","09:00-09:30","09:30-10:00",
                "11:30-12:00","12:00-12:30","12:30-13:00","13:00-13:30",
                "13:30-14:00","14:00-14:30","14:30-15:00",
                "15:40-16:10","16:10-16:40","17:30-18:00"
            };

            
            var result = Calculations.AvailablePeriods(startTimes, durations, beginWorkingTime, endWorkingTime, consultationTime);

            
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void AvailablePeriods_NoFreeSlotLongConsultation_ReturnsEmpty()
        {
            
            TimeSpan[] startTimes = new[]
            {
                TimeSpan.Parse("08:00"),
                TimeSpan.Parse("09:30"),
                TimeSpan.Parse("11:00"),
                TimeSpan.Parse("13:00"),
                TimeSpan.Parse("15:00"),
                TimeSpan.Parse("17:00")
            };
            int[] durations = {90, 90, 120, 120, 120, 60};
            TimeSpan beginWorkingTime = TimeSpan.Parse("08:00");
            TimeSpan endWorkingTime = TimeSpan.Parse("18:00");
            int consultationTime = 120; 

            var result = Calculations.AvailablePeriods(startTimes, durations, beginWorkingTime, endWorkingTime, consultationTime);

            
            Assert.IsEmpty(result);
        }
    }
}