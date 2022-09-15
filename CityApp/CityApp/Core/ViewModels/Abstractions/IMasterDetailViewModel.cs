using System;
using System.Collections.Generic;

namespace CityApp.Core.ViewModels.Abstractions
{
	public interface IMasterDetailViewModel : IViewModel
    {
        IReadOnlyList<Type> Children { get; set; }
    }
}
