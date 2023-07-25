namespace LFI.Sync.DataManager
{
    public class TransactionResult
    {
        /// <summary>
        /// Returns the primary key for a newly created object. Only useful on inserts.
        /// </summary>
        public string PrimaryKey { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="TransactionResult"/> is success.
        /// </summary>
        /// <value><c>true</c> if success; otherwise, <c>false</c>.</value>
        public bool Success { get; set; }

        /// <summary>
        /// Any message output by the DataManager.
        /// </summary>s
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the transaction info.
        /// </summary>
        /// <value>The transaction info.</value>
        public string TransactionInfo { get; set; }
    }
}