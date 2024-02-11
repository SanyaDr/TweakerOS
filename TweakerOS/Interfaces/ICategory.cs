using System.Collections.ObjectModel;
using System.Security.Cryptography.Pkcs;
using System.Security.Policy;

namespace TweakerOS.Interfaces;

public interface ICategory
{
    public string Name { get; }
    public string Description { get; }
    
    public ReadOnlyCollection<ITweak> Tweaks { get; }
}