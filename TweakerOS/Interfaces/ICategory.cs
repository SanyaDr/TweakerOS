using System.Collections.ObjectModel;
using System.Security.Cryptography.Pkcs;
using System.Security.Policy;

namespace TweakerOS.Interfaces;

public interface ICategory
{
    public string Name { get; }         // Название категории которое отображается на экране
    public string Description { get; }  // Описание категории которое отображается на экране
    public string SystemCodeName { get; }   // Системное название категории, необходимо для связи категории с другими элементами (используется только в коде)
    public ReadOnlyCollection<ITweak> Tweaks { get; }
}