using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain.ViewsModels;

namespace WebStore.ViewModels
{
    public class SelectableSectionsViewModel
    {
        public IEnumerable<SectionViewModel> Sections { get; init; }

        public int? SectionId { get; init; }

        public int? ParentSectionId { get; init; }
    }
}
