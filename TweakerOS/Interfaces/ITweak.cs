namespace TweakerOS.Interfaces;

public interface ITweak
{
    /// <summary>
    /// Название твика, название которое будет отображаться на экране
    /// </summary>
    public string Name { get; }
    /// <summary>
    /// Описание твика, что он выполняет и для чего
    /// </summary>
    public string Description { get; }
    /// <summary>
    /// Получить применен ли твик?
    /// <exception cref="NotImplementedException">Ошибка если твик имеет большое количество изменений, проверка которых нецелесообразна.
    /// Рекомендуется обработать ошибку и вывести кнопки Вкл/Выкл на экран</exception>
    /// </summary>
    /// <returns> true - Твик применен.
    /// false - Твик не применен</returns>
    public bool GetTweakIsApplied();     // получить применен ли твик
    /// <summary>
    /// После применения твика требуется перезагрузка проводника или всего устройства
    /// <returns>true - Требуется перезагрузка; false - Перезагрузка не требуется</returns>
    /// </summary>
    public bool RebootRequires { get; }
    /// <summary>
    /// Применить твик
    /// </summary>
    public void ApplyTweak();
    /// <summary>
    /// Отменить твик, "вернуть к заводским настройкам"
    /// </summary>
    public void RestoreToFactory();
}