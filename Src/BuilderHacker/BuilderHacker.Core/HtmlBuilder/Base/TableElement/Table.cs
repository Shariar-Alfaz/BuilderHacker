using BuilderHacker.Abstraction.HtmlBuilder;

namespace BuilderHacker.Core.HtmlBuilder.Base.TableElement
{
    /// <summary>
    /// Represents an HTML <table> element containing table-related child nodes.
    /// </summary>
    public sealed class Table : Element, IBaseTable
    {
        public Table(params IBaseTable[] children) : base("table", children) { }

    }
}
