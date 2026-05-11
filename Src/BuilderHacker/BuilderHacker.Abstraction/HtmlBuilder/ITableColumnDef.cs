namespace BuilderHacker.Abstraction.HtmlBuilder
{
    /// <summary>
    /// Represents the definition of a table column within a data table structure.
    /// </summary>
    /// <remarks>Implementations of this interface provide metadata describing a column, such as its name,
    /// data type, and other characteristics relevant to table schemas. This interface extends <see cref="IBaseTable"/>,
    /// allowing integration with broader table definition functionality.</remarks>
    public interface ITableColumnDef : IBaseTable
    {
    }
}
