namespace Price.WebApi.Models.Packet
{
    public enum TaskStatus
    {
        /// <summary>
        /// Обработка не начата
        /// </summary>
        NotProcessed = 0,
        /// <summary>
        /// Ошибка при обработке
        /// </summary>
        Error = 1,
        /// <summary>
        /// Обработка успешно завершена
        /// </summary>
        Ok = 2,
        /// <summary>
        /// 
        /// </summary>
        Inprocess = 3
    }
}