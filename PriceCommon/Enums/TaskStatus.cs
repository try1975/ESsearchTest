namespace PriceCommon.Enums
{
    public enum TaskStatus
    {
        /// <summary>
        /// Не обработан
        /// </summary>
        [EnumLocalizeAttribite("Не обработан")]
        NotInitialized = 0,
        /// <summary>
        /// В очереди
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
        /// В процессе
        /// </summary>
        [EnumLocalizeAttribite("В процессе")]
        InProcess = 4,
        /// <summary>
        /// Прекращено по таймауту
        /// </summary>
        [EnumLocalizeAttribite("Прекращено по таймауту")]
        BreakByTimeout = 5,
        /// <summary>
        /// Прекращено
        /// </summary>
        [EnumLocalizeAttribite("Прекращено")]
        Break = 6,
        /// <summary>
        /// Проверено
        /// </summary>
        [EnumLocalizeAttribite("Проверено")]
        Checked = 7
    }
}