namespace Common.Dto.Model.Packet
{
    public enum TaskStatus
    {
        /// <summary>
        /// Не инициализирован
        /// </summary>
        NotInitialized = 0,
        /// <summary>
        /// Обработка не начата
        /// </summary>
        InQueue = 1,
        /// <summary>
        /// Ошибка при обработке
        /// </summary>
        Error = 2,
        /// <summary>
        /// Обработка успешно завершена
        /// </summary>
        Ok = 3,
        /// <summary>
        /// 
        /// </summary>
        InProcess = 4
    }
}