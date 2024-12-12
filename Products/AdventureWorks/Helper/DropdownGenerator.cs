using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdventureWorks.Helper
{
    public class DropdownGenerator<T>
    {
        private readonly Func<T, string> _textSelector;
        private readonly Func<T, string> _valueSelector;

        public DropdownGenerator(Func<T, string> textSelector, Func<T, string> valueSelector)
        {
            _textSelector = textSelector;
            _valueSelector = valueSelector;
        }

        public IEnumerable<SelectListItem> PrepareSelectList(IQueryable<T> queryable)
        {
            return queryable.Select(x => new SelectListItem
            {
                Text = _textSelector(x),
                Value = _valueSelector(x)
            }).ToList();
        }
    }
}