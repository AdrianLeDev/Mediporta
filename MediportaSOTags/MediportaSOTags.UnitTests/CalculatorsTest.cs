using MediportaSOTags.Helpers;
namespace MediportaSOTags.UnitTests
{
    public class CalculatorsTest
    {
        [Theory]
        [InlineData(2528440, 60251871, 4.20)]
        [InlineData(10, 100, 10)]
        public void CalculatePercentPart_WithTwoValues_ReturnsCorrectValue(int tagCount, int allTagsCount, decimal expectedResult)
        {

            //Act
            var result = Calculators.CalculatePercentPart(tagCount, allTagsCount);
            //Assert 
            Assert.Equal(expectedResult, result);
           
        }
        [Theory]
        [InlineData(60251871, 2528440, -1)]
        [InlineData(100, 10, -1)]
        public void CalculatePercentPart_WithTagValueBiggerThan_ReturnsErrorValueMinusOne(int tagCount, int allTagsCount, decimal expectedResult)
        {

            //Act
            var result = Calculators.CalculatePercentPart(tagCount, allTagsCount);
            //Assert 
            Assert.Equal(expectedResult, result);

        }
    }
}