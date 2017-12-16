using Common.Dto.Logic;

namespace Common.Dto.Model.Packet
{
    public enum TaskStatus
    {
        /// <summary>
        /// Не инициализирован
        /// </summary>
        [EnumLocalizeAttribite("Не обработан")]
        NotInitialized = 0,
        /// <summary>
        /// Обработка не начата
        /// </summary>
        [EnumLocalizeAttribite("В очереди")]
        InQueue = 1,
        /// <summary>
        /// Ошибка при обработке
        /// </summary>
        [EnumLocalizeAttribite("Ошибка")]
        Error = 2,
        /// <summary>
        /// Обработка успешно завершена
        /// </summary>
        [EnumLocalizeAttribite("Завершено")]
        Ok = 3,
        /// <summary>
        /// 
        /// </summary>
        [EnumLocalizeAttribite("В процессе")]
        InProcess = 4
    }
}