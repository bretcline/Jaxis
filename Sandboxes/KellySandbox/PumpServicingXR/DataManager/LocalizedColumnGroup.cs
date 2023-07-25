namespace LFI.Sync.DataManager
{
    /// <summary>
    /// Column group is a unit that defines a primary table column, its reference ID column, the originating table name
    /// and maps that to a property withing the Localization table.
    /// </summary>
    public class LocalizedColumnGroup
    {
        private int _index;

        /// <summary>
        /// Initializes a new instance of the <see cref="LocalizedColumnGroup"/> class.
        /// </summary>
        /// <param name="sourceColumnName">The column.</param>
        /// <param name="sourceColumnID">The ref ID sourceColumnName.</param>
        /// <param name="foreignTableName">The table.</param>
        /// <param name="foreignColumnName">Name of the foreign column.</param>
        /// <param name="foreignColumnID">The foreign column ID.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="index">The index.</param>
        public LocalizedColumnGroup(
            string sourceColumnName,
            string sourceColumnID,
            string foreignTableName,
            string foreignColumnName,
            string foreignColumnID,
            string propertyName,
            int index )
        {
            SourceColumnName = sourceColumnName;
            SourceColumnID = sourceColumnID;
            ForeignTableName = foreignTableName;
            ForeignColumnName = foreignColumnName;
            ForeignColumnID = foreignColumnID;
            PropertyName = propertyName;
            _index = index;
        }

        public string PropertyName { get; set; }
        public string SourceColumnName { get; set; }
        public string SourceColumnID { get; set; }
        public string ForeignTableName { get; set; }
        public string ForeignColumnName { get; set; }
        public string ForeignColumnID { get; set; }
        public string UniqueColumnID { get { return ForeignColumnID + _index; } }
    }
}