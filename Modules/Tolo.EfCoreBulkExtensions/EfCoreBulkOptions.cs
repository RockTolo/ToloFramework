namespace Tolo.EfCoreBulkExtensions
{
    public class EfCoreBulkOptions
    {
        /// <summary>
        /// 是否启用EfCoreBulk处理批量增删改
        /// </summary>
        public bool IsEnabled { get; set; } = true;

        /// <summary>
        /// 启用阀值，如：数据量大于100就启用EfCoreBulk处理批量增删改
        /// </summary>
        public int EnabledThreshold { get; set; } = 100;
    }
}
