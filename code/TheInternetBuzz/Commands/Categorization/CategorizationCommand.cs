
using TheInternetBuzz.Data.Categorization;
using TheInternetBuzz.Services.Categorization;

namespace TheInternetBuzz.Commands.Categorization
{
    public class CategorizationCommand : ICategorizationCommand
    {
        public CategoriesList GetCategories()
        {
            return new CategorizationService().GetCategories();
        }
    }
}