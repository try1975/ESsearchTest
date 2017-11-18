namespace Common.Dto.Model.Packet
{
    public enum TaskStatus
    {
        /// <summary>
        /// Обработка не начата
        /// </summary>
        InQueue = 0,
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
        InProcess = 3
    }
}