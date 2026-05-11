using BuilderHacker.Abstraction.HtmlBuilder;

namespace BuilderHacker.Core.HtmlBuilder.Base.TableElement
{
    /// <summary>
    /// Represents an HTML <thead> table header section element.
    /// </summary>
    public sealed class Thead : Element, IBaseTable
    {
        public Thead(params ITableRow[] children) : base("thead", children)
        { }
    }
}
