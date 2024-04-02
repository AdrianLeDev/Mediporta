using MediportaSOTags.Entities;

namespace MediportaSOTags.Helpers
{
    public class Calculators
    {
        public static decimal CalculatePercentPart(int tagCount, int allTagsCount)
        {
            if(tagCount < allTagsCount) {
                return Math.Round((((decimal)100 * tagCount) / allTagsCount), 2);
            }
            else {
                return -1;
            }
            
        }

    }
}
