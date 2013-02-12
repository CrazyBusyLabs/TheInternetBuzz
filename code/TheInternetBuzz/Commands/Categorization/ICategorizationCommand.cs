using System;

using TheInternetBuzz.Data.Categorization;

namespace TheInternetBuzz.Commands.Categorization
{
    public interface ICategorizationCommand
    {
        CategoriesList GetCategories();
    }
}