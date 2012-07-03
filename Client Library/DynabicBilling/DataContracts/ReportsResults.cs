using System.Runtime.Serialization;

namespace DynabicBilling.RestApiDataContract
{
    /// <summary>
    /// Represents the result of two dimensional MDX query
    /// </summary>
    /// <typeparam name="Column">Type of data used on Columns</typeparam>
    /// <typeparam name="Row">Type of data used on Rows</typeparam>
    /// <typeparam name="Result">Type of data used for result</typeparam>
    [DataContract]
    public class TwoDimensionCubeResult<Column, Row, Result>
    {
        /// <summary>
        /// Represents dimension of Columns
        /// </summary>
        [DataMember]
        public Column ColumnDimension { get; set; }

        /// <summary>
        /// Represents dimension of Rows
        /// </summary>
        [DataMember]
        public Row RowDimension { get; set; }

        /// <summary>
        /// Represents value of the result
        /// </summary>
        [DataMember]
        public Result Value { get; set; }
    }

    /// <summary>
    /// Represents the result of one dimensional MDX query
    /// </summary>
    /// <typeparam name="X">Type of data used on X axis</typeparam>
    /// <typeparam name="Y">Type of data used on Y axis</typeparam>
    [DataContract]
    public class OneDimensionResult<X, Y>
    {
        /// <summary>
        /// Represents dimension of X axis
        /// </summary>
        [DataMember]
        public X XAxis { get; set; }

        /// <summary>
        /// Represents dimension of Y axis
        /// </summary>
        [DataMember]
        public Y YAxis { get; set; }
    }

}
