using System;

namespace intSoft.MVC.Core.Helpers.UiGrid
{
    public interface IUiGridDefinitionProvider
    {
        UiGridDefinition GetUiGridDefinition(Type type, bool isDrafts = false);
    }
}
